using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_UICurrencyGroup : G_UIBase
{
    public override void Awake()
    {
        base.Awake();

        m_fInitialPosX = transform.localPosition.x;
    }

    public void ResetCtrl()
    {
        if (m_eUIType == GT_UI.None)
            return;

        if (!gameObject.activeInHierarchy)
            return;

        if (m_vTable == null)
            return;

        if (m_vCtrl == null)
            return;

        if (m_dicCtrls == null)
            return;

        List<G_CurrencyGroupData> vList = G_CurrencyGroupData.GetCurrencyGroupData(m_eUIType);
        if (vList == null)
            return;

        transform.localPosition = Vector3.zero;

        DeleteAllChildInTable(ref m_vTable);
        m_dicCtrls.Clear();

        G_UICurrencyCtrl vItemCtrl = null;
        for (int i = 0; i < vList.Count; ++i)
        {
            GameObject vObj = CreateUI(m_vCtrl, m_vTable.gameObject);
            if (vObj != null)
            {
                vItemCtrl = vObj.GetComponent<G_UICurrencyCtrl>();
                if (vItemCtrl != null)
                    m_dicCtrls.Add(vList[i].a_eCurrencyType, vItemCtrl);
            }

            if (vItemCtrl != null)
                vItemCtrl.ResetCtrl(vList[i].a_eCurrencyType);
        }

        ResetPosition();
        StartCoroutine(ResetTable());
    }

    public void UpdateCurrencys()
    {
        if (m_dicCtrls == null)
            return;

        foreach (G_UICurrencyCtrl vCtrl in m_dicCtrls.Values)
            vCtrl.UpdateCurrency();
    }

    public void UpdateCurrency(GT_Currency eType)
    {
        if (m_dicCtrls.ContainsKey(eType) == false)
            return;

        m_dicCtrls[eType].UpdateCurrency();
    }

    public void ChangeUIMenuType(GT_UI eType)
    {
        m_eUIType = eType;
        ResetCtrl();
    }

    // 재화 표기 개수에 따라 포지션을 맞춰줘야 함
    private void ResetPosition()
    {
        if (m_vCtrl == null)
        {
            return;
        }

        // 1개를 기준을 첫 Position 을 잡았음
        int iCalcCount = m_dicCtrls.Count - 1;
        if (iCalcCount <= 0)
            return;

        // Currency Item 의 Width + Table 의 PaddingX = 하나의 ItemSize
        G_UICurrencyCtrl vCtrl = m_vCtrl.GetComponent<G_UICurrencyCtrl>();
        if (vCtrl == null)
            return;

        float fItemWidth = vCtrl.GetSizeWidth();
        if (m_vTable != null)
            fItemWidth += m_vTable.padding.x;

        // 첫번째 아이템의 padding 적용
        fItemWidth += m_vTable.padding.x;

        // 최종 이동해야 할 거리
        float fMoveX = fItemWidth * iCalcCount;
        transform.localPosition = new Vector3(m_fInitialPosX - fMoveX, transform.localPosition.y);
    }

    private IEnumerator ResetTable()
    {
        if (m_vTable != null)
            m_vTable.repositionNow = true;

        yield return null;

        if (m_vTable != null)
            m_vTable.Reposition();
    }

    #region Variable

    private float m_fInitialPosX = 0.0f;
    private Dictionary<GT_Currency, G_UICurrencyCtrl> m_dicCtrls = new Dictionary<GT_Currency, G_UICurrencyCtrl>();

    #endregion

    [SerializeField, Rename("UI Type")]
    private GT_UI m_eUIType = GT_UI.None;

    [SerializeField, Rename("Currency item prefab")]
    protected GameObject m_vCtrl = null;

    [SerializeField, Rename("Table")]
    protected UITable m_vTable = null;
}