using UnityEngine;

public class G_UIMainStage : G_UIBase
{
    public override void Initialize()
    {
        if (m_vCurrencyUI != null)
            m_vCurrencyUI.ResetCtrl();


    }

    [SerializeField, Rename("Currency UI")]
    protected G_UICurrencyGroup m_vCurrencyUI = null;
}