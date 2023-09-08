using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_UIGuideQuest : G_UIBase
{
    public void ResetCtrl()
    {
        G_UserDataMGR.a_instance.a_vQuestData.GetCurrentGuideQuest(ref m_vCurrent);
        if (m_vCurrent == null)
        {
            // If it does not exist, create a new one
            // First guide quest data setting
            G_UserDataMGR.a_instance.a_vQuestData.InitializeGuideQuest();
            G_UserDataMGR.a_instance.a_vQuestData.GetCurrentGuideQuest(ref m_vCurrent);
            if (m_vCurrent != null)
            {

            }    
        }


    }

    public void GetCurrentQuest(ref G_QuestTraking vRetData)
    {
        vRetData = m_vCurrent;
    }

    #region Variables

    
    private G_QuestTraking m_vCurrent = null;

    #endregion

    [Header("Group : normal")]
    [SerializeField, Rename("Open Group")]
    protected GameObject m_vOpenGroup = null;

    [SerializeField, Rename("Close Group")]
    protected GameObject m_vCloseGroup = null;

    [Header("Group : static")]
    [SerializeField, Rename("Static Object")]
    protected GameObject m_vStaticObject = null;

    [SerializeField, Rename("Static Open Group")]
    protected GameObject m_vStaticOpenGroup = null;

    [SerializeField, Rename("Static Close Group")]
    protected GameObject m_vStaticCloseGroup = null;

    [Header("Open Info")]
    [SerializeField]
    protected List<UILabel> m_arrayNameLabel = null;

    [SerializeField]
    protected List<UILabel> m_arrayContentLabel = null;

    [SerializeField]
    protected List<UISprite> m_arrayRewardItem = null;

    [SerializeField]
    protected List<UILabel> m_arrayLabel_reward_count = null;

    [SerializeField]
    protected List<GameObject> m_arrayRewardNotice = null;

    [SerializeField]
    protected List<GameObject> m_arrayCompleteFocus = null;
}
