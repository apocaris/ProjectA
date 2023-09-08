using CodeStage.AntiCheat.ObscuredTypes;
using Spine;
using System;
using System.Collections.Generic;
using System.Numerics;

// Data stored only locally, including settings
[Serializable]
public class G_UserLocalData
{
    public G_UserLocalData() { Clear(); }

    public void Clear()
    {
        m_iLastStage = 0;
        m_iLastDungeon_Normal_1 = 0;
        m_iLastDungeon_Normal_2 = 0;
        m_iLastDungeon_Normal_3 = 0;
        m_iLastDungeon_Special_1 = 0;
        m_iLastDungeon_Special_2 = 0;
        m_iLastDungeon_Special_3 = 0;
        m_bBGM = true;
        m_bSE = true;
        m_bShowDamage = true;
        m_bCamShake = true;
        m_bAutoSleep = true;
        m_bPushNotice = true;
        m_bIngameVFX = true;
        m_iCamPosition = 0;
        m_iCPOrder = -1;
        m_iCPCount = 0;
        m_vCPEndTime = DateTime.MinValue;
        m_vCPDelayOver = DateTime.MinValue;
        m_iStopRarityAbility = (int)GT_Class.Legend;
        m_iStopRarityEnchant = (int)GT_Class.Legend;
        m_iStopRarityPotential = (int)GT_Class.Hero;
        m_iFirstAdBuffLog = 1;
        m_bFirstStart = true;
        m_bNewEquipment = false;
        m_bNewSkill = false;
        m_bNewPet = false;
        m_strUsedRuby = "0";

        if (m_vChatBlockList != null)
            m_vChatBlockList.Clear();
    }

    private void UpdatePushNotice()
    {

    }

    #region variables

    public int a_iLastStage { get { return m_iLastStage; } set { m_iLastStage = value; } }
    private int m_iLastStage = 0;

    public int a_iLastDungeon_Normal_1 { get { return m_iLastDungeon_Normal_1; } set { m_iLastDungeon_Normal_1 = value; } }
    private int m_iLastDungeon_Normal_1 = 0;

    public int a_iLastDungeon_Normal_2 { get { return m_iLastDungeon_Normal_2; } set { m_iLastDungeon_Normal_2 = value; } }
    private int m_iLastDungeon_Normal_2 = 0;

    public int a_iLastDungeon_Normal_3 { get { return m_iLastDungeon_Normal_3; } set { m_iLastDungeon_Normal_3 = value; } }
    private int m_iLastDungeon_Normal_3 = 0;

    public int a_iLastDungeon_Special_1 { get { return m_iLastDungeon_Special_1; } set { m_iLastDungeon_Special_1 = value; } }
    private int m_iLastDungeon_Special_1 = 0;

    public int a_iLastDungeon_Special_2 { get { return m_iLastDungeon_Special_2; } set { m_iLastDungeon_Special_2 = value; } }
    private int m_iLastDungeon_Special_2 = 0;

    public int a_iLastDungeon_Special_3 { get { return m_iLastDungeon_Special_3; } set { m_iLastDungeon_Special_3 = value; } }
    private int m_iLastDungeon_Special_3 = 0;

    public bool a_bBGM { get { return m_bBGM; } set { m_bBGM = value; } }
    private bool m_bBGM = true;

    public bool a_bSE { get { return m_bSE; } set { m_bSE = value; } }
    private bool m_bSE = true;

    public bool a_bShowDamage { get { return m_bShowDamage; } set { m_bShowDamage = value; } }
    private bool m_bShowDamage = true;

    public bool a_bCamShake { get { return m_bCamShake; } set { m_bCamShake = value; } }
    private bool m_bCamShake = true;

    public bool a_bAutoSleep { get { return m_bAutoSleep; } set { m_bAutoSleep = value; } }
    private bool m_bAutoSleep = true;

    public bool a_bPushNotice { get { return m_bPushNotice; } set { m_bPushNotice = value; UpdatePushNotice(); } }
    private bool m_bPushNotice = true;

    public bool a_bIngameVFX { get { return m_bIngameVFX; } set { m_bIngameVFX = value; } }
    private bool m_bIngameVFX = true;

    public int a_iCamPosition { get { return m_iCamPosition; } set { m_iCamPosition = value; } }
    private int m_iCamPosition = 0;

    // Chance package order
    public int a_iCPOrder { get { return m_iCPOrder; } set { m_iCPOrder = value; } }
    private int m_iCPOrder = -1;

    // Chance package exposure count
    public int a_iCPCount { get { return m_iCPCount; } set { m_iCPCount = value; } }
    private int m_iCPCount = 0;

    // Chance package end time
    public DateTime a_vCPEndTime { get { return m_vCPEndTime; } set { m_vCPEndTime = value; } }
    private DateTime m_vCPEndTime = DateTime.MinValue;

    // Chance package appearance cooltime
    public DateTime a_vCPDelayOver { get { return m_vCPDelayOver; } set { m_vCPDelayOver = value; } }
    private DateTime m_vCPDelayOver = DateTime.MinValue;

    // Rating stop option to use in random numerical content
    public int a_iStopRarityAbility { get { return m_iStopRarityAbility; } set { m_iStopRarityAbility = value; } }
    private int m_iStopRarityAbility = (int)GT_Class.Legend;

    public int a_iStopRarityEnchant { get { return m_iStopRarityEnchant; } set { m_iStopRarityEnchant = value; } }
    private int m_iStopRarityEnchant = (int)GT_Class.Legend;

    public int a_iStopRarityPotential { get { return m_iStopRarityPotential; } set { m_iStopRarityPotential = value; } }
    private int m_iStopRarityPotential = (int)GT_Class.Hero;

    public int a_iWeaponSkinID { get { return m_iWeaponSkinID; } set { m_iWeaponSkinID = value; } }
    private int m_iWeaponSkinID = 0;

    public int a_iWingSkinID { get { return m_iWingSkinID; } set { m_iWingSkinID = value; } }
    private int m_iWingSkinID = 0;

    // Leave the first ad buff log
    public int a_iFirstAdBuffLog { get { return m_iFirstAdBuffLog; } set { m_iFirstAdBuffLog = value; } }
    private int m_iFirstAdBuffLog = 1;

    // Install the game and start the first flag
    public bool a_bFirstStart { get { return m_bFirstStart; } set { m_bFirstStart = value; } }
    private bool m_bFirstStart = true;

    // Excluding the number that can transcend batch synthesis
    public bool a_bExceptOverlevelCnt { get { return m_bExceptOverlevelCnt; } set { m_bExceptOverlevelCnt = value; } }
    private bool m_bExceptOverlevelCnt = false;

    public bool a_bNewEquipment { get { return m_bNewEquipment; } set { m_bNewEquipment = value; } }
    private bool m_bNewEquipment = false;

    public bool a_bNewSkill { get { return m_bNewSkill; } set { m_bNewSkill = value; } }
    private bool m_bNewSkill = false;

    public bool a_bNewPet { get { return m_bNewPet; } set { m_bNewPet = value; } }
    private bool m_bNewPet = false;

    public string a_strUsedRuby { get { return m_strUsedRuby; } set { m_strUsedRuby = value; } }
    private string m_strUsedRuby = "0";

    // Chat block list
    private List<string> m_vChatBlockList = new List<string>();

    #endregion
}

[Serializable]
// Old : G_UserInstanceData
public class G_UserPlayData
{
    public G_UserPlayData() { Clear(); }

    public void Clear()
    {
        m_vLastQuitTime = DateTime.Now;
        m_iPlayTime = 0;
        m_iUsedRuby = 0;
    }

    #region variables

    public DateTime a_vLastQuitTime { get { return m_vLastQuitTime; } set { m_vLastQuitTime = value; } }
    private DateTime m_vLastQuitTime = DateTime.Now;

    public int a_iPlayTime { get { return m_iPlayTime; } set { m_iPlayTime = value; } }
    private int m_iPlayTime = 0;

    public BigInteger a_iUsedRuby { get { return m_iUsedRuby; } set { m_iUsedRuby = value; } }
    private BigInteger m_iUsedRuby = 0;

    public string a_strIndate { get { return m_strIndate; } set { m_strIndate = value; } }
    private string m_strIndate = string.Empty;

    #endregion
}

[Serializable]
public class G_UserBaseData
{
    public G_UserBaseData() { Clear(); }

    public void Clear()
    {
        m_strUserID = string.Empty;
        m_strNickname = string.Empty;
        m_strPlatform = string.Empty;
        m_strDeviceToken = string.Empty;
        m_strLastVersion = string.Empty;
        m_strIndate = string.Empty;
    }

    public string a_strUserID { get { return m_strUserID; } set { m_strUserID = value; } }
    private string m_strUserID = string.Empty;

    public string a_strNickname { get { return m_strNickname; } set { m_strNickname = value; } }
    private string m_strNickname = string.Empty;

    public string a_strPlatform { get { return m_strPlatform; } set { m_strPlatform = value; } }
    private string m_strPlatform = string.Empty;

    public string a_strDeviceToken { get { return m_strDeviceToken; } set { m_strDeviceToken = value; } }
    private string m_strDeviceToken = string.Empty;

    public string a_strLastVersion { get { return m_strLastVersion; } set { m_strLastVersion = value; } }
    private string m_strLastVersion = string.Empty;

    public string a_strIndate { get { return m_strIndate; } set { m_strIndate = value; } }
    private string m_strIndate = string.Empty;
}

[Serializable]
public class G_UserAccountData
{
    public G_UserAccountData() { Clear(); }
    public void Clear()
    {
        m_iLevel = 1;
        m_strExp = "0";
        m_vLastAccessTime = DateTime.MinValue.AddDays(1);
        m_strIndate = string.Empty;
    }

    public void SetEXP(BigInteger iAmount)
    {
        BigInteger iExp = iAmount;
        while (true)
        {
            G_ExpAccountData vExp;
            G_GlobalDataMGR.a_instance.GetData<G_ExpAccountData>(GT_DataTable.ExpAccount, m_iLevel, out vExp);
            if (vExp != null)
            {
                if (vExp.a_iExp == 0)
                    break;

                else if (iExp >= vExp.a_iExp)
                {
                    m_iLevel += 1;
                    iExp -= vExp.a_iExp;
                }
                else
                    break;
            }
            else
                break;
        }

        m_strExp = iExp.ToString();

        //G_UserDataMGR.a_instance.SaveUserAccountData();
        //G_SCBackendMGR.a_instance.FLAG_UPDATE_USER_ACCOUNT_DATA_DB = true;
    }

    public bool AddEXP(BigInteger iAmount)
    {
        bool bLevelUp = false;
        BigInteger iExp = GetExp();
        iExp += iAmount;
        while (true)
        {
            G_ExpAccountData vExp;
            G_GlobalDataMGR.a_instance.GetData<G_ExpAccountData>(GT_DataTable.ExpAccount, m_iLevel, out vExp);
            if (vExp != null)
            {
                if (vExp.a_iExp == 0)
                    break;

                else if (iExp >= vExp.a_iExp)
                {
                    m_iLevel += 1;
                    iExp -= vExp.a_iExp;
                    bLevelUp = true;
                }
                else
                    break;
            }
            else
                break;
        }

        m_strExp = iExp.ToString();

        //G_UserDataMGR.a_instance.a_bFlag_UserSaveByKillMonster = true;

        //2023-04-17 즉시저장 하지 않고 쿨타임을 두고 처리합니다
        //G_UserDataMGR.a_instance.SaveUserAccountData();
        //G_SCBackendMGR.a_instance.FLAG_UPDATE_USER_ACCOUNT_DATA_DB = true;

        return bLevelUp;
    }

    public BigInteger GetExp()
    {
        return BigInteger.Parse(m_strExp.GetDecrypted());
    }

    public ObscuredInt a_iLevel { get { return m_iLevel; } set { m_iLevel = value; } }
    private ObscuredInt m_iLevel = 1;

    public ObscuredString a_strExp { get { return m_strExp; } set { m_strExp = value; } }
    private ObscuredString m_strExp = "0";

    public DateTime a_vLastAccessTime { get { return m_vLastAccessTime; } set { m_vLastAccessTime = value; } }
    private DateTime m_vLastAccessTime = DateTime.MinValue.AddDays(1);

    public string a_strIndate { get { return m_strIndate; } set { m_strIndate = value; } }
    private string m_strIndate = string.Empty;
}

[Serializable]
public class G_UserCurrencyData
{
    public G_UserCurrencyData() {  }

    public void Clear()
    {
        if (m_dicCurrencys == null)
            m_dicCurrencys = new Dictionary<GT_Currency, ObscuredString>();
        else
            m_dicCurrencys.Clear();

        m_strIndate = string.Empty;
    }

    public void SetStartPossesion()
    {
        List<G_StartPossessionData> vCurrencyGroup = null;
        G_StartPossessionData.GetStartPossesionGroupData(GT_StartPossession.Currency, ref vCurrencyGroup);
        if (vCurrencyGroup != null)
        {
            for (int i = 0; i < vCurrencyGroup.Count; ++i)
                SetCurrency((GT_Currency)vCurrencyGroup[i].a_iDetailType, vCurrencyGroup[i].a_iCount);
        }
    }

    public bool AddNewCurrency(GT_Currency eType)
    {
        if (m_dicCurrencys.ContainsKey(eType))
            return false;

        m_dicCurrencys.Add(eType, "0");
        return true;
    }

    public bool IsEnoughCurrency(GT_Currency eType, BigInteger iTargetValue)
    {
        if (m_dicCurrencys.ContainsKey(eType) == false)
            return false;

        BigInteger iCurrentValue = BigInteger.Parse(m_dicCurrencys[eType].GetDecrypted());
        if (iCurrentValue >= iTargetValue)
            return true;

        return false;
    }

    public BigInteger GetCurrency(GT_Currency eType)
    {
        if (m_dicCurrencys.ContainsKey(eType) == false)
            return 0;

        return BigInteger.Parse(m_dicCurrencys[eType].GetDecrypted());
    }

    public void SetCurrency(GT_Currency eType, BigInteger iValue)
    {
        if (m_dicCurrencys.ContainsKey(eType) == false)
            return;

        if (iValue < 0)
            iValue = 0;

        m_dicCurrencys[eType] = iValue.ToString();
    }

    public void UpdateCurrency(GT_Currency eType, BigInteger iAmount)
    {
        if (!m_dicCurrencys.ContainsKey(eType))
            return;

        BigInteger iTargetValue = BigInteger.Parse(m_dicCurrencys[eType].GetDecrypted());
        iTargetValue += iAmount;
        if (iTargetValue < 0)
            iTargetValue = 0;
        m_dicCurrencys[eType] = iTargetValue.ToString();

        if (eType == GT_Currency.Ruby)
        {
            if (iAmount < 0)
            {
                BigInteger iCurValue = BigInteger.Parse(G_UserDataMGR.a_instance.a_vLocalData.a_strUsedRuby);
                iCurValue += (iAmount * -1);
                G_UserDataMGR.a_instance.a_vLocalData.a_strUsedRuby = iCurValue.ToString();
                // Local Data Save
            }
        }
    }

    public string GetSendServerData()
    {
        // Format: Product number_quantity^Product number_quantity^Product number_quantity ... like this ...
        string strData = string.Empty;
        foreach (KeyValuePair<GT_Currency, ObscuredString> vData in m_dicCurrencys)
        {
            string strCurrency;
            if (string.IsNullOrEmpty(strData))
                strCurrency = string.Format("{0}", (int)vData.Key);
            else
                strCurrency = string.Format("^{0}", (int)vData.Key);
            strCurrency += string.Format("*{0}", vData.Value.GetDecrypted());
            strData += strCurrency;
        }
        return strData;
    }

    private Dictionary<GT_Currency, ObscuredString> m_dicCurrencys = new Dictionary<GT_Currency, ObscuredString>();
    public string a_strIndate { get { return m_strIndate; } set { m_strIndate = value; } }
    private string m_strIndate = string.Empty;
}

// Old : G_UserGrowthData
[Serializable]
public class G_UserLoadoutData
{
    #region Gear

    public void UpdateLoadGear(GT_Equipment eType, int iID)
    {
        if (!m_dicGears.ContainsKey(eType))
            m_dicGears.Add(eType, 0);

        m_dicGears[eType] = iID;
    }

    public ObscuredInt GetLoadGear(GT_Equipment eType)
    {
        if (!m_dicGears.ContainsKey(eType))
            return 0;

        return m_dicGears[eType];
    }

    #endregion

    #region Pet

    public void UpdateLoadPet(int iPreset, int iSlot, int iID)
    {
        if (!m_dicPets.ContainsKey(iPreset))
            m_dicPets.Add(iPreset, new List<G_Slot>());

        // first get the slot data
        G_Slot vSlotData = null;
        List<G_Slot> vSlotList = m_dicPets[iPreset];
        if (vSlotList != null)
        {
            if (vSlotList.Count >= 3)
                return;

            for (int i = 0; i < vSlotList.Count; ++i)
            {
                if (vSlotList[i] == null)
                    continue;

                if (vSlotList[i].a_iSlot == iSlot)
                {
                    vSlotData = vSlotList[i];
                    break;
                }
            }

            if (vSlotData == null)
            {
                vSlotList.Add(new G_Slot(iSlot, iID));
            }
            else
            {
                // Cannot be duplicated
                if (vSlotData.a_iID == iID)
                    return;

                vSlotData.a_iID = iID;
            }
        }
    }

    public bool UnloadPet(int iPreset, int iID)
    {
        bool bOK = false;
        if (!m_dicPets.ContainsKey(iPreset))
            return bOK;

        G_Slot vSlotData = null;
        List<G_Slot> vSlotList = m_dicPets[iPreset];
        if (vSlotList != null)
        {
            for (int i = 0; i < vSlotList.Count; ++i)
            {
                if (vSlotList[i] == null)
                    continue;

                if (vSlotList[i].a_iID == iID)
                {
                    vSlotData = vSlotList[i];
                    break;
                }
            }

            if (vSlotData != null)
            {
                vSlotList.Remove(vSlotData);
                bOK = true;
            }
        }

        return bOK;
    }

    public int GetCurrentPresetLeaderPet()
    {
        if (!m_dicPets.ContainsKey(m_iPetPreset))
            return 0;

        List<G_Slot> vList = m_dicPets[m_iPetPreset];
        if (vList == null)
            return 0;

        for (int i = 0; i < vList.Count; ++i)
        {
            if (vList[i] == null)
                continue;

            if (vList[i].a_iSlot == 0)
                return vList[i].a_iID;
        }

        return 0;
    }

    #endregion

    #region Rune

    public bool CheckLoadRune()
    {
        if (m_dicRunes != null)
        {
            foreach (List<string> vList in m_dicRunes.Values)
            {
                if (vList == null)
                    continue;

                for (int i = 0; i < vList.Count; ++i)
                {
                    G_Rune vRune = null;
                    G_UserDataMGR.a_instance.a_vEquipmentData.GetRuneData(vList[i], ref vRune);
                    if (vRune != null)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public bool CheckLoadRuneTarget(int iID)
    {
        if (m_dicRunes != null)
        {
            foreach (List<string> vList in m_dicRunes.Values)
            {
                if (vList == null)
                    continue;

                for (int i = 0; i < vList.Count; ++i)
                {
                    G_Rune vRune = null;
                    G_UserDataMGR.a_instance.a_vEquipmentData.GetRuneData(vList[i], ref vRune);
                    if (vRune != null)
                    {
                        if (vRune.a_iID == iID)
                            return true;
                    }
                }
            }
        }

        return false;
    }

    public void GetRuneList(int iPreset, ref List<string> vRetData)
    {
        if (!m_dicRunes.ContainsKey(iPreset))
            return;

        vRetData = m_dicRunes[iPreset];
    }

    public void InsertRune(int iPreset, string strPK)
    {
        if (!m_dicRunes.ContainsKey(iPreset))
            m_dicRunes.Add(iPreset, new List<string>());

        for (int i = 0; i < m_dicRunes[iPreset].Count; ++i)
        {
            if (m_dicRunes[iPreset][i] == strPK)
                return;
        }

        m_dicRunes[iPreset].Add(strPK);
    }

    public void ExtractRune(int iPreset, string strPK)
    {
        if (!m_dicRunes.ContainsKey(iPreset))
            return;

        m_dicRunes[iPreset].Remove(strPK);
    }

    public bool ExistLoadRune(int iPreset, string strPK)
    {
        if (!m_dicRunes.ContainsKey(iPreset))
            return false;

        for (int i = 0; i < m_dicRunes[iPreset].Count; ++i)
        {
            if (m_dicRunes[iPreset][i] == strPK)
                return true;
        }

        return false;
    }

    public bool ExistAllLoadRune(string strPK)
    {
        foreach (List<string> vData in m_dicRunes.Values)
        {
            if (vData == null)
                continue;

            for (int i = 0; i < vData.Count; ++i)
            {
                if (vData[i] == strPK)
                    return true;
            }
        }

        return false;
    }

    public void ClearLoadPartsRune(int iPreset)
    {
        if (!m_dicRunes.ContainsKey(iPreset))
            return;

        G_Utils.RemoveListAll(m_dicRunes[iPreset]);
    }

    #endregion

    #region Send Server Data

    public string GetServerSendEquipmentData()
    {
        string strData = string.Empty;
        foreach (KeyValuePair<GT_Equipment, ObscuredInt> vData in m_dicGears)
        {
            string strEquipped = string.Empty;
            if (strData.Length == 0)
                strEquipped = string.Format("{0}", (int)vData.Key);
            else
                strEquipped = string.Format("^{0}", (int)vData.Key);
            strEquipped += string.Format("_{0}", vData.Value.GetDecrypted());
            strData += strEquipped;
        }

        return strData;
    }

    public string GetServerSendRuneData()
    {
        if (m_dicRunes == null)
            return "0";

        if (m_dicRunes.Count == 0)
            return "0";

        string strData = string.Empty;
        foreach (KeyValuePair<int, List<string>> vData in m_dicRunes)
        {
            if (vData.Value == null)
                continue;
            if (vData.Value.Count == 0)
                continue;

            if (strData.Length != 0)
                strData += "^";

            string strOnce = string.Empty;
            for (int i = 0; i < vData.Value.Count; ++i)
            {
                if (i == 0)
                {
                    strOnce += string.Format("{0}_{1}", vData.Key, vData.Value[i]);
                }
                else
                {
                    strOnce += string.Format("*{0}_{1}", vData.Key, vData.Value[i]);
                }
            }
            strData += strOnce;
        }

        if (strData.Length == 0)
            strData = "0";

        return strData;
    }

    public string GetServerSendPetData()
    {
        if (m_dicPets == null)
            return "0";

        if (m_dicPets.Count == 0)
            return "0";

        string strData = string.Empty;
        foreach (KeyValuePair<int, List<G_Slot>> vData in m_dicPets)
        {
            if (vData.Value == null)
                continue;

            if (vData.Value.Count == 0)
                continue;

            if (strData.Length != 0)
                strData += "^";

            string strItem = string.Empty;
            for (int i = 0; i < vData.Value.Count; ++i)
            {
                G_Slot vSlotData = vData.Value[i];
                if (vSlotData == null)
                    continue;

                if (i == 0)
                    strItem += string.Format("{0}_{1}_{2}", vData.Key, vSlotData.a_iSlot, vSlotData.a_iID);
                else
                    strItem += string.Format("*{0}_{1}_{2}", vData.Key, vSlotData.a_iSlot, vSlotData.a_iID);
            }

            strData += strItem;
        }

        return strData;
    }

    #endregion

    // Gear
    private Dictionary<GT_Equipment, ObscuredInt> m_dicGears = new Dictionary<GT_Equipment, ObscuredInt>();

    // Pet
    private Dictionary<int, List<G_Slot>> m_dicPets = new Dictionary<int, List<G_Slot>>();

    // Rune
    public Dictionary<int, List<string>> a_dicRunes { get { return m_dicRunes; } set { m_dicRunes = value; } }
    private Dictionary<int, List<string>> m_dicRunes = new Dictionary<int, List<string>>();


    public int a_iRunePreset { get { return m_iRunePreset; } set { m_iRunePreset = value; } }
    private int m_iRunePreset = 0;

    public int a_iPetPreset { get { return m_iPetPreset; } set { m_iPetPreset = value; } }
    private int m_iPetPreset = 0;

    public string a_strIndate { get { return m_strIndate; } set { m_strIndate = value; } }
    private string m_strIndate = string.Empty;
}

public class G_UserStatusData
{
    public G_UserStatusData() { Clear(); }

    public void Clear()
    {
        m_strIndate = string.Empty;

        if (m_dicBaseStatData == null)
            m_dicBaseStatData = new Dictionary<GT_BaseStat, ObscuredInt>();
        else
            m_dicBaseStatData.Clear();

        if (m_dicPropertyStatData == null)
            m_dicPropertyStatData = new Dictionary<GT_BaseStat, ObscuredInt>();
        else
            m_dicPropertyStatData.Clear();

        if (m_dicWingStatData == null)
            m_dicWingStatData = new Dictionary<GT_BaseStat, ObscuredInt>();
        else
            m_dicWingStatData.Clear();

        if (m_dicArtifactStatData == null)
            m_dicArtifactStatData = new Dictionary<int, ObscuredInt>();
        else
            m_dicArtifactStatData.Clear();
    }

    public void CreateFirstData()
    {
        // 능력치 최초 구성 (Load 이후 최초 or 데이터가 없는 경우)
        List<G_StatData> vStatList = null;
        if (m_dicBaseStatData.Count == 0)
        {
            G_BaseStatData.GetStatList(ref vStatList);
            if (vStatList != null)
            {
                for (int i = 0; i < vStatList.Count; ++i)
                    m_dicBaseStatData.Add(vStatList[i].a_eType, 1);
            }
        }

        if (m_dicPropertyStatData.Count == 0)
        {
            G_PropertyStatData.GetStatList(ref vStatList);
            if (vStatList != null)
            {
                for (int i = 0; i < vStatList.Count; ++i)
                    m_dicPropertyStatData.Add(vStatList[i].a_eType, 1);
            }
        }

        if (m_dicWingStatData.Count == 0)
        {
            G_WingStatData.GetStatList(ref vStatList);
            if (vStatList != null)
            {
                for (int i = 0; i < vStatList.Count; ++i)
                    m_dicWingStatData.Add(vStatList[i].a_eType, 1);
            }
        }

        if (m_dicArtifactStatData.Count == 0)
        {
            Dictionary<int, G_GlobalDataClass> vOutDic;
            G_GlobalDataMGR.a_instance.GetTable<G_GlobalDataTable>(GT_DataTable.Artifact, out vOutDic);
            if (vOutDic != null)
            {
                for (var enumerator = vOutDic.GetEnumerator(); enumerator.MoveNext();)
                {
                    G_ArtifactData vData = (G_ArtifactData)enumerator.Current.Value;
                    if (vData == null)
                        continue;

                    m_dicArtifactStatData.Add(vData.a_iID, 0);
                }
            }
        }
    }

    #region Base

    public int GetBaseStatValue(GT_BaseStat eType)
    {
        if (m_dicBaseStatData.ContainsKey(eType) == false)
            return 1;

        return m_dicBaseStatData[eType];
    }

    public void SetBaseStatValue(GT_BaseStat eType, int iValue)
    {
        if (m_dicBaseStatData.ContainsKey(eType) == false)
            return;

        m_dicBaseStatData[eType] = iValue;
    }

    public string GetServerSendData_BaseStat()
    {
        string strData = string.Empty;
        foreach (KeyValuePair<GT_BaseStat, ObscuredInt> vData in m_dicBaseStatData)
        {
            string strStat;
            if (string.IsNullOrEmpty(strData))
                strStat = string.Format("{0}", (int)vData.Key);
            else
                strStat = string.Format("^{0}", (int)vData.Key);
            strStat += string.Format("*{0}", vData.Value.GetDecrypted());
            strData += strStat;
        }
        return strData;
    }

    #endregion

    #region 특성 능력치 관련
    public int GetPropertyStatValue(GT_BaseStat eType)
    {
        if (m_dicPropertyStatData.ContainsKey(eType) == false)
            return 1;

        return m_dicPropertyStatData[eType];
    }

    public void SetPropertyStatValue(GT_BaseStat eType, int iValue)
    {
        if (m_dicPropertyStatData.ContainsKey(eType) == false)
            return;

        m_dicPropertyStatData[eType] = iValue;
    }

    public string GetServerSendData_PropertyStat()
    {
        string strData = string.Empty;
        foreach (KeyValuePair<GT_BaseStat, ObscuredInt> vData in m_dicPropertyStatData)
        {
            string strStat;
            if (string.IsNullOrEmpty(strData))
                strStat = string.Format("{0}", (int)vData.Key);
            else
                strStat = string.Format("^{0}", (int)vData.Key);
            strStat += string.Format("*{0}", vData.Value.GetDecrypted());
            strData += strStat;
        }
        return strData;
    }
    #endregion

    #region 날개 능력치 관련
    public int GetWingStatValue(GT_BaseStat eType)
    {
        if (m_dicWingStatData.ContainsKey(eType) == false)
            return 1;

        return m_dicWingStatData[eType];
    }

    public void SetWingStatValue(GT_BaseStat eType, int iValue)
    {
        if (m_dicWingStatData.ContainsKey(eType) == false)
            return;

        m_dicWingStatData[eType] = iValue;
    }

    public string GetServerSendData_WingStat()
    {
        string strData = string.Empty;
        foreach (KeyValuePair<GT_BaseStat, ObscuredInt> vData in m_dicWingStatData)
        {
            string strStat;
            if (string.IsNullOrEmpty(strData))
                strStat = string.Format("{0}", (int)vData.Key);
            else
                strStat = string.Format("^{0}", (int)vData.Key);
            strStat += string.Format("*{0}", vData.Value.GetDecrypted());
            strData += strStat;
        }
        return strData;
    }
    #endregion

    #region 아티팩트 능력치 관련
    public int GetArtifactStatValue(int iArtifactID)
    {
        if (m_dicArtifactStatData.ContainsKey(iArtifactID) == false)
            return 0;

        return m_dicArtifactStatData[iArtifactID];
    }

    public void SetArtifactStatValue(int iArtifactID, int iValue)
    {
        if (m_dicArtifactStatData.ContainsKey(iArtifactID) == false)
            return;

        m_dicArtifactStatData[iArtifactID] = iValue;
    }

    public string GetServerSendData_ArtifactStat()
    {
        string strData = string.Empty;
        foreach (KeyValuePair<int, ObscuredInt> vData in m_dicArtifactStatData)
        {
            string strStat;
            if (string.IsNullOrEmpty(strData))
                strStat = string.Format("{0}", vData.Key);
            else
                strStat = string.Format("^{0}", vData.Key);
            strStat += string.Format("*{0}", vData.Value.GetDecrypted());
            strData += strStat;
        }
        return strData;
    }
    #endregion

    public Dictionary<GT_BaseStat, ObscuredInt> a_dicBaseStatData { get { return m_dicBaseStatData; } }
    private Dictionary<GT_BaseStat, ObscuredInt> m_dicBaseStatData = new Dictionary<GT_BaseStat, ObscuredInt>();

    public Dictionary<GT_BaseStat, ObscuredInt> a_dicPropertyStatData { get { return m_dicPropertyStatData; } }
    private Dictionary<GT_BaseStat, ObscuredInt> m_dicPropertyStatData = new Dictionary<GT_BaseStat, ObscuredInt>();

    public Dictionary<GT_BaseStat, ObscuredInt> a_dicWingStatData { get { return m_dicWingStatData; } }
    private Dictionary<GT_BaseStat, ObscuredInt> m_dicWingStatData = new Dictionary<GT_BaseStat, ObscuredInt>();

    public Dictionary<int, ObscuredInt> a_dicArtifactStatData { get { return m_dicArtifactStatData; } set { m_dicArtifactStatData = value; } }
    private Dictionary<int, ObscuredInt> m_dicArtifactStatData = new Dictionary<int, ObscuredInt>();

    public string a_strIndate { get { return m_strIndate; } set { m_strIndate = value; } }
    private string m_strIndate = string.Empty;
}

public class G_UserEquipmentData
{
    #region Rune

    public void CreateRune(string strPK, int iID)
    {
        G_ExtragearData vBaseData;
        G_GlobalDataMGR.a_instance.GetData<G_ExtragearData>(GT_DataTable.ExtraGear, iID, out vBaseData);
        if (vBaseData == null)
            return;

        int iMaxInventoryCount = (int)G_GlobalDataMGR.a_instance.GetGlobalVariable(GT_GlobalVariable.EXTRA_GEAR_INVENTORY_CAPACITY);
        if (m_vRunes.Count >= iMaxInventoryCount)
            return;

        G_Rune vNew = new G_Rune();
        vNew.a_strPK = strPK;
        vNew.a_iID = iID;
        vNew.a_iLevel = 1;
        vNew.a_iOverLevel = 0;
        vNew.a_iCount = 1;
        vNew.a_iMaxLevel = vBaseData.a_iStartMaxlevel;
        vNew.a_bIsLocked = false;
        m_vRunes.Add(vNew);
    }

    public void GetRuneList(ref List<G_Rune> vRetData)
    {
        vRetData = m_vRunes;
    }

    public int GetRuneListCount()
    {
        return m_vRunes.Count;
    }

    public void GetRuneData(string strPK, ref G_Rune vRetData)
    {
        if (m_vRunes == null)
            return;

        for (int i = 0; i < m_vRunes.Count; ++i)
        {
            if (m_vRunes[i] == null)
                continue;

            if (m_vRunes[i].a_strPK == strPK)
            {
                vRetData = m_vRunes[i];
                break;
            }
        }
    }

    public int GetRuneInvenIndex(string strPK)
    {
        if (m_vRunes == null)
            return -1;

        for (int i = 0; i < m_vRunes.Count; ++i)
        {
            if (m_vRunes[i] == null)
                continue;

            if (m_vRunes[i].a_strPK == strPK)
            {
                return i;
            }
        }

        return -1;
    }

    public bool DeleteRune(string strPK)
    {
        if (m_vRunes == null)
            return false;

        G_Rune vRune = null;
        for (int i = 0; i < m_vRunes.Count; ++i)
        {
            if (m_vRunes[i] == null)
                continue;

            if (m_vRunes[i].a_strPK == strPK)
            {
                vRune = m_vRunes[i];
                break;
            }
        }

        if (vRune != null)
        {
            m_vRunes.Remove(vRune);
            return true;
        }

        return false;
    }

    public void UpdateRuneLevel(string strPK, int iLevel)
    {
        G_Rune vRune = null;
        GetRuneData(strPK, ref vRune);
        if (vRune != null)
        {
            vRune.a_iLevel = iLevel;
        }
    }

    public bool UpdateRuneMaxLevel(string strPK, int iMaxlevel)
    {
        G_Rune vRune = null;
        GetRuneData(strPK, ref vRune);
        if (vRune != null)
        {
            vRune.a_iMaxLevel = iMaxlevel;
        }

        return false;
    }

    public bool UpdateRuneLockState(string strPK, bool bState)
    {
        if (m_vRunes == null)
            return false;

        for (int i = 0; i < m_vRunes.Count; ++i)
        {
            if (m_vRunes[i] == null)
                continue;

            if (m_vRunes[i].a_strPK == strPK)
            {
                m_vRunes[i].a_bIsLocked = bState;
                return true;
            }
        }

        return false;
    }

    public string GetSendServerData_Extra()
    {
        if (m_vRunes.Count == 0)
            return "0";

        string strData = string.Empty;
        for (int i = 0; i < m_vRunes.Count; ++i)
        {
            if (i == 0)
                strData += string.Format("{0}", m_vRunes[i].GetServerSendData());
            else
                strData += string.Format("^{0}", m_vRunes[i].GetServerSendData());
        }
        return strData;
    }

    #endregion

    public List<G_Rune> a_vRunes { get { return m_vRunes; } }
    private List<G_Rune> m_vRunes = new List<G_Rune>();

    public string a_strIndate { get { return m_strIndate; } set { m_strIndate = value; } }
    private string m_strIndate = string.Empty;
}

// Old : G_UserSkillEquippedData
// Skill Equip Information
public class G_UserSkillLoadData
{

}

// Old : G_UserSkillOwnedData
// Skill Possession Information
public class G_UserSkillSetData
{

}

// Old : G_UserAbilityData
public class G_UserPotentialData
{

}

public class G_UserStageData
{

}

[Serializable]
public class G_UserQuestData
{
    public G_UserQuestData() { Clear(); }

    public void Clear()
    {
        m_vCurrentGuideQuest = null;
    }

    #region Guide Quest

    public void InitializeGuideQuest()
    {
        List<G_MissionData> vList = null;
        G_MissionData.GetMissionListByCategory(GT_QuestCategory.Guide, ref vList);
        if (vList != null)
        {
            for (int i = 0; i < vList.Count; ++i)
            {
                if (vList[i] == null)
                    continue;

                if (vList[i].a_iReqMissionID == 0)
                {
                    if (m_vCurrentGuideQuest == null)
                        m_vCurrentGuideQuest = new G_QuestTraking();

                    m_vCurrentGuideQuest.Clear();
                    m_vCurrentGuideQuest.a_iID = vList[i].a_iID;
                    break;
                }
            }
        }
    }

    public void GetCurrentGuideQuest(ref G_QuestTraking vRetData)
    {
        vRetData = m_vCurrentGuideQuest;
    }

    public void UpdateGuideQuest(GT_Quest eType, BigInteger iValue, int iCondition = 0, bool bAdd = true)
    {
        if (m_vCurrentGuideQuest == null)
            return;

        G_MissionData vBase;
        G_GlobalDataMGR.a_instance.GetData<G_MissionData>(GT_DataTable.Mission, m_vCurrentGuideQuest.a_iID, out vBase);
        if (vBase == null)
            return;

        if (vBase.a_eType != eType)
            return;

        switch (eType)
        {
            case GT_Quest.WatchAd:
            case GT_Quest.Accumulated_Damage:
            case GT_Quest.Play_Arena:
                {
                    m_vCurrentGuideQuest.a_iValue = vBase.a_iTargetValue;
                    m_vCurrentGuideQuest.a_eState = GT_QuestState.Completed;
                }
                break;
            default:
                {
                    // Check Condition
                    if (vBase.a_iConditionValue > 0)
                    {
                        if (vBase.a_iConditionValue != iCondition)
                            return;
                    }

                    if (bAdd)
                        m_vCurrentGuideQuest.a_iValue += iValue;
                    else
                        m_vCurrentGuideQuest.a_iValue = iValue;

                    //ExceptProcessGuideQuest(eType, ref vBase, ref m_vCurrentGuideQuest);

                    if (m_vCurrentGuideQuest.a_iValue >= vBase.a_iTargetValue)
                    {
                        m_vCurrentGuideQuest.a_iValue = vBase.a_iTargetValue;
                        m_vCurrentGuideQuest.a_eState = GT_QuestState.Completed;
                    }
                    else
                        m_vCurrentGuideQuest.a_eState = GT_QuestState.Ongoing;
                }
                break;
        }
    }

    public void ClearGuideQuest()
    {
        if (m_vCurrentGuideQuest == null)
            return;

        m_vCurrentGuideQuest.a_eState = GT_QuestState.Finished;
    }

    public void SetCurrentGuideQuest(string strData)
    {
        if (string.IsNullOrEmpty(strData) || strData.Equals("0"))
            return;

        string[] arrayDatas = strData.Split('_');
        if (arrayDatas.Length == 3)
        {
            int iID = int.Parse(arrayDatas[0]);
            int iState = int.Parse(arrayDatas[1]);
            BigInteger iValue = BigInteger.Parse(arrayDatas[2]);

            G_MissionData vBase;
            G_GlobalDataMGR.a_instance.GetData<G_MissionData>(GT_DataTable.Mission, iID, out vBase);
            if (vBase != null)
            {
                if (m_vCurrentGuideQuest == null)
                {
                    m_vCurrentGuideQuest = new G_QuestTraking();
                }

                m_vCurrentGuideQuest.a_iID = iID;
                m_vCurrentGuideQuest.a_iValue = iValue;
                m_vCurrentGuideQuest.a_eState = (GT_QuestState)iState;
            }
        }
    }

    public string GetSendServerData_GuideQuest()
    {
        string strData;
        if (m_vCurrentGuideQuest == null)
            strData = "0";
        else
        {
            int iState = (int)m_vCurrentGuideQuest.a_eState;
            strData = string.Format("{0}_{1}_{2}", m_vCurrentGuideQuest.a_iID, iState, m_vCurrentGuideQuest.a_iValue);
        }

        return strData;
    }

    private G_QuestTraking m_vCurrentGuideQuest = null;

    #endregion

    public string a_strIndate { get { return m_strIndate; } set { m_strIndate = value; } }
    private string m_strIndate = string.Empty;
}

public class G_UserShopData
{

}

public class G_UserAttendanceData
{

}

public class G_UserADData
{

}

public class G_UserRankingData
{

}

public class G_UserMailBoxData
{

}

[Serializable]
public class G_UserNetworkFlagData
{
    public void Clear()
    {
        m_bFLAG_UPDATE_ACCOUNT_DATA_DB = false;
        m_bFLAG_UPDATE_BASE_DATA_DB = false;
        m_bFLAG_UPDATE_CURRENCY_DATA_DB = false;
        m_bFLAG_UPDATE_LOADOUT_DATA_DB = false;
        m_bFLAG_UPDATE_STAT_DATA_DB = false;
        m_bFLAG_UPDATE_EQUIPMENT_DATA_DB = false;
        m_bFLAG_UPDATE_LOCAL_DATA_DB = false;
        m_bFLAG_UPDATE_SKILL_LOAD_DATA_DB = false;
        m_bFLAG_UPDATE_SKILL_SET_DATA_DB = false;
        m_bFLAG_UPDATE_POTENTIAL_DATA_DB = false;
        m_bFLAG_UPDATE_PET_DATA_DB = false;
        m_bFLAG_UPDATE_STAGE_DATA_DB = false;
        m_bFLAG_UPDATE_QUEST_DATA_DB = false;
        m_bFLAG_UPDATE_ATTENDANCE_DATA_DB = false;
        m_bFLAG_UPDATE_AD_DATA_DB = false;
        m_bFLAG_UPDATE_SHOP_DATA_DB = false;
    }

    public bool FLAG_UPDATE_ACCOUNT_DATA_DB
    {
        get { return m_bFLAG_UPDATE_ACCOUNT_DATA_DB; }
        set
        {
            bool bPrev = m_bFLAG_UPDATE_ACCOUNT_DATA_DB;
            m_bFLAG_UPDATE_ACCOUNT_DATA_DB = value;
            if (!bPrev && value)
                G_UserDataMGR.a_instance.SaveNetworkFlagData();
        }
    }
    private bool m_bFLAG_UPDATE_ACCOUNT_DATA_DB = false;

    public bool FLAG_UPDATE_BASE_DATA_DB
    {
        get { return m_bFLAG_UPDATE_BASE_DATA_DB; }
        set
        {
            bool bPrev = m_bFLAG_UPDATE_BASE_DATA_DB;
            m_bFLAG_UPDATE_BASE_DATA_DB = value;
            if (!bPrev && value)
                G_UserDataMGR.a_instance.SaveNetworkFlagData();
        }
    }
    private bool m_bFLAG_UPDATE_BASE_DATA_DB = false;

    public bool FLAG_UPDATE_CURRENCY_DATA_DB
    {
        get { return m_bFLAG_UPDATE_CURRENCY_DATA_DB; }
        set
        {
            bool bPrev = m_bFLAG_UPDATE_CURRENCY_DATA_DB;
            m_bFLAG_UPDATE_CURRENCY_DATA_DB = value;
            if (!bPrev && value)
                G_UserDataMGR.a_instance.SaveNetworkFlagData();
        }
    }
    private bool m_bFLAG_UPDATE_CURRENCY_DATA_DB = false;

    public bool FLAG_UPDATE_USER_GROWTH_DATA_DB
    {
        get { return m_bFLAG_UPDATE_LOADOUT_DATA_DB; }
        set
        {
            bool bPrev = m_bFLAG_UPDATE_LOADOUT_DATA_DB;
            m_bFLAG_UPDATE_LOADOUT_DATA_DB = value;
            if (!bPrev && value)
                G_UserDataMGR.a_instance.SaveNetworkFlagData();
        }
    }
    private bool m_bFLAG_UPDATE_LOADOUT_DATA_DB = false;

    public bool FLAG_UPDATE_USER_STAT_DATA_DB
    {
        get { return m_bFLAG_UPDATE_STAT_DATA_DB; }
        set
        {
            bool bPrev = m_bFLAG_UPDATE_STAT_DATA_DB;
            m_bFLAG_UPDATE_STAT_DATA_DB = value;
            if (!bPrev && value)
                G_UserDataMGR.a_instance.SaveNetworkFlagData();
        }
    }
    private bool m_bFLAG_UPDATE_STAT_DATA_DB = false;

    public bool FLAG_UPDATE_USER_INSTANCE_DATA_DB
    {
        get { return m_bFLAG_UPDATE_LOCAL_DATA_DB; }
        set
        {
            bool bPrev = m_bFLAG_UPDATE_LOCAL_DATA_DB;
            m_bFLAG_UPDATE_LOCAL_DATA_DB = value;
            if (!bPrev && value)
                G_UserDataMGR.a_instance.SaveNetworkFlagData();
        }
    }
    private bool m_bFLAG_UPDATE_LOCAL_DATA_DB = false;

    public bool FLAG_UPDATE_USER_EQUIPMENT_DATA_DB
    {
        get { return m_bFLAG_UPDATE_EQUIPMENT_DATA_DB; }
        set
        {
            bool bPrev = m_bFLAG_UPDATE_EQUIPMENT_DATA_DB;
            m_bFLAG_UPDATE_EQUIPMENT_DATA_DB = value;
            if (!bPrev && value)
                G_UserDataMGR.a_instance.SaveNetworkFlagData();
        }
    }
    private bool m_bFLAG_UPDATE_EQUIPMENT_DATA_DB = false;

    public bool FLAG_UPDATE_USER_SKILL_EQUIPPED_DATA_DB
    {
        get { return m_bFLAG_UPDATE_SKILL_LOAD_DATA_DB; }
        set
        {
            bool bPrev = m_bFLAG_UPDATE_SKILL_LOAD_DATA_DB;
            m_bFLAG_UPDATE_SKILL_LOAD_DATA_DB = value;
            if (!bPrev && value)
                G_UserDataMGR.a_instance.SaveNetworkFlagData();
        }
    }
    private bool m_bFLAG_UPDATE_SKILL_LOAD_DATA_DB = false;

    public bool FLAG_UPDATE_USER_SKILL_OWNED_DATA_DB
    {
        get { return m_bFLAG_UPDATE_SKILL_SET_DATA_DB; }
        set
        {
            bool bPrev = m_bFLAG_UPDATE_SKILL_SET_DATA_DB;
            m_bFLAG_UPDATE_SKILL_SET_DATA_DB = value;
            if (!bPrev && value)
                G_UserDataMGR.a_instance.SaveNetworkFlagData();
        }
    }
    private bool m_bFLAG_UPDATE_SKILL_SET_DATA_DB = false;

    public bool FLAG_UPDATE_USER_ABILITY_DATA_DB
    {
        get { return m_bFLAG_UPDATE_POTENTIAL_DATA_DB; }
        set
        {
            bool bPrev = m_bFLAG_UPDATE_POTENTIAL_DATA_DB;
            m_bFLAG_UPDATE_POTENTIAL_DATA_DB = value;
            if (!bPrev && value)
                G_UserDataMGR.a_instance.SaveNetworkFlagData();
        }
    }
    private bool m_bFLAG_UPDATE_POTENTIAL_DATA_DB = false;

    public bool FLAG_UPDATE_USER_PET_DATA_DB
    {
        get { return m_bFLAG_UPDATE_PET_DATA_DB; }
        set
        {
            bool bPrev = m_bFLAG_UPDATE_PET_DATA_DB;
            m_bFLAG_UPDATE_PET_DATA_DB = value;
            if (!bPrev && value)
                G_UserDataMGR.a_instance.SaveNetworkFlagData();
        }
    }
    private bool m_bFLAG_UPDATE_PET_DATA_DB = false;

    public bool FLAG_UPDATE_USER_GAME_PROGRESS_DATA_DB
    {
        get { return m_bFLAG_UPDATE_STAGE_DATA_DB; }
        set
        {
            bool bPrev = m_bFLAG_UPDATE_STAGE_DATA_DB;
            m_bFLAG_UPDATE_STAGE_DATA_DB = value;
            if (!bPrev && value)
                G_UserDataMGR.a_instance.SaveNetworkFlagData();
        }
    }
    private bool m_bFLAG_UPDATE_STAGE_DATA_DB = false;

    public bool FLAG_UPDATE_USER_MISSION_DATA_DB
    {
        get { return m_bFLAG_UPDATE_QUEST_DATA_DB; }
        set
        {
            bool bPrev = m_bFLAG_UPDATE_QUEST_DATA_DB;
            m_bFLAG_UPDATE_QUEST_DATA_DB = value;
            if (!bPrev && value)
                G_UserDataMGR.a_instance.SaveNetworkFlagData();
        }
    }
    private bool m_bFLAG_UPDATE_QUEST_DATA_DB = false;

    public bool FLAG_UPDATE_USER_ATTENDANCE_DATA_DB
    {
        get { return m_bFLAG_UPDATE_ATTENDANCE_DATA_DB; }
        set
        {
            bool bPrev = m_bFLAG_UPDATE_ATTENDANCE_DATA_DB;
            m_bFLAG_UPDATE_ATTENDANCE_DATA_DB = value;
            if (!bPrev && value)
                G_UserDataMGR.a_instance.SaveNetworkFlagData();
        }
    }
    private bool m_bFLAG_UPDATE_ATTENDANCE_DATA_DB = false;

    public bool FLAG_UPDATE_USER_AD_DATA_DB
    {
        get { return m_bFLAG_UPDATE_AD_DATA_DB; }
        set
        {
            bool bPrev = m_bFLAG_UPDATE_AD_DATA_DB;
            m_bFLAG_UPDATE_AD_DATA_DB = value;
            if (!bPrev && value)
                G_UserDataMGR.a_instance.SaveNetworkFlagData();
        }
    }
    private bool m_bFLAG_UPDATE_AD_DATA_DB = false;

    public bool FLAG_UPDATE_USER_SHOP_DATA_DB
    {
        get { return m_bFLAG_UPDATE_SHOP_DATA_DB; }
        set
        {
            bool bPrev = m_bFLAG_UPDATE_SHOP_DATA_DB;
            m_bFLAG_UPDATE_SHOP_DATA_DB = value;
            if (!bPrev && value)
                G_UserDataMGR.a_instance.SaveNetworkFlagData();
        }
    }
    private bool m_bFLAG_UPDATE_SHOP_DATA_DB = false;
}