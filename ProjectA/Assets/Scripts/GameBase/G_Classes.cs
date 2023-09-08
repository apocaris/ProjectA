using CodeStage.AntiCheat.ObscuredTypes;
using System;
using System.Numerics;

[Serializable]
public class G_Slot
{
    public G_Slot() { Clear(); }

    public G_Slot(int iSlot, int iID)
    {
        m_iSlot = iSlot;
        m_iID = iID;
    }

    public void Clear()
    {
        m_iSlot = 0;
        m_iID = 0;
    }

    public int a_iSlot { get { return m_iSlot; } set { m_iSlot = value; } }
    private int m_iSlot = 0;

    public int a_iID { get { return m_iID; } set { m_iID = value; } }
    private int m_iID = 0;
}

[Serializable]
public class G_QuestTraking
{
    public G_QuestTraking() { }

    public void Clear()
    {
        m_iID = 0;
        m_iValue = 0;
        m_eState = GT_QuestState.Ongoing;
    }

    public void Initialize()
    {
        m_iValue = 0;
        m_eState = GT_QuestState.Ongoing;
    }

    // Used to update once when reflecting initial server data
    public void UpdateProgress()
    {
        G_MissionData vBase;
        G_GlobalDataMGR.a_instance.GetData<G_MissionData>(GT_DataTable.Mission, m_iID, out vBase);
        if (vBase == null)
            return;

        if (vBase.a_eSubCategory != GT_QuestSubCategory.Repeat && m_eState == GT_QuestState.Finished)
            return;

        if (vBase.a_eType == GT_Quest.Complete_Level_Account)
            m_iValue = G_UserDataMGR.a_instance.a_vAccountData.a_iLevel.GetDecrypted();

        if (m_iValue >= vBase.a_iTargetValue && vBase.a_iTargetValue > 0)
        {
            if (vBase.a_eSubCategory != GT_QuestSubCategory.Repeat)
                m_iValue = vBase.a_iTargetValue;
            m_eState = GT_QuestState.Completed;
        }
        else
        {
            m_eState = GT_QuestState.Ongoing;
        }
    }

    public int a_iID { get { return m_iID; } set { m_iID = value; } }
    private int m_iID = 0;

    public BigInteger a_iValue { get { return m_iValue; } set { m_iValue = value; } }
    private BigInteger m_iValue = 0;

    public GT_QuestState a_eState { get { return m_eState; } set { m_eState = value; } }
    private GT_QuestState m_eState = GT_QuestState.Ongoing;
}

#region Gear

[Serializable]
public class G_Equipment
{
    public G_Equipment() { Clear(); }

    public virtual void Clear()
    {
        m_iID = 0;
        m_iLevel = 1;
        m_iOverLevel = 0;
        m_iCount = 0;
    }

    public int GetMaterialCount()
    {
        if (m_iCount.GetDecrypted() <= 1)
            return 0;

        return m_iCount.GetDecrypted() - 1;
    }

    public virtual string GetServerSendData()
    {
        return string.Format("{0}_{1}_{2}_{3}", m_iID.GetDecrypted(), m_iLevel.GetDecrypted(), m_iOverLevel.GetDecrypted(), m_iCount.GetDecrypted());
    }

    public ObscuredInt a_iID { get { return m_iID; } set { m_iID = value; } }
    protected ObscuredInt m_iID = 0;

    public ObscuredInt a_iLevel { get { return m_iLevel; } set { m_iLevel = value; } }
    protected ObscuredInt m_iLevel = 1;

    public ObscuredInt a_iOverLevel { get { return m_iOverLevel; } set { m_iOverLevel = value; } }
    protected ObscuredInt m_iOverLevel = 0;

    public ObscuredInt a_iCount { get { return m_iCount; } set { m_iCount = value; } }
    protected ObscuredInt m_iCount = 0;
}

[Serializable]
public class G_Gear : G_Equipment
{
    public G_Gear() { Clear(); }

    public override void Clear()
    {
        base.Clear();

        m_fPotentialValue = 0.0f;
    }

    public override string GetServerSendData()
    {
        string strData = base.GetServerSendData();
        strData += string.Format("_{0}", m_fPotentialValue.GetDecrypted());
        return strData;
    }

    // 잠재효과 값
    public ObscuredFloat a_fPotentialValue { get { return m_fPotentialValue; } set { m_fPotentialValue = value; } }
    private ObscuredFloat m_fPotentialValue = 0.0f;
}

[Serializable]
public class G_Rune : G_Equipment
{
    public G_Rune() { Clear(); }

    public override void Clear()
    {
        base.Clear();

        m_strPK = string.Empty;
        m_iMaxLevel = 1;
        m_bIsLocked = false;
    }

    public override string GetServerSendData()
    {
        string strData = base.GetServerSendData();
        strData = string.Format("{0}_{1}_{2}_{3}", m_strPK, strData, m_iMaxLevel.GetDecrypted(), m_bIsLocked == false ? 0 : 1);
        return strData;
    }

    public string a_strPK { get { return m_strPK; } set { m_strPK = value; } }
    private string m_strPK = string.Empty;

    public ObscuredInt a_iMaxLevel { get { return m_iMaxLevel; } set { m_iMaxLevel = value; } }
    private ObscuredInt m_iMaxLevel = 1;

    public bool a_bIsLocked { get { return m_bIsLocked; } set { m_bIsLocked = value; } }
    private bool m_bIsLocked = false;
}

[Serializable]
public class G_Wing : G_Equipment
{
    public G_Wing() { Clear(); }

    public override void Clear()
    {
        base.Clear();
    }

    public override string GetServerSendData()
    {
        string strData = base.GetServerSendData();
        strData += string.Format("_{0}", m_bIsWakeup.GetDecrypted() == false ? 0 : 1);
        return strData;
    }

    public ObscuredBool a_bIsWakeup { get { return m_bIsWakeup; } set { m_bIsWakeup = value; } }
    private ObscuredBool m_bIsWakeup = false;
}

[Serializable]
public class G_Costume : G_Equipment
{
    public G_Costume() { Clear(); }

    public override void Clear()
    {
        base.Clear();

        m_fPotentialValue = 0.0f;
    }

    public override string GetServerSendData()
    {
        string strData = base.GetServerSendData();
        strData += string.Format("_{0}", m_fPotentialValue.GetDecrypted());
        return strData;
    }

    public ObscuredFloat a_fPotentialValue { get { return m_fPotentialValue; } set { m_fPotentialValue = value; } }
    private ObscuredFloat m_fPotentialValue = 0.0f;
}

[Serializable]
public class G_Pet : G_Equipment
{
    public G_Pet() { Clear(); }

    public override void Clear()
    {
        base.Clear();
    }

    public override string GetServerSendData()
    {
        return base.GetServerSendData();
    }
}

#endregion
