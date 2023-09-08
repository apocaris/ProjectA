using System;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class RenameAttribute : PropertyAttribute
{
    public string NewName { get; private set; }
    public bool bArray { get; private set; }
    public RenameAttribute(string name)
    {
        NewName = name;
    }

    public RenameAttribute(string name, bool bState)
    {
        NewName = name;
        bArray = bState;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(RenameAttribute))]
public class RenameEditor : PropertyDrawer
{    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if ((attribute as RenameAttribute).bArray == false)
            EditorGUI.PropertyField(position, property, new GUIContent((attribute as RenameAttribute).NewName));
        else
        {
            EditorGUI.PropertyField(position, property, 
                new GUIContent((attribute as RenameAttribute).NewName + " " + property.displayName.Split(' ')[1]));
        }
    }
}
#endif

public static class G_UIExtension
{
    public static void ResetStaticState(this UnityEngine.GameObject vObj)
    {
        if (vObj != null)
        {
            vObj.SetActive(false);
            vObj.SetActive(true);
        }
    }
}

public class G_UIBase : G_Object
{
    public virtual void Awake() { }

    public virtual void Initialize() { }

    public GameObject CreateUI(string strUIName, GameObject vParent)
    {
        if (IsNull(vParent))
            return null;

        GameObject vResourceObj = LoadResource<GameObject>(strUIName);//Resources.Load(strUIName) as GameObject; //리소스 경로에서 strUIName 프리팹을 가져옵니다
        if (IsNull(vResourceObj))
            return null;

        GameObject vNewObject = Instantiate(vResourceObj, vParent.transform);
        if (null == vNewObject)
        {
            Debug.LogWarning("Create UI Obj is Not Exist ... Name : " + strUIName);
            return null;
        }

        return vNewObject;
    }

    public GameObject CreateUI(GameObject vChild, GameObject vParent)
    {
        if (IsNull(vParent) || IsNull(vChild))
            return null;

        GameObject vNewObject = Instantiate(vChild, vParent.transform);
        if (null == vNewObject)
        {
            Debug.LogWarning("Create UI Obj is Not Exist");
            return null;
        }

        return vNewObject;
    }

    public Material LoadShaderMaterial(string strShaderName)
    {
        Material vRetObj =  LoadResource<Material>(strShaderName);
        if (IsNull(vRetObj))
            return null;

        return vRetObj;
    }

    public Color32 HexToColor(int HexVal)
    {
        byte R = (byte)((HexVal >> 16) & 0xFF);
        byte G = (byte)((HexVal >> 8) & 0xFF);
        byte B = (byte)((HexVal) & 0xFF);
        return new Color32(R, G, B, 255);
    }

    public static string HexToBBCode(int HexVal)
    {
        byte R = (byte)((HexVal >> 16) & 0xFF);
        byte G = (byte)((HexVal >> 8) & 0xFF);
        byte B = (byte)((HexVal) & 0xFF);
        return string.Format("{0:X2}{1:X2}{2:X2}", R, G, B);
    }

    public Color32 BBCodeToColor32(string strBBCode)
    {
        string strHex = string.Format("FF{0}", strBBCode);
        int iHex = int.Parse(strHex, System.Globalization.NumberStyles.HexNumber);
        byte R = (byte)((iHex>> 16) & 0xFF);
        byte G = (byte)((iHex>> 8) & 0xFF);
        byte B = (byte)((iHex) & 0xFF);
        return new Color32(R, G, B, 255);
    }

    public string ColorToBBCode(Color32 vColor)
    {
        return vColor.r.ToString("X2") + vColor.g.ToString("X2") + vColor.b.ToString("X2");
    }

    public Vector3 GetUIWorldPosition(GameObject vUIObject)
    {
        Vector3 vWorldPosition = Vector3.zero;
        Transform vTransform = vUIObject.transform;
        while (null != vTransform)
        {
            vWorldPosition += vTransform.localPosition;
            vTransform = vTransform.parent;
            if (null != vTransform.GetComponent<UIRoot>())
                return vWorldPosition;
        }
        return Vector3.zero;
    }

    public void OpenAgreeOfTerms()
    {
        Application.OpenURL("http://mkt.onlinestory.co.kr/data_dev1/agreement/service_bh.txt");
    }

    public IEnumerator ResetScroll(UIScrollView vScrollView, UIGrid vGrid = null, UITable vTable = null, Action vOnFinish = null)
    {
        if (vGrid != null)
            vGrid.repositionNow = true;

        if (vTable != null)
            vTable.repositionNow = true;

        yield return null;

        if (vScrollView != null)
            vScrollView.ResetPosition();

        if (vOnFinish != null)
            vOnFinish.Invoke();
    }

    /// <summary>
    /// 스크롤 뷰의 특정 차일드 order로 offset맞추기
    /// </summary>
    /// <param name="bVertical">세로/가로 스크롤 여부</param>
    /// <param name="vScrollView">스크롤뷰 컴포넌트</param>
    /// <param name="iTargetOrder">타겟 차일드 위치(1이 begin)</param>
    /// <param name="iChildCount">스크롤뷰 차일드 총 수</param>
    /// <param name="fCellSize">셀 하나당 사이즈(grid의 width or height)</param>
    public void ResetScrollCenterOnChild(bool bVertical, UIScrollView vScrollView, int iTargetOrder, int iChildCount, float fCellSize)
    {
        UIPanel vPanel = vScrollView.GetComponent<UIPanel>();
        if (vPanel == null)
            return;

        int iBoaderCount;
        if (bVertical)
            iBoaderCount = (int)(vPanel.GetViewSize().y / fCellSize);
        else
            iBoaderCount = (int)(vPanel.GetViewSize().x / fCellSize);

        Vector3 vPos = Vector3.zero;
        if (iTargetOrder < iBoaderCount)
            return;

        int iMaxOffsetCount;
        float fLastOffset = 0.0f;
        if (iChildCount - iTargetOrder < iBoaderCount)
        {
            iTargetOrder = iChildCount;
            iMaxOffsetCount = iBoaderCount - 1 + (iBoaderCount / 2);
            if(bVertical)
                fLastOffset = (fCellSize * (iBoaderCount + 1)) - vPanel.GetViewSize().y;
            else
                fLastOffset = (fCellSize * (iBoaderCount + 1)) - vPanel.GetViewSize().x;
        }
        else
            iMaxOffsetCount = iBoaderCount - 1;

        if (bVertical)
            vPos.y += (fCellSize * (iTargetOrder - iMaxOffsetCount)) + fLastOffset;
        else
            vPos.x -= (fCellSize * (iTargetOrder - iMaxOffsetCount)) + fLastOffset;

        vScrollView.MoveRelative(vPos);        
    }

    public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
    {
        // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
        int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
        return start.AddDays(daysToAdd);
    }

    public void ResetSpriteWithRatio(UISprite vSprite, string strSpriteName, int iOriginWidth)
    {
        UISpriteData vSpriteData = vSprite.GetSprite(strSpriteName);
        if (vSpriteData != null)
        {
            vSprite.spriteName = strSpriteName;

            float fRatio = (float)vSpriteData.height / vSpriteData.width;
            vSprite.width = iOriginWidth;
            vSprite.height = (int)(iOriginWidth * fRatio);
        }
    }

    public void DeleteAllChildInTable(ref UITable vTable)
    {
        if (vTable == null || (vTable != null && vTable.transform.childCount == 0))
            return;

        int iChildCount = vTable.transform.childCount;
        while (true)
        {
            if (iChildCount <= 0 && vTable.transform.childCount == 0)
                break;

            GameObject vChild = vTable.transform.GetChild(iChildCount - 1).gameObject;
            if (vChild != null)
                NGUITools.Destroy(vChild);

            --iChildCount;
        }
    }

    public void DeleteAllChildInGrid(ref UIGrid vGrid)
    {
        if (vGrid == null || (vGrid != null && vGrid.transform.childCount == 0))
            return;

        int iChildCount = vGrid.transform.childCount;
        while (true)
        {
            if (iChildCount <= 0 && vGrid.transform.childCount == 0)
                break;

            GameObject vChild = vGrid.transform.GetChild(iChildCount - 1).gameObject;
            if (vChild != null)
                NGUITools.Destroy(vChild);

            --iChildCount;
        }
    }
}