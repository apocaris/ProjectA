using System.Numerics;
using UnityEngine;

public class G_UICurrencyCtrl : G_UIBase
{
    public void ResetCtrl(GT_Currency eType)
    {
        G_CurrencyData vBase;
        G_GlobalDataMGR.a_instance.GetData<G_CurrencyData>(GT_DataTable.Currency, (int)eType, out vBase);
        if (vBase == null)
        {
            return;
        }

        m_eType = eType;
        m_eDisplayType = vBase.a_eDisplayType;

        if (m_vSprite_icon != null)
            m_vSprite_icon.spriteName = vBase.a_strIconKey;

        UpdateCurrency();

        gameObject.SetActive(true);
    }

    public void UpdateCurrency()
    {
        if (m_vLabel_amount != null)
        {
            // Get currency user data
            BigInteger iAmount = G_UserDataMGR.a_instance.a_vCurrencyData.GetCurrency(m_eType);

            string strValue = string.Empty;
            switch (m_eDisplayType)
            {
                case GT_CurrencyDisplay.Number:
                    {
                        strValue = G_Utils.GetCommaNumberText(iAmount);
                    }
                    break;
                case GT_CurrencyDisplay.Alphabet:
                    {
                        strValue = G_Utils.GetLargeValue(iAmount);
                    }
                    break;
            }

            m_vLabel_amount.text = string.Format(m_strFormat, strValue);
        }
    }

    private void SetFormat(GT_NumberFormat eFormat)
    {
        switch (eFormat)
        {
            case GT_NumberFormat.None:
                m_strFormat = "{0:n0}";
                break;
            case GT_NumberFormat.Normal:
            case GT_NumberFormat.Cross:
                m_strFormat = "x{0:n0}";
                break;
            case GT_NumberFormat.Plus:
                m_strFormat = "+{0:n0}";
                break;
            case GT_NumberFormat.Minus:
                m_strFormat = "-{0:n0}";
                break;
            case GT_NumberFormat.Max:
                m_strFormat = "MAX";
                break;
        }
    }

    public void ShowTooltip()
    {
        if (m_vSprite_icon == null)
            return;

        //G_GameMGR.a_instance.a_vGameSceneCtrl.ShowCurrencyTooltip(m_eType, m_vSprite_icon.transform.position);
    }

    public void HideTooltip()
    {
        //G_GameMGR.a_instance.a_vGameSceneCtrl.HideCurrencyTooltip();
    }

    public float GetSizeWidth()
    {
        float fSize = 0.0f;
        switch (m_eSizeTarget)
        {
            case GT_UISizeTarget.Widget:
                {
                    UIWidget vWidget = GetComponent<UIWidget>();
                    if (vWidget != null)
                        fSize = vWidget.width;
                }
                break;
            case GT_UISizeTarget.BoxCollider:
                {
                    BoxCollider vBoxCollider = GetComponent<BoxCollider>();
                    if (vBoxCollider != null)
                        fSize = vBoxCollider.size.x;
                }
                break;
        }

        fSize *= transform.localScale.x;
        return fSize;
    }

    private string m_strFormat = string.Empty;

    public GT_Currency a_eType { get { return m_eType; } }
    private GT_Currency m_eType = GT_Currency.None;

    public GT_CurrencyDisplay a_eDisplayType { get { return m_eDisplayType; } }
    private GT_CurrencyDisplay m_eDisplayType = GT_CurrencyDisplay.None;

    public GT_UISizeTarget a_eSizeTarget { get { return m_eSizeTarget; } }
    [SerializeField, Rename("Size target")]
    private GT_UISizeTarget m_eSizeTarget = GT_UISizeTarget.Widget;

    [SerializeField, Rename("Icon")]
    private UISprite m_vSprite_icon = null;

    [SerializeField, Rename("Label amount")]
    private UILabel m_vLabel_amount = null;
}
