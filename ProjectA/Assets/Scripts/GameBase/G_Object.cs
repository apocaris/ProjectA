using System.Numerics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

using Random = UnityEngine.Random;
using Resources = UnityEngine.Resources;

[AddComponentMenu("G_Client/Object/Game Object Base")]
public class G_Object : MonoBehaviour
{
    public T LoadResource<T>(string strName) where T : class
    {
        /*
        한번 할당한 리소스는 static dictionary에 있으면 거기서 꺼내쓴다.
        2020.07.14. I/O최소화 -> CPU부담 최적화 실험.
        */
        if (m_vLoadedResources.ContainsKey(strName))
            return m_vLoadedResources[strName] as T;
        else
        {
            m_vLoadedResources.Add(strName, Resources.Load(strName) as T);
            return m_vLoadedResources[strName] as T;
        }
    }

    public SkeletonDataAsset LoadSkeletonResource(string strName)
    {
        if(m_vLoadedResources.ContainsKey(strName))
            return m_vLoadedResources[strName] as SkeletonDataAsset;
        else
        {
            m_vLoadedResources.Add(strName, Resources.Load(strName));
            return m_vLoadedResources[strName] as SkeletonDataAsset;
        }
    }

    

    public static void EditorLog(string strLog, int iLevel = 0)
    {
#if UNITY_EDITOR || UNITY_ANDROID
        if (iLevel == 0)
            Debug.Log("UNITY_EDITOR : " + strLog);
        else if (iLevel == 1)
            Debug.LogWarning("UNITY_EDITOR : " + strLog);
        else if (iLevel == 2)
            Debug.LogError("UNITY_EDITOR : " + strLog);
        else
            Debug.LogError("UNITY_EDITOR LOG LEVEL OUT OF LIMIT");
#endif
    }


    public void EnableObject()
    {
        gameObject.SetActive(true);
    }

    public void DisableObject()
    {
        gameObject.SetActive(false);
    }

    public void DestroySelf(float fTime = 0.0f)
    {
        if (fTime == 0)
            Destroy(gameObject);
        else
            StartCoroutine(DestroyDelay(fTime));
    }

    protected IEnumerator DestroyDelay(float fTime)
    {
        yield return new WaitForSeconds(fTime);

        Destroy(gameObject);
    }

    protected IEnumerator ExecDelegateWaitTime(EventDelegate vDelegate, float fTime)
    {
        yield return new WaitForSeconds(fTime);

        if (vDelegate != null)
            vDelegate.Execute();
    }

    public bool IsNull(Object vObj)
    {
        if (null == vObj)
        {
            Debug.LogWarning("Obj is Null ... ! Check Object Type");
            return true;
        }
        return false;
    }

    public bool IsOverUI()
    {
        if (UICamera.hoveredObject == null || UICamera.hoveredObject == UICamera.fallThrough)
            return false;
        else
            return true;
    }

    //public GameObject GetCurrentSceneUI()
    //{
    //    GameObject vOBJ = GameObject.Find(G_Constant.m_strSceneMGR);
    //    if (vOBJ != null)
    //        return vOBJ;

    //    return null;
    //}

    //public T GetCurrentSceneCtrl<T>()
    //{
    //    GameObject vObj = GameObject.Find(G_Constant.m_strSceneMGR);
    //    if (null != vObj)
    //    {
    //        T vCtrl = vObj.GetComponent<T>();
    //        if (null != vCtrl)
    //            return vCtrl;
    //    }
    //    return default(T);
    //}

    public EventDelegate CreateEventDelegate(MonoBehaviour vTarget, string strMethodName, params object[] vParam)
    {
        EventDelegate vDelegate = new EventDelegate(vTarget, strMethodName);
        for (int i = 0; i < vParam.Length; ++i)
            vDelegate.parameters[i].value = vParam[i];

        return vDelegate;
    }

    public void PlayParticle(ParticleSystem vParticle)
    {
        if (vParticle == null)
            return;

        vParticle.Clear();
        vParticle.Simulate(0);
        vParticle.Play();
    }

    public IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }

    public int GetLayerMask(string strName)
    {
        return 1 << LayerMask.NameToLayer(strName);
    }

    public static BigInteger RandomWIthBigInteger(BigInteger iMin, BigInteger iMax)
    {
        int iDigit = iMin.ToString().Length;
        float fMin;
        float fMax;
        if (iDigit > 9)
        {
            iDigit -= 9; //소수점 여덟자리 정도까진 남기자
            fMin = (float)(iMin / BigInteger.Pow(10, iDigit));
            fMax = (float)(iMax / BigInteger.Pow(10, iDigit));

            float fRandom = Random.Range(fMin, fMax);
            return (BigInteger)fRandom * BigInteger.Pow(10, iDigit);
        }
        else
        {
            fMin = (float)iMin;
            fMax = (float)iMax;

            float fRandom = Random.Range(fMin, fMax);
            return (BigInteger)fRandom;
        }
    }

    public double DividiBigInteger(BigInteger iNum1, BigInteger iNum2)
    {
        return System.Math.Exp(BigInteger.Log(iNum1) - BigInteger.Log(iNum2));
    }

    public string GetCountryISOCode()
    {
        string strISOCode;
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Korean:
                strISOCode = "KR";
                break;
            case SystemLanguage.English:
                strISOCode = "EN";
                break;
            case SystemLanguage.Japanese:
                strISOCode = "JP";
                break;
            case SystemLanguage.Chinese:
            case SystemLanguage.ChineseTraditional:
            case SystemLanguage.ChineseSimplified:
                strISOCode = "CN";
                break;
            case SystemLanguage.Indonesian:
                strISOCode = "ID";
                break;
            default:
                strISOCode = "EN";
                break;
        }
        return strISOCode;
    }

    //public static string GetCurrentISOCode()
    //{
    //    if (G_UserDataMGR.a_instance == null)
    //        return "KR";

    //    string strISOCode = string.Empty;
    //    switch (G_UserDataMGR.a_instance.a_eSystemLanguage)
    //    {
    //        case SystemLanguage.Korean:
    //            strISOCode = "KR";
    //            break;
    //        case SystemLanguage.English:
    //            strISOCode = "EN";
    //            break;
    //        case SystemLanguage.Japanese:
    //            strISOCode = "JP";
    //            break;
    //        case SystemLanguage.Chinese:
    //        case SystemLanguage.ChineseTraditional:
    //        case SystemLanguage.ChineseSimplified:
    //            strISOCode = "CN";
    //            break;
    //        case SystemLanguage.Indonesian:
    //            strISOCode = "ID";
    //            break;
    //        default:
    //            strISOCode = "EN";
    //            break;
    //    }
    //    return strISOCode;
    //}

    //public static string GetSignInWithAppleLanguageCode()
    //{
    //    string strLanguageCode = string.Empty;
    //    switch (G_UserDataMGR.a_instance.a_eSystemLanguage)
    //    {
    //        case SystemLanguage.Korean:
    //            strLanguageCode = "ko_KR";
    //            break;
    //        case SystemLanguage.Japanese:
    //            strLanguageCode = "ja_JP";
    //            break;
    //        case SystemLanguage.Chinese:
    //        case SystemLanguage.ChineseTraditional:
    //            strLanguageCode = "zh_CN";
    //            break;
    //        case SystemLanguage.ChineseSimplified:
    //            strLanguageCode = "zh_TW";
    //            break;
    //        case SystemLanguage.Indonesian:
    //            strLanguageCode = "id_ID";
    //            break;
    //        default:
    //            strLanguageCode = "en_US";
    //            break;
    //    }

    //    return strLanguageCode;
    //}

    private static Dictionary<string, object> m_vLoadedResources = new Dictionary<string, object>();
}
