using System.Numerics;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using System;
using System.Linq;

using Random = UnityEngine.Random;

public class G_GlobalDataTable
{
    public void Load(JSONNode vNode, GT_DataTable eTableType)
    {
        ClearTable();

        for (int i = 0; i < vNode.Count; i++)
        {
            G_GlobalDataClass vData = G_GlobalDataClass.CreateTableData(eTableType);

            if (vData != null)
            {
                if (eTableType == GT_DataTable.Text) //Key가 문자열인 경우
                {
                    string strKey = string.Empty;
                    vData.Load(vNode[i], out strKey);
                    vData.AddTextData(this, strKey);
                }
                else //Key가 정수형인 경우
                {
                    vData.Load(vNode[i]);
                    vData.AddData(this);
                }
            }
        }
    }

    public void ClearTable()
    {
        m_dicTable.Clear();
    }

    public void AddData(G_GlobalDataClass vData)
    {
        if (m_dicTable.ContainsKey(vData.a_iID))
            m_dicTable[vData.a_iID] = vData;
        else
            m_dicTable.Add(vData.a_iID, vData);
    }

    public void AddTextData(G_GlobalDataClass vData, string strKey)
    {
        if (m_dicTextTable.ContainsKey(strKey))
            m_dicTextTable[strKey] = vData;
        else
            m_dicTextTable.Add(strKey, vData);
    }

    /*
    public T GetData<T>(int iID) where T : G_GlobalDataClass
    {
        return m_dicTable.ContainsKey(iID) ? m_dicTable[iID] as T : null;
    }
    */

    public void GetData<T>(int iID, out T vRetData) where T : G_GlobalDataClass
    {
        vRetData = null;
        if (!m_dicTable.ContainsKey(iID))
            return;

        vRetData = m_dicTable[iID] as T;
    }

    /*
    public T GetData<T>(string strKey) where T : G_GlobalDataClass
    {
        return m_dicTextTable.ContainsKey(strKey) ? m_dicTextTable[strKey] as T : null;
    }
    */

    public void GetData<T>(string strKey, out T vRetData) where T : G_GlobalDataClass
    {
        vRetData = null;
        if (!m_dicTextTable.ContainsKey(strKey))
            return;

        vRetData = m_dicTextTable[strKey] as T;
    }

    public Dictionary<int, G_GlobalDataClass> GetTable() { return m_dicTable; }
    private Dictionary<int, G_GlobalDataClass> m_dicTable = new Dictionary<int, G_GlobalDataClass>();

    public Dictionary<string, G_GlobalDataClass> GetTextTable() { return m_dicTextTable; }
    private Dictionary<string, G_GlobalDataClass> m_dicTextTable = new Dictionary<string, G_GlobalDataClass>();
}

public class G_GlobalDataClass
{
    /* json차트 기반 테이블 데이터의 마스터 부모 클래스 */

    public static G_GlobalDataClass CreateTableData(GT_DataTable eTableType)
    {
        //테이블 타입에 따른 데이터 클래스 반환
        switch (eTableType)
        {
            case GT_DataTable.Text: return new G_LocalizeText();
            case GT_DataTable.Equipment: return new G_EquipmentData();
            case GT_DataTable.Costume: return new G_CostumeData();
            case GT_DataTable.Skill: return new G_SkillData();
            case GT_DataTable.SkillShape: return new G_SkillShapeData();
            case GT_DataTable.SkillLevel: return new G_SkillLevelData();
            case GT_DataTable.MonsterStatus: return new G_MonsterStatusData();
            case GT_DataTable.ExpAccount: return new G_ExpAccountData();
            case GT_DataTable.Currency: return new G_CurrencyData();
            case GT_DataTable.BaseStat: return new G_BaseStatData();
            case GT_DataTable.PropertyStat: return new G_PropertyStatData();
            case GT_DataTable.WingStat: return new G_WingStatData();
            case GT_DataTable.OverLevel: return new G_OverLevelData();
            case GT_DataTable.Potential: return new G_PotentialData();
            case GT_DataTable.Grade: return new G_GradeData();
            case GT_DataTable.CurrencyGroup: return new G_CurrencyGroupData();
            case GT_DataTable.GlobalVariable: return new G_GlobalVariableData();
            case GT_DataTable.StartPossession: return new G_StartPossessionData();
            case GT_DataTable.Ability: return new G_AbilityData();
            case GT_DataTable.Probability: return new G_ProbabilityData();
            case GT_DataTable.ExtraGear: return new G_ExtragearData();
            case GT_DataTable.SupportUnit: return new G_SupportUnitData();
            case GT_DataTable.Stage: return new G_StageData();
            case GT_DataTable.Dungeon: return new G_DungeonData();
            case GT_DataTable.Loot: return new G_LootData();
            case GT_DataTable.Monster: return new G_MonsterData();
            case GT_DataTable.Buff: return new G_BuffData();
            case GT_DataTable.Debuff: return new G_DebuffData();
            case GT_DataTable.AdBuff: return new G_AdBuffData();
            case GT_DataTable.Wing: return new G_WingData();
            case GT_DataTable.TreasureHouseRankReward: return new G_TreasureHouseRankReward();
            case GT_DataTable.Mission: return new G_MissionData();
            case GT_DataTable.Reward: return new G_RewardData();
            case GT_DataTable.Product: return new G_ProductData();
            case GT_DataTable.Pass: return new G_PassData();
            case GT_DataTable.PassProduct: return new G_PassProductData();
            case GT_DataTable.Summon: return new G_SummonData();
            case GT_DataTable.OfflineReward: return new G_OfflineRewardData();
            case GT_DataTable.Attendance: return new G_AttendanceData();
            case GT_DataTable.Banner: return new G_BannerData();
            case GT_DataTable.RankReward: return new G_RankRewardData();
            case GT_DataTable.ContentsOpen: return new G_ContentsOpenData();
            case GT_DataTable.TestHallRankReward: return new G_TestHallRankRewardData();
            case GT_DataTable.Artifact: return new G_ArtifactData();
        }

        return null;
    }

    public virtual void AddData(G_GlobalDataTable vTable)
    {
        //테이블 타입에 따른 필수 조건 검사 후 데이터 삽입
        vTable.AddData(this);
    }

    public virtual void AddTextData(G_GlobalDataTable vTable, string strKey)
    {
        //테이블 타입에 따른 필수 조건 검사 후 데이터 삽입
        if (m_iID > 0 && !string.IsNullOrEmpty(strKey))
            vTable.AddTextData(this, strKey);
    }

    public virtual void Load(JSONNode vNode)
    {
        m_iID = G_JSONCtrl.GetINT(vNode, m_strKEY_ID, 0);
    }

    public virtual void Load(JSONNode vNode, out string strKey)
    {
        m_iID = G_JSONCtrl.GetINT(vNode, m_strKEY_ID, 0);
        strKey = string.Empty;
    }

    public int a_iID { get { return m_iID; } set { m_iID = value; } }
    private int m_iID = 0;

    #region ConstParseKey
    private const string m_strKEY_ID = "ID";
    #endregion
}

// 보유, 장착 효과 클래스
public class G_EffectStatClass
{
    public G_EffectStatClass(GT_BaseStat eStatType, float fValue, float fGrowValue)
    {
        m_eStatType = eStatType;
        m_fValue = fValue;
        m_fGrowValue = fGrowValue;
    }

    public GT_BaseStat a_eStatType { get { return m_eStatType; } }
    private GT_BaseStat m_eStatType = GT_BaseStat.None;

    public float a_fValue { get { return m_fValue; } }
    private float m_fValue = 0.0f;

    public float a_fGrowValue { get { return m_fGrowValue; } }
    private float m_fGrowValue = 0.0f;
}

// 탑승 효과 클래스
public class G_RideEffectStatClass
{
    public G_RideEffectStatClass(GT_BaseStat eStatType, float fValue)
    {
        m_eStatType = eStatType;
        m_fValue = fValue;
    }

    public GT_BaseStat a_eStatType { get { return m_eStatType; } }
    private GT_BaseStat m_eStatType = GT_BaseStat.None;

    public float a_fValue { get { return m_fValue; } }
    private float m_fValue = 0.0f;

}

public class G_GradeData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_strName = G_JSONCtrl.GetString(vNode, m_strNAME);
        m_bIsUse = G_JSONCtrl.GetBOOL(vNode, m_strIS_USE);
        a_strColor = G_JSONCtrl.GetBBCodeToTextColor(vNode, m_strCOLOR);
        m_strSpriteFrameName = G_JSONCtrl.GetString(vNode, m_strSPRITE_FRAME_NAME);
        m_iEnchantBaseCost = G_JSONCtrl.GetINT(vNode, m_strENCHANT_COST);
    }

    public string a_strName { get { return m_strName; } set { m_strName = value; } }
    private string m_strName = string.Empty;

    public bool a_bIsUse { get { return m_bIsUse; } set { m_bIsUse = value; } }
    private bool m_bIsUse = false;

    public string a_strColor { get { return m_strColor; } set { m_strColor = value; } }
    private string m_strColor = string.Empty;

    public string a_strSpriteFrameName { get { return m_strSpriteFrameName; } set { m_strSpriteFrameName = value; } }
    private string m_strSpriteFrameName = string.Empty;

    public int a_iEnchantBaseCost { get { return m_iEnchantBaseCost; } set { m_iEnchantBaseCost = value; } }
    private int m_iEnchantBaseCost = 0;

    #region ConstParseKey
    private static string m_strNAME = "NAME";
    private static string m_strIS_USE = "IS_USE";
    private static string m_strCOLOR = "COLOR";
    private static string m_strSPRITE_FRAME_NAME = "SPRITE_FRAME_NAME";
    private static string m_strENCHANT_COST = "ENCHANT_COST";
    #endregion
}

public class G_OverLevelData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_iGroupID = G_JSONCtrl.GetINT(vNode, m_strOVERLEVEL_GROUP_ID);
        m_iNowOverlevelStep = G_JSONCtrl.GetINT(vNode, m_strNOW_OVERLEVEL_STEP);
        m_iMaxOverlevelStep = G_JSONCtrl.GetINT(vNode, m_strMAX_OVERLEVEL_STEP);
        m_iStartLevel = G_JSONCtrl.GetINT(vNode, m_strSTART_LEVEL);
        m_iMaxLevel = G_JSONCtrl.GetINT(vNode, m_strMAX_LEVEL);
        m_iOverlevelCostValue = G_JSONCtrl.GetINT(vNode, m_strOVERLEVEL_COST_VALUE);
        m_iOverlevelCostObjectCount = G_JSONCtrl.GetINT(vNode, m_strOVERLEVEL_COST_OBEJCT_COUNT);
        m_fLevelupCostGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strLEVELUP_COST_GROW_VALUE);

        // 그룹 별로 모아놓는다
        if (m_dicOverLevelDatas.ContainsKey(m_iGroupID))
            m_dicOverLevelDatas[m_iGroupID].Add(this);
        else
        {
            List<G_OverLevelData> vList = new List<G_OverLevelData>();
            vList.Add(this);

            m_dicOverLevelDatas.Add(m_iGroupID, vList); 
        }
    }

    public static void ClearStaticValues()
    {
        if (m_dicOverLevelDatas != null)
        {
            m_dicOverLevelDatas.Clear();
        }
    }

    public static void GetOverlevelStepData(int iGroupID, int iLevel, ref G_OverLevelData vRetData)
    {
        if (m_dicOverLevelDatas.ContainsKey(iGroupID) == false)
        {
            Debug.LogError("GetOverlevelStepData :: Not found data by groupid : " + iGroupID);
            return;
        }

        if (iLevel > m_dicOverLevelDatas[iGroupID].Count - 1)
        {
            Debug.LogError("GetOverlevelStepData :: Not found data by level : " + iLevel);
            return;
        }

        vRetData = m_dicOverLevelDatas[iGroupID][iLevel];
    }

    public static void GetOverlevelGroupList(int iGroupID, ref List<G_OverLevelData> vRetData)
    {
        if (m_dicOverLevelDatas.ContainsKey(iGroupID) == false)
        {
            Debug.LogError("GetOverlevelStepData :: Not found data by groupid : " + iGroupID);
            return;
        }

        vRetData = m_dicOverLevelDatas[iGroupID];
    }

    public int a_iGroupID { get { return m_iGroupID; } }
    private int m_iGroupID = 0;

    public int a_iNowOverlevelStep { get { return m_iNowOverlevelStep; } }
    private int m_iNowOverlevelStep = 0;
    public int a_iMaxOverlevelStep { get { return m_iMaxOverlevelStep; } }
    private int m_iMaxOverlevelStep = 0;

    public int a_iStartLevel { get { return m_iStartLevel; } }
    private int m_iStartLevel = 0;

    public int a_iMaxLevel { get { return m_iMaxLevel; } }
    private int m_iMaxLevel = 0;

    public int a_iOverlevelCostValue { get { return m_iOverlevelCostValue; } }
    private int m_iOverlevelCostValue = 0;

    public int a_iOverLevelCostObjectCount { get { return m_iOverlevelCostObjectCount; } }
    private int m_iOverlevelCostObjectCount = 0;

    public float a_fLevelupCostGrowValue { get { return m_fLevelupCostGrowValue; } }
    private float m_fLevelupCostGrowValue = 0.0f;

    public static Dictionary<int, List<G_OverLevelData>> m_dicOverLevelDatas = new Dictionary<int, List<G_OverLevelData>>();

    #region ConstParseKey
    private static string m_strOVERLEVEL_GROUP_ID = "OVERLEVEL_GROUP_ID";
    private static string m_strNOW_OVERLEVEL_STEP = "NOW_OVERLEVEL_STEP";
    private static string m_strMAX_OVERLEVEL_STEP = "MAX_OVERLEVEL_STEP";
    private static string m_strSTART_LEVEL = "START_LEVEL";
    private static string m_strMAX_LEVEL = "MAX_LEVEL";
    private static string m_strOVERLEVEL_COST_VALUE = "OVERLEVEL_COST_VALUE";
    private static string m_strOVERLEVEL_COST_OBEJCT_COUNT = "OVERLEVEL_COST_OBJECT_COUNT";
    private static string m_strLEVELUP_COST_GROW_VALUE = "LEVELUP_COST_GROW_VALUE";
    #endregion
}

public class G_MinMaxClass<T>
{
    public G_MinMaxClass(T vMin, T vMax)
    {
        m_vMinValue = vMin;
        m_vMaxValue = vMax;
    }

    public T a_vMaxValue { get { return m_vMaxValue; } set { m_vMaxValue = value; } }
    private T m_vMaxValue;

    public T a_vMinValue { get { return m_vMinValue; } set { m_vMinValue = value; } }
    private T m_vMinValue;
}

public class G_PotentialData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strPOTENTIAL_TYPE);

        float iMin = G_JSONCtrl.GetFLOAT(vNode, m_strPOTENTIAL_MIN_1);
        float iMax = G_JSONCtrl.GetFLOAT(vNode, m_strPOTENTIAL_MAX_1);
        G_MinMaxClass<float> vNewData = new G_MinMaxClass<float>(iMin, iMax);
        m_vPotentialValueRangeList.Add(vNewData);

        iMin = G_JSONCtrl.GetFLOAT(vNode, m_strPOTENTIAL_MIN_2);
        iMax = G_JSONCtrl.GetFLOAT(vNode, m_strPOTENTIAL_MAX_2);
        vNewData = new G_MinMaxClass<float>(iMin, iMax);
        m_vPotentialValueRangeList.Add(vNewData);

        iMin = G_JSONCtrl.GetFLOAT(vNode, m_strPOTENTIAL_MIN_3);
        iMax = G_JSONCtrl.GetFLOAT(vNode, m_strPOTENTIAL_MAX_3);
        vNewData = new G_MinMaxClass<float>(iMin, iMax);
        m_vPotentialValueRangeList.Add(vNewData);

        iMin = G_JSONCtrl.GetFLOAT(vNode, m_strPOTENTIAL_MIN_4);
        iMax = G_JSONCtrl.GetFLOAT(vNode, m_strPOTENTIAL_MAX_4);
        vNewData = new G_MinMaxClass<float>(iMin, iMax);
        m_vPotentialValueRangeList.Add(vNewData);

        iMin = G_JSONCtrl.GetFLOAT(vNode, m_strPOTENTIAL_MIN_5);
        iMax = G_JSONCtrl.GetFLOAT(vNode, m_strPOTENTIAL_MAX_5);
        vNewData = new G_MinMaxClass<float>(iMin, iMax);
        m_vPotentialValueRangeList.Add(vNewData);

        m_eOpenConditionType = (GT_PotentialCondition)G_JSONCtrl.GetINT(vNode, m_strPOTENTIAL_OPEN_CONDITION_TYPE);
        m_iOpenConditionValue = G_JSONCtrl.GetINT(vNode, m_strPOTENTIAL_OPEN_VALUE);
        m_iResetCostValue = G_JSONCtrl.GetINT(vNode, m_strPOTENTIAL_RESET_COST_VALUE);
        m_iProbGroupID = G_JSONCtrl.GetINT(vNode, m_strPROB_GROUP_ID);
    }

    public void GetAllMinMaxGroup(ref List<G_MinMaxClass<float>> vRetData)
    {
        vRetData = m_vPotentialValueRangeList;
    }

    public void GetMinMaxGroup(int iIndex, ref G_MinMaxClass<float> vRetData)
    {
        if (iIndex > m_vPotentialValueRangeList.Count - 1)
            return;

        vRetData = m_vPotentialValueRangeList[iIndex];
    }

    public float GetPotentialMinValue()
    {
        // 가장 첫번째의 Min 값
        if (m_vPotentialValueRangeList.Count == 0)
            return 0.0f;

        return m_vPotentialValueRangeList[0].a_vMinValue;
    }

    public float GetPotentialMaxValue()
    {
        // 가장 마지막의 Max 값
        if (m_vPotentialValueRangeList.Count == 0)
            return 0.0f;

        int iLastIndex = m_vPotentialValueRangeList.Count - 1;
        return m_vPotentialValueRangeList[iLastIndex].a_vMaxValue;
    }

    public GT_BaseStat a_eStatType { get { return m_eStatType; } } 
    private GT_BaseStat m_eStatType = GT_BaseStat.None;

    private List<G_MinMaxClass<float>> m_vPotentialValueRangeList = new List<G_MinMaxClass<float>>();

    public GT_PotentialCondition a_eOpenConditionType { get { return m_eOpenConditionType; } }
    private GT_PotentialCondition m_eOpenConditionType = 0;

    public int a_iOpenConditionValue { get { return m_iOpenConditionValue; } }
    private int m_iOpenConditionValue = 0;

    public int a_iResetCostValue { get { return m_iResetCostValue; } }
    private int m_iResetCostValue = 0;

    public int a_iProbGroupID { get { return m_iProbGroupID; } }
    private int m_iProbGroupID = 0;

    #region ConstParseKey
    private static string m_strPOTENTIAL_TYPE = "POTENTIAL_TYPE";
    private static string m_strPOTENTIAL_MIN_1 = "POTENTIAL_MIN_1";
    private static string m_strPOTENTIAL_MAX_1 = "POTENTIAL_MAX_1";
    private static string m_strPOTENTIAL_MIN_2 = "POTENTIAL_MIN_2";
    private static string m_strPOTENTIAL_MAX_2 = "POTENTIAL_MAX_2";
    private static string m_strPOTENTIAL_MIN_3 = "POTENTIAL_MIN_3";
    private static string m_strPOTENTIAL_MAX_3 = "POTENTIAL_MAX_3";
    private static string m_strPOTENTIAL_MIN_4 = "POTENTIAL_MIN_4";
    private static string m_strPOTENTIAL_MAX_4 = "POTENTIAL_MAX_4";
    private static string m_strPOTENTIAL_MIN_5 = "POTENTIAL_MIN_5";
    private static string m_strPOTENTIAL_MAX_5 = "POTENTIAL_MAX_5";
    private static string m_strPOTENTIAL_OPEN_CONDITION_TYPE = "POTENTIAL_OPEN_CONDITION_TYPE";
    private static string m_strPOTENTIAL_OPEN_VALUE = "POTENTIAL_OPEN_VALUE";
    private static string m_strPOTENTIAL_RESET_COST_VALUE = "POTENTIAL_RESET_COST_VALUE";
    private static string m_strPROB_GROUP_ID = "PROB_GROUP_ID";
    #endregion
}

public class G_SkillData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eGrade = (GT_Grade)G_JSONCtrl.GetINT(vNode, m_strGRADE);
        m_strNameKey = G_JSONCtrl.GetString(vNode, m_strNAME_KEY);
        m_ePossessionType = (GT_Possession)G_JSONCtrl.GetINT(vNode, m_strPOSSESSION_TYPE);
        m_strIconSpriteKey = G_JSONCtrl.GetString(vNode, m_strICON_SPRITE_KEY);
        m_eApplyType = (GT_SkillApply)G_JSONCtrl.GetINT(vNode, m_strAPPLY_TYPE);
        m_eType = (GT_Skill)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_bIsSingle = G_JSONCtrl.GetINT(vNode, m_strIS_SINGLE) == 0 ? false : true;
        m_iShapeID = G_JSONCtrl.GetINT(vNode, m_strSHAPE_ID);

        GT_BaseStat eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strHOLDING_EFFECT_1_TYPE);
        float fValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_1_VALUE);
        float fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_1_GROW_VALUE);
        if(eStatType > GT_BaseStat.None)
            m_vHoldingEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strHOLDING_EFFECT_2_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_2_VALUE);
        fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_2_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vHoldingEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        m_eLevelupCostType = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_TYPE);
        m_iLevelupCostValue = G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_VALUE);
        m_eLevelupCostGrowBasisType = (GT_CalcValueBasis)G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_GROW_BASIS_TYPE);
        m_fLevelupCostGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strLEVELUP_COST_GROW_VALUE);
        m_iOverlevelGroupID = G_JSONCtrl.GetINT(vNode, m_strOVERLEVEL_GROUP_ID);
        m_eTeardownGetCurrencyType = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strTEARDOWN_GET_CURRENCY_TYPE);
        m_iTeardownGetValue = G_JSONCtrl.GetINT(vNode, m_strTEARDOWN_GET_VALUE);
        m_iPotentialID = G_JSONCtrl.GetINT(vNode, m_strPOTENTIAL_ID);
        m_strDescKey = G_JSONCtrl.GetString(vNode, m_strDESC_KEY);

        if (m_dicSkillApplyTypeGroup.ContainsKey(m_eApplyType) == false)
            m_dicSkillApplyTypeGroup[m_eApplyType] = new List<int>();
        m_dicSkillApplyTypeGroup[m_eApplyType].Add(a_iID);
    }

    public static void ClearStaticValues()
    {
        if (m_dicSkillApplyTypeGroup != null)
        {
            m_dicSkillApplyTypeGroup.Clear();
        }
    }

    public static void GetSkillApplyTypeList(GT_SkillApply eType, ref List<int> vRetData)
    {
        if (m_dicSkillApplyTypeGroup.ContainsKey(eType) == false)
            return;

        vRetData = m_dicSkillApplyTypeGroup[eType];
    }

    public GT_Grade a_eGrade { get { return m_eGrade; } }
    private GT_Grade m_eGrade = GT_Grade.None;

    public string a_strNameKey { get { return m_strNameKey; } }
    private string m_strNameKey = string.Empty;

    public GT_Possession a_ePossessionType { get { return m_ePossessionType; } }
    private GT_Possession m_ePossessionType = GT_Possession.None;

    public string a_strIconSpriteKey { get { return m_strIconSpriteKey; } }
    private string m_strIconSpriteKey = string.Empty;

    public GT_SkillApply a_eApplyType { get { return m_eApplyType; } }
    private GT_SkillApply m_eApplyType = GT_SkillApply.None;

    public GT_Skill a_eType { get { return m_eType; } }
    private GT_Skill m_eType = GT_Skill.None;

    public bool a_bIsSingle { get { return m_bIsSingle; } }
    private bool m_bIsSingle = false;

    public int a_iShapeID { get { return m_iShapeID; } }
    private int m_iShapeID = 0;

    public List<G_EffectStatClass> a_vHoldingEffects { get { return m_vHoldingEffects; } }
    private List<G_EffectStatClass> m_vHoldingEffects = new List<G_EffectStatClass>();

    public GT_Currency a_eLevelupCostType { get { return m_eLevelupCostType; } }
    private GT_Currency m_eLevelupCostType = GT_Currency.None;

    public int a_iLevelupCostValue { get { return m_iLevelupCostValue; } }
    private int m_iLevelupCostValue = 0;

    public GT_CalcValueBasis a_eLevelupCostGrowBasisType { get { return m_eLevelupCostGrowBasisType; } }
    private GT_CalcValueBasis m_eLevelupCostGrowBasisType = GT_CalcValueBasis.None;

    public float a_fLevelupCostGrowValue { get { return m_fLevelupCostGrowValue; } }
    private float m_fLevelupCostGrowValue = 0.0f;

    public int a_iOverlevelGroupID { get { return m_iOverlevelGroupID; } }
    private int m_iOverlevelGroupID = 0;

    public GT_Currency a_eTeardownGetCurrencyType { get { return m_eTeardownGetCurrencyType; } }
    private GT_Currency m_eTeardownGetCurrencyType = 0;

    public int a_iTeardownGetValue { get { return m_iTeardownGetValue; } }
    private int m_iTeardownGetValue = 0;

    public int a_iPotentialID { get { return m_iPotentialID; } }
    private int m_iPotentialID = 0;

    public string a_strDescKey { get { return m_strDescKey; } }
    private string m_strDescKey = string.Empty;

    private static Dictionary<GT_SkillApply, List<int>> m_dicSkillApplyTypeGroup = new Dictionary<GT_SkillApply, List<int>>();

    #region ConstParseKey
    private static string m_strGRADE = "GRADE";
    private static string m_strNAME_KEY = "NAME_KEY";
    private static string m_strPOSSESSION_TYPE = "POSSESION_TYPE";
    private static string m_strICON_SPRITE_KEY = "ICON_SPRITE_KEY";
    private static string m_strAPPLY_TYPE = "APPLY_TYPE";
    private static string m_strTYPE = "TYPE";
    private static string m_strIS_SINGLE = "IS_SINGLE";
    private static string m_strSHAPE_ID = "SHAPE_ID";
    private static string m_strHOLDING_EFFECT_1_TYPE = "HOLDING_EFFECT_1_TYPE";
    private static string m_strHOLDING_EFFECT_1_VALUE = "HOLDING_EFFECT_1_VALUE";
    private static string m_strHOLDING_EFFECT_1_GROW_VALUE = "HOLDING_EFFECT_1_GROW_VALUE";
    private static string m_strHOLDING_EFFECT_2_TYPE = "HOLDING_EFFECT_2_TYPE";
    private static string m_strHOLDING_EFFECT_2_VALUE = "HOLDING_EFFECT_2_VALUE";
    private static string m_strHOLDING_EFFECT_2_GROW_VALUE = "HOLDING_EFFECT_2_GROW_VALUE";
    private static string m_strLEVELUP_COST_TYPE = "LEVELUP_COST_TYPE";
    private static string m_strLEVELUP_COST_VALUE = "LEVELUP_COST_VALUE";
    private static string m_strLEVELUP_COST_GROW_BASIS_TYPE = "LEVELUP_COST_GROW_BASIS_TYPE";
    private static string m_strLEVELUP_COST_GROW_VALUE = "LEVELUP_COST_GROW_VALUE";
    private static string m_strOVERLEVEL_GROUP_ID = "OVERLEVEL_GROUP_ID";
    private static string m_strTEARDOWN_GET_CURRENCY_TYPE = "TEARDOWN_GET_CURRENCY_TYPE";
    private static string m_strTEARDOWN_GET_VALUE = "TEARDOWN_GET_VALUE";
    private static string m_strPOTENTIAL_ID = "POTENTIAL_ID";
    private static string m_strDESC_KEY = "DESC_KEY";
    #endregion
}

public class G_SkillShapeData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_bIsWorld = G_JSONCtrl.GetINT(vNode, m_strIS_WORLD) == 1 ? true : false;
        m_strUseFxID = G_JSONCtrl.GetString(vNode, m_strUSE_FX_ID);
        m_strHitFxID = G_JSONCtrl.GetString(vNode, m_strHIT_FX_ID);
        m_strShootFxID = G_JSONCtrl.GetString(vNode, m_strSHOOT_FX_ID);
        m_strSummonFxID = G_JSONCtrl.GetString(vNode, m_strSUMMON_FX_ID);
        m_strUseSoundKey = G_JSONCtrl.GetString(vNode, m_strUSE_SOUND_KEY);
        m_strRangeDetectorID = G_JSONCtrl.GetString(vNode, m_strRANGE_DETECTOR_ID);
        m_fCamShakePower = G_JSONCtrl.GetFLOAT(vNode, m_strCAM_SHAKE_POWER);
        m_fCamShakeTime = G_JSONCtrl.GetFLOAT(vNode, m_strCAM_SHAKE_TIME);
        m_iMotionType = G_JSONCtrl.GetINT(vNode, m_strMOTION_TYPE);
        m_fMotionDelay = G_JSONCtrl.GetFLOAT(vNode, m_strMOTION_DELAY);
    }
    
    public bool a_bIsWorld { get { return m_bIsWorld; } }
    private bool m_bIsWorld = false;

    public string a_strUseFxID { get { return m_strUseFxID; } }
    private string m_strUseFxID = string.Empty;
    
    public string a_strHitFxID { get { return m_strHitFxID; } }
    private string m_strHitFxID = string.Empty;

    public string a_strShootFxID { get { return m_strShootFxID; } }
    private string m_strShootFxID = string.Empty;
    
    public string a_strSummonFxID { get { return m_strSummonFxID; } }
    private string m_strSummonFxID = string.Empty;

    public string a_strUseSoundKey { get { return m_strUseSoundKey; } }
    private string m_strUseSoundKey = string.Empty;

    public string a_strRangeDetectorID { get { return m_strRangeDetectorID; } }
    private string m_strRangeDetectorID = string.Empty;

    public float a_fCamShakePower { get { return m_fCamShakePower; } }
    private float m_fCamShakePower = 0.0f;

    public float a_fCamShakeTime { get { return m_fCamShakeTime; } }
    private float m_fCamShakeTime = 0.0f;

    public int a_iMotionType { get { return m_iMotionType; } }
    private int m_iMotionType = 0;

    public float a_fMotionDelay { get { return m_fMotionDelay; } }
    private float m_fMotionDelay = 0.0f;

    #region ConstParseKey    
    private static string m_strIS_WORLD = "IS_WORLD";
    private static string m_strUSE_FX_ID = "USE_FX_ID";
    private static string m_strHIT_FX_ID = "HIT_FX_ID";
    private static string m_strSHOOT_FX_ID = "SHOOT_FX_ID";
    private static string m_strSUMMON_FX_ID = "SUMMON_FX_ID";
    private static string m_strUSE_SOUND_KEY = "USE_SOUND_KEY";
    private static string m_strRANGE_DETECTOR_ID = "RANGE_DETECTOR_ID";
    private static string m_strCAM_SHAKE_POWER = "CAM_SHAKE_POWER";
    private static string m_strCAM_SHAKE_TIME = "CAM_SHAKE_TIME";
    private static string m_strMOTION_TYPE = "MOTION_TYPE";
    private static string m_strMOTION_DELAY = "MOTION_DELAY";
    private static string m_strDARK_EFFECT_TYPE = "DARK_EFFECT_TYPE";
    #endregion
}

public class G_SkillLevelData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eSkillType = (GT_Skill)G_JSONCtrl.GetINT(vNode, m_strSKILL_TYPE);
        m_ePossesionType = (GT_Possession)G_JSONCtrl.GetINT(vNode, m_strPOSSESION_TYPE);
        m_eEquipEffectType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strEQUIP_EFFECT_TYPE);
        m_fDamage = G_JSONCtrl.GetFLOAT(vNode, m_strDAMAGE);
        m_fDamageGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strDAMAGE_GROW_VALUE);
        m_iMaxTargetCount = G_JSONCtrl.GetINT(vNode, m_strMAX_TARGET_COUNT);
        m_iHitCount = G_JSONCtrl.GetINT(vNode, m_strHIT_COUNT);
        m_iDetectCount = G_JSONCtrl.GetINT(vNode, m_strDETECT_COUNT);
        m_fCoolTime = G_JSONCtrl.GetFLOAT(vNode, m_strCOOL_TIME);
        m_fSpeed = G_JSONCtrl.GetFLOAT(vNode, m_strSPEED);
        m_fLifeTime = G_JSONCtrl.GetFLOAT(vNode, m_strLIFE_TIME);
        m_iSummonCount = G_JSONCtrl.GetINT(vNode, m_strSUMMON_COUNT);
        m_iSummonType = G_JSONCtrl.GetINT(vNode, m_strSUMMON_TYPE);
        m_iSummonAIType = G_JSONCtrl.GetINT(vNode, m_strSUMMON_AI_TYPE);
        m_iSummonDestroyType = G_JSONCtrl.GetINT(vNode, m_strSUMMON_DES_TYPE);
        m_iUnitStatusID = G_JSONCtrl.GetINT(vNode, m_strUNIT_STATUS_ID);
        m_iBuffID = G_JSONCtrl.GetINT(vNode, m_strBUFF_ID);
        m_iDebuffID = G_JSONCtrl.GetINT(vNode, m_strDEBUFF_ID);
    }

    public GT_Skill a_eSkillType { get { return m_eSkillType; } }
    private GT_Skill m_eSkillType = GT_Skill.None;

    public GT_Possession a_ePossesionType { get { return m_ePossesionType; } }
    private GT_Possession m_ePossesionType = GT_Possession.None;

    public GT_BaseStat a_eEquipEffectType { get { return m_eEquipEffectType; } }
    private GT_BaseStat m_eEquipEffectType = GT_BaseStat.None;

    public float a_fDamage { get { return m_fDamage; } }
    private float m_fDamage = 0;

    public float a_fDamageGrowValue { get { return m_fDamageGrowValue; } }
    private float m_fDamageGrowValue = 0.0f;

    public int a_iMaxTargetCount { get { return m_iMaxTargetCount; } }
    private int m_iMaxTargetCount = 0;

    public int a_iHitCount { get { return m_iHitCount; } }
    private int m_iHitCount = 0;

    public int a_iDetectCount { get { return m_iDetectCount; } }
    private int m_iDetectCount = 0;

    public float a_fCoolTime { get { return m_fCoolTime; } }
    private float m_fCoolTime = 0.0f;

    public float a_fSpeed { get { return m_fSpeed; } }
    private float m_fSpeed = 0.0f;

    public float a_fLifeTime { get { return m_fLifeTime; } }
    private float m_fLifeTime = 0.0f;

    public int a_iSummonCount { get { return m_iSummonCount; } }
    private int m_iSummonCount = 0;

    public int a_iSummonType { get { return m_iSummonType; } }
    private int m_iSummonType = 0;

    public int a_iSummonAIType { get { return m_iSummonAIType; } }
    private int m_iSummonAIType = 0;

    public int a_iSummonDestroyType { get { return m_iSummonDestroyType; } }
    private int m_iSummonDestroyType = 0;

    public int a_iUnitStatusID { get { return m_iUnitStatusID; } }
    private int m_iUnitStatusID = 0;

    public int a_iBuffID { get { return m_iBuffID; } }
    private int m_iBuffID = 0;

    public int a_iDebuffID { get { return m_iDebuffID; } }
    private int m_iDebuffID = 0;

    #region ConstParseKey
    static string m_strSKILL_TYPE = "SKILL_TYPE";
    static string m_strPOSSESION_TYPE = "POSSESION_TYPE";
    static string m_strEQUIP_EFFECT_TYPE = "EQUIP_EFFECT_TYPE";
    static string m_strDAMAGE = "DAMAGE";
    static string m_strDAMAGE_GROW_VALUE = "DAMAGE_GROW_VALUE";
    static string m_strMAX_TARGET_COUNT = "MAX_TARGET_COUNT";
    static string m_strHIT_COUNT = "HIT_COUNT";
    static string m_strDETECT_COUNT = "DETECT_COUNT";
    static string m_strCOOL_TIME = "COOL_TIME";
    static string m_strSPEED = "SPEED";
    static string m_strLIFE_TIME = "LIFE_TIME";
    static string m_strSUMMON_COUNT = "SUMMON_COUNT";
    static string m_strSUMMON_TYPE = "SUMMON_TYPE";
    static string m_strSUMMON_AI_TYPE = "SUMMON_AI_TYPE";
    static string m_strSUMMON_DES_TYPE = "SUMMON_DES_TYPE";
    static string m_strUNIT_STATUS_ID = "UNIT_STATUS_ID";
    static string m_strBUFF_ID = "BUFF_ID";
    static string m_strDEBUFF_ID = "DEBUFF_ID";
    #endregion
}

public class G_MonsterStatusData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_fAttackCoolTime = G_JSONCtrl.GetFLOAT(vNode, m_strATTACK_COOL_TIME);
        m_fAttackRange = G_JSONCtrl.GetFLOAT(vNode, m_strATTACK_RANGE);
        m_fAttackSpeed = G_JSONCtrl.GetFLOAT(vNode, m_strATTACK_SPEED);
        m_fMoveSpeed = G_JSONCtrl.GetFLOAT(vNode, m_strMOVE_SPEED);
    }

    public float a_fAttackCoolTime { get { return m_fAttackCoolTime; } }
    public float a_fAttackRange { get { return m_fAttackRange; } }
    public float a_fAttackSpeed { get { return m_fAttackSpeed; } }
    public float a_fMoveSpeed { get { return m_fMoveSpeed; } }

    private float m_fAttackCoolTime = 0.0f;
    private float m_fAttackRange = 0.0f;
    private float m_fAttackSpeed = 0.0f;
    private float m_fMoveSpeed = 0.0f;

    #region ConstParseKey
    static string m_strATTACK_COOL_TIME = "ATTACK_COOL_TIME";
    static string m_strATTACK_RANGE = "ATTACK_RANGE";
    static string m_strATTACK_SPEED = "ATTACK_SPEED";
    static string m_strMOVE_SPEED = "MOVE_SPEED";
    #endregion
}

public class G_ExpAccountData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);
        m_iExp = BigInteger.Parse(G_JSONCtrl.GetString(vNode, m_strEXP));
        m_eAccountLevelEffectStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strACCOUNT_LEVEL_EFFECT_STAT_TYPE);
        m_fAccountLevelEffectStatValue = G_JSONCtrl.GetFLOAT(vNode, m_strACCOUNT_LEVEL_EFFECT_STAT_VALUE);
    }

    public BigInteger a_iExp { get { return m_iExp; } }
    private BigInteger m_iExp = 0;

    public GT_BaseStat a_eAccountLevelEffectStatType { get { return m_eAccountLevelEffectStatType; } }
    private GT_BaseStat m_eAccountLevelEffectStatType = GT_BaseStat.None;

    public float a_fAccountLevelEffectStatValue { get { return m_fAccountLevelEffectStatValue; } }
    private float m_fAccountLevelEffectStatValue = 0.0f;

    #region ConstParseKey
    private static string m_strEXP = "EXP";
    private static string m_strACCOUNT_LEVEL_EFFECT_STAT_TYPE = "ACCOUNT_LEVEL_EFFECT_STAT_TYPE";
    private static string m_strACCOUNT_LEVEL_EFFECT_STAT_VALUE = "ACCOUNT_LEVEL_EFFECT_STAT_VALUE";
    #endregion
}

public class G_AttendanceData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_Attendance)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_iDay = G_JSONCtrl.GetINT(vNode, m_strDAY);
        m_iRandomRewardType = G_JSONCtrl.GetINT(vNode, m_strRANDOM_REWARD_TYPE);
        m_iRewardID = G_JSONCtrl.GetINT(vNode, m_strREWARD_ID);

        if (m_dicAttendanceList.ContainsKey(m_eType))
        {
            m_dicAttendanceList[m_eType].Add(a_iID);
        }
        else
        {
            List<int> vList = new List<int>();
            vList.Add(a_iID);
            m_dicAttendanceList.Add(m_eType, vList);
        }
    }

    public static void ClearStaticValues()
    {
        if (m_dicAttendanceList != null)
        {
            m_dicAttendanceList.Clear();
        }
    }

    public static void GetAttendanceList(GT_Attendance eType, ref List<int> vRetData)
    {
        if (m_dicAttendanceList.ContainsKey(eType) == false)
            return;
        
        vRetData = m_dicAttendanceList[eType];
    }

    public static int GetAttendanceMaxSize(GT_Attendance eType)
    {
        if (m_dicAttendanceList.ContainsKey(eType) == false)
            return 0;

        return m_dicAttendanceList[eType].Count;
    }

    public GT_Attendance a_eType { get { return m_eType; } }
    public int a_iDay { get { return m_iDay; } }
    public int a_iRandomRewardType { get { return m_iRandomRewardType; } }
    public int a_iRewardID { get { return m_iRewardID; } }

    private GT_Attendance m_eType = 0;
    private int m_iDay = 0;
    private int m_iRandomRewardType = 0;
    private int m_iRewardID = 0;

    private static Dictionary<GT_Attendance, List<int>> m_dicAttendanceList = new Dictionary<GT_Attendance, List<int>>();

    #region ConstParseKey
    static string m_strTYPE = "TYPE";
    static string m_strDAY = "DAY";
    static string m_strRANDOM_REWARD_TYPE = "RANDOM_REWARD_TYPE";
    static string m_strREWARD_ID = "REWARD_ID";
    #endregion
}

public class G_CurrencyData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);
        m_strNameKey = G_JSONCtrl.GetString(vNode, m_strNAME_TEXT_ID);
        m_strDescKey = G_JSONCtrl.GetString(vNode, m_strDESC_TEXT_ID);
        m_strIconKey = G_JSONCtrl.GetString(vNode, m_strICON_SPRITE);
        m_iInitAmount = G_JSONCtrl.GetINT(vNode, m_strINITAL_AMOUNT);
        m_iMaxAmount = G_JSONCtrl.GetINT(vNode, m_strMAX_AMOUNT);
        m_eRefillType = (GT_Refill)G_JSONCtrl.GetINT(vNode, m_strRIFILL_TYPE);
        m_iRefillValue = G_JSONCtrl.GetINT(vNode, m_strREFILL_VALUE);
        m_iRefillAmount = G_JSONCtrl.GetINT(vNode, m_strREFILL_AMOUNT);
        m_iRefillMaximum = G_JSONCtrl.GetINT(vNode, m_strREFILL_MAXIMUM);
        m_eFormatType = (GT_NumberFormat)G_JSONCtrl.GetINT(vNode, m_strFORMAT_TYPE);
        m_eDisplayType = (GT_CurrencyDisplay)G_JSONCtrl.GetINT(vNode, m_strWRITE_TYPE);
        m_iProbGroupID = G_JSONCtrl.GetINT(vNode, m_strPROB_GROUP_ID);
    }

    public string a_strNameKey { get { return m_strNameKey; } }
    public string a_strDescKey { get { return m_strDescKey; } }
    public string a_strIconKey { get { return m_strIconKey; } }
    public int a_iInitAmount { get { return m_iInitAmount; } }
    public int a_iMaxAmount { get { return m_iMaxAmount; } }
    public GT_Refill a_eRefillType { get { return m_eRefillType; } }
    public int a_iRefillValue { get { return m_iRefillValue; } }
    public int a_iRefillAmount { get { return m_iRefillAmount; } }
    public int a_iRefillMaximum { get { return m_iRefillMaximum; } }
    public GT_NumberFormat a_eFormatType { get { return m_eFormatType; } }
    public GT_CurrencyDisplay a_eDisplayType { get { return m_eDisplayType;} }

    private string m_strNameKey = string.Empty;
    private string m_strDescKey = string.Empty;
    private string m_strIconKey = string.Empty;
    private int m_iInitAmount = 0;
    private int m_iMaxAmount = 0;
    private GT_Refill m_eRefillType = 0;
    private int m_iRefillValue = 0;
    private int m_iRefillAmount = 0;
    private int m_iRefillMaximum = 0;
    private GT_NumberFormat m_eFormatType = GT_NumberFormat.None;
    private GT_CurrencyDisplay m_eDisplayType = GT_CurrencyDisplay.None;

    public int a_iProbGroupID { get { return m_iProbGroupID; } }
    private int m_iProbGroupID = 0;

    #region ConstParseKey
    static string m_strNAME_TEXT_ID = "NAME_TEXT_ID";
    static string m_strDESC_TEXT_ID = "DESC_TEXT_ID";
    static string m_strICON_SPRITE = "ICON_SPRITE";
    static string m_strINITAL_AMOUNT = "INITIAL_AMOUNT";
    static string m_strMAX_AMOUNT = "MAX_ACOUNT";
    static string m_strRIFILL_TYPE = "RIFILL_TYPE";
    static string m_strREFILL_VALUE = "RIFILL_VALUE";
    static string m_strREFILL_AMOUNT = "REFILL_AMOUNT";
    static string m_strREFILL_MAXIMUM = "REFILL_MAXIMUM";
    static string m_strFORMAT_TYPE = "FORMAT_TYPE";
    static string m_strWRITE_TYPE = "WRITE_TYPE";
    static string m_strPROB_GROUP_ID = "PROB_GROUP_ID";
    #endregion
}

public class G_RewardDataClass : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_iRewardID = G_JSONCtrl.GetINT(vNode, m_strREWARD_ID);
        m_iRatioGroup = G_JSONCtrl.GetINT(vNode, m_strRATIO_GROUP);
        m_iRatio = G_JSONCtrl.GetINT(vNode, m_strRATIO);
        m_iType = G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_iValue = G_JSONCtrl.GetINT(vNode, m_strVALUE);
        m_iCount = BigInteger.Parse(G_JSONCtrl.GetString(vNode, m_strCOUNT));

        if(m_dicRewardData.ContainsKey(m_iRewardID))
        {
            if (m_dicRewardData[m_iRewardID].ContainsKey(m_iRatioGroup))
                m_dicRewardData[m_iRewardID][m_iRatioGroup].Add(this);
            else
            {
                List<G_RewardDataClass> vList = new List<G_RewardDataClass>();
                vList.Add(this);
                m_dicRewardData[m_iRewardID].Add(m_iRatioGroup, vList);
            }
        }
        else
        {
            Dictionary<int, List<G_RewardDataClass>> vDic = new Dictionary<int, List<G_RewardDataClass>>();
            m_dicRewardData.Add(m_iRewardID, vDic);

            List<G_RewardDataClass> vList = new List<G_RewardDataClass>();
            vList.Add(this);
            m_dicRewardData[m_iRewardID].Add(m_iRatioGroup, vList);
        }
    }

    public static void ClearStaticValues()
    {
        if (m_dicRewardData != null)
        {
            m_dicRewardData.Clear();
        }
    }

    public static List<G_RewardDataClass> GetReward(int iRewardID)
    {
        List<G_RewardDataClass> vRewards = new List<G_RewardDataClass>();
        if (!m_dicRewardData.ContainsKey(iRewardID))
            return null;

        Dictionary<int, List<G_RewardDataClass>> vRatioGroupList = m_dicRewardData[iRewardID];
        if (vRatioGroupList == null)
            return null;

        if (vRatioGroupList.Count <= 0)
            return null;

        for(var enumerator = vRatioGroupList.GetEnumerator(); enumerator.MoveNext();)
        {
            int iKey = enumerator.Current.Key;
            if (iKey == 0)
            {
                //확정 보상 지급
                for (int i = 0; i < vRatioGroupList[iKey].Count; ++i)
                {
                    vRewards.Add(vRatioGroupList[iKey][i]);
                }
            }
            else
            {
                //랜덤 보상 지급
                int iTotalRatio = 0;
                int iRatioCounter = 0;
                for (int i = 0; i < vRatioGroupList[iKey].Count; ++i)
                    iTotalRatio += vRatioGroupList[iKey][i].a_iRatio;

                //그룹에서 1개 선택
                int iSelectRatio = Random.Range(0, iTotalRatio);
                for (int i = 0; i < vRatioGroupList[iKey].Count; ++i)
                {
                    if (iSelectRatio >= iRatioCounter && (iSelectRatio <= vRatioGroupList[iKey][i].a_iRatio + iRatioCounter))
                    {
                        vRewards.Add(vRatioGroupList[iKey][i]);
                        break;
                    }
                    iRatioCounter += vRatioGroupList[iKey][i].a_iRatio;
                }
            }
        }

        return vRewards;
    }

    public static List<G_RewardDataClass> GetAllRewardList(int iRewardID)
    {
        List<G_RewardDataClass> vRewards = new List<G_RewardDataClass>();
        if (!m_dicRewardData.ContainsKey(iRewardID))
            return null;


        Dictionary<int, List<G_RewardDataClass>> vRatioGroupList = m_dicRewardData[iRewardID];
        if (vRatioGroupList == null)
            return null;

        for(var enumerator = vRatioGroupList.GetEnumerator(); enumerator.MoveNext();)
        {
            int iGroup = enumerator.Current.Key;
            for (int i = 0; i < vRatioGroupList[iGroup].Count; ++i)
            {
                vRewards.Add(vRatioGroupList[iGroup][i]);
            }
        }

        return vRewards;
    }

    public int a_iRewardID { get { return m_iRewardID; } }
    public int a_iRatioGroup { get { return m_iRatioGroup; } }
    public int a_iRatio { get { return m_iRatio; } }
    /// <summary>
    /// 1 : 재화, 2 : 아이템
    /// </summary>
    public int a_iType { get { return m_iType; } }
    public int a_iValue { get { return m_iValue; } }
    public BigInteger a_iCount { get { return m_iCount; } }

    private int m_iRewardID = 0;
    private int m_iRatioGroup = 0;
    private int m_iRatio = 0;
    private int m_iType = 0;
    private int m_iValue = 0;
    private BigInteger m_iCount = 0;

    //m_dicRewardData[RewardID][RatioGroup][index]
    private static Dictionary<int, Dictionary<int, List<G_RewardDataClass>>> m_dicRewardData = new Dictionary<int, Dictionary<int, List<G_RewardDataClass>>>();

    #region ConstParseKey
    static string m_strREWARD_ID = "REWARD_ID";
    static string m_strRATIO_GROUP = "RATIO_GROUP";
    static string m_strRATIO = "RATIO";
    static string m_strTYPE = "TYPE";
    static string m_strVALUE = "VALUE";
    static string m_strCOUNT = "COUNT";
    #endregion
}

public class G_StatData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_strNameKey = G_JSONCtrl.GetString(vNode, m_strNAME_KEY);
        m_strSpriteKey = G_JSONCtrl.GetString(vNode, m_strSPRITE_KEY);
        m_fStatValue = G_JSONCtrl.GetFLOAT(vNode, m_strSTAT_VALUE);
        m_fStatGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strSTAT_GROW_VALUE);
        m_eCostType = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strCOST_TYPE);
        m_iCostAmount = G_JSONCtrl.GetINT(vNode, m_strCOST_AMOUNT);
        m_iCostGrowBasisType = (GT_CalcValueBasis)G_JSONCtrl.GetINT(vNode, m_strCOST_GROW_BASIS_TYPE);
        m_fCostGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strCOST_GROW_VALUE);
        m_iMaxLevel = G_JSONCtrl.GetINT(vNode, m_strMAX_LEVEL);
        m_iLockType = G_JSONCtrl.GetINT(vNode, m_strLOCK_TYPE);
        m_iLockValue = G_JSONCtrl.GetINT(vNode, m_strLOCK_VALUE);
        m_bUIOnoffType = G_JSONCtrl.GetBOOL(vNode, m_strUI_ON_OFF_TYPE);
        m_fPowerBasisValue = G_JSONCtrl.GetFLOAT(vNode, m_strPOWER_BASIS_VALUE);
        m_fPowerScore = G_JSONCtrl.GetFLOAT(vNode, m_strPOWER_SCORE);
    }

    public GT_BaseStat a_eType { get { return m_eType; } }
    private GT_BaseStat m_eType = GT_BaseStat.None;

    public string a_strNameKey { get { return m_strNameKey; } }
    private string m_strNameKey = string.Empty;

    public string a_strSpriteKey { get { return m_strSpriteKey; } }
    private string m_strSpriteKey = string.Empty;

    public float a_fStatValue { get { return m_fStatValue; } }
    private float m_fStatValue = 0.0f;

    public float a_fStatGrowValue { get { return m_fStatGrowValue; } }
    private float m_fStatGrowValue = 0.0f;

    public GT_Currency a_eCostType { get { return m_eCostType; } }
    private GT_Currency m_eCostType = GT_Currency.None;

    public int a_iCostAmount { get { return m_iCostAmount; } }
    private int m_iCostAmount = 0;

    public GT_CalcValueBasis a_iCostGrowBasisType { get { return m_iCostGrowBasisType; } }
    private GT_CalcValueBasis m_iCostGrowBasisType = GT_CalcValueBasis.None;

    public float a_fCostGrowValue { get { return m_fCostGrowValue; } }
    private float m_fCostGrowValue = 0.0f;

    public int a_iMaxLevel { get { return m_iMaxLevel; } }
    private int m_iMaxLevel = 0;

    public int a_iLockType { get { return m_iLockType; } } //ID로 연결해야합니다
    private int m_iLockType = 0;

    public int a_iLockValue { get { return m_iLockValue; } }
    private int m_iLockValue = 0;

    public bool a_bUIOnoffType { get { return m_bUIOnoffType; } }
    private bool m_bUIOnoffType = false;

    public float a_fPowerBasisValue { get { return m_fPowerBasisValue; } }
    private float m_fPowerBasisValue = 0.0f;

    public float a_fPowerScore { get { return m_fPowerScore; } }
    private float m_fPowerScore = 0.0f;

    #region ConstParseKey
    private static string m_strTYPE = "TYPE";
    private static string m_strNAME_KEY = "NAME_KEY";
    private static string m_strSPRITE_KEY = "SPRITE_KEY";
    private static string m_strSTAT_VALUE = "STAT_VALUE";
    private static string m_strSTAT_GROW_VALUE = "STAT_GROW_VALUE";
    private static string m_strCOST_TYPE = "COST_TYPE";
    private static string m_strCOST_AMOUNT = "COST_AMOUNT";
    private static string m_strCOST_GROW_BASIS_TYPE = "COST_GROW_BASIS_TYPE";
    private static string m_strCOST_GROW_VALUE = "COST_GROW_VALUE";
    private static string m_strMAX_LEVEL = "MAX_LEVEL";
    private static string m_strLOCK_TYPE = "LOCK_TYPE";
    private static string m_strLOCK_VALUE = "LOCK_VALUE";
    private static string m_strUI_ON_OFF_TYPE = "UI_ON_OFF_TYPE";
    private static string m_strPOWER_BASIS_VALUE = "POWER_BASIS_VALUE";
    private static string m_strPOWER_SCORE = "POWER_SCORE";
    #endregion
}

public class G_BaseStatData : G_StatData
{
    public G_BaseStatData() { }

    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        if (m_vStatList == null)
            m_vStatList = new List<G_StatData>();

        m_vStatList.Add(this);

        if (m_vStatViewList == null)
            m_vStatViewList = new List<GT_BaseStat>();

        switch (a_eType)
        {
            case GT_BaseStat.Base_Attack:
            case GT_BaseStat.Base_HP:
            case GT_BaseStat.Base_Defense:
            case GT_BaseStat.Base_Recovery:
            case GT_BaseStat.Base_Decrease_HitDamage:
            case GT_BaseStat.Base_AttackSpeed:
            case GT_BaseStat.Base_MoveSpeed:
            case GT_BaseStat.Base_Increase_Attack_BaseDamage:
            case GT_BaseStat.Base_Increase_SkillDamage:
            case GT_BaseStat.Base_CriticalRate:
            case GT_BaseStat.Base_CriticalDamage:
            case GT_BaseStat.Base_SuperCriticalRate:
            case GT_BaseStat.Base_SuperCriticalDamage:
            case GT_BaseStat.Base_HyperCriticalRate:
            case GT_BaseStat.Base_HyperCriticalDamage:
            case GT_BaseStat.Base_Increase_Attack_BaseMonsterDamage:
            case GT_BaseStat.Base_Increase_Attack_BossMonsterDamage:
            case GT_BaseStat.Base_Decrease_Hit_BaseMonsterDamage:
            case GT_BaseStat.Base_Decrease_Hit_BossMonsterDamage:
            case GT_BaseStat.Base_Increase_Gain_EquipmentItem:
            case GT_BaseStat.Base_Increase_Gain_EquipmentEnchantItem:
            case GT_BaseStat.Base_Increase_Gain_SkillEnchantItem:
            case GT_BaseStat.Base_Increase_Gain_GoldAmount:
            case GT_BaseStat.Base_Increase_Gain_ExpAmount:
                m_vStatViewList.Add(a_eType);
                break;
        }
    }

    public static void ClearStaticValues()
    {
        if (m_vStatList != null)
        {
            G_Utils.RemoveListAll(ref m_vStatList);
        }

        if (m_vStatViewList != null)
        {
            G_Utils.RemoveListAll(ref m_vStatViewList);
        }
    }

    public static void GetStatList(ref List<G_StatData> vRefData)
    {
        vRefData = m_vStatList;
    }

    public static void GetStatData(GT_BaseStat eType, ref G_StatData vRefData)
    {
        for (int i = 0; i < m_vStatList.Count; ++i)
        {
            if (m_vStatList[i].a_eType == eType)
            {
                vRefData = m_vStatList[i];
                break;
            }
        }
    }

    public static void GetStatviewList(ref List<GT_BaseStat> vRetData)
    {
        vRetData = m_vStatViewList;
    }

    private static List<G_StatData> m_vStatList = new List<G_StatData>();
    private static List<GT_BaseStat> m_vStatViewList = new List<GT_BaseStat>();
}

public class G_PropertyStatData : G_StatData
{
    public G_PropertyStatData() { }

    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        if (m_vStatList == null)
            m_vStatList = new List<G_StatData>();

        m_vStatList.Add(this);
    }

    public static void ClearStaticValues()
    {
        if (m_vStatList != null)
        {
            G_Utils.RemoveListAll(ref m_vStatList);
        }
    }

    public static void GetStatList(ref List<G_StatData> vRefData)
    {
        vRefData = m_vStatList;
    }

    public static void GetStatData(GT_BaseStat eType, ref G_StatData vRefData)
    {
        for (int i = 0; i < m_vStatList.Count; ++i)
        {
            if (m_vStatList[i].a_eType == eType)
            {
                vRefData = m_vStatList[i];
                break;
            }
        }
    }

    private static List<G_StatData> m_vStatList = new List<G_StatData>();
}

public class G_WingStatData : G_StatData
{
    public G_WingStatData() { }

    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_fSuccessProb = G_JSONCtrl.GetFLOAT(vNode, m_strSUCCESS_PROB);
        m_fSuccessProbDown = G_JSONCtrl.GetFLOAT(vNode, m_strSUCCESS_PROB_DOWN);

        if (m_vStatList == null)
            m_vStatList = new List<G_StatData>();

        m_vStatList.Add(this);
    }

    public static void ClearStaticValues()
    {
        if (m_vStatList != null)
        {
            G_Utils.RemoveListAll(ref m_vStatList);
        }
    }

    public static void GetStatList(ref List<G_StatData> vRefData)
    {
        vRefData = m_vStatList;
    }

    public static void GetStatData(GT_BaseStat eType, ref G_StatData vRefData)
    {
        for (int i = 0; i < m_vStatList.Count; ++i)
        {
            if (m_vStatList[i].a_eType == eType)
            {
                vRefData = m_vStatList[i];
                break;
            }
        }
    }

    private static List<G_StatData> m_vStatList = new List<G_StatData>();

    public float a_fSuccessProb { get { return m_fSuccessProb; } }
    private float m_fSuccessProb = 0.0f;

    public float a_fSuccessProbDown { get { return m_fSuccessProbDown; } }
    private float m_fSuccessProbDown = 0.0f;

    #region ConstParseKey
    private static string m_strSUCCESS_PROB = "SUCCESS_PROB";
    private static string m_strSUCCESS_PROB_DOWN = "SUCCESS_PROB_DOWN";
    #endregion
}

public class G_EquipmentData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_Equipment)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_eGrade = (GT_Grade)G_JSONCtrl.GetINT(vNode, m_strGRADE);
        m_eRarity = (GT_Rarity)G_JSONCtrl.GetINT(vNode, m_strRARITY);
        m_strNameTextKey = G_JSONCtrl.GetString(vNode, m_strNAME_TEXT_KEY);
        m_strDescTextKey = G_JSONCtrl.GetString(vNode, m_strDESC_TEXT_ID);
        m_strSpriteKey = G_JSONCtrl.GetString(vNode, m_strSPRITE_KEY);
        m_strAttachmentName = G_JSONCtrl.GetString(vNode, m_strATTACHMENT_NAME);
        m_bIsAni = G_JSONCtrl.GetBOOL(vNode, m_strIS_ANI);
        m_iAniFrame = G_JSONCtrl.GetINT(vNode, m_strANI_FRAME);

        GT_BaseStat eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strHOLDING_EFFECT_1_TYPE);
        float fValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_1_VALUE);
        float fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_1_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vHoldingEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strHOLDING_EFFECT_2_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_2_VALUE);
        fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_2_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vHoldingEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strEQUIP_EFFECT_1_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_1_VALUE);
        fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_1_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vEquipEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strEQUIP_EFFECT_2_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_2_VALUE);
        fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_2_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vEquipEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        m_eLevelupCostType = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_TYPE);
        m_iLevelupCostValue = G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_VALUE);
        m_eLevelupCostGrowBasisType = (GT_CalcValueBasis)G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_GROW_BASIS_TYPE);
        m_fLevelupCostGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strLEVELUP_COST_GROW_VALUE);
        m_iOverlevelGroupID = G_JSONCtrl.GetINT(vNode, m_strOVERLEVEL_GROUP_ID);
        m_iSynthesisGetItemID = G_JSONCtrl.GetINT(vNode, m_strSYNTHESIS_GET_ITEM_ID);
        m_iPotentialID = G_JSONCtrl.GetINT(vNode, m_strPOTENTIAL_ID);

        if (m_dicEquipmentTypeGroup.ContainsKey(m_eType) == false)
            m_dicEquipmentTypeGroup[m_eType] = new List<G_EquipmentData>();
        m_dicEquipmentTypeGroup[m_eType].Add(this);
    }

    public static void ClearStaticValues()
    {
        if (m_dicEquipmentTypeGroup != null)
        {
            m_dicEquipmentTypeGroup.Clear();
        }
    }

    public static void GetEquipmentTypeList(GT_Equipment eType, ref List<G_EquipmentData> vRetData)
    {
        if (m_dicEquipmentTypeGroup.ContainsKey(eType) == false)
            return;

        vRetData = m_dicEquipmentTypeGroup[eType];
    }

    public GT_Equipment a_eType { get { return m_eType; } }
    private GT_Equipment m_eType = GT_Equipment.None;

    public GT_Grade a_eGrade { get { return m_eGrade; } }
    private GT_Grade m_eGrade = GT_Grade.None;

    public GT_Rarity a_eRarity { get { return m_eRarity; } }
    private GT_Rarity m_eRarity = GT_Rarity.None;

    public string a_strNameTextKey { get { return m_strNameTextKey; } }
    private string m_strNameTextKey = string.Empty;

    public string a_strDescTextKey { get { return m_strDescTextKey; } }
    private string m_strDescTextKey = string.Empty;

    public string a_strSpriteKey { get { return m_strSpriteKey; } }
    private string m_strSpriteKey = string.Empty;

    public string a_strAttachmentName { get { return m_strAttachmentName; } }
    private string m_strAttachmentName = string.Empty;

    public bool a_bIsAni { get { return m_bIsAni; } }
    private bool m_bIsAni = false;

    public int a_iAniFrame { get { return m_iAniFrame; } }
    private int m_iAniFrame = 0;

    public List<G_EffectStatClass> a_vHoldingEffects { get { return m_vHoldingEffects; } }
    private List<G_EffectStatClass> m_vHoldingEffects = new List<G_EffectStatClass>();

    public List<G_EffectStatClass> a_vEquipEffects { get { return m_vEquipEffects; } }
    private List<G_EffectStatClass> m_vEquipEffects = new List<G_EffectStatClass>();

    public GT_Currency a_eLevelupCostType { get { return m_eLevelupCostType; } }
    private GT_Currency m_eLevelupCostType = GT_Currency.None;

    public int a_iLevelupCostValue { get { return m_iLevelupCostValue; } }
    private int m_iLevelupCostValue = 0;

    public GT_CalcValueBasis a_eLevelupCostGrowBasisType { get { return m_eLevelupCostGrowBasisType; } }
    private GT_CalcValueBasis m_eLevelupCostGrowBasisType = GT_CalcValueBasis.None;

    public float a_fLevelupCostGrowValue { get { return m_fLevelupCostGrowValue; } }
    private float m_fLevelupCostGrowValue = 0.0f;

    public int a_iOverlevelGroupID { get { return m_iOverlevelGroupID; } }
    private int m_iOverlevelGroupID = 0;

    public int a_iSynthesisGetItemID { get { return m_iSynthesisGetItemID; } }
    private int m_iSynthesisGetItemID = 0;

    public int a_iPotentialID { get { return m_iPotentialID; } }
    private int m_iPotentialID = 0;

    private static Dictionary<GT_Equipment, List<G_EquipmentData>> m_dicEquipmentTypeGroup = new Dictionary<GT_Equipment, List<G_EquipmentData>>();

    #region ConstParseKey
    private static string m_strTYPE = "TYPE";
    private static string m_strGRADE = "GRADE";
    private static string m_strRARITY = "RARITY";
    private static string m_strNAME_TEXT_KEY = "NAME_TEXT_KEY";
    private static string m_strDESC_TEXT_ID = "DESC_TEXT_ID";
    private static string m_strSPRITE_KEY = "SPRITE_KEY";
    private static string m_strATTACHMENT_NAME = "ATTACHMENT_NAME";
    private static string m_strIS_ANI = "IS_ANI";
    private static string m_strANI_FRAME = "ANI_FRAME";
    private static string m_strHOLDING_EFFECT_1_TYPE = "HOLDING_EFFECT_1_TYPE";
    private static string m_strHOLDING_EFFECT_1_VALUE = "HOLDING_EFFECT_1_VALUE";
    private static string m_strHOLDING_EFFECT_1_GROW_VALUE = "HOLDING_EFFECT_1_GROW_VALUE";
    private static string m_strHOLDING_EFFECT_2_TYPE = "HOLDING_EFFECT_2_TYPE";
    private static string m_strHOLDING_EFFECT_2_VALUE = "HOLDING_EFFECT_2_VALUE";
    private static string m_strHOLDING_EFFECT_2_GROW_VALUE = "HOLDING_EFFECT_2_GROW_VALUE";
    private static string m_strEQUIP_EFFECT_1_TYPE = "EQUIP_EFFECT_1_TYPE";
    private static string m_strEQUIP_EFFECT_1_VALUE = "EQUIP_EFFECT_1_VALUE";
    private static string m_strEQUIP_EFFECT_1_GROW_VALUE = "EQUIP_EFFECT_1_GROW_VALUE";
    private static string m_strEQUIP_EFFECT_2_TYPE = "EQUIP_EFFECT_2_TYPE";
    private static string m_strEQUIP_EFFECT_2_VALUE = "EQUIP_EFFECT_2_VALUE";
    private static string m_strEQUIP_EFFECT_2_GROW_VALUE = "EQUIP_EFFECT_2_GROW_VALUE";
    private static string m_strLEVELUP_COST_TYPE = "LEVELUP_COST_TYPE";
    private static string m_strLEVELUP_COST_VALUE = "LEVELUP_COST_VALUE";
    private static string m_strLEVELUP_COST_GROW_BASIS_TYPE = "LEVELUP_COST_GROW_BASIS_TYPE";
    private static string m_strLEVELUP_COST_GROW_VALUE = "LEVELUP_COST_GROW_VALUE";
    private static string m_strOVERLEVEL_GROUP_ID = "OVERLEVEL_GROUP_ID";
    private static string m_strSYNTHESIS_GET_ITEM_ID = "SYNTHESIS_GET_ITEM_ID";
    private static string m_strPOTENTIAL_ID = "POTENTIAL_ID";
    #endregion
}

public class G_CostumeData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_Equipment)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_eGrade = (GT_Grade)G_JSONCtrl.GetINT(vNode, m_strGRADE);
        m_strNameTextKey = G_JSONCtrl.GetString(vNode, m_strNAME_TEXT_KEY);
        m_strDescTextKey = G_JSONCtrl.GetString(vNode, m_strDESC_TEXT_ID);
        m_strSpriteKey = G_JSONCtrl.GetString(vNode, m_strSPRITE_KEY);
        m_strAttachmentName = G_JSONCtrl.GetString(vNode, m_strATTACHMENT_NAME);
        m_bIsAni = G_JSONCtrl.GetBOOL(vNode, m_strIS_ANI);
        m_iAniFrame = G_JSONCtrl.GetINT(vNode, m_strANI_FRAME);
        m_eShortcutType = (GT_Shortcut)G_JSONCtrl.GetINT(vNode, m_strSHORT_CUT_TYPE);

        GT_BaseStat eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strHOLDING_EFFECT_1_TYPE);
        float fValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_1_VALUE);
        float fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_1_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vHoldingEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strHOLDING_EFFECT_2_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_2_VALUE);
        fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_2_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vHoldingEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strCOSTUME_EFFECT_1_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strCOSTUME_EFFECT_1_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vCostumeEffects.Add(new G_EffectStatClass(eStatType, fValue, 0));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strCOSTUME_EFFECT_2_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strCOSTUME_EFFECT_2_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vCostumeEffects.Add(new G_EffectStatClass(eStatType, fValue, 0));

        m_eLevelupCostType = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_TYPE);
        m_iLevelupCostValue = G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_VALUE);
        m_eLevelupCostGrowBasisType = (GT_CalcValueBasis)G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_GROW_BASIS_TYPE);
        m_fLevelupCostGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strLEVELUP_COST_GROW_VALUE);
        m_iOverlevelGroupID = G_JSONCtrl.GetINT(vNode, m_strOVERLEVEL_GROUP_ID);
        m_iPotentialID = G_JSONCtrl.GetINT(vNode, m_strPOTENTIAL_ID);
    }

    public string GetSlotIconName()
    {
        return string.Format("SLOT_{0}", m_strSpriteKey);
    }

    public GT_Equipment a_eType { get { return m_eType; } }
    private GT_Equipment m_eType = GT_Equipment.None;

    public GT_Grade a_eGrade { get { return m_eGrade; } }
    private GT_Grade m_eGrade = GT_Grade.None;

    public string a_strNameTextKey { get { return m_strNameTextKey; } }
    private string m_strNameTextKey = string.Empty;

    public string a_strDescTextKey { get { return m_strDescTextKey; } }
    private string m_strDescTextKey = string.Empty;

    public string a_strSpriteKey { get { return m_strSpriteKey; } }
    private string m_strSpriteKey = string.Empty;

    public string a_strAttachmentName { get { return m_strAttachmentName; } }
    private string m_strAttachmentName = string.Empty;

    public bool a_bIsAni { get { return m_bIsAni; } }
    private bool m_bIsAni = false;

    public int a_iAniFrame { get { return m_iAniFrame; } }
    private int m_iAniFrame = 0;

    public GT_Shortcut a_eShortcutType { get { return m_eShortcutType; } }
    private GT_Shortcut m_eShortcutType = GT_Shortcut.None;

    public List<G_EffectStatClass> a_vHoldingEffects { get { return m_vHoldingEffects; } }
    private List<G_EffectStatClass> m_vHoldingEffects = new List<G_EffectStatClass>();

    public List<G_EffectStatClass> a_vCostumeEffects { get { return m_vCostumeEffects; } }
    private List<G_EffectStatClass> m_vCostumeEffects = new List<G_EffectStatClass>();

    public GT_Currency a_eLevelupCostType { get { return m_eLevelupCostType; } }
    private GT_Currency m_eLevelupCostType = GT_Currency.None;

    public int a_iLevelupCostValue { get { return m_iLevelupCostValue; } }
    private int m_iLevelupCostValue = 0;

    public GT_CalcValueBasis a_eLevelupCostGrowBasisType { get { return m_eLevelupCostGrowBasisType; } }
    private GT_CalcValueBasis m_eLevelupCostGrowBasisType = GT_CalcValueBasis.None;

    public float a_fLevelupCostGrowValue { get { return m_fLevelupCostGrowValue; } }
    private float m_fLevelupCostGrowValue = 0.0f;

    public int a_iOverlevelGroupID { get { return m_iOverlevelGroupID; } }
    private int m_iOverlevelGroupID = 0;

    public int a_iPotentialID { get { return m_iPotentialID; } }
    private int m_iPotentialID = 0;

    #region ConstParseKey
    private static string m_strTYPE = "TYPE";
    private static string m_strGRADE = "GRADE";
    private static string m_strNAME_TEXT_KEY = "NAME_TEXT_KEY";
    private static string m_strDESC_TEXT_ID = "DESC_TEXT_ID";
    private static string m_strSPRITE_KEY = "SPRITE_KEY";
    private static string m_strATTACHMENT_NAME = "ATTACHMENT_NAME";
    private static string m_strIS_ANI = "IS_ANI";
    private static string m_strANI_FRAME = "ANI_FRAME";
    private static string m_strSHORT_CUT_TYPE = "SHORT_CUT_TYPE";
    private static string m_strHOLDING_EFFECT_1_TYPE = "HOLDING_EFFECT_1_TYPE";
    private static string m_strHOLDING_EFFECT_1_VALUE = "HOLDING_EFFECT_1_VALUE";
    private static string m_strHOLDING_EFFECT_1_GROW_VALUE = "HOLDING_EFFECT_1_GROW_VALUE";
    private static string m_strHOLDING_EFFECT_2_TYPE = "HOLDING_EFFECT_2_TYPE";
    private static string m_strHOLDING_EFFECT_2_VALUE = "HOLDING_EFFECT_2_VALUE";
    private static string m_strHOLDING_EFFECT_2_GROW_VALUE = "HOLDING_EFFECT_2_GROW_VALUE";
    private static string m_strCOSTUME_EFFECT_1_TYPE = "COSTUME_EFFECT_1_TYPE";
    private static string m_strCOSTUME_EFFECT_1_VALUE = "COSTUME_EFFECT_1_VALUE";
    private static string m_strCOSTUME_EFFECT_2_TYPE = "COSTUME_EFFECT_2_TYPE";
    private static string m_strCOSTUME_EFFECT_2_VALUE = "COSTUME_EFFECT_2_VALUE";
    private static string m_strLEVELUP_COST_TYPE = "LEVELUP_COST_TYPE";
    private static string m_strLEVELUP_COST_VALUE = "LEVELUP_COST_VALUE";
    private static string m_strLEVELUP_COST_GROW_BASIS_TYPE = "LEVELUP_COST_GROW_BASIS_TYPE";
    private static string m_strLEVELUP_COST_GROW_VALUE = "LEVELUP_COST_GROW_VALUE";
    private static string m_strOVERLEVEL_GROUP_ID = "OVERLEVEL_GROUP_ID";
    private static string m_strPOTENTIAL_ID = "POTENTIAL_ID";
    #endregion
}

public class G_CharacterPetData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_iGrade = G_JSONCtrl.GetINT(vNode, m_strGRADE);
        m_strNameKey = G_JSONCtrl.GetString(vNode, m_strNAME_KEY);
        m_strIconKey = G_JSONCtrl.GetString(vNode, m_strICON_KEY);
        m_strImageName = G_JSONCtrl.GetString(vNode, m_strIMAGE_NAME);
        m_strAttachmentName = G_JSONCtrl.GetString(vNode, m_strATTACHMENT_NAME);
        m_fSize = G_JSONCtrl.GetFLOAT(vNode, m_strSIZE);
        m_fStatInit = G_JSONCtrl.GetFLOAT(vNode, m_strSTAT_INIT);
        m_fStatIncrement = G_JSONCtrl.GetFLOAT(vNode, m_strSTAT_INCREMENT);
        m_iLvUpCost1 = BigInteger.Parse(G_JSONCtrl.GetString(vNode, m_strLV_UP_COST_1));
        m_iLvUpCost2 = BigInteger.Parse(G_JSONCtrl.GetString(vNode, m_strLV_UP_COST_2));
        m_iLvUpCost3 = BigInteger.Parse(G_JSONCtrl.GetString(vNode, m_strLV_UP_COST_3));
    }

    public int a_iGrade { get { return m_iGrade; } }
    public string a_strNameKey { get { return m_strNameKey; } }
    public string a_strIconKey { get { return m_strIconKey; } }
    public string a_strImageName { get { return m_strImageName; } }
    public string a_strAttachmentName { get { return m_strAttachmentName; } }
    public float a_fSize { get { return m_fSize; } }
    public float a_fStatInit { get { return m_fStatInit; } }
    public float a_fStatIncrement { get { return m_fStatIncrement; } }
    public BigInteger a_iLvUpCost1 { get { return m_iLvUpCost1; } }
    public BigInteger a_iLvUpCost2 { get { return m_iLvUpCost2; } }
    public BigInteger a_iLvUpCost3 { get { return m_iLvUpCost3; } }

    private int m_iGrade = 0;
    private string m_strNameKey = string.Empty;
    private string m_strIconKey = string.Empty;
    private string m_strImageName = string.Empty;
    private string m_strAttachmentName = string.Empty;
    private float m_fSize = 0.0f;
    private float m_fStatInit = 0.0f;
    private float m_fStatIncrement = 0.0f;
    private BigInteger m_iLvUpCost1 = 0;
    private BigInteger m_iLvUpCost2 = 0;
    private BigInteger m_iLvUpCost3 = 0;

    #region ConstParseKey
    static string m_strGRADE = "GRADE";
    static string m_strNAME_KEY = "NAME_KEY";
    static string m_strICON_KEY = "ICON_KEY";
    static string m_strIMAGE_NAME = "IMAGE_NAME";
    static string m_strATTACHMENT_NAME = "ATTACHMENT_NAME";
    static string m_strSIZE = "SIZE";
    static string m_strSTAT_INIT = "STAT_INIT";
    static string m_strSTAT_INCREMENT = "STAT_INCREMENT";
    static string m_strLV_UP_COST_1 = "LV_UP_COST_1";
    static string m_strLV_UP_COST_2 = "LV_UP_COST_2";
    static string m_strLV_UP_COST_3 = "LV_UP_COST_3";
    #endregion
}

public class G_MissionData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eCategory = (GT_QuestCategory)G_JSONCtrl.GetINT(vNode, m_strCATEGORY);
        m_eSubCategory = (GT_QuestSubCategory)G_JSONCtrl.GetINT(vNode, m_strSUB_CATEGORY);
        m_iLinkPassID = G_JSONCtrl.GetINT(vNode, m_strLINK_PASS_ID);
        m_iOrder = G_JSONCtrl.GetINT(vNode, m_strORDER);
        m_eShortcutType = (GT_Shortcut)G_JSONCtrl.GetINT(vNode, m_strSHORTCUT_UI_TYPE);
        m_eType = (GT_Quest)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_strNameKey = G_JSONCtrl.GetString(vNode, m_strNAME_KEY);
        m_strDescKey = G_JSONCtrl.GetString(vNode, m_strDESC);
        m_iConditionValue = G_JSONCtrl.GetINT(vNode, m_strCONDITION_VALUE);
        m_iTargetValue = G_JSONCtrl.GetINT(vNode, m_strTARGET_VALUE);
        m_iReqMissionID = G_JSONCtrl.GetINT(vNode, m_strREQ_MISSION_ID);
        m_iNormalRewardID = G_JSONCtrl.GetINT(vNode, m_strNORMAL_REWARD_ID);
        m_iSpecialRewardID = G_JSONCtrl.GetINT(vNode, m_strSPECIAL_REWARD_ID);

        // 메인 카테고리별 모음
        if (m_dicCategoryGroup.ContainsKey(m_eCategory))
            m_dicCategoryGroup[m_eCategory].Add(this);
        else
        {
            List<G_MissionData> vList = new List<G_MissionData>
            {
                this
            };
            m_dicCategoryGroup.Add(m_eCategory, vList);
        }

        // 서브 카테고리별 모음
        if (m_dicSubCategoryGroup.ContainsKey(m_eSubCategory))
            m_dicSubCategoryGroup[m_eSubCategory].Add(this);
        else
        {
            List<G_MissionData> vList = new List<G_MissionData>();
            vList.Add(this);
            m_dicSubCategoryGroup.Add(m_eSubCategory, vList);
        }

        // 미션 타입별 모음
        if (m_dicIdByTypeGroup.ContainsKey(m_eType))
            m_dicIdByTypeGroup[m_eType].Add(a_iID);
        else
        {
            List<int> vList = new List<int>
            {
                a_iID
            };
            m_dicIdByTypeGroup.Add(m_eType, vList);
        }
    }

    public static void ClearStaticValues()
    {
        if (m_dicCategoryGroup != null)
        {
            m_dicCategoryGroup.Clear();
        }

        if (m_dicSubCategoryGroup != null)
        {
            m_dicSubCategoryGroup.Clear();
        }

        if (m_dicIdByTypeGroup != null)
        {
            m_dicIdByTypeGroup.Clear();
        }
    }

    public static void GetMissionListByCategory(GT_QuestCategory eCategory, ref List<G_MissionData> vRetList)
    {
        if (m_dicCategoryGroup.ContainsKey(eCategory) == false)
            return;

        vRetList = m_dicCategoryGroup[eCategory];
    }

    public static void GetMissionListBySubCategory(GT_QuestSubCategory eSubCategory, ref List<G_MissionData> vRetList)
    {
        if (m_dicSubCategoryGroup.ContainsKey(eSubCategory) == false)
            return;

        vRetList = m_dicSubCategoryGroup[eSubCategory];
    }

    public static void GetMissionIDListByMissionType(GT_Quest eType, ref List<int> vIndexList)
    {
        if (m_dicIdByTypeGroup.ContainsKey(eType) == false)
            return;

        vIndexList = m_dicIdByTypeGroup[eType];
    }

    public static int GetLastGuideQuestID()
    {
        int iLastGuideQuestID = G_Constant.m_iLastGuideQuestID;
        if (m_dicCategoryGroup.ContainsKey(GT_QuestCategory.Guide) == false)
        {
            return iLastGuideQuestID;
        }

        List<G_MissionData> vMissionList = m_dicCategoryGroup[GT_QuestCategory.Guide];
        if (vMissionList != null && vMissionList.Count > 0)
        {
            iLastGuideQuestID = vMissionList[vMissionList.Count - 1].a_iID;
        }

        return iLastGuideQuestID;
    }
    
    public GT_QuestCategory a_eCategory { get { return m_eCategory; } }
    private GT_QuestCategory m_eCategory = GT_QuestCategory.None;

    public GT_QuestSubCategory a_eSubCategory { get { return m_eSubCategory; } }
    private GT_QuestSubCategory m_eSubCategory = GT_QuestSubCategory.None;

    public int a_iLinkPassID { get { return m_iLinkPassID; } }
    private int m_iLinkPassID = 0;

    public int a_iOrder { get { return m_iOrder; } set { m_iOrder = value; } }
    private int m_iOrder = 0;

    public GT_Shortcut a_eShortcutType { get { return m_eShortcutType; } }
    private GT_Shortcut m_eShortcutType = GT_Shortcut.None;

    public GT_Quest a_eType { get { return m_eType; } }
    private GT_Quest m_eType = 0;

    public string a_strNameKey { get { return m_strNameKey; } }
    private string m_strNameKey = string.Empty;

    public string a_strDescKey { get { return m_strDescKey; } }
    private string m_strDescKey = string.Empty;

    public int a_iConditionValue { get { return m_iConditionValue; } }
    private int m_iConditionValue = 0;

    public int a_iTargetValue { get { return m_iTargetValue; } }
    private int m_iTargetValue = 0;

    public int a_iReqMissionID { get { return m_iReqMissionID; } }
    private int m_iReqMissionID = 0;

    public int a_iNormalRewardID { get { return m_iNormalRewardID; } }
    private int m_iNormalRewardID = 0;

    public int a_iSpecialRewardID { get { return m_iSpecialRewardID; } }
    private int m_iSpecialRewardID = 0;

    private static Dictionary<GT_QuestCategory, List<G_MissionData>> m_dicCategoryGroup = new Dictionary<GT_QuestCategory, List<G_MissionData>>();
    private static Dictionary<GT_QuestSubCategory, List<G_MissionData>> m_dicSubCategoryGroup = new Dictionary<GT_QuestSubCategory, List<G_MissionData>>();
    private static Dictionary<GT_Quest, List<int>> m_dicIdByTypeGroup = new Dictionary<GT_Quest, List<int>>();

    #region ConstParseKey
    static string m_strCATEGORY = "CATEGORY";
    static string m_strSUB_CATEGORY = "SUB_CATEGORY";
    static string m_strLINK_PASS_ID = "LINK_PASS_ID";
    static string m_strORDER = "ORDER";
    static string m_strSHORTCUT_UI_TYPE = "SHORTCUT_UI_TYPE";
    static string m_strTYPE = "TYPE";
    static string m_strNAME_KEY = "NAME_KEY";
    static string m_strDESC = "DESC";
    static string m_strCONDITION_VALUE = "CONDITION_VALUE";
    static string m_strTARGET_VALUE = "TARGET_VALUE";
    static string m_strREQ_MISSION_ID = "REQ_MISSION_ID";
    static string m_strNORMAL_REWARD_ID = "NORMAL_REWARD_ID";
    static string m_strSPECIAL_REWARD_ID = "SPECIAL_REWARD_ID";
    #endregion
}

public class G_RewardData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_iRewardID = G_JSONCtrl.GetINT(vNode, m_strREWARD_ID);
        m_iProbGroupID = G_JSONCtrl.GetINT(vNode, m_strPROB_GROUP_ID);
        m_eType = (GT_Reward)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_eDetailType = (GT_Equipment)G_JSONCtrl.GetINT(vNode, m_strDETAIL_TYPE);
        m_iValue = G_JSONCtrl.GetINT(vNode, m_strVALUE);
        m_iCount = G_JSONCtrl.GetINT(vNode, m_strCOUNT);

        // RewardID 별 모음
        if (m_dicRewardGroup.ContainsKey(m_iRewardID))
            m_dicRewardGroup[m_iRewardID].Add(this);
        else
        {
            List<G_RewardData> vList = new List<G_RewardData>();
            vList.Add(this);
            m_dicRewardGroup.Add(m_iRewardID, vList);
        }
    }

    public static void ClearStaticValues()
    {
        if (m_dicRewardGroup != null)
        {
            m_dicRewardGroup.Clear();
        }
    }

    public static void GetRewardListByRewardID(int iRewardID, ref List<G_RewardData> vRetList)
    {
        if (m_dicRewardGroup.ContainsKey(iRewardID) == false)
            return;

        vRetList = m_dicRewardGroup[iRewardID];
    }

    public int a_iRewardID { get { return m_iRewardID; } }
    private int m_iRewardID = 0;

    public int a_iProbGroupID { get { return m_iProbGroupID; } }
    private int m_iProbGroupID = 0;

    public GT_Reward a_eType { get { return m_eType; } }
    private GT_Reward m_eType = GT_Reward.None;

    public GT_Equipment a_eDetailType { get { return m_eDetailType; } }
    private GT_Equipment m_eDetailType = GT_Equipment.None;

    public int a_iValue { get { return m_iValue; } }
    private int m_iValue = 0;

    public int a_iCount { get { return m_iCount; } }
    private int m_iCount = 0;

    private static Dictionary<int, List<G_RewardData>> m_dicRewardGroup = new Dictionary<int, List<G_RewardData>>();
    
    #region ConstParseKey
    static string m_strREWARD_ID = "REWARD_ID";
    static string m_strPROB_GROUP_ID = "PROB_GROUP_ID";
    static string m_strTYPE = "TYPE";
    static string m_strDETAIL_TYPE = "DETAIL_TYPE";
    static string m_strVALUE = "VALUE";
    static string m_strCOUNT = "COUNT";
    #endregion
}

public class G_CurrencyGroupData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eMenuType = (GT_UI)G_JSONCtrl.GetINT(vNode, m_strUI_MENU_TYPE);
        m_eCurrencyType = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strCURRENCY_TYPE);

        if (m_dicCurrencyGroup.ContainsKey(m_eMenuType))
        {
            m_dicCurrencyGroup[m_eMenuType].Add(this);
        }
        else
        {
            List<G_CurrencyGroupData> vDataList = new List<G_CurrencyGroupData>
            {
                this
            };
            m_dicCurrencyGroup.Add(m_eMenuType, vDataList);
        }
    }

    public static void ClearStaticValues()
    {
        if (m_dicCurrencyGroup != null)
        {
            m_dicCurrencyGroup.Clear();
        }
    }

    public static List<G_CurrencyGroupData> GetCurrencyGroupData(GT_UI eType)
    {
        if (m_dicCurrencyGroup.ContainsKey(eType) == false)
            return null;

        return m_dicCurrencyGroup[eType];
    }

    public GT_UI a_eMenuType { get { return m_eMenuType; } }
    private GT_UI m_eMenuType = GT_UI.None;

    public GT_Currency a_eCurrencyType { get { return m_eCurrencyType; } }
    private GT_Currency m_eCurrencyType = GT_Currency.None;

    private static Dictionary<GT_UI, List<G_CurrencyGroupData>> m_dicCurrencyGroup = new Dictionary<GT_UI, List<G_CurrencyGroupData>>();

    #region ConstParseKey
    private static string m_strUI_MENU_TYPE = "UI_MENU_TYPE";
    private static string m_strCURRENCY_TYPE = "CURRENCY_TYPE";
    #endregion
}

public class G_GlobalVariableData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_strVariable = G_JSONCtrl.GetString(vNode, m_strVARIABLE);
        m_fValue = G_JSONCtrl.GetFLOAT(vNode, m_strVAR_VALUE);

        ParseGrouping();
    }

    public static void ClearStaticValues()
    {
        if (m_dicSkillSlotOpenConditions != null)
        {
            m_dicSkillSlotOpenConditions.Clear();
        }

        if (m_dicAbilitySlotOpenCondtions != null)
        {
            m_dicAbilitySlotOpenCondtions.Clear();
        }
    }

    private void ParseGrouping()
    {
        int iCount;
        // 어빌리티 슬롯 개방 조건들
        if (m_strVariable.Contains("ABILITY_SLOT") && m_strVariable.Contains("OPEN_ACCOUNT_LEVEL"))
        {
            iCount = m_dicAbilitySlotOpenCondtions.Count;
            if (m_dicAbilitySlotOpenCondtions.ContainsKey(iCount))
                return;

            m_dicAbilitySlotOpenCondtions.Add(iCount, (int)m_fValue);
        }
        // 스킬 슬롯 개방 조건들
        else if (m_strVariable.Contains("SKILL_SLOT") && m_strVariable.Contains("OPEN_ACCOUNT_LEVEL"))
        {
            iCount = m_dicSkillSlotOpenConditions.Count;
            if (m_dicSkillSlotOpenConditions.ContainsKey(iCount))
                return;

            m_dicSkillSlotOpenConditions.Add(iCount, (int)m_fValue);
        }
    }

    public static bool GetOpenConditionData(GT_OpenCondition eContents, ref Dictionary<int, int> dicRetData)
    {
        switch (eContents)
        {
            case GT_OpenCondition.SkillSlot:
                dicRetData = m_dicSkillSlotOpenConditions;
                break;
            case GT_OpenCondition.AbilitySlot:
                dicRetData = m_dicAbilitySlotOpenCondtions;
                break;
        }

        if (dicRetData == null)
            return false;

        return true;
    }

    public string a_strVariable { get { return m_strVariable; } }
    private string m_strVariable = string.Empty;

    public float a_fValue { get { return m_fValue; } }
    private float m_fValue = 0.0f;

    private static Dictionary<int, int> m_dicSkillSlotOpenConditions = new Dictionary<int, int>();
    private static Dictionary<int, int> m_dicAbilitySlotOpenCondtions = new Dictionary<int, int>();

    #region ConstParseKey
    private static string m_strVARIABLE = "VARIABLE";
    private static string m_strVAR_VALUE = "VAR_VALUE";
    #endregion
}

public class G_StartPossessionData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_StartPossession)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_iDetailType = G_JSONCtrl.GetINT(vNode, m_strDETAIL_TYPE);
        m_iItemID = G_JSONCtrl.GetINT(vNode, m_strITEM_ID);
        m_iCount = G_JSONCtrl.GetINT(vNode, m_strCOUNT);

        if (m_dicStartPossessionGroup.ContainsKey(m_eType))
        {
            m_dicStartPossessionGroup[m_eType].Add(this);
        }
        else
        {
            List<G_StartPossessionData> vDataList = new List<G_StartPossessionData>
            {
                this
            };
            m_dicStartPossessionGroup.Add(m_eType, vDataList);
        }
    }

    public static void ClearStaticValues()
    {
        if (m_dicStartPossessionGroup != null)
        {
            m_dicStartPossessionGroup.Clear();
        }
    }

    public static void GetStartPossesionGroupData(GT_StartPossession eType, ref List<G_StartPossessionData> vRetData)
    {
        if (m_dicStartPossessionGroup.ContainsKey(eType) == false)
            return;

        vRetData = m_dicStartPossessionGroup[eType];
    }

    public GT_StartPossession a_eType { get { return m_eType; } }
    private GT_StartPossession m_eType = GT_StartPossession.None;

    public int a_iDetailType { get { return m_iDetailType; } }
    private int m_iDetailType = 0;

    public int a_iItemID { get { return m_iItemID; } }
    private int m_iItemID = 0;

    public int a_iCount { get { return m_iCount; } }
    private int m_iCount = 0;

    private static Dictionary<GT_StartPossession, List<G_StartPossessionData>> m_dicStartPossessionGroup = new Dictionary<GT_StartPossession, List<G_StartPossessionData>>();

    #region ConstParseKey
    private static string m_strTYPE = "TYPE";
    private static string m_strDETAIL_TYPE = "DETAIL_TYPE";
    private static string m_strITEM_ID = "ITEM_ID";
    private static string m_strCOUNT = "COUNT";
    #endregion
}

public class G_AbilityData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_iProbGroupID = G_JSONCtrl.GetINT(vNode, m_strPROB_GROUP_ID);
        m_iPropClassID = G_JSONCtrl.GetINT(vNode, m_strPROB_CLASS_ID);
        m_eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_fMinValue = G_JSONCtrl.GetFLOAT(vNode, m_strMIN);
        m_fMaxValue = G_JSONCtrl.GetFLOAT(vNode, m_strMAX);

        if (m_dicAbilityClassGroup.ContainsKey(m_iPropClassID))
            m_dicAbilityClassGroup[m_iPropClassID].Add(this);
        else
        {
            List<G_AbilityData> vList = new List<G_AbilityData>();
            vList.Add(this);
            m_dicAbilityClassGroup.Add(m_iPropClassID, vList);
        }
    }

    public static void ClearStaticValues()
    {
        if (m_dicAbilityClassGroup != null)
        {
            m_dicAbilityClassGroup.Clear();
        }
    }

    public static List<G_AbilityData> GetAbilityClassGroup(int iClassID)
    {
        if (m_dicAbilityClassGroup.ContainsKey(iClassID) == false)
        {
            Debug.LogError("G_AbilityData :: GetAbilityClassGroup :: Not found Data iClassID : " + iClassID);
            return null;
        }

        return m_dicAbilityClassGroup[iClassID];
    }

    public int a_iProbGroupID { get { return m_iProbGroupID; } }
    private int m_iProbGroupID = 0;

    public int a_iPropClassID { get { return m_iPropClassID; } }
    private int m_iPropClassID = 0;

    public GT_BaseStat a_eStatType { get { return m_eStatType; } }
    private GT_BaseStat m_eStatType = GT_BaseStat.None;

    public float a_fMinValue { get { return m_fMinValue; } }
    private float m_fMinValue = 0.0f;

    public float a_fMaxValue { get { return m_fMaxValue; } }
    private float m_fMaxValue = 0.0f;

    private static Dictionary<int, List<G_AbilityData>> m_dicAbilityClassGroup = new Dictionary<int, List<G_AbilityData>>();

    #region ConstParseKey
    private static string m_strPROB_GROUP_ID = "PROB_GROUP_ID";
    private static string m_strPROB_CLASS_ID = "PROB_CLASS_ID";
    private static string m_strTYPE = "TYPE";
    private static string m_strMIN = "MIN";
    private static string m_strMAX = "MAX";
    #endregion
}

public class G_ProbabilityData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eTargetType = (GT_ProbTarget)G_JSONCtrl.GetINT(vNode, m_strTARGET_TYPE);
        m_eClass = (GT_Class)G_JSONCtrl.GetINT(vNode, m_strCLASS);
        m_strNameTextKey = G_JSONCtrl.GetString(vNode, m_strNAME_TEXT_KEY);
        m_strSpriteKey = G_JSONCtrl.GetString(vNode, m_strSPRITE_KEY);
        m_eItemGrade = (GT_Grade)G_JSONCtrl.GetINT(vNode, m_strITEM_GRADE);
        m_eItemRarity = (GT_Rarity)G_JSONCtrl.GetINT(vNode, m_strITEM_RARITY);
        m_iTargetTypeOrID = G_JSONCtrl.GetINT(vNode, m_strTARGET_TYPE_OR_ID);
        m_iCount = G_JSONCtrl.GetBIGINTEGER(vNode, m_strCOUNT);
        m_iProbGroupID = G_JSONCtrl.GetINT(vNode, m_strPROB_GROUP_ID);
        m_iProbValue = G_JSONCtrl.GetINT(vNode, m_strPROB_VALUE);

        if (m_dicProbabilityGroup.ContainsKey(m_iProbGroupID))
            m_dicProbabilityGroup[m_iProbGroupID].Add(this);
        else
        {
            List<G_ProbabilityData> vList = new List<G_ProbabilityData>();
            vList.Add(this);
            m_dicProbabilityGroup.Add(m_iProbGroupID, vList);
        }
    }

    public static void ClearStaticValues()
    {
        if (m_dicProbabilityGroup != null)
        {
            m_dicProbabilityGroup.Clear();
        }
    }

    public static void GetProbabilityGroup(int iGroupID, ref List<G_ProbabilityData> vRetData)
    {
        if (m_dicProbabilityGroup.ContainsKey(iGroupID) == false)
        {
            Debug.LogWarning("G_ProbabilityData :: GetProbabilityGroup :: Not found probabiliry group id : " + iGroupID);
            return;
        }

        vRetData = m_dicProbabilityGroup[iGroupID];
    }

    public GT_ProbTarget a_eTargetType { get { return m_eTargetType; } set { m_eTargetType = value; } }
    private GT_ProbTarget m_eTargetType = GT_ProbTarget.None;

    public GT_Class a_eClass { get { return m_eClass; } }
    private GT_Class m_eClass = GT_Class.Normal;

    public string a_strNameTextKey { get { return m_strNameTextKey; } }
    private string m_strNameTextKey = string.Empty;

    public string a_strSpriteKey { get { return m_strSpriteKey; } }
    private string m_strSpriteKey = string.Empty;

    public GT_Grade a_eItemGrade { get { return m_eItemGrade; } }
    private GT_Grade m_eItemGrade = GT_Grade.None;

    public GT_Rarity a_eItemRarity { get { return m_eItemRarity; } }
    private GT_Rarity m_eItemRarity = GT_Rarity.None;

    public int a_iTargetTypeOrID { get { return m_iTargetTypeOrID; } set { m_iTargetTypeOrID = value; } }
    private int m_iTargetTypeOrID = 0;

    public BigInteger a_iCount { get { return m_iCount; } set { m_iCount = value; } }
    private BigInteger m_iCount = 0;

    public int a_iProbGroupID { get { return m_iProbGroupID; } }
    private int m_iProbGroupID = 0;

    public int a_iProbValue { get { return m_iProbValue; } }
    private int m_iProbValue = 0;

    private static Dictionary<int, List<G_ProbabilityData>> m_dicProbabilityGroup = new Dictionary<int, List<G_ProbabilityData>>();

    #region ConstParseKey
    private static string m_strTARGET_TYPE = "TARGET_TYPE";
    private static string m_strCLASS = "CLASS";
    private static string m_strNAME_TEXT_KEY = "NAME_TEXT_KEY";
    private static string m_strSPRITE_KEY = "SPRITE_KEY";
    private static string m_strITEM_GRADE = "ITEM_GRADE";
    private static string m_strITEM_RARITY = "ITEM_RARITY";
    private static string m_strTARGET_TYPE_OR_ID = "TARGET_TYPE_OR_ID";
    private static string m_strCOUNT = "COUNT";
    private static string m_strPROB_GROUP_ID = "PROB_GROUP_ID";
    private static string m_strPROB_VALUE = "PROB_VALUE";
    #endregion
}

public class G_AccessUnitData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_Equipment)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_eGrade = (GT_Grade)G_JSONCtrl.GetINT(vNode, m_strGRADE);
        m_eParity = (GT_Rarity)G_JSONCtrl.GetINT(vNode, m_strPARITY);
        m_strNameTextKey = G_JSONCtrl.GetString(vNode, m_strNAME_TEXT_KEY);
        m_strSpriteKey = G_JSONCtrl.GetString(vNode, m_strSPRITE_KEY);
        m_strAttachmentName = G_JSONCtrl.GetString(vNode, m_strATTACHMENT_NAME);
        m_bIsAni = G_JSONCtrl.GetBOOL(vNode, m_strIS_ANI);
        m_iAniFrame = G_JSONCtrl.GetINT(vNode, m_strANI_FRAME);

        GT_BaseStat eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strHOLDING_EFFECT_1_TYPE);
        float fValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_1_VALUE);
        float fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_1_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vHoldingEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strHOLDING_EFFECT_2_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_2_VALUE);
        fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_2_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vHoldingEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strEQUIP_EFFECT_1_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_1_VALUE);
        fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_1_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vEquipEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strEQUIP_EFFECT_2_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_2_VALUE);
        fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_2_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vEquipEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        m_eLevelupCostType = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_TYPE);
        m_iLevelupCostValue = G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_VALUE);
        m_eLevelupCostGrowBasisType = (GT_CalcValueBasis)G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_GROW_BASIS_TYPE);
        m_fLevelupCostGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strLEVELUP_COST_GROW_VALUE);
        m_iOverlevelGroupID = G_JSONCtrl.GetINT(vNode, m_strOVERLEVEL_GROUP_ID);
        m_iEnchantSlotCount = G_JSONCtrl.GetINT(vNode, m_strENCHANT_SLOT_COUNT);
        m_iProbGroupID = G_JSONCtrl.GetINT(vNode, m_strPROB_GROUP_ID);
        m_iPotentialID = G_JSONCtrl.GetINT(vNode, m_strPOTENTIAL_ID);
        m_iSkill_1_ID = G_JSONCtrl.GetINT(vNode, m_strSKILL_1_ID);
    }

    public GT_Equipment a_eType { get { return m_eType; } }
    private GT_Equipment m_eType = GT_Equipment.None;

    public GT_Grade a_eGrade { get { return m_eGrade; } }
    private GT_Grade m_eGrade = GT_Grade.None;

    public GT_Rarity a_eParity { get { return m_eParity; } }
    private GT_Rarity m_eParity = GT_Rarity.None;

    public string a_strNameTextKey { get { return m_strNameTextKey; } }
    private string m_strNameTextKey = string.Empty;

    public string a_strSpriteKey { get { return m_strSpriteKey; } }
    private string m_strSpriteKey = string.Empty;

    public string a_strAttachmentName { get { return m_strAttachmentName; } }
    private string m_strAttachmentName = string.Empty;

    public bool a_bIsAni { get { return m_bIsAni; } }
    private bool m_bIsAni = false;

    public int a_iAniFrame { get { return m_iAniFrame; } }
    private int m_iAniFrame = 0;

    public List<G_EffectStatClass> a_vHoldingEffects { get { return m_vHoldingEffects; } }
    private List<G_EffectStatClass> m_vHoldingEffects = new List<G_EffectStatClass>();

    public List<G_EffectStatClass> a_vEquipEffects { get { return m_vEquipEffects; } }
    private List<G_EffectStatClass> m_vEquipEffects = new List<G_EffectStatClass>();

    public GT_Currency a_eLevelupCostType { get { return m_eLevelupCostType; } }
    private GT_Currency m_eLevelupCostType = GT_Currency.None;

    public int a_iLevelupCostValue { get { return m_iLevelupCostValue; } }
    private int m_iLevelupCostValue = 0;

    public GT_CalcValueBasis a_eLevelupCostGrowBasisType { get { return m_eLevelupCostGrowBasisType; } }
    private GT_CalcValueBasis m_eLevelupCostGrowBasisType = GT_CalcValueBasis.None;

    public float a_fLevelupCostGrowValue { get { return m_fLevelupCostGrowValue; } }
    private float m_fLevelupCostGrowValue = 0.0f;

    public int a_iOverlevelGroupID { get { return m_iOverlevelGroupID; } }
    private int m_iOverlevelGroupID = 0;

    public int a_iEnchantSlotCount { get { return m_iEnchantSlotCount; } }
    private int m_iEnchantSlotCount = 0;

    public int a_iProbGroupID { get { return m_iProbGroupID; } }
    private int m_iProbGroupID = 0;

    public int a_iPotentialID { get { return m_iPotentialID; } }
    private int m_iPotentialID = 0;

    public int a_iSkill_1_ID { get { return m_iSkill_1_ID; } }
    private int m_iSkill_1_ID = 0;

    #region ConstParseKey
    private static string m_strTYPE = "TYPE";
    private static string m_strGRADE = "GRADE";
    private static string m_strPARITY = "PARITY";
    private static string m_strNAME_TEXT_KEY = "NAME_TEXT_KEY";
    private static string m_strSPRITE_KEY = "SPRITE_KEY";
    private static string m_strATTACHMENT_NAME = "ATTACHMENT_NAME";
    private static string m_strIS_ANI = "IS_ANI";
    private static string m_strANI_FRAME = "ANI_FRAME";
    private static string m_strHOLDING_EFFECT_1_TYPE = "HOLDING_EFFECT_1_TYPE";
    private static string m_strHOLDING_EFFECT_1_VALUE = "HOLDING_EFFECT_1_VALUE";
    private static string m_strHOLDING_EFFECT_1_GROW_VALUE = "HOLDING_EFFECT_1_GROW_VALUE";
    private static string m_strHOLDING_EFFECT_2_TYPE = "HOLDING_EFFECT_2_TYPE";
    private static string m_strHOLDING_EFFECT_2_VALUE = "HOLDING_EFFECT_2_VALUE";
    private static string m_strHOLDING_EFFECT_2_GROW_VALUE = "HOLDING_EFFECT_2_GROW_VALUE";
    private static string m_strEQUIP_EFFECT_1_TYPE = "EQUIP_EFFECT_1_TYPE";
    private static string m_strEQUIP_EFFECT_1_VALUE = "EQUIP_EFFECT_1_VALUE";
    private static string m_strEQUIP_EFFECT_1_GROW_VALUE = "EQUIP_EFFECT_1_GROW_VALUE";
    private static string m_strEQUIP_EFFECT_2_TYPE = "EQUIP_EFFECT_2_TYPE";
    private static string m_strEQUIP_EFFECT_2_VALUE = "EQUIP_EFFECT_2_VALUE";
    private static string m_strEQUIP_EFFECT_2_GROW_VALUE = "EQUIP_EFFECT_2_GROW_VALUE";
    private static string m_strLEVELUP_COST_TYPE = "LEVELUP_COST_TYPE";
    private static string m_strLEVELUP_COST_VALUE = "LEVELUP_COST_VALUE";
    private static string m_strLEVELUP_COST_GROW_BASIS_TYPE = "LEVELUP_COST_GROW_BASIS_TYPE";
    private static string m_strLEVELUP_COST_GROW_VALUE = "LEVELUP_COST_GROW_VALUE";
    private static string m_strOVERLEVEL_GROUP_ID = "OVERLEVEL_GROUP_ID";
    private static string m_strENCHANT_SLOT_COUNT = "ENCHANT_SLOT_COUNT";
    private static string m_strPROB_GROUP_ID = "PROB_GROUP_ID";
    private static string m_strPOTENTIAL_ID = "POTENTIAL_ID";
    private static string m_strSKILL_1_ID = "SKILL_1_ID";
    #endregion
}

public class G_ExtragearData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_Equipment)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_eGrade = (GT_Grade)G_JSONCtrl.GetINT(vNode, m_strGRADE);
        m_eExtragearType = (GT_RuneType)G_JSONCtrl.GetINT(vNode, m_strEXTRA_GEAR_TYPE);
        m_strNameTextKey = G_JSONCtrl.GetString(vNode, m_strNAME_TEXT_KEY);
        m_strDescTextKey = G_JSONCtrl.GetString(vNode, m_strDESC_TEXT_ID);
        m_strSpriteKey = G_JSONCtrl.GetString(vNode, m_strSPRITE_KEY);
        m_fWeightValue = G_JSONCtrl.GetFLOAT(vNode, m_strWEIGHT_VALUE);
        m_fWeightGrowthValue = G_JSONCtrl.GetFLOAT(vNode, m_strWEIGHT_GROW_VALUE);

        GT_BaseStat eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strEQUIP_EFFECT_1_TYPE);
        float fValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_1_VALUE);
        float fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_1_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vEquipEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strEQUIP_EFFECT_2_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_2_VALUE);
        fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_2_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vEquipEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        m_eLevelupCostType = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_TYPE);
        m_iLevelupCostValue = G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_VALUE);
        m_eLevelupCostGrowBasisType = (GT_CalcValueBasis)G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_GROW_BASIS_TYPE);
        m_fLevelupCostGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strLEVELUP_COST_GROW_VALUE);
        m_iMaxOverlevelStep = G_JSONCtrl.GetINT(vNode, m_strMAX_OVERLEVEL_STEP);
        m_iStartMaxlevel = G_JSONCtrl.GetINT(vNode, m_strSTART_MAX_LEVEL);
        m_iRandomExpandLevelMin = G_JSONCtrl.GetINT(vNode, m_strRANDOM_LEVEL_EXPAND_MIN);
        m_iRandomExpandLevelMax = G_JSONCtrl.GetINT(vNode, m_strRANDOM_LEVEL_EXPAND_MAX);
        m_iOverlevelCostValue = G_JSONCtrl.GetINT(vNode, m_strOVERLEVEL_COST_VALUE);
        m_eOverlevelCostGrowBasisType = (GT_CalcValueBasis)G_JSONCtrl.GetINT(vNode, m_strOVERLEVEL_COST_GROW_BASIS_TYPE);
        m_fOverlevelCostGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strOVERLEVEL_COST_GROW_VALUE);
        m_eTeardownGetCurrencyType = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strTEARDOWN_GET_CURRENCY_TYPE);
        m_iTeardownGetValue = G_JSONCtrl.GetINT(vNode, m_strTEARDOWN_GET_VALUE);
        m_iPotentialID = G_JSONCtrl.GetINT(vNode, m_strPOTENTIAL_ID);
    }

    public GT_Equipment a_eType { get { return m_eType; } }
    private GT_Equipment m_eType = GT_Equipment.None;

    public GT_Grade a_eGrade { get { return m_eGrade; } }
    private GT_Grade m_eGrade = GT_Grade.None;

    public GT_RuneType a_eExtragearType { get { return m_eExtragearType; } }
    private GT_RuneType m_eExtragearType = GT_RuneType.None;

    public string a_strNameTextKey { get { return m_strNameTextKey; } }
    private string m_strNameTextKey = string.Empty;

    public string a_strDescTextKey { get { return m_strDescTextKey; } }
    private string m_strDescTextKey = string.Empty;

    public string a_strSpriteKey { get { return m_strSpriteKey; } }
    private string m_strSpriteKey = string.Empty;

    public float a_fWeightValue { get { return m_fWeightValue; } }
    private float m_fWeightValue = 0.0f;

    public float a_fWeightGrowthValue { get { return m_fWeightGrowthValue; } }
    private float m_fWeightGrowthValue = 0.0f;

    public List<G_EffectStatClass> a_vEquipEffects { get { return m_vEquipEffects; } }
    private List<G_EffectStatClass> m_vEquipEffects = new List<G_EffectStatClass>();

    public GT_Currency a_eLevelupCostType { get { return m_eLevelupCostType; } }
    private GT_Currency m_eLevelupCostType = GT_Currency.None;

    public int a_iLevelupCostValue { get { return m_iLevelupCostValue; } }
    private int m_iLevelupCostValue = 0;

    public GT_CalcValueBasis a_eLevelupCostGrowBasisType { get { return m_eLevelupCostGrowBasisType; } }
    private GT_CalcValueBasis m_eLevelupCostGrowBasisType = GT_CalcValueBasis.None;

    public float a_fLevelupCostGrowValue { get { return m_fLevelupCostGrowValue; } }
    private float m_fLevelupCostGrowValue = 0.0f;

    public int a_iMaxOverlevelStep { get { return m_iMaxOverlevelStep; } }
    private int m_iMaxOverlevelStep = 0;

    public int a_iStartMaxlevel { get { return m_iStartMaxlevel; } }
    private int m_iStartMaxlevel = 0;

    public int a_iRandomExpandLevelMin { get { return m_iRandomExpandLevelMin; } }
    private int m_iRandomExpandLevelMin = 0;

    public int a_iRandomExpandLevelMax { get { return m_iRandomExpandLevelMax; } }
    private int m_iRandomExpandLevelMax = 0;

    public int a_iOverlevelCostValue { get { return m_iOverlevelCostValue; } }
    private int m_iOverlevelCostValue = 0;

    public GT_CalcValueBasis a_eOverlevelCostGrowBasisType { get { return m_eOverlevelCostGrowBasisType; } }
    private GT_CalcValueBasis m_eOverlevelCostGrowBasisType = GT_CalcValueBasis.None;

    public float a_fOverlevelCostGrowValue { get { return m_fOverlevelCostGrowValue; } }
    private float m_fOverlevelCostGrowValue = 0.0f;

    public GT_Currency a_eTeardownGetCurrencyType { get { return m_eTeardownGetCurrencyType; } }
    private GT_Currency m_eTeardownGetCurrencyType = 0;

    public int a_iTeardownGetValue { get { return m_iTeardownGetValue; } }
    private int m_iTeardownGetValue = 0;

    public int a_iPotentialID { get { return m_iPotentialID; } }
    private int m_iPotentialID = 0;

    #region ConstParseKey
    private static string m_strTYPE = "TYPE";
    private static string m_strGRADE = "GRADE";
    private static string m_strEXTRA_GEAR_TYPE = "EXTRA_GEAR_TYPE";
    private static string m_strNAME_TEXT_KEY = "NAME_TEXT_KEY";
    private static string m_strDESC_TEXT_ID = "DESC_TEXT_ID";
    private static string m_strSPRITE_KEY = "SPRITE_KEY";
    private static string m_strWEIGHT_VALUE = "WEIGHT_VALUE";
    private static string m_strWEIGHT_GROW_VALUE = "WEIGHT_GROW_VALUE";
    private static string m_strEQUIP_EFFECT_1_TYPE = "EQUIP_EFFECT_1_TYPE";
    private static string m_strEQUIP_EFFECT_1_VALUE = "EQUIP_EFFECT_1_VALUE";
    private static string m_strEQUIP_EFFECT_1_GROW_VALUE = "EQUIP_EFFECT_1_GROW_VALUE";
    private static string m_strEQUIP_EFFECT_2_TYPE = "EQUIP_EFFECT_2_TYPE";
    private static string m_strEQUIP_EFFECT_2_VALUE = "EQUIP_EFFECT_2_VALUE";
    private static string m_strEQUIP_EFFECT_2_GROW_VALUE = "EQUIP_EFFECT_2_GROW_VALUE";
    private static string m_strLEVELUP_COST_TYPE = "LEVELUP_COST_TYPE";
    private static string m_strLEVELUP_COST_VALUE = "LEVELUP_COST_VALUE";
    private static string m_strLEVELUP_COST_GROW_BASIS_TYPE = "LEVELUP_COST_GROW_BASIS_TYPE";
    private static string m_strLEVELUP_COST_GROW_VALUE = "LEVELUP_COST_GROW_VALUE";
    private static string m_strMAX_OVERLEVEL_STEP = "MAX_OVERLEVEL_STEP";
    private static string m_strSTART_MAX_LEVEL = "START_MAX_LEVEL";
    private static string m_strRANDOM_LEVEL_EXPAND_MIN = "RANDOM_LEVEL_EXPAND_MIN";
    private static string m_strRANDOM_LEVEL_EXPAND_MAX = "RANDOM_LEVEL_EXPAND_MAX";
    private static string m_strOVERLEVEL_COST_VALUE = "OVERLEVEL_COST_VALUE";
    private static string m_strOVERLEVEL_COST_GROW_BASIS_TYPE = "OVERLEVEL_COST_GROW_BASIS_TYPE";
    private static string m_strOVERLEVEL_COST_GROW_VALUE = "OVERLEVEL_COST_GROW_VALUE";
    private static string m_strTEARDOWN_GET_CURRENCY_TYPE = "TEARDOWN_GET_CURRENCY_TYPE";
    private static string m_strTEARDOWN_GET_VALUE = "TEARDOWN_GET_VALUE";
    private static string m_strPOTENTIAL_ID = "POTENTIAL_ID";
    #endregion
}

public class G_SupportUnitData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_Equipment)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_eGrade = (GT_Grade)G_JSONCtrl.GetINT(vNode, m_strGRADE);
        m_strNameTextKey = G_JSONCtrl.GetString(vNode, m_strNAME_TEXT_KEY);
        m_strDescTextKey = G_JSONCtrl.GetString(vNode, m_strDESC_TEXT_ID);
        m_strSpriteKey = G_JSONCtrl.GetString(vNode, m_strSPRITE_KEY);
        m_strAttachmentName = G_JSONCtrl.GetString(vNode, m_strATTACHMENT_NAME);
        m_bIsAni = G_JSONCtrl.GetBOOL(vNode, m_strIS_ANI);
        m_iAniFrame = G_JSONCtrl.GetINT(vNode, m_strANI_FRAME);

        GT_BaseStat eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strHOLDING_EFFECT_1_TYPE);
        float fValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_1_VALUE);
        float fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_1_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vHoldingEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strHOLDING_EFFECT_2_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_2_VALUE);
        fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_2_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vHoldingEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strEQUIP_EFFECT_1_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_1_VALUE);
        fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_1_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vEquipEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strEQUIP_EFFECT_2_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_2_VALUE);
        fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_2_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vEquipEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strRIDE_EFFECT_1_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strRIDE_EFFECT_1_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vRideEffects.Add(new G_RideEffectStatClass(eStatType, fValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strRIDE_EFFECT_2_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strRIDE_EFFECT_2_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vRideEffects.Add(new G_RideEffectStatClass(eStatType, fValue));

        m_eLevelupCostType = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_TYPE);
        m_iLevelupCostValue = G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_VALUE);
        m_eLevelupCostGrowBasisType = (GT_CalcValueBasis)G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_GROW_BASIS_TYPE);
        m_fLevelupCostGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strLEVELUP_COST_GROW_VALUE);
        m_iOverlevelGroupID = G_JSONCtrl.GetINT(vNode, m_strOVERLEVEL_GROUP_ID);
        m_eTeardownGetCurrencyType = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strTEARDOWN_GET_CURRENCY_TYPE);
        m_iTeardownGetValue = G_JSONCtrl.GetINT(vNode, m_strTEARDOWN_GET_VALUE);
        m_iPotentialID = G_JSONCtrl.GetINT(vNode, m_strPOTENTIAL_ID);
    }

    public GT_Equipment a_eType { get { return m_eType; } }
    private GT_Equipment m_eType = GT_Equipment.None;

    public GT_Grade a_eGrade { get { return m_eGrade; } }
    private GT_Grade m_eGrade = GT_Grade.None;

    public string a_strNameTextKey { get { return m_strNameTextKey; } }
    private string m_strNameTextKey = string.Empty;

    public string a_strDescTextKey { get { return m_strDescTextKey; } }
    private string m_strDescTextKey = string.Empty;

    public string a_strSpriteKey { get { return m_strSpriteKey; } }
    private string m_strSpriteKey = string.Empty;

    public string a_strAttachmentName { get { return m_strAttachmentName; } }
    private string m_strAttachmentName = string.Empty;

    public bool a_bIsAni { get { return m_bIsAni; } }
    private bool m_bIsAni = false;

    public int a_iAniFrame { get { return m_iAniFrame; } }
    private int m_iAniFrame = 0;

    public List<G_EffectStatClass> a_vHoldingEffects { get { return m_vHoldingEffects; } }
    private List<G_EffectStatClass> m_vHoldingEffects = new List<G_EffectStatClass>();

    public List<G_EffectStatClass> a_vEquipEffects { get { return m_vEquipEffects; } }
    private List<G_EffectStatClass> m_vEquipEffects = new List<G_EffectStatClass>();

    public List<G_RideEffectStatClass> a_vRideEffects { get { return m_vRideEffects; } }
    private List<G_RideEffectStatClass> m_vRideEffects = new List<G_RideEffectStatClass>();

    public GT_Currency a_eLevelupCostType { get { return m_eLevelupCostType; } }
    private GT_Currency m_eLevelupCostType = GT_Currency.None;

    public int a_iLevelupCostValue { get { return m_iLevelupCostValue; } }
    private int m_iLevelupCostValue = 0;

    public GT_CalcValueBasis a_eLevelupCostGrowBasisType { get { return m_eLevelupCostGrowBasisType; } }
    private GT_CalcValueBasis m_eLevelupCostGrowBasisType = GT_CalcValueBasis.None;

    public float a_fLevelupCostGrowValue { get { return m_fLevelupCostGrowValue; } }
    private float m_fLevelupCostGrowValue = 0.0f;

    public int a_iOverlevelGroupID { get { return m_iOverlevelGroupID; } }
    private int m_iOverlevelGroupID = 0;

    public GT_Currency a_eTeardownGetCurrencyType { get { return m_eTeardownGetCurrencyType; } }
    private GT_Currency m_eTeardownGetCurrencyType = 0;

    public int a_iTeardownGetValue { get { return m_iTeardownGetValue; } }
    private int m_iTeardownGetValue = 0;

    public int a_iPotentialID { get { return m_iPotentialID; } }
    private int m_iPotentialID = 0;

    #region ConstParseKey
    private static string m_strTYPE = "TYPE";
    private static string m_strGRADE = "GRADE";
    private static string m_strNAME_TEXT_KEY = "NAME_TEXT_KEY";
    private static string m_strDESC_TEXT_ID = "DESC_TEXT_ID";
    private static string m_strSPRITE_KEY = "SPRITE_KEY";
    private static string m_strATTACHMENT_NAME = "ATTACHMENT_NAME";
    private static string m_strIS_ANI = "IS_ANI";
    private static string m_strANI_FRAME = "ANI_FRAME";
    private static string m_strHOLDING_EFFECT_1_TYPE = "HOLDING_EFFECT_1_TYPE";
    private static string m_strHOLDING_EFFECT_1_VALUE = "HOLDING_EFFECT_1_VALUE";
    private static string m_strHOLDING_EFFECT_1_GROW_VALUE = "HOLDING_EFFECT_1_GROW_VALUE";
    private static string m_strHOLDING_EFFECT_2_TYPE = "HOLDING_EFFECT_2_TYPE";
    private static string m_strHOLDING_EFFECT_2_VALUE = "HOLDING_EFFECT_2_VALUE";
    private static string m_strHOLDING_EFFECT_2_GROW_VALUE = "HOLDING_EFFECT_2_GROW_VALUE";
    private static string m_strEQUIP_EFFECT_1_TYPE = "EQUIP_EFFECT_1_TYPE";
    private static string m_strEQUIP_EFFECT_1_VALUE = "EQUIP_EFFECT_1_VALUE";
    private static string m_strEQUIP_EFFECT_1_GROW_VALUE = "EQUIP_EFFECT_1_GROW_VALUE";
    private static string m_strEQUIP_EFFECT_2_TYPE = "EQUIP_EFFECT_2_TYPE";
    private static string m_strEQUIP_EFFECT_2_VALUE = "EQUIP_EFFECT_2_VALUE";
    private static string m_strEQUIP_EFFECT_2_GROW_VALUE = "EQUIP_EFFECT_2_GROW_VALUE";
    private static string m_strRIDE_EFFECT_1_TYPE = "RIDE_EFFECT_1_TYPE";
    private static string m_strRIDE_EFFECT_1_VALUE = "RIDE_EFFECT_1_VALUE";
    private static string m_strRIDE_EFFECT_2_TYPE = "RIDE_EFFECT_2_TYPE";
    private static string m_strRIDE_EFFECT_2_VALUE = "RIDE_EFFECT_2_VALUE";
    private static string m_strLEVELUP_COST_TYPE = "LEVELUP_COST_TYPE";
    private static string m_strLEVELUP_COST_VALUE = "LEVELUP_COST_VALUE";
    private static string m_strLEVELUP_COST_GROW_BASIS_TYPE = "LEVELUP_COST_GROW_BASIS_TYPE";
    private static string m_strLEVELUP_COST_GROW_VALUE = "LEVELUP_COST_GROW_VALUE";
    private static string m_strOVERLEVEL_GROUP_ID = "OVERLEVEL_GROUP_ID";
    private static string m_strTEARDOWN_GET_CURRENCY_TYPE = "TEARDOWN_GET_CURRENCY_TYPE";
    private static string m_strTEARDOWN_GET_VALUE = "TEARDOWN_GET_VALUE";
    private static string m_strPOTENTIAL_ID = "POTENTIAL_ID";
    #endregion
}

public class G_BuffData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_BuffType)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_fTime = G_JSONCtrl.GetFLOAT(vNode, m_strTIME);
        m_fValue = G_JSONCtrl.GetFLOAT(vNode, m_strVALUE);
    }

    public GT_BuffType a_eType { get { return m_eType; } }
    private GT_BuffType m_eType = GT_BuffType.None;

    public float a_fTime { get { return m_fTime; } }
    private float m_fTime = 0.0f;

    public float a_fValue { get { return m_fValue; } }
    private float m_fValue = 0.0f;

    #region ConstParseKey
    private static string m_strTYPE = "TYPE";
    private static string m_strTIME = "TIME";
    private static string m_strVALUE = "VALUE";
    #endregion
}

public class G_DebuffData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_Debuff)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_fTime = G_JSONCtrl.GetFLOAT(vNode, m_strTIME);
        m_fValue = G_JSONCtrl.GetFLOAT(vNode, m_strVALUE);
    }

    public GT_Debuff a_eType { get { return m_eType; } }
    private GT_Debuff m_eType = GT_Debuff.None;

    public float a_fTime { get { return m_fTime; } }
    private float m_fTime = 0.0f;

    public float a_fValue { get { return m_fValue; } }
    private float m_fValue = 0.0f;

    #region ConstParseKey
    private static string m_strTYPE = "TYPE";
    private static string m_strTIME = "TIME";
    private static string m_strVALUE = "VALUE";
    #endregion
}

// 메인 타입별 스테이지 데이터 모음
public class G_StageByFieldSubType
{
    public G_StageByFieldSubType() { Clear(); }

    public void Clear()
    {
        if (m_dicStageGroup == null)
            m_dicStageGroup = new Dictionary<int, List<G_StageData>>();
        else
            m_dicStageGroup.Clear();

        m_eSubType = GT_FieldSub.None;
    }

    public void AddStage(ref G_StageData vData)
    {
        if (m_eSubType != vData.a_eSubType)
            return;

        if (m_dicStageGroup.ContainsKey(vData.a_iGroup) == false)
            m_dicStageGroup[vData.a_iGroup] = new List<G_StageData>();

        // 중복 체크
        if (m_dicStageGroup[vData.a_iGroup].Count > 0)
        {
            for (int i = 0; i < m_dicStageGroup[vData.a_iGroup].Count; ++i)
                if (m_dicStageGroup[vData.a_iGroup][i].a_iID == vData.a_iID)
                    return;
        }

        m_dicStageGroup[vData.a_iGroup].Add(vData);
    }

    // 월드별로 모음
    private Dictionary<int, List<G_StageData>> m_dicStageGroup = new Dictionary<int, List<G_StageData>>();

    public GT_FieldSub a_eSubType { get { return m_eSubType; } set { m_eSubType = value; } }
    private GT_FieldSub m_eSubType = GT_FieldSub.None;
}



public class G_StageData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_Field)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_eSubType = (GT_FieldSub)G_JSONCtrl.GetINT(vNode, m_strSUB_TYPE);
        m_iGroup = G_JSONCtrl.GetINT(vNode, m_strGROUP);
        m_iStageNumber = G_JSONCtrl.GetINT(vNode, m_strSTAGE_NUMBER);
        m_iLevel = G_JSONCtrl.GetINT(vNode, m_strLEVEL);
        m_strNameKey = G_JSONCtrl.GetString(vNode, m_strNAME_KEY);
        m_strFieldName = G_JSONCtrl.GetString(vNode, m_strFIELD_NAME);
        m_iKillPoint = G_JSONCtrl.GetINT(vNode, m_strKILL_POINT);
        m_iGoalPoint = G_JSONCtrl.GetINT(vNode, m_strGOAL_POINT);
        m_fTimeLimit = G_JSONCtrl.GetFLOAT(vNode, m_strTIME_LIMIT);
        m_fSpawnDelay = G_JSONCtrl.GetFLOAT(vNode, m_strSPAWN_DELAY);
        /*m_iSpawnMin = G_JSONCtrl.GetINT(vNode, m_strSPAWN_MIN);
        m_iSpawnMax = G_JSONCtrl.GetINT(vNode, m_strSPAWN_MAX);*/
        m_iSpawnCount = G_JSONCtrl.GetINT(vNode, m_strSPAWN_COUNT);
        m_iMaxSpawnCount = G_JSONCtrl.GetINT(vNode, m_strMAX_SPAWN_COUNT);

        int iSpawnMonsterID = G_JSONCtrl.GetINT(vNode, m_strMONSTER_ID_1);
        m_vSpawnMonsterIDs.Add(iSpawnMonsterID);
        iSpawnMonsterID = G_JSONCtrl.GetINT(vNode, m_strMONSTER_ID_2);
        m_vSpawnMonsterIDs.Add(iSpawnMonsterID);
        iSpawnMonsterID = G_JSONCtrl.GetINT(vNode, m_strMONSTER_ID_3);
        m_vSpawnMonsterIDs.Add(iSpawnMonsterID);

        m_iBossID = G_JSONCtrl.GetINT(vNode, m_strBOSS_ID);
        m_iSpecialReq = G_JSONCtrl.GetINT(vNode, m_strSPECIAL_REQ);
        m_iDisplayReward = G_JSONCtrl.GetINT(vNode, m_strDISPLAY_REWARD);
        m_iNormalReward = G_JSONCtrl.GetINT(vNode, m_strNORMAL_REWARD);
        m_iClearReward = G_JSONCtrl.GetINT(vNode, m_strCLEAR_REWARD);
        m_iOfflineReward = G_JSONCtrl.GetINT(vNode, m_strOFFLINE_REWARD);
        m_eEnterConditionEquipItemType = (GT_Equipment)G_JSONCtrl.GetINT(vNode, m_strENTER_CONDITION_EQUIP_ITEM_TYPE);
        m_iEnterConditionEquipItemID = G_JSONCtrl.GetINT(vNode, m_strENTER_CONDITION_EQUIP_ITEM_ID);
        m_iBossSummonGaugeMaxValue = G_JSONCtrl.GetINT(vNode, m_strBOSS_SUMMON_GAUGE_MAX_VALUE);
        m_iBossSummonGaugeGetValue = G_JSONCtrl.GetINT(vNode, m_strBOSS_SUMMON_GAUGE_GET_VALUE);
        m_iPetPointMax = G_JSONCtrl.GetINT(vNode, m_strPET_POINT_MAX);
        m_iMonsterKillGetPetPoint = G_JSONCtrl.GetINT(vNode, m_strMONSTER_KILL_GET_PET_POINT);

        if (m_dicFieldWorldGroup.ContainsKey(m_eSubType) == false)
            m_dicFieldWorldGroup[m_eSubType] = new Dictionary<int, List<int>>();

        if (m_dicFieldWorldGroup[m_eSubType].ContainsKey(m_iGroup) == false)
            m_dicFieldWorldGroup[m_eSubType][m_iGroup] = new List<int>();

        // 중복 검사
        bool bDuplicate = false;
        if (m_dicFieldWorldGroup[m_eSubType][m_iGroup].Count > 0)
        {
            for (int i = 0; i < m_dicFieldWorldGroup[m_eSubType][m_iGroup].Count; ++i)
            {
                if (m_dicFieldWorldGroup[m_eSubType][m_iGroup][i] == a_iID)
                {
                    bDuplicate = true;
                    break;
                }
            }
        }

        if (bDuplicate == false)
            m_dicFieldWorldGroup[m_eSubType][m_iGroup].Add(a_iID);

        // 날개의 시련만 모음
        if (m_eType == GT_Field.Wing_Trial)
        {
            if (m_dicFieldWingTrialGroup.ContainsKey(a_iStageNumber) == false)
                m_dicFieldWingTrialGroup.Add(a_iStageNumber, a_iID);
        }
    }

    public static void ClearStaticValues()
    {
        if (m_dicFieldWorldGroup != null)
        {
            m_dicFieldWorldGroup.Clear();
        }

        if (m_dicFieldWingTrialGroup != null)
        {
            m_dicFieldWingTrialGroup.Clear();
        }
    }

    public static void GetWorldStageList(GT_FieldSub eSubType, int iWorldID, ref List<int> vRetData)
    {
        if (m_dicFieldWorldGroup.ContainsKey(eSubType) == false)
            return;

        if (m_dicFieldWorldGroup[eSubType].ContainsKey(iWorldID) == false)
            return;

        vRetData = m_dicFieldWorldGroup[eSubType][iWorldID];
    }

    public static int GetFirstStageID(GT_FieldSub eSubType, int iWorldID)
    {
        if (m_dicFieldWorldGroup.ContainsKey(eSubType) == false)
            return 0;

        if (m_dicFieldWorldGroup[eSubType].Count == 0)
            return 0;

        if (m_dicFieldWorldGroup[eSubType].ContainsKey(iWorldID) == false)
            return 0;

        if (m_dicFieldWorldGroup[eSubType][iWorldID].Count == 0)
            return 0;

        return m_dicFieldWorldGroup[eSubType][iWorldID][0];
    }

    // 마지막 스테이지 직전의 스테이지 아이디를 반환한다...
    public static int GetLastStageID(GT_FieldSub eSubType, int iWorldID)
    {
        if (m_dicFieldWorldGroup.ContainsKey(eSubType) == false)
            return 0;

        if (m_dicFieldWorldGroup[eSubType].Count == 0)
            return 0;

        if (m_dicFieldWorldGroup[eSubType].ContainsKey(iWorldID) == false)
            return 0;

        if (m_dicFieldWorldGroup[eSubType][iWorldID].Count == 0)
            return 0;

        int iSize = m_dicFieldWorldGroup[eSubType][iWorldID].Count;
        return m_dicFieldWorldGroup[eSubType][iWorldID][iSize - 1];
    }

    // 모든 월드의 끝 스테이지 (최종 스테이지) 를 알아온다
    public static int GetFinalStageID(GT_FieldSub eSubType)
    {
        if (m_dicFieldWorldGroup.ContainsKey(eSubType) == false)
            return 0;

        if (m_dicFieldWorldGroup[eSubType].Count == 0)
            return 0;

        int iStageID = 0;
        Dictionary<int, List<int>> vAllWorldStages = m_dicFieldWorldGroup[eSubType];
        if (vAllWorldStages.Count > 0)
        {
            int iEndIndex;
            List<int> vEndWorldStageList;
            switch (eSubType)
            {
                case GT_FieldSub.Stage_Normal:
                case GT_FieldSub.Stage_Conquest:
                    iEndIndex = vAllWorldStages.Count;
                    break;
                default:
                    iEndIndex = vAllWorldStages.Count - 1;
                    break;
            }
            vEndWorldStageList = vAllWorldStages[iEndIndex];
            if (vEndWorldStageList.Count > 0)
            {
                iStageID = vEndWorldStageList[vEndWorldStageList.Count - 1];
            }
        }

        return iStageID;
    }

    public static int GetFirstDungeonStageID(GT_FieldSub eSubType)
    {
        if (m_dicFieldWorldGroup.ContainsKey(eSubType) == false)
            return 0;

        if (m_dicFieldWorldGroup[eSubType].Count == 0)
            return 0;

        // 던전의 GroupID는 없음 (0)
        if (m_dicFieldWorldGroup[eSubType].ContainsKey(0) == false)
            return 0;

        if (m_dicFieldWorldGroup[eSubType][0].Count == 0)
            return 0;

        return m_dicFieldWorldGroup[eSubType][0][0];
    }

    public static int GetFirstWingTestStageID()
    {
        if (m_dicFieldWingTrialGroup == null)
            return 0;
        
        if (m_dicFieldWingTrialGroup.Count == 0)
            return 0;

        return m_dicFieldWingTrialGroup[1];
    }

    public static int GetStageID(GT_FieldSub eSubType, int iWorldID, int iStageNumber)
    {
        if (m_dicFieldWorldGroup.ContainsKey(eSubType) == false)
            return 0;

        if (m_dicFieldWorldGroup[eSubType].Count == 0)
            return 0;

        if (m_dicFieldWorldGroup[eSubType].ContainsKey(iWorldID) == false)
            return 0;

        if (m_dicFieldWorldGroup[eSubType][iWorldID].Count == 0)
            return 0;

        if (iStageNumber > m_dicFieldWorldGroup[eSubType][iWorldID].Count - 1)
            return 0;

        return m_dicFieldWorldGroup[eSubType][iWorldID][iStageNumber];
    }

    public static int GetMaxWorldCount(GT_FieldSub eSubType, int iWorldID)
    {
        if (m_dicFieldWorldGroup.ContainsKey(eSubType) == false)
            return 0;

        if (m_dicFieldWorldGroup[eSubType].Count == 0)
            return 0;

        if (m_dicFieldWorldGroup[eSubType].ContainsKey(iWorldID) == false)
            return 0;

        return m_dicFieldWorldGroup[eSubType].Count;
    }

    public void GetSpawnMonsterIDs(ref List<int> vRetData)
    {
        vRetData = m_vSpawnMonsterIDs;
    }

    public static int GetWingTrialStageID(int iStageNumber)
    {
        if (m_dicFieldWingTrialGroup.ContainsKey(iStageNumber) == false)
            return 0;

        return m_dicFieldWingTrialGroup[iStageNumber];
    }

    public GT_Field a_eType { get { return m_eType; } }
    private GT_Field m_eType = GT_Field.None;

    public GT_FieldSub a_eSubType { get { return m_eSubType; } }
    private GT_FieldSub m_eSubType = GT_FieldSub.None;

    public int a_iGroup { get { return m_iGroup; } }
    private int m_iGroup = 0;

    public int a_iStageNumber { get { return m_iStageNumber; } }
    private int m_iStageNumber = 0;

    public int a_iLevel { get { return m_iLevel; } }
    private int m_iLevel = 0;

    public string a_strNameKey { get { return m_strNameKey; } }
    private string m_strNameKey = string.Empty;

    public string a_strFieldName { get { return m_strFieldName; } }
    private string m_strFieldName = string.Empty;

    public int a_iKillPoint { get { return m_iKillPoint; } }
    private int m_iKillPoint = 0;

    public int a_iGoalPoint { get { return m_iGoalPoint; } }
    private int m_iGoalPoint = 0;

    public float a_fTimeLimit { get { return m_fTimeLimit; } }
    private float m_fTimeLimit = 0;

    public float a_fSpawnDelay { get { return m_fSpawnDelay; } }
    private float m_fSpawnDelay = 0;

    /*public int a_iSpawnMin { get { return m_iSpawnMin; } set { m_iSpawnMin = value; } }
    private int m_iSpawnMin = 0;

    public int a_iSpawnMax { get { return m_iSpawnMax; } set { m_iSpawnMax = value; } }
    private int m_iSpawnMax = 0;*/

    public int a_iSpawnCount { get { return m_iSpawnCount; } }
    private int m_iSpawnCount = 0;

    public int a_iMaxSpawnCount { get { return m_iMaxSpawnCount; } }
    private int m_iMaxSpawnCount = 0;

    private List<int> m_vSpawnMonsterIDs = new List<int>();

    public int a_iBossID { get { return m_iBossID; } }
    private int m_iBossID = 0;

    public int a_iSpecialReq { get { return m_iSpecialReq; } }
    private int m_iSpecialReq = 0;

    public int a_iDisplayReward { get { return m_iDisplayReward; } }
    private int m_iDisplayReward = 0;

    public int a_iNormalReward { get { return m_iNormalReward; } }
    private int m_iNormalReward = 0;

    public int a_iClearReward { get { return m_iClearReward; } }
    private int m_iClearReward = 0;

    public int a_iOfflineReward { get { return m_iOfflineReward; } }
    private int m_iOfflineReward = 0;

    public GT_Equipment a_eEnterConditionEquipItemType { get { return m_eEnterConditionEquipItemType; } }
    private GT_Equipment m_eEnterConditionEquipItemType = 0;

    public int a_iEnterConditionEquipItemID { get { return m_iEnterConditionEquipItemID; } }
    private int m_iEnterConditionEquipItemID = 0;

    public int a_iBossSummonGaugeMaxValue { get { return m_iBossSummonGaugeMaxValue;} }
    private int m_iBossSummonGaugeMaxValue = 0;

    public int a_iBossSummonGaugeGetValue { get { return m_iBossSummonGaugeGetValue; } }
    private int m_iBossSummonGaugeGetValue = 0;

    public int a_iPetPointMax { get { return m_iPetPointMax; } }
    private int m_iPetPointMax = 0;

    public int a_iMonsterKillGetPetPoint { get { return m_iMonsterKillGetPetPoint; } }
    private int m_iMonsterKillGetPetPoint = 0;

    private static Dictionary<GT_FieldSub, Dictionary<int, List<int>>> m_dicFieldWorldGroup = new Dictionary<GT_FieldSub, Dictionary<int, List<int>>>();

    private static Dictionary<int, int> m_dicFieldWingTrialGroup = new Dictionary<int, int>();

    #region ConstParseKey
    private static string m_strTYPE = "TYPE";
    private static string m_strSUB_TYPE = "SUB_TYPE";
    private static string m_strGROUP = "GROUP";
    private static string m_strSTAGE_NUMBER = "STAGE_NUMBER";
    private static string m_strLEVEL = "LEVEL";
    private static string m_strNAME_KEY = "NAME_KEY";
    private static string m_strFIELD_NAME = "FIELD_NAME";
    private static string m_strKILL_POINT = "KILL_POINT";
    private static string m_strGOAL_POINT = "GOAL_POINT";
    private static string m_strTIME_LIMIT = "TIME_LIMIT";
    private static string m_strSPAWN_DELAY = "SPAWN_DELAY";
    /*private static string m_strSPAWN_MIN = "SPAWN_MIN";
    private static string m_strSPAWN_MAX = "SPAWN_MAX";*/
    private static string m_strSPAWN_COUNT = "SPAWN_COUNT";
    private static string m_strMAX_SPAWN_COUNT = "MAX_SPAWN_COUNT";
    private static string m_strMONSTER_ID_1 = "MONSTER_ID_1";
    private static string m_strMONSTER_ID_2 = "MONSTER_ID_2";
    private static string m_strMONSTER_ID_3 = "MONSTER_ID_3";
    private static string m_strBOSS_ID = "BOSS_ID";
    private static string m_strSPECIAL_REQ = "SPECIAL_REQ";
    private static string m_strDISPLAY_REWARD = "DISPLAY_REWARD";
    private static string m_strNORMAL_REWARD = "NORMAL_REWARD";
    private static string m_strCLEAR_REWARD = "CLEAR_REWARD";
    private static string m_strOFFLINE_REWARD = "OFFLINE_REWARD";
    private static string m_strENTER_CONDITION_EQUIP_ITEM_TYPE = "ENTER_CONDITION_EQUIP_ITEM_TYPE";
    private static string m_strENTER_CONDITION_EQUIP_ITEM_ID = "ENTER_CONDITION_EQUIP_ITEM_ID";
    private static string m_strBOSS_SUMMON_GAUGE_MAX_VALUE = "BOSS_SUMMON_GAUGE_MAX_VALUE";
    private static string m_strBOSS_SUMMON_GAUGE_GET_VALUE = "BOSS_SUMMON_GAUGE_GET_VALUE";
    private static string m_strPET_POINT_MAX = "PET_POINT_MAX";
    private static string m_strMONSTER_KILL_GET_PET_POINT = "MONSTER_KILL_GET_PET_POINT";
    #endregion
}

public class G_DungeonData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_Field)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_eSubType = (GT_FieldSub)G_JSONCtrl.GetINT(vNode, m_strSUB_TYPE);
        m_strNameKey = G_JSONCtrl.GetString(vNode, m_strNAME_KEY);
        m_strDescKey = G_JSONCtrl.GetString(vNode, m_strDESC_KEY);
        m_eEnterCurrencyType = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strENTER_CURRENCY_TYPE);

        if (m_dicFieldDungeonGroup.ContainsKey(m_eType) == false)
            m_dicFieldDungeonGroup[m_eType] = new List<int>();

        // 중복 검사
        bool bDuplicate = false;
        if (m_dicFieldDungeonGroup[m_eType].Count > 0)
        {
            for (int i = 0; i < m_dicFieldDungeonGroup[m_eType].Count; ++i)
            {
                if (m_dicFieldDungeonGroup[m_eType][i] == a_iID)
                {
                    bDuplicate = true;
                    break;
                }
            }
        }

        if (!bDuplicate)
            m_dicFieldDungeonGroup[m_eType].Add(a_iID);

        if (m_dicFieldDungeonSubTypeGroup.ContainsKey(m_eSubType))
            return;

        m_dicFieldDungeonSubTypeGroup.Add(m_eSubType, this);
    }

    public static void ClearStaticValues()
    {
        if (m_dicFieldDungeonGroup != null)
        {
            m_dicFieldDungeonGroup.Clear();
        }

        if (m_dicFieldDungeonSubTypeGroup != null)
        {
            m_dicFieldDungeonSubTypeGroup.Clear();
        }
    }

    public static void GetDungeonList(GT_Field eType, ref List<int> vRetData)
    {
        if (m_dicFieldDungeonGroup.ContainsKey(eType) == false)
            return;

        vRetData = m_dicFieldDungeonGroup[eType];
    }

    public static int GetMaxDungeonCount(GT_Field eType)
    {
        if (m_dicFieldDungeonGroup.ContainsKey(eType) == false)
            return 0;

        return m_dicFieldDungeonGroup[eType].Count;
    }

    public static int GetFirstDungeonID(GT_Field eType)
    {
        if (m_dicFieldDungeonGroup.ContainsKey(eType) == false)
            return 0;

        if (m_dicFieldDungeonGroup[eType].Count == 0)
            return 0;

        return m_dicFieldDungeonGroup[eType][0];
    }

    public static int GetLastDungeonID(GT_Field eType)
    {
        if (m_dicFieldDungeonGroup.ContainsKey(eType) == false)
            return 0;

        if (m_dicFieldDungeonGroup[eType].Count == 0)
            return 0;

        return m_dicFieldDungeonGroup[eType][m_dicFieldDungeonGroup[eType].Count - 1];
    }

    public static void GetDungeonDataBySubtype(GT_FieldSub eSubType, ref G_DungeonData vRet)
    {
        if (m_dicFieldDungeonSubTypeGroup.ContainsKey(eSubType) == false)
            return;

        vRet = m_dicFieldDungeonSubTypeGroup[eSubType];
    }

    public GT_Field a_eType { get { return m_eType; } set { m_eType = value; } }
    private GT_Field m_eType = GT_Field.None;

    public GT_FieldSub a_eSubType { get { return m_eSubType; } set { m_eSubType = value; } }
    private GT_FieldSub m_eSubType = GT_FieldSub.None;

    public string a_strNameKey { get { return m_strNameKey; } set { m_strNameKey = value; } }
    private string m_strNameKey = string.Empty;

    public string a_strDescKey { get { return m_strDescKey; } set { m_strDescKey = value; } }
    private string m_strDescKey = string.Empty;

    public GT_Currency a_eEnterCurrencyType { get { return m_eEnterCurrencyType; } set { m_eEnterCurrencyType = value; } }
    private GT_Currency m_eEnterCurrencyType = GT_Currency.None;

    public static Dictionary<GT_Field, List<int>> m_dicFieldDungeonGroup = new Dictionary<GT_Field, List<int>>();
    public static Dictionary<GT_FieldSub, G_DungeonData> m_dicFieldDungeonSubTypeGroup = new Dictionary<GT_FieldSub, G_DungeonData>();

    #region ConstParseKey
    private static string m_strTYPE = "TYPE";
    private static string m_strSUB_TYPE = "SUB_TYPE";
    private static string m_strNAME_KEY = "NAME_KEY";
    private static string m_strDESC_KEY = "DESC_KEY";
    private static string m_strENTER_CURRENCY_TYPE = "ENTER_CURRENCY_TYPE";
    #endregion
}

public class G_LootData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_iGroup = G_JSONCtrl.GetINT(vNode, m_strGROUP_ID);
        m_eFieldRewardType = (GT_FieldLoot)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_eRefStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strAPPLY_STAT_TYPE);
        m_iDropProb = G_JSONCtrl.GetINT(vNode, m_strDROP_PROB);
        m_iProbGroupID = G_JSONCtrl.GetINT(vNode, m_strPROB_GROUP_ID);

        if (m_dicLootGroupDatas.ContainsKey(m_iGroup) == false)
            m_dicLootGroupDatas.Add(m_iGroup, new List<G_LootData>());

        m_dicLootGroupDatas[m_iGroup].Add(this);
    }

    public static void ClearStaticValues()
    {
        if (m_dicLootGroupDatas != null)
        {
            m_dicLootGroupDatas.Clear();
        }
    }

    public static void GetLootGroupData(int iGroupID, ref List<G_LootData> vRetData)
    {
        if (m_dicLootGroupDatas.ContainsKey(iGroupID) == false)
            return;

        vRetData = m_dicLootGroupDatas[iGroupID];
    }

    public int a_iGroup { get { return m_iGroup; } set { m_iGroup = value; } }
    private int m_iGroup = 0;

    public GT_FieldLoot a_eFieldRewardType { get { return m_eFieldRewardType; } set { m_eFieldRewardType = value; } }
    private GT_FieldLoot m_eFieldRewardType = GT_FieldLoot.None;

    // Ability type to refer to
    // Must be used after calculating the abilities the character has
    public GT_BaseStat a_eRefStatType { get { return m_eRefStatType; } set { m_eRefStatType = value; } }
    private GT_BaseStat m_eRefStatType = GT_BaseStat.None;

    public int a_iDropProb { get { return m_iDropProb; } set { m_iDropProb = value; } }
    private int m_iDropProb = 0;

    public int a_iProbGroupID { get { return m_iProbGroupID; } set { m_iProbGroupID = value; } }
    private int m_iProbGroupID = 0;

    private static Dictionary<int, List<G_LootData>> m_dicLootGroupDatas = new Dictionary<int, List<G_LootData>>();

    #region ConstParseKey
    private string m_strGROUP_ID = "GROUP_ID";
    private string m_strTYPE = "TYPE";
    private string m_strAPPLY_STAT_TYPE = "APPLY_STAT_TYPE";
    private string m_strDROP_PROB = "DROP_PROB";
    private string m_strPROB_GROUP_ID = "PROB_GROUP_ID";
    #endregion
}

public class G_MonsterData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_Monster)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_fHP = G_JSONCtrl.GetFLOAT(vNode, m_strHP);
        m_fAttack = G_JSONCtrl.GetFLOAT(vNode, m_strATK);
        m_fDefense = G_JSONCtrl.GetFLOAT(vNode, m_strDEF);
        m_fUnitSize = G_JSONCtrl.GetFLOAT(vNode, m_strUNIT_SIZE);
        m_fFXSize = G_JSONCtrl.GetFLOAT(vNode, m_strFX_SIZE);
        m_fAniAccel = G_JSONCtrl.GetFLOAT(vNode, m_strANI_ACCEL);
        m_iStatusID = G_JSONCtrl.GetINT(vNode, m_strSTATUS_ID);
        m_strShapeID = G_JSONCtrl.GetString(vNode, m_strSHAPE_ID);
        m_bIsAggressive = G_JSONCtrl.GetINT(vNode, m_strAGGRESSIVE) == 0 ? false : true;
    }

    public GT_Monster a_eType { get { return m_eType; } set { m_eType = value; } }
    private GT_Monster m_eType = GT_Monster.None;

    public float a_fHP { get { return m_fHP; } set { m_fHP = value; } }
    private float m_fHP = 0.0f;

    public float a_fAttack { get { return m_fAttack; } set { m_fAttack = value; } }
    private float m_fAttack = 0.0f;

    public float a_fDefense { get { return m_fDefense; } set { m_fDefense = value; } }
    private float m_fDefense = 0.0f;

    public float a_fUnitSize { get { return m_fUnitSize; } set { m_fUnitSize = value; } }
    private float m_fUnitSize = 0.0f;

    public float a_fFXSize { get { return m_fFXSize; } set { m_fFXSize = value; } }
    private float m_fFXSize = 0.0f;

    public float a_fAniAccel { get { return m_fAniAccel; } set { m_fAniAccel = value; } }
    private float m_fAniAccel = 0.0f;

    public int a_iStatusID { get { return m_iStatusID; } set { m_iStatusID = value; } }
    private int m_iStatusID = 0;

    public string a_strShapeID { get { return m_strShapeID; } set { m_strShapeID = value; } }
    private string m_strShapeID = string.Empty;

    public bool a_bIsAggressive { get { return m_bIsAggressive; } set { m_bIsAggressive = value; } }
    private bool m_bIsAggressive = false;

    #region ConstParseKey
    private const string m_strTYPE = "TYPE";
    private const string m_strHP = "HP";
    private const string m_strATK = "ATK";
    private const string m_strDEF = "DEF";
    private const string m_strUNIT_SIZE = "UNIT_SIZE";
    private const string m_strFX_SIZE = "FX_SIZE";
    private const string m_strANI_ACCEL = "ANI_ACCEL";
    private const string m_strSTATUS_ID = "STATUS_ID";
    private const string m_strSHAPE_ID = "SHAPE_ID";
    private const string m_strAGGRESSIVE = "AGGRESSIVE";
    #endregion
}

public class G_AdBuffData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_strNameKey = G_JSONCtrl.GetString(vNode, m_strNAME_KEY);
        m_iTime = G_JSONCtrl.GetINT(vNode, m_strTIME);
        m_fValue = G_JSONCtrl.GetFLOAT(vNode, m_strVALUE);
        m_fLevelupGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strLEVELUP_GROW_VALUE);
        m_iMaxLevel = G_JSONCtrl.GetINT(vNode, m_strMAX_LEVEL);
        m_iExp = G_JSONCtrl.GetINT(vNode, m_strEXP);
    }

    public GT_BaseStat a_eType { get { return m_eType; }  }
    private GT_BaseStat m_eType = GT_BaseStat.None;

    public string a_strNameKey { get { return m_strNameKey; } }
    private string m_strNameKey = string.Empty;

    public int a_iTime { get { return m_iTime; } }
    private int m_iTime = 0;

    public float a_fValue { get { return m_fValue; } }
    private float m_fValue = 0.0f;

    public float a_fLevelupGrowValue { get { return m_fLevelupGrowValue; } }
    private float m_fLevelupGrowValue = 0.0f;

    public int a_iMaxLevel { get { return m_iMaxLevel; } }
    private int m_iMaxLevel = 0;

    public float a_iExp { get { return m_iExp; } }
    private int m_iExp = 0;

    #region ConstParseKey
    private static string m_strTYPE = "TYPE";
    private static string m_strNAME_KEY = "NAME_KEY";
    private static string m_strTIME = "TIME";
    private static string m_strVALUE = "VALUE";
    private static string m_strLEVELUP_GROW_VALUE = "LEVELUP_GROW_VALUE";
    private static string m_strMAX_LEVEL = "MAX_LEVEL";
    private static string m_strEXP = "EXP";
    #endregion
}

public class G_WingWakeupResourceData
{
    public G_WingWakeupResourceData(GT_WakeupResource eType, int iID, int iCount)
    {
        m_eResourceType = eType;
        m_iResourceID = iID;
        m_iResourceCount = iCount;
    }

    public GT_WakeupResource a_eResourceType { get { return m_eResourceType; } }
    private GT_WakeupResource m_eResourceType = 0;

    public int a_iResourceID { get { return m_iResourceID; } }
    private int m_iResourceID = 0;

    public int a_iResourceCount { get { return m_iResourceCount; } }
    private int m_iResourceCount = 0;
}

public class G_WingData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_Equipment)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_eGrade = (GT_Grade)G_JSONCtrl.GetINT(vNode, m_strGRADE);
        m_strNameTextKey = G_JSONCtrl.GetString(vNode, m_strNAME_TEXT_KEY);
        m_strSpriteKey = G_JSONCtrl.GetString(vNode, m_strSPRITE_KEY);
        m_strAttachmentName = G_JSONCtrl.GetString(vNode, m_strATTACHMENT_NAME);
        m_bIsAni = G_JSONCtrl.GetBOOL(vNode, m_strIS_ANI);
        m_iAniFrame = G_JSONCtrl.GetINT(vNode, m_strANI_FRAME);
        m_fGetWingStatSuccessProb = G_JSONCtrl.GetFLOAT(vNode, m_strGET_WING_STAT_SUCCESS_PROB);

        GT_BaseStat eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strHOLDING_EFFECT_1_TYPE);
        float fValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_1_VALUE);
        float fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_1_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vHoldingEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strHOLDING_EFFECT_2_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_2_VALUE);
        fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strHOLDING_EFFECT_2_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vHoldingEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strEQUIP_EFFECT_1_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_1_VALUE);
        fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_1_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vEquipEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        eStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strEQUIP_EFFECT_2_TYPE);
        fValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_2_VALUE);
        fGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strEQUIP_EFFECT_2_GROW_VALUE);
        if (eStatType > GT_BaseStat.None)
            m_vEquipEffects.Add(new G_EffectStatClass(eStatType, fValue, fGrowValue));

        m_eLevelupCostType = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_TYPE);
        m_iLevelupCostValue = G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_VALUE);
        m_eLevelupCostGrowBasisType = (GT_CalcValueBasis)G_JSONCtrl.GetINT(vNode, m_strLEVELUP_COST_GROW_BASIS_TYPE);
        m_fLevelupCostGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strLEVELUP_COST_GROW_VALUE);
        m_iOverlevelGroupID = G_JSONCtrl.GetINT(vNode, m_strOVERLEVEL_GROUP_ID);
        m_iEnchantSlotCount = G_JSONCtrl.GetINT(vNode, m_strENCHANT_SLOT_COUNT);
        m_iProbGroupID = G_JSONCtrl.GetINT(vNode, m_strPROB_GROUP_ID);
        m_iGetConditionClearStageID = G_JSONCtrl.GetINT(vNode, m_strGET_CONDITION_CLEAR_STAGE_ID);
        m_iGetConditionWingStatLevel = G_JSONCtrl.GetINT(vNode, m_strGET_CONDITION_WING_STAT_LEVEL);
        m_iWingStatMaxLevel = G_JSONCtrl.GetINT(vNode, m_strWING_STAT_MAX_LEVEL);
        m_iWakeupConditionLevel = G_JSONCtrl.GetINT(vNode, m_strWAKE_UP_CONDITION_LEVEL);

        GT_WakeupResource eResourceType = (GT_WakeupResource)G_JSONCtrl.GetINT(vNode, m_strWAKE_UP_RESOURCE_1_TYPE);
        int iResourceID = G_JSONCtrl.GetINT(vNode, m_strWAKE_UP_RESOURCE_1_ID);
        int iResourceCount = G_JSONCtrl.GetINT(vNode, m_strWAKE_UP_RESOURCE_1_COUNT);
        if (eResourceType > GT_WakeupResource.None)
            m_vWakeupResources.Add(new G_WingWakeupResourceData(eResourceType, iResourceID, iResourceCount));

        eResourceType = (GT_WakeupResource)G_JSONCtrl.GetINT(vNode, m_strWAKE_UP_RESOURCE_2_TYPE);
        iResourceID = G_JSONCtrl.GetINT(vNode, m_strWAKE_UP_RESOURCE_2_ID);
        iResourceCount = G_JSONCtrl.GetINT(vNode, m_strWAKE_UP_RESOURCE_2_COUNT);
        if (eResourceType > GT_WakeupResource.None)
            m_vWakeupResources.Add(new G_WingWakeupResourceData(eResourceType, iResourceID, iResourceCount));

        eResourceType = (GT_WakeupResource)G_JSONCtrl.GetINT(vNode, m_strWAKE_UP_RESOURCE_3_TYPE);
        iResourceID = G_JSONCtrl.GetINT(vNode, m_strWAKE_UP_RESOURCE_3_ID);
        iResourceCount = G_JSONCtrl.GetINT(vNode, m_strWAKE_UP_RESOURCE_3_COUNT);
        if (eResourceType > GT_WakeupResource.None)
            m_vWakeupResources.Add(new G_WingWakeupResourceData(eResourceType, iResourceID, iResourceCount));

        eResourceType = (GT_WakeupResource)G_JSONCtrl.GetINT(vNode, m_strWAKE_UP_RESOURCE_4_TYPE);
        iResourceID = G_JSONCtrl.GetINT(vNode, m_strWAKE_UP_RESOURCE_4_ID);
        iResourceCount = G_JSONCtrl.GetINT(vNode, m_strWAKE_UP_RESOURCE_4_COUNT);
        if (eResourceType > GT_WakeupResource.None)
            m_vWakeupResources.Add(new G_WingWakeupResourceData(eResourceType, iResourceID, iResourceCount));

        m_eWakeupCurrencyType = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strWAKE_UP_CURRENCY_TYPE);
        m_iWakeupCost = G_JSONCtrl.GetINT(vNode, m_strWAKE_UP_COST);
        m_eWakeupEffectStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strWAKE_UP_EFFECT_STAT_TYPE);
        m_fWakeupEffectValue = G_JSONCtrl.GetFLOAT(vNode, m_strWAKE_UP_EFFECT_VALUE);
        m_iReqWingID = G_JSONCtrl.GetINT(vNode, m_strREQ_WING_ID);

        if (a_iID > m_iLastWingID)
        {
            m_iLastWingID = a_iID;
        }
    }

    public GT_Equipment a_eType { get { return m_eType; } }
    private GT_Equipment m_eType = GT_Equipment.None;

    public GT_Grade a_eGrade { get { return m_eGrade; } }
    private GT_Grade m_eGrade = GT_Grade.None;

    public string a_strNameTextKey { get { return m_strNameTextKey; } }
    private string m_strNameTextKey = string.Empty;

    public string a_strSpriteKey { get { return m_strSpriteKey; } }
    private string m_strSpriteKey = string.Empty;

    public string a_strAttachmentName { get { return m_strAttachmentName; } }
    private string m_strAttachmentName = string.Empty;

    public bool a_bIsAni { get { return m_bIsAni; } }
    private bool m_bIsAni = false;

    public int a_iAniFrame { get { return m_iAniFrame; } }
    private int m_iAniFrame = 0;

    public float a_fGetWingStatSuccessProb { get { return m_fGetWingStatSuccessProb; } }
    private float m_fGetWingStatSuccessProb = 0.0f;

    public List<G_EffectStatClass> a_vHoldingEffects { get { return m_vHoldingEffects; } }
    private List<G_EffectStatClass> m_vHoldingEffects = new List<G_EffectStatClass>();

    public List<G_EffectStatClass> a_vEquipEffects { get { return m_vEquipEffects; } }
    private List<G_EffectStatClass> m_vEquipEffects = new List<G_EffectStatClass>();

    public GT_Currency a_eLevelupCostType { get { return m_eLevelupCostType; } }
    private GT_Currency m_eLevelupCostType = GT_Currency.None;

    public int a_iLevelupCostValue { get { return m_iLevelupCostValue; } }
    private int m_iLevelupCostValue = 0;

    public GT_CalcValueBasis a_eLevelupCostGrowBasisType { get { return m_eLevelupCostGrowBasisType; } }
    private GT_CalcValueBasis m_eLevelupCostGrowBasisType = GT_CalcValueBasis.None;

    public float a_fLevelupCostGrowValue { get { return m_fLevelupCostGrowValue; } }
    private float m_fLevelupCostGrowValue = 0.0f;

    public int a_iOverlevelGroupID { get { return m_iOverlevelGroupID; } }
    private int m_iOverlevelGroupID = 0;

    public int a_iEnchantSlotCount { get { return m_iEnchantSlotCount; } }
    private int m_iEnchantSlotCount = 0;

    public int a_iProbGroupID { get { return m_iProbGroupID; } }
    private int m_iProbGroupID = 0;

    public int a_iGetConditionClearStageID { get { return m_iGetConditionClearStageID; } }
    private int m_iGetConditionClearStageID = 0;

    public int a_iGetConditionWingStatLevel { get { return m_iGetConditionWingStatLevel; } }
    private int m_iGetConditionWingStatLevel = 0;

    public int a_iWingStatMaxLevel { get { return m_iWingStatMaxLevel; } }
    private int m_iWingStatMaxLevel = 0;

    public int a_iWakeupConditionLevel { get { return m_iWakeupConditionLevel; } }
    private int m_iWakeupConditionLevel = 0;

    public List<G_WingWakeupResourceData> a_vWakeupResources { get { return m_vWakeupResources; } }
    private List<G_WingWakeupResourceData> m_vWakeupResources = new List<G_WingWakeupResourceData>();

    public GT_Currency a_eWakeupCurrencyType { get { return m_eWakeupCurrencyType; } }
    private GT_Currency m_eWakeupCurrencyType = GT_Currency.None;

    public int a_iWakeupCost { get { return m_iWakeupCost; } }
    private int m_iWakeupCost = 0;

    public GT_BaseStat a_eWakeupEffectStatType { get { return m_eWakeupEffectStatType; } }
    private GT_BaseStat m_eWakeupEffectStatType = GT_BaseStat.None;

    public float a_fWakeupEffectValue { get { return m_fWakeupEffectValue; } }
    private float m_fWakeupEffectValue = 0.0f;

    public int a_iReqWingID { get { return m_iReqWingID; } }
    private int m_iReqWingID = 0;

    // 최종 날개 ID
    public static int a_iLastWingID { get { return m_iLastWingID; } }
    private static int m_iLastWingID = 0;

    #region ConstParseKey
    private static string m_strTYPE = "TYPE";
    private static string m_strGRADE = "GRADE";
    private static string m_strNAME_TEXT_KEY = "NAME_TEXT_KEY";
    private static string m_strSPRITE_KEY = "SPRITE_KEY";
    private static string m_strATTACHMENT_NAME = "ATTACHMENT_NAME";
    private static string m_strIS_ANI = "IS_ANI";
    private static string m_strANI_FRAME = "ANI_FRAME";
    private static string m_strGET_WING_STAT_SUCCESS_PROB = "GET_WING_STAT_SUCCESS_PROB";
    private static string m_strHOLDING_EFFECT_1_TYPE = "HOLDING_EFFECT_1_TYPE";
    private static string m_strHOLDING_EFFECT_1_VALUE = "HOLDING_EFFECT_1_VALUE";
    private static string m_strHOLDING_EFFECT_1_GROW_VALUE = "HOLDING_EFFECT_1_GROW_VALUE";
    private static string m_strHOLDING_EFFECT_2_TYPE = "HOLDING_EFFECT_2_TYPE";
    private static string m_strHOLDING_EFFECT_2_VALUE = "HOLDING_EFFECT_2_VALUE";
    private static string m_strHOLDING_EFFECT_2_GROW_VALUE = "HOLDING_EFFECT_2_GROW_VALUE";
    private static string m_strEQUIP_EFFECT_1_TYPE = "EQUIP_EFFECT_1_TYPE";
    private static string m_strEQUIP_EFFECT_1_VALUE = "EQUIP_EFFECT_1_VALUE";
    private static string m_strEQUIP_EFFECT_1_GROW_VALUE = "EQUIP_EFFECT_1_GROW_VALUE";
    private static string m_strEQUIP_EFFECT_2_TYPE = "EQUIP_EFFECT_2_TYPE";
    private static string m_strEQUIP_EFFECT_2_VALUE = "EQUIP_EFFECT_2_VALUE";
    private static string m_strEQUIP_EFFECT_2_GROW_VALUE = "EQUIP_EFFECT_2_GROW_VALUE";
    private static string m_strLEVELUP_COST_TYPE = "LEVELUP_COST_TYPE";
    private static string m_strLEVELUP_COST_VALUE = "LEVELUP_COST_VALUE";
    private static string m_strLEVELUP_COST_GROW_BASIS_TYPE = "LEVELUP_COST_GROW_BASIS_TYPE";
    private static string m_strLEVELUP_COST_GROW_VALUE = "LEVELUP_COST_GROW_VALUE";
    private static string m_strOVERLEVEL_GROUP_ID = "OVERLEVEL_GROUP_ID";
    private static string m_strENCHANT_SLOT_COUNT = "ENCHANT_SLOT_COUNT";
    private static string m_strPROB_GROUP_ID = "PROB_GROUP_ID";
    private static string m_strGET_CONDITION_CLEAR_STAGE_ID = "GET_CONDITION_CLEAR_STAGE_ID";
    private static string m_strGET_CONDITION_WING_STAT_LEVEL = "GET_CONDITION_WING_STAT_LEVEL";
    private static string m_strWING_STAT_MAX_LEVEL = "WING_STAT_MAX_LEVEL";
    private static string m_strWAKE_UP_CONDITION_LEVEL = "WAKE_UP_CONDITION_LEVEL";
    private static string m_strWAKE_UP_RESOURCE_1_TYPE = "WAKE_UP_RESOURCE_1_TYPE";
    private static string m_strWAKE_UP_RESOURCE_1_ID = "WAKE_UP_RESOURCE_1_ID";
    private static string m_strWAKE_UP_RESOURCE_1_COUNT = "WAKE_UP_RESOURCE_1_COUNT";
    private static string m_strWAKE_UP_RESOURCE_2_TYPE = "WAKE_UP_RESOURCE_2_TYPE";
    private static string m_strWAKE_UP_RESOURCE_2_ID = "WAKE_UP_RESOURCE_2_ID";
    private static string m_strWAKE_UP_RESOURCE_2_COUNT = "WAKE_UP_RESOURCE_2_COUNT";
    private static string m_strWAKE_UP_RESOURCE_3_TYPE = "WAKE_UP_RESOURCE_3_TYPE";
    private static string m_strWAKE_UP_RESOURCE_3_ID = "WAKE_UP_RESOURCE_3_ID";
    private static string m_strWAKE_UP_RESOURCE_3_COUNT = "WAKE_UP_RESOURCE_3_COUNT";
    private static string m_strWAKE_UP_RESOURCE_4_TYPE = "WAKE_UP_RESOURCE_4_TYPE";
    private static string m_strWAKE_UP_RESOURCE_4_ID = "WAKE_UP_RESOURCE_4_ID";
    private static string m_strWAKE_UP_RESOURCE_4_COUNT = "WAKE_UP_RESOURCE_4_COUNT";
    private static string m_strWAKE_UP_CURRENCY_TYPE = "WAKE_UP_CURRENCY_TYPE";
    private static string m_strWAKE_UP_COST = "WAKE_UP_COST";
    private static string m_strWAKE_UP_EFFECT_STAT_TYPE = "WAKE_UP_EFFECT_STAT_TYPE";
    private static string m_strWAKE_UP_EFFECT_VALUE = "WAKE_UP_EFFECT_VALUE";
    private static string m_strREQ_WING_ID = "REQ_WING_ID";
    #endregion
}

public class G_TreasureHouseRankReward : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_strRank = G_JSONCtrl.GetString(vNode, m_strRANK);
        m_iScore = G_JSONCtrl.GetINT(vNode, m_strSCORE);
        m_eCurrencyType = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strCURRENCY_TYPE);
        m_iCount = G_JSONCtrl.GetINT(vNode, m_strCOUNT);

        m_vList.Add(this);
    }

    public static void ClearStaticValues()
    {
        if (m_vList != null)
        {
            G_Utils.RemoveListAll(ref m_vList);
        }
    }

    public static void GetRankData(BigInteger iScore, ref G_TreasureHouseRankReward vRetData)
    {
        for (int i = 0; i < m_vList.Count; ++i)
        {
            if (iScore >= m_vList[m_vList.Count - 1].m_iScore)
                vRetData = m_vList[m_vList.Count - 1];
            else
            {
                if (iScore >= m_vList[i].m_iScore && iScore < m_vList[i + 1].m_iScore)
                    vRetData = m_vList[i];
            }
        }

        // 아무것도 없으면 맨처음꺼로 세팅
        if (vRetData == null)
            vRetData = m_vList[0];
    }

    public string a_strRank { get { return m_strRank; } }
    private string m_strRank = string.Empty;

    public int a_iScore { get { return m_iScore; } }
    private int m_iScore = 0;

    public GT_Currency a_eCurrencyType { get { return m_eCurrencyType; } }
    private GT_Currency m_eCurrencyType = GT_Currency.None;

    public int a_iCount { get { return m_iCount; } }
    private int m_iCount = 0;

    private static List<G_TreasureHouseRankReward> m_vList = new List<G_TreasureHouseRankReward>();

    #region ConstParseKey
    private static string m_strRANK = "RANK";
    private static string m_strSCORE = "SCORE";
    private static string m_strCURRENCY_TYPE = "CURRENCY_TYPE";
    private static string m_strCOUNT = "COUNT";
    #endregion
}

public class G_ProductData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_Product)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_iReqProductID = G_JSONCtrl.GetINT(vNode, m_strREQ_PRODUCT_ID);
        m_iReqMissionID = G_JSONCtrl.GetINT(vNode, m_strREQ_MISSION_ID);
        m_iReqMissionCount = G_JSONCtrl.GetINT(vNode, m_strREQ_MISSION_COUNT);
        m_iRewardID = G_JSONCtrl.GetINT(vNode, m_strREWARD_ID);
        m_iCount =G_JSONCtrl.GetINT(vNode, m_strCOUNT);
        m_iMileageCount = G_JSONCtrl.GetINT(vNode, m_strMILEAGE_COUNT);

        int iCostCurrencyType = G_JSONCtrl.GetINT(vNode, m_strCOST_CURRENCY_TYPE);
        m_eCostCurrencyType = (GT_Currency)iCostCurrencyType;
        m_iCostAmount = G_JSONCtrl.GetINT(vNode, m_strCOST_AMOUNT);
        m_eLimitType = (GT_ProductLimit)G_JSONCtrl.GetINT(vNode, m_strLIMIT_TYPE);
        m_iLimitCount = G_JSONCtrl.GetINT(vNode, m_strLIMIT_COUNT);
        m_strNameKey = G_JSONCtrl.GetString(vNode, m_strNAME_KEY);
        m_strDescKey = G_JSONCtrl.GetString(vNode, m_strDESC_KEY);
        m_strSpriteKey = G_JSONCtrl.GetString(vNode, m_strSPRITE_KEY);

        string strTime = G_JSONCtrl.GetString(vNode, m_strSTART_TIME);
        if (strTime.Equals("0"))
            m_vStartTime = DateTime.MinValue;
        else
            m_vStartTime = DateTime.Parse(strTime);

        strTime = G_JSONCtrl.GetString(vNode, m_strEND_TIME);
        if (strTime.Equals("0"))
            m_vEndTime = DateTime.MinValue;
        else
            m_vEndTime = DateTime.Parse(strTime);

        m_strPurchaseID = G_JSONCtrl.GetString(vNode, m_strPURCHASE_ID);
        m_iRecommendOrder = G_JSONCtrl.GetINT(vNode, m_strRECOMMEND_ORDER);

        if ((int)m_eType > 600 && (int)m_eType < 700)
        {
            // 일반 패키지
            m_vNormalPackageProducts.Add(a_iID);
        }

        if (m_dicProductTypeGroup.ContainsKey(m_eType) == false)
            m_dicProductTypeGroup.Add(m_eType, new List<int>());
        m_dicProductTypeGroup[m_eType].Add(a_iID);

        if (m_iRecommendOrder > 0)
            m_vRecommendProducts.Add(this);
    }

    public static void ClearStaticValues()
    {
        if (m_dicProductTypeGroup != null)
        {
            m_dicProductTypeGroup.Clear();
        }

        if (m_vNormalPackageProducts != null)
        {
            G_Utils.RemoveListAll(ref m_vNormalPackageProducts);
        }

        if (m_vRecommendProducts != null)
        {
            G_Utils.RemoveListAll(ref m_vRecommendProducts);
        }

        a_vExchangeEventCurrencyData = null;
    }

    public static void GetProductTypeList(GT_Product eType, ref List<int> vRetData)
    {
        if (m_dicProductTypeGroup.ContainsKey(eType) == false)
            return;

        vRetData = m_dicProductTypeGroup[eType];
    }

    public static void GetRecommendProducts(ref List<G_ProductData> vRetData)
    {
        if (m_vRecommendProducts.Count == 0)
            return;

        vRetData = m_vRecommendProducts.OrderBy(p => p.a_iRecommendOrder).ToList();
    }

    public static void GetNormalPackageProducts(ref List<int> vRetData)
    {
        if (m_vNormalPackageProducts == null)
            return;
        if (m_vNormalPackageProducts.Count == 0)
            return;

        vRetData = m_vNormalPackageProducts;
    }

    public static int GetChancePackageCount()
    {
        if (m_dicProductTypeGroup.ContainsKey(GT_Product.Chance) == false)
            return 0;

        // ChancePackageOrder 의 시작은 0부터 ....
        return m_dicProductTypeGroup[GT_Product.Chance].Count;
    }

    public static int GetChancePackageID(int iIndex)
    {
        if (m_dicProductTypeGroup.ContainsKey(GT_Product.Chance) == false)
            return 0;

        List<int> vList = m_dicProductTypeGroup[GT_Product.Chance];
        if (vList == null || (vList != null && vList.Count == 0))
            return 0;

        if (iIndex > vList.Count - 1)
            return 0;

        return vList[iIndex];
    }

    public static void CheckExchangeEventCurrencyData()
    {
        a_vExchangeEventCurrencyData = null;

        /*
        DateTime vCurrentTime = G_SCBackendMGR.a_instance.a_vServerTime;

        List<int> vProductList = null;
        GetProductTypeList(GT_Product.Exchange_Currency, ref vProductList);
        if (vProductList != null && vProductList.Count > 0)
        {
            for (int i = 0; i < vProductList.Count; ++i)
            {
                if (a_vExchangeEventCurrencyData != null)
                    break;

                G_ProductData vBase = G_GlobalDataMGR.a_instance.GetData<G_ProductData>(GT_DataTable.Product, vProductList[i]);
                if (vBase == null)
                    continue;

                // 기간 체크
                if (vBase.a_vStartTime == DateTime.MinValue || vBase.a_vEndTime == DateTime.MinValue)
                    continue;

                if (vBase.a_vStartTime <= vCurrentTime && vCurrentTime < vBase.a_vEndTime)
                    a_vExchangeEventCurrencyData = vBase;
            }
        }

        // 종료된 이벤트 재화가 남아있는지 검사
        for (GT_Currency eType = GT_Currency.EventShopTicket1; eType <= GT_Currency.EventShopTicket3; ++eType)
        {
            if (a_vExchangeEventCurrencyData != null)
            {
                // 현재 진행중인 이벤트 재화와 동일한 경우 pass
                if (a_vExchangeEventCurrencyData.a_eCostCurrencyType == eType)
                    continue;
            }

            BigInteger iAmount = G_UserDataMGR.a_instance.a_vUserCurrencyData.GetCurrency(eType);
            if (iAmount > 0)
            {
                // 초기화 및 저장
                G_UserDataMGR.a_instance.a_vUserCurrencyData.UpdateCurrency(eType, -iAmount);
                break;
            }
        }
        */
    }

    public GT_Product a_eType { get { return m_eType; } }
    private GT_Product m_eType = GT_Product.None;

    public int a_iReqProductID { get { return m_iReqProductID; } }
    private int m_iReqProductID = 0;

    public int a_iReqMissionID { get { return m_iReqMissionID; } }
    private int m_iReqMissionID = 0;

    public int a_iReqMissionCount { get { return m_iReqMissionCount; } }
    private int m_iReqMissionCount = 0;

    public int a_iBuyConditiondAccountLevel { get { return m_iBuyConditionAccountLevel; } }
    private int m_iBuyConditionAccountLevel = 0;

    public int a_iRewardID { get { return m_iRewardID; } }
    private int m_iRewardID = 0;

    public int a_iCount { get { return m_iCount; } }
    private int m_iCount = 0;

    public int a_iMileageCount { get { return m_iMileageCount; } }
    private int m_iMileageCount = 0;

    public GT_Currency a_eCostCurrencyType { get { return m_eCostCurrencyType; } }
    private GT_Currency m_eCostCurrencyType = GT_Currency.None;

    public int a_iCostAmount { get { return m_iCostAmount; } }
    private int m_iCostAmount = 0;

    public GT_ProductLimit a_eLimitType { get { return m_eLimitType; } }
    private GT_ProductLimit m_eLimitType = GT_ProductLimit.None;

    public int a_iLimitCount { get { return m_iLimitCount; } }
    private int m_iLimitCount = 0;

    public string a_strNameKey { get { return m_strNameKey; } }
    private string m_strNameKey = string.Empty;

    public string a_strDescKey { get { return m_strDescKey; } }
    private string m_strDescKey = string.Empty;

    public string a_strSpriteKey { get { return m_strSpriteKey; } }
    private string m_strSpriteKey = string.Empty;

    public DateTime a_vStartTime { get { return m_vStartTime; } }
    private DateTime m_vStartTime = DateTime.MinValue;

    public DateTime a_vEndTime { get { return m_vEndTime; } }
    private DateTime m_vEndTime = DateTime.MinValue;

    public string a_strPurchaseID { get { return m_strPurchaseID; } }
    private string m_strPurchaseID = string.Empty;

    public int a_iRecommendOrder { get { return m_iRecommendOrder; } }
    private int m_iRecommendOrder = 0;

    private static Dictionary<GT_Product, List<int>> m_dicProductTypeGroup = new Dictionary<GT_Product, List<int>>();
    private static List<int> m_vNormalPackageProducts = new List<int>();
    private static List<G_ProductData> m_vRecommendProducts = new List<G_ProductData>();
    public static G_ProductData a_vExchangeEventCurrencyData = null;

    #region ConstParseKey
    private static string m_strTYPE = "TYPE";
    private static string m_strREQ_PRODUCT_ID = "REQ_PRODUCT_ID";
    private static string m_strREQ_MISSION_ID = "REQ_MISSION_ID";
    private static string m_strREQ_MISSION_COUNT = "REQ_MISSION_COUNT";
    private static string m_strBUY_CONDITION_ACCOUNT_LEVEL = "BUY_CONDITION_ACCOUNT_LEVEL";
    private static string m_strREWARD_ID = "REWARD_ID";
    private static string m_strCOUNT = "COUNT";
    private static string m_strMILEAGE_COUNT = "MILEAGE_COUNT";
    private static string m_strCOST_CURRENCY_TYPE = "COST_CURRENCY_TYPE";
    private static string m_strCOST_AMOUNT = "COST_AMOUNT";
    private static string m_strLIMIT_TYPE = "LIMIT_TYPE";
    private static string m_strLIMIT_COUNT = "LIMIT_COUNT";
    private static string m_strNAME_KEY = "NAME_KEY";
    private static string m_strDESC_KEY = "DESC_KEY";
    private static string m_strSPRITE_KEY = "SPRITE_KEY";
    private static string m_strSTART_TIME = "START_TIME";
    private static string m_strEND_TIME = "END_TIME";
    private static string m_strPURCHASE_ID = "PURCHASE_ID";
    private static string m_strRECOMMEND_ORDER = "RECOMMEND_ORDER";
    #endregion
}

public class G_PassData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_Pass)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_eCategory = (GT_PassCategory)G_JSONCtrl.GetINT(vNode, m_strCATEGORY);
        m_iProductID = G_JSONCtrl.GetINT(vNode, m_strPRODUCT_ID);
        m_iLevelCount = G_JSONCtrl.GetINT(vNode, m_strLEVEL_COUNT);
        m_iProductGroupID = G_JSONCtrl.GetINT(vNode, m_strPRODUCT_GROUP_ID);

        string strTime = G_JSONCtrl.GetString(vNode, m_strSTART_TIME);
        if (strTime.Equals("0"))
            m_vStartTime = DateTime.MinValue;
        else
            m_vStartTime = DateTime.Parse(strTime);

        strTime = G_JSONCtrl.GetString(vNode, m_strEND_TIME);
        if (strTime.Equals("0"))
            m_vEndTime = DateTime.MinValue;
        else
            m_vEndTime = DateTime.Parse(strTime);

        m_iMissionID = G_JSONCtrl.GetINT(vNode, m_strMISSION_ID);
        m_strPassConditionKey = G_JSONCtrl.GetString(vNode, m_strPASS_CONTIION_KEY);

        if (m_dicGroupByCategoryType.ContainsKey(m_eCategory) == false)
        {
            List<int> vList = new List<int>();
            vList.Add(a_iID);
            m_dicGroupByCategoryType.Add(m_eCategory, vList);
        }
        else
            m_dicGroupByCategoryType[m_eCategory].Add(a_iID);

        if (m_vAttendancePassData == null)
        {
            if (m_eType == GT_Pass.Attendence)
                m_vAttendancePassData = this;
        }
    }

    public static void ClearStaticValues()
    {
        if (m_dicGroupByCategoryType != null)
        {
            m_dicGroupByCategoryType.Clear();
        }

        m_vAttendancePassData = null;
    }

    public static void GetPassDataByCategory(GT_PassCategory eCategory, ref List<int> vRetData)
    {
        if (m_dicGroupByCategoryType.ContainsKey(eCategory) == false)
            return;

        vRetData = m_dicGroupByCategoryType[eCategory];
    }

    //public static int GetPassDataByType(GT_PassType eType)
    //{
    //    if (m_dicGroupByPassType.ContainsKey(eType) == false)
    //        return 0;

    //    return m_dicGroupByPassType[eType];
    //}

    public GT_Pass a_eType { get { return m_eType; } }
    private GT_Pass m_eType = GT_Pass.None;

    public GT_PassCategory a_eCategory { get { return m_eCategory; } }
    private GT_PassCategory m_eCategory = GT_PassCategory.None;

    public int a_iProductID { get { return m_iProductID; } }
    private int m_iProductID = 0;

    public int a_iLevelCount { get { return m_iLevelCount; } }
    private int m_iLevelCount = 0;

    public int a_iProductGroupID { get { return m_iProductGroupID; } }
    private int m_iProductGroupID = 0;

    public DateTime a_vStartTime { get { return m_vStartTime; } }
    private DateTime m_vStartTime = DateTime.MinValue;

    public DateTime a_vEndTime { get { return m_vEndTime; } }
    private DateTime m_vEndTime = DateTime.MinValue;

    public int a_iMissionID { get { return m_iMissionID; } }
    private int m_iMissionID = 0;

    public string a_strPassConditionKey { get { return m_strPassConditionKey; } }
    private string m_strPassConditionKey = string.Empty;

    private static Dictionary<GT_PassCategory, List<int>> m_dicGroupByCategoryType = new Dictionary<GT_PassCategory, List<int>>();
    //private static Dictionary<GT_PassType, int> m_dicGroupByPassType = new Dictionary<GT_PassType, int>();

    public static G_PassData m_vAttendancePassData = null;

    #region ConstParseKey
    private static string m_strTYPE = "TYPE";
    private static string m_strCATEGORY = "CATEGORY";
    private static string m_strPRODUCT_ID = "PRODUCT_ID";
    private static string m_strLEVEL_COUNT = "LEVEL_COUNT";
    private static string m_strPRODUCT_GROUP_ID = "PRODUCT_GROUP_ID";
    private static string m_strSTART_TIME = "START_TIME";
    private static string m_strEND_TIME = "END_TIME";
    private static string m_strMISSION_ID = "MISSION_ID";
    private static string m_strPASS_CONTIION_KEY = "PASS_CONDITION_KEY";
    #endregion
}

public class G_PassProductData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_iGroupID = G_JSONCtrl.GetINT(vNode, m_strGROUP_ID);
        m_iLevel = G_JSONCtrl.GetINT(vNode, m_strLEVEL);
        m_iTargetValue = G_JSONCtrl.GetINT(vNode, m_strTARGET_VALUE);
        m_eType = (GT_PassReward)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_iRewardID = G_JSONCtrl.GetINT(vNode, m_strREWARD_ID);

        if (m_dicPassProductByGroupID.ContainsKey(m_iGroupID))
            m_dicPassProductByGroupID[m_iGroupID].Add(a_iID);
        else
        {
            List<int> vNewData = new List<int>();
            vNewData.Add(a_iID);
            m_dicPassProductByGroupID.Add(m_iGroupID, vNewData);
        }
    }

    public static void ClearStaticValues()
    {
        if (m_dicPassProductByGroupID != null)
        {
            m_dicPassProductByGroupID.Clear();
        }
    }

    public static void GetProductIDList(int iGroupID, GT_PassReward eRewardType, ref List<int> vRetData)
    {
        if (vRetData == null)   
            vRetData = new List<int>();
        vRetData.Clear();

        List<int> vGroupList = null;
        GetProductGroupList(iGroupID, ref vGroupList);
        if (vGroupList == null) 
            return;

        /*
        for (int i = 0; i < vGroupList.Count; ++i)
        {
            G_PassProductData vData = G_GlobalDataMGR.a_instance.GetData<G_PassProductData>(GT_DataTable.PassProduct, vGroupList[i]);
            if (vData == null)
                continue;

            if (vData.a_eType == eRewardType)
                vRetData.Add(vData.a_iID);
        }
        */
    }

    public static void GetProductGroupList(int iGroupID, ref List<int> vRetData)
    {
        if (m_dicPassProductByGroupID.ContainsKey(iGroupID) == false)
            return;

        vRetData = m_dicPassProductByGroupID[iGroupID];
    }

    public int a_iGroupID { get { return m_iGroupID; } }
    private int m_iGroupID = 0;

    public int a_iLevel { get { return m_iLevel; } }
    private int m_iLevel = 0;

    public int a_iTargetValue { get { return m_iTargetValue; } }
    private int m_iTargetValue = 0;

    public GT_PassReward a_eType { get { return m_eType; } }
    private GT_PassReward m_eType = 0;

    public int a_iRewardID { get { return m_iRewardID; } }
    private int m_iRewardID = 0;

    private static Dictionary<int, List<int>> m_dicPassProductByGroupID = new Dictionary<int, List<int>>();

    #region ConstParseKey
    private static string m_strGROUP_ID = "GROUP_ID";
    private static string m_strLEVEL = "LEVEL";
    private static string m_strTARGET_VALUE = "TARGET_VALUE";
    private static string m_strTYPE = "TYPE";
    private static string m_strREWARD_ID = "REWARD_ID";
    #endregion
}

public class G_SummonData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eTabType = (GT_Summon)G_JSONCtrl.GetINT(vNode, m_strTAB_TYPE);
        m_iLevel = G_JSONCtrl.GetINT(vNode, m_strLEVEL);
        m_iExp = G_JSONCtrl.GetINT(vNode, m_strEXP);
        m_iLvUpRewardID = G_JSONCtrl.GetINT(vNode, m_strLV_UP_REWARD_ID);
        m_iLvUpRewardValue = G_JSONCtrl.GetINT(vNode, m_strLV_UP_REWARD_VALUE);
        m_eLvUpRewardRarity = (GT_Rarity)G_JSONCtrl.GetINT(vNode, m_strLV_UP_REWARD_RARITY);
        m_iRewardID = G_JSONCtrl.GetINT(vNode, m_strREWARD_ID);

        if (m_dicSummonGroup.ContainsKey(m_eTabType) == false)
        {
            List<int> vNewList = new List<int>();
            vNewList.Add(a_iID);
            m_dicSummonGroup.Add(m_eTabType, vNewList);
        }
        else
            m_dicSummonGroup[m_eTabType].Add(a_iID);
    }

    public static void ClearStaticValues()
    {
        if (m_dicSummonGroup != null)
        {
            m_dicSummonGroup.Clear();
        }
    }

    public static void GetSummonGroupList(GT_Summon eTabType, ref List<int> vRetList)
    {
        if (m_dicSummonGroup.ContainsKey(eTabType) == false)
            return;

        vRetList = m_dicSummonGroup[eTabType];
    }

    public GT_Summon a_eTabType { get { return m_eTabType; }}
    private GT_Summon m_eTabType = GT_Summon.None;

    public int a_iLevel { get { return m_iLevel; } }    
    private int m_iLevel = 0;

    public int a_iExp { get { return m_iExp; } }    
    private int m_iExp = 0;

    public int a_iLvUpRewardID { get { return m_iLvUpRewardID; } }
    private int m_iLvUpRewardID = 0;

    public int a_iLvUpRewardValue { get { return m_iLvUpRewardValue; } }
    private int m_iLvUpRewardValue = 0;

    public GT_Rarity a_eLvUpRewardRarity { get { return m_eLvUpRewardRarity; } }
    private GT_Rarity m_eLvUpRewardRarity = GT_Rarity.None;

    public int a_iRewardID { get { return m_iRewardID; } }
    private int m_iRewardID = 0;

    public static Dictionary<GT_Summon, List<int>> m_dicSummonGroup = new Dictionary<GT_Summon, List<int>>();

    #region ConstParseKey
    private static string m_strTAB_TYPE = "TAB_TYPE";
    private static string m_strLEVEL = "LEVEL";
    private static string m_strEXP = "EXP";
    private static string m_strLV_UP_REWARD_ID = "LV_UP_REWARD_ID";
    private static string m_strLV_UP_REWARD_VALUE = "LV_UP_REWARD_VALUE";
    private static string m_strLV_UP_REWARD_RARITY = "LV_UP_REWARD_RARITY";
    private static string m_strREWARD_ID = "REWARD_ID";
    #endregion
}

public class G_OfflineRewardData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_iGroupID = G_JSONCtrl.GetINT(vNode, m_strGROUP_ID);
        m_eOfflineRewardType = (GT_OfflineReward)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_eDetailType = (GT_Equipment)G_JSONCtrl.GetINT(vNode, m_strDETAIL_TYPE);
        m_iValue = G_JSONCtrl.GetINT(vNode, m_strVALUE);
        m_eRefStatType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strAPPLY_STAT_TYPE);
        m_fDropProb = G_JSONCtrl.GetFLOAT(vNode, m_strDROP_PROB);
        m_iCount = G_JSONCtrl.GetBIGINTEGER(vNode, m_strCOUNT);
        m_iRewardModulus = G_JSONCtrl.GetINT(vNode, m_strREWARD_MODULUS);

        if (m_dicOfflineRewardGroupDatas.ContainsKey(m_iGroupID) == false)
            m_dicOfflineRewardGroupDatas.Add(m_iGroupID, new List<int>());

        m_dicOfflineRewardGroupDatas[m_iGroupID].Add(a_iID);
    }

    public static void ClearStaticValues()
    {
        if (m_dicOfflineRewardGroupDatas != null)
        {
            m_dicOfflineRewardGroupDatas.Clear();
        }
    }

    public static void GetOfflineRewardGroup(int iGroupID, ref List<int> vRetData)
    {
        if (m_dicOfflineRewardGroupDatas.ContainsKey(iGroupID) == false)
            return;

        vRetData = m_dicOfflineRewardGroupDatas[iGroupID];
    }

    public int a_iGroupID { get { return m_iGroupID; } } 
    private int m_iGroupID = 0;

    public GT_OfflineReward a_eOfflineRewardType { get { return m_eOfflineRewardType; } }
    private GT_OfflineReward m_eOfflineRewardType = GT_OfflineReward.None;

    public GT_Equipment a_eDetailType { get { return m_eDetailType; } }
    private GT_Equipment m_eDetailType = GT_Equipment.None;

    public int a_iValue { get { return m_iValue; } }
    private int m_iValue = 0;

    public GT_BaseStat a_eRefStatType { get { return m_eRefStatType; } }
    private GT_BaseStat m_eRefStatType = GT_BaseStat.None;

    public float a_fDropProb { get { return m_fDropProb;} }
    private float m_fDropProb = 0;

    public BigInteger a_iCount { get { return m_iCount; } }
    private BigInteger m_iCount = 0;

    public int a_iRewardModulus { get { return m_iRewardModulus; } }
    private int m_iRewardModulus = 0;

    private static Dictionary<int, List<int>> m_dicOfflineRewardGroupDatas = new Dictionary<int, List<int>>();

    #region ConstParseKey
    private static string m_strGROUP_ID = "GROUP_ID";
    private static string m_strTYPE = "TYPE";
    private static string m_strDETAIL_TYPE = "DETAIL_TYPE";
    private static string m_strVALUE = "VALUE";
    private static string m_strAPPLY_STAT_TYPE = "APPLY_STAT_TYPE";
    private static string m_strDROP_PROB = "DROP_PROB";
    private static string m_strCOUNT = "COUNT";
    private static string m_strREWARD_MODULUS = "REWARD_MODULUS";
    #endregion
}

public class G_BannerData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eType = (GT_Banner)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_strNameKey = G_JSONCtrl.GetString(vNode, m_strNAME_KEY);
        m_strDescKey = G_JSONCtrl.GetString(vNode, m_strDESC_KEY);
        m_strUnityObjectName = G_JSONCtrl.GetString(vNode, m_strUNITY_OBJECT_NAME);

        string strTime = G_JSONCtrl.GetString(vNode, m_strSTART_TIME);
        if (strTime.Equals("0"))
            m_vStartTime = DateTime.MinValue;
        else
            m_vStartTime = DateTime.Parse(strTime);

        strTime = G_JSONCtrl.GetString(vNode, m_strEND_TIME);
        if (strTime.Equals("0"))
            m_vEndTime = DateTime.MinValue;
        else
            m_vEndTime = DateTime.Parse(strTime);

        m_eShortcutType = (GT_Shortcut)G_JSONCtrl.GetINT(vNode, m_strSHORTCUT_UI_TYPE);

        if (!m_dicGroupByType.ContainsKey(m_eType))
            m_dicGroupByType.Add(m_eType, new List<int>());
        m_dicGroupByType[m_eType].Add(a_iID);
    }

    public static void ClearStaticValues()
    {
        if (m_dicGroupByType != null)
        {
            m_dicGroupByType.Clear();
        }
    }

    public static void GetListByType(GT_Banner eType, ref List<int> vRetData)
    {
        if (!m_dicGroupByType.ContainsKey(eType))
            return;

        vRetData = m_dicGroupByType[eType];
    }

    public GT_Banner a_eType { get { return m_eType; } }
    private GT_Banner m_eType = GT_Banner.None;

    public string a_strNameKey { get { return m_strNameKey; } }
    private string m_strNameKey = string.Empty;

    public string a_strDescKey { get { return m_strDescKey; } }
    private string m_strDescKey = string.Empty;

    public string a_strUnityObjectName { get { return m_strUnityObjectName; } }
    private string m_strUnityObjectName = string.Empty;

    public DateTime a_vStartTime { get { return m_vStartTime; } }
    private DateTime m_vStartTime = DateTime.MinValue;

    public DateTime a_vEndTime { get { return m_vEndTime; } }
    private DateTime m_vEndTime = DateTime.MinValue;

    public GT_Shortcut a_eShortcutType { get { return m_eShortcutType; } }
    private GT_Shortcut m_eShortcutType = GT_Shortcut.None;

    private static Dictionary<GT_Banner, List<int>> m_dicGroupByType = new Dictionary<GT_Banner, List<int>>();

    #region ConstParseKey
    private static string m_strTYPE = "TYPE";
    private static string m_strNAME_KEY = "NAME_KEY";
    private static string m_strDESC_KEY = "DESC_KEY";
    private static string m_strUNITY_OBJECT_NAME = "UNITY_OBJECT_NAME";
    private static string m_strSTART_TIME = "START_TIME";
    private static string m_strEND_TIME = "END_TIME";
    private static string m_strSHORTCUT_UI_TYPE = "SHORTCUT_UI_TYPE";
    #endregion
}

public class G_RankRewardData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eContentsType = (GT_RankReward)G_JSONCtrl.GetINT(vNode, m_strCONTENTS_ID);
        m_iBeginRank = G_JSONCtrl.GetINT(vNode, m_strBEGIN_RANK);
        m_iEndRank = G_JSONCtrl.GetINT(vNode, m_strEND_RANK);
        m_iRewardID = G_JSONCtrl.GetINT(vNode, m_strREWARD_ID);

        if (!m_dicGroupByContents.ContainsKey(m_eContentsType))
            m_dicGroupByContents.Add(m_eContentsType, new List<int>());
        m_dicGroupByContents[m_eContentsType].Add(a_iID);
    }

    public static void ClearStaticValues()
    {
        if (m_dicGroupByContents != null)
        {
            m_dicGroupByContents.Clear();
        }
    }

    public static void GetListByContents(GT_RankReward eType, ref List<int> vRetData)
    {
        if (!m_dicGroupByContents.ContainsKey(eType))
            return;

        vRetData = m_dicGroupByContents[eType];
    }

    public GT_RankReward a_eContentsType { get { return m_eContentsType; } }
    private GT_RankReward m_eContentsType = GT_RankReward.None;

    public int a_iBeginRank { get { return m_iBeginRank; } }
    private int m_iBeginRank = 0;

    public int a_iEndRank { get { return m_iEndRank; } }
    private int m_iEndRank = 0;

    public int a_iRewardID { get { return m_iRewardID; } }
    private int m_iRewardID = 0;

    private static Dictionary<GT_RankReward, List<int>> m_dicGroupByContents = new Dictionary<GT_RankReward, List<int>>();

    #region ConstParseKey
    private static string m_strCONTENTS_ID = "CONTENTS_ID";
    private static string m_strTYPE = "TYPE"; // 현재는 안쓰임
    private static string m_strBEGIN_RANK = "BEGIN_RANK";
    private static string m_strEND_RANK = "END_RANK";
    private static string m_strREWARD_ID = "REWARD_ID";
    #endregion
}

public class G_ContentsOpenData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eMenuType = (GT_UI)G_JSONCtrl.GetINT(vNode, m_strUI_MENU_TYPE);
        m_eOpenType = (GT_ContentsOpen)G_JSONCtrl.GetINT(vNode, m_strOPEN_TYPE);
        m_iOpenConditionValue = G_JSONCtrl.GetINT(vNode, m_strOPEN_CONDITION_VALUE);
    }

    public GT_UI a_eMenuType { get { return m_eMenuType; } }
    private GT_UI m_eMenuType = GT_UI.None;

    public GT_ContentsOpen a_eOpenType { get { return m_eOpenType; } }
    private GT_ContentsOpen m_eOpenType = GT_ContentsOpen.None;

    public int a_iOpenConditionValue { get { return m_iOpenConditionValue; } }
    private int m_iOpenConditionValue = 0;

    #region ConstParseKey
    private static string m_strUI_MENU_TYPE = "UI_MENU_TYPE";
    private static string m_strOPEN_TYPE = "OPEN_TYPE";
    private static string m_strOPEN_CONDITION_VALUE = "OPEN_CONDITION_VALUE";
    #endregion
}

public class G_TestHallRankRewardData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_strRank = G_JSONCtrl.GetString(vNode, m_strRANK);
        m_iScore = G_JSONCtrl.GetINT(vNode, m_strSCORE);
        m_eCurrencyType = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strCURRENCY_TYPE);
        m_iCount = G_JSONCtrl.GetINT(vNode, m_strCOUNT);

        m_vList.Add(this);
    }

    public static void ClearStaticValues()
    {
        if (m_vList != null)
        {
            G_Utils.RemoveListAll(ref m_vList);
        }
    }

    public static void GetRankData(BigInteger iScore, ref G_TestHallRankRewardData vRetData)
    {
        for (int i = 0; i < m_vList.Count; ++i)
        {
            if (iScore >= m_vList[m_vList.Count - 1].m_iScore)
                vRetData = m_vList[m_vList.Count - 1];
            else
            {
                if (iScore >= m_vList[i].m_iScore && iScore < m_vList[i + 1].m_iScore)
                    vRetData = m_vList[i];
            }
        }

        // 아무것도 없으면 맨처음꺼로 세팅
        if (vRetData == null)
            vRetData = m_vList[0];
    }

    public string a_strRank { get { return m_strRank; } set { m_strRank = value; } }
    public int a_iScore { get { return m_iScore; } set { m_iScore = value; } }
    public GT_Currency a_eCurrencyType { get { return m_eCurrencyType; } set { m_eCurrencyType = value; } }
    public int a_iCount { get { return m_iCount; } set { m_iCount = value; } }

    private string m_strRank = string.Empty;
    private int m_iScore = 0;
    private GT_Currency m_eCurrencyType = GT_Currency.None;
    private int m_iCount = 0;

    private static List<G_TestHallRankRewardData> m_vList = new List<G_TestHallRankRewardData>();

    #region ConstParseKey
    private static string m_strRANK = "RANK";
    private static string m_strSCORE = "SCORE";
    private static string m_strCURRENCY_TYPE = "CURRENCY_TYPE";
    private static string m_strCOUNT = "COUNT";
    #endregion
}

public class G_ArtifactData : G_GlobalDataClass
{
    public override void Load(JSONNode vNode)
    {
        base.Load(vNode);

        m_eArtifactType = (GT_Artifact)G_JSONCtrl.GetINT(vNode, m_strARTIFACT_TYPE);
        m_eClass = (GT_Class)G_JSONCtrl.GetINT(vNode, m_strCLASS);
        m_strClassNameTextKey = G_JSONCtrl.GetString(vNode, m_strCLASS_NAME_TEXT_KEY);
        m_eType = (GT_BaseStat)G_JSONCtrl.GetINT(vNode, m_strTYPE);
        m_strNameKey = G_JSONCtrl.GetString(vNode, m_strNAME_KEY);
        m_strSpriteKey = G_JSONCtrl.GetString(vNode, m_strSPRITE_KEY);
        m_fStatValue = G_JSONCtrl.GetFLOAT(vNode, m_strSTAT_VALUE);
        m_fStatGrowValue = G_JSONCtrl.GetFLOAT(vNode, m_strSTAT_GROW_VALUE);
        m_eCostType = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strCOST_TYPE);
        m_iMaxLevel = G_JSONCtrl.GetINT(vNode, m_strMAX_LEVEL);
        m_fDownProb = G_JSONCtrl.GetFLOAT(vNode, m_strDOWN_PROB);
        m_eSellCurrency = (GT_Currency)G_JSONCtrl.GetINT(vNode, m_strSELL_CURRENCY);
        m_iSellPrice = G_JSONCtrl.GetINT(vNode, m_strSELL_PRICE);

        if (m_dicArtifactTypeGroup.ContainsKey(m_eArtifactType) == false)
            m_dicArtifactTypeGroup.Add(m_eArtifactType, new List<int>());
        m_dicArtifactTypeGroup[m_eArtifactType].Add(a_iID);

        if (m_dicArtifactCurrencyTypeGroup.ContainsKey(m_eCostType) == false)
            m_dicArtifactCurrencyTypeGroup.Add(m_eCostType, a_iID);
    }

    public static void ClearStaticValues()
    {
        if (m_dicArtifactTypeGroup != null)
        {
            m_dicArtifactTypeGroup.Clear();
        }

        if (m_dicArtifactCurrencyTypeGroup != null)
        {
            m_dicArtifactCurrencyTypeGroup.Clear();
        }
    }

    public static void GetArtifactGroupByType(GT_Artifact eType, ref List<int> vRetData)
    {
        if (m_dicArtifactTypeGroup.ContainsKey(eType) == false)
            return;

        vRetData = m_dicArtifactTypeGroup[eType];
    }

    public static int GetArtifactIDByCurrencyType(GT_Currency eType)
    {
        if (m_dicArtifactCurrencyTypeGroup.ContainsKey(eType) == false)
            return 0;

        return m_dicArtifactCurrencyTypeGroup[eType];
    }

    public GT_Artifact a_eArtifactType { get { return m_eArtifactType; } set { m_eArtifactType = value; } }
    public GT_Class a_eClass { get { return m_eClass; } }
    public string a_strClassNameTextKey { get { return m_strClassNameTextKey; } }
    public GT_BaseStat a_eType { get { return m_eType; } set { m_eType = value; } }
    public string a_strNameKey { get { return m_strNameKey; } set { m_strNameKey = value; } }
    public string a_strSpriteKey { get { return m_strSpriteKey; } set { m_strSpriteKey = value; } }
    public float a_fStatValue { get { return m_fStatValue; } set { m_fStatValue = value; } }
    public float a_fStatGrowValue { get { return m_fStatGrowValue; } set { m_fStatGrowValue = value; } }
    public GT_Currency a_eCostType { get { return m_eCostType; } set { m_eCostType = value; } }
    public int a_iMaxLevel { get { return m_iMaxLevel; } set { m_iMaxLevel = value; } }
    public float a_fDownProb { get { return m_fDownProb; } set { m_fDownProb = value; } }
    public GT_Currency a_eSellCurrency { get { return m_eSellCurrency; } set { m_eSellCurrency = value; } }
    public int a_iSellPrice { get { return m_iSellPrice; } set { m_iSellPrice = value; } }

    private GT_Artifact m_eArtifactType = 0;
    private GT_Class m_eClass = GT_Class.Normal;
    private string m_strClassNameTextKey = string.Empty;
    private GT_BaseStat m_eType = 0;
    private string m_strNameKey = string.Empty;
    private string m_strSpriteKey = string.Empty;
    private float m_fStatValue = 0;
    private float m_fStatGrowValue = 0.0f;
    private GT_Currency m_eCostType = GT_Currency.None;
    private int m_iMaxLevel = 0;
    private float m_fDownProb = 0.0f;
    private GT_Currency m_eSellCurrency = GT_Currency.None;
    private int m_iSellPrice = 0;

    public static Dictionary<GT_Artifact, List<int>> m_dicArtifactTypeGroup = new Dictionary<GT_Artifact, List<int>>();
    public static Dictionary<GT_Currency, int> m_dicArtifactCurrencyTypeGroup = new Dictionary<GT_Currency, int>();

    #region ConstParseKey
    private static string m_strARTIFACT_TYPE = "ARTIFACT_TYPE";
    private static string m_strCLASS = "CLASS";
    private static string m_strCLASS_NAME_TEXT_KEY = "CLASS_NAME_TEXT_KEY";
    private static string m_strTYPE = "TYPE";
    private static string m_strNAME_KEY = "NAME_KEY";
    private static string m_strSPRITE_KEY = "SPRITE_KEY";
    private static string m_strSTAT_VALUE = "STAT_VALUE";
    private static string m_strSTAT_GROW_VALUE = "STAT_GROW_VALUE";
    private static string m_strCOST_TYPE = "COST_TYPE";
    private static string m_strMAX_LEVEL = "MAX_LEVEL";
    private static string m_strDOWN_PROB = "DOWN_PROB";
    private static string m_strSELL_CURRENCY = "SELL_CURRENCY";
    private static string m_strSELL_PRICE = "SELL_PRICE";
    #endregion
}


public class G_LocalizeText : G_GlobalDataClass
{
    public override void Load(JSONNode vNode, out string strKey)
    {
        base.Load(vNode);

        m_strTextKey = G_JSONCtrl.GetString(vNode, m_strKEY_TEXT);
        m_strKR = G_JSONCtrl.GetString(vNode, m_strKEY_KR);
        m_strEN = G_JSONCtrl.GetString(vNode, m_strKEY_EN);
        m_strJP = G_JSONCtrl.GetString(vNode, m_strKEY_JP);
        m_strTW = G_JSONCtrl.GetString(vNode, m_strKEY_TW);
        m_strCN = G_JSONCtrl.GetString(vNode, m_strKEY_CN);
        m_strID = G_JSONCtrl.GetString(vNode, m_strKEY_IDN);

        strKey = m_strTextKey;
    }

    public string GetLocalize()
    {
        return string.Empty;
    }

    public string a_strTextKey { get { return m_strTextKey; } }
    public string a_strKR { get { return m_strKR; } }
    public string a_strEN { get { return m_strEN; } }
    public string a_strJP { get { return m_strJP; } }
    public string a_strTW { get { return m_strTW; } }
    public string a_strCN { get { return m_strCN; } }
    public string a_strTH { get { return m_strTH; } }
    public string a_strDE { get { return m_strDE; } }
    public string a_strFR { get { return m_strFR; } }
    public string a_strSP { get { return m_strSP; } }
    public string a_strRU { get { return m_strRU; } }
    public string a_strID { get { return m_strID; } }
    public string a_strPT { get { return m_strPT; } }

    string m_strTextKey = string.Empty;
    string m_strKR = string.Empty;
    string m_strEN = string.Empty;
    string m_strJP = string.Empty;
    string m_strTW = string.Empty;
    string m_strCN = string.Empty;
    string m_strTH = string.Empty;
    string m_strDE = string.Empty;
    string m_strFR = string.Empty;
    string m_strSP = string.Empty;
    string m_strRU = string.Empty;
    string m_strID = string.Empty;
    string m_strPT = string.Empty;

    #region ConstParseKey
    static string m_strKEY_TEXT = "TEXT";
    static string m_strKEY_KR = "KR";
    static string m_strKEY_EN = "EN";
    static string m_strKEY_JP = "JP";
    static string m_strKEY_TW = "TW";
    static string m_strKEY_CN = "CN";
    static string m_strKEY_TH = "TH";
    static string m_strKEY_DE = "DE";
    static string m_strKEY_FR = "FR";
    static string m_strKEY_SP = "SP";
    static string m_strKEY_RU = "RU";
    static string m_strKEY_IDN = "IDN";
    static string m_strKEY_PT = "PT";
    #endregion
}