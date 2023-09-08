using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
 
/// <summary>
/// UI that is fixed regardless of content
/// </summary>

public class G_UIFixedMain : G_UIBase
{
    public override void Initialize()
    {
        base.Initialize();

        if (m_vAlwaysUpdate == null)
        {
            m_vAlwaysUpdate = CreateEventDelegate(this, "AlwaysUpdate");
            if (G_GameMGR.a_instance.a_vGameScene != null)
            {
                G_GameMGR.a_instance.a_vGameScene.AddAlwaysUpdate(ref m_vAlwaysUpdate);
            }
        }
        

    }

    public void AlwaysUpdate()
    {

    }

    public void ResetCtrl()
    {
        if (m_vLabel_nick != null)
            m_vLabel_nick.text = G_UserDataMGR.a_instance.a_vBaseData.a_strNickname;

        UpdateExp();
    }

    #region Account Info

    public void UpdateExp()
    {
        if (m_vLabel_lv != null)
            m_vLabel_lv.text = string.Format("Lv.{0}", G_UserDataMGR.a_instance.a_vAccountData.a_iLevel);

        G_ExpAccountData vBase;
        G_GlobalDataMGR.a_instance.GetData<G_ExpAccountData>(GT_DataTable.ExpAccount, G_UserDataMGR.a_instance.a_vAccountData.a_iLevel, out vBase);
        if (vBase != null)
        {
            double dProgress = 1.0f;
            BigInteger iExp = G_UserDataMGR.a_instance.a_vAccountData.GetExp();
            if (vBase.a_iExp != 0)
            {
                dProgress = (double)iExp / (double)vBase.a_iExp;
            }

            if (m_vSprite_exp != null)
                m_vSprite_exp.fillAmount = (float)dProgress;
        }
    }

    #endregion

    #region Variables

    private EventDelegate m_vAlwaysUpdate = null;

    #endregion

    [Header("Group: Account")]
    [SerializeField, Rename("Character icon")]
    private UISprite m_vSprite_character = null;

    [SerializeField, Rename("Exp progress")]
    private UISprite m_vSprite_exp = null;

    [SerializeField, Rename("Label nick")]
    private UILabel m_vLabel_nick = null;

    [SerializeField, Rename("Label level")]
    private UILabel m_vLabel_lv = null;

    [SerializeField, Rename("Label battle power")]
    private UILabel m_vLabel_bp = null;

    
}
