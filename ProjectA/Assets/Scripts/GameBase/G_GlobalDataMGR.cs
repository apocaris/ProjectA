using System.Collections;
using System.Collections.Generic;

using SimpleJSON;
using System;
using System.Linq;


/*
 * Author : 최신
 * Date : 2019-04-07
 * Desc : Read-Only 데이터 관리자입니다. .json포맷으로 Export된 데이터들을 관리합니다.
 */

public class G_FileData
{
    /*
     * 단일 파일 안에, 다수의 테이블(Excel Sheet)이 존재할 수 있습니다.
     */
    public G_FileData(string strFileName, string[] strTableNames, GT_DataTable[] eTypes)
    {
        m_strFileName = strFileName;
        m_strTableNames = strTableNames;
        m_eTypes = eTypes;
    }

    public string a_strFileName { get { return m_strFileName; } set { m_strFileName = value; } }
    public string[] a_strTableNames { get { return m_strTableNames; } set { m_strTableNames = value; } }
    public GT_DataTable[] a_eTypes { get { return m_eTypes; } set { m_eTypes = value; } }

    protected string m_strFileName;
    protected string[] m_strTableNames;
    protected GT_DataTable[] m_eTypes;
}

public static class G_Text
{
    public static string Get(string strKey)
    {
        return G_GlobalDataMGR.a_instance.GetLocalizeText(strKey);
    }
}

public class G_GlobalDataMGR : G_SimpleMGR<G_GlobalDataMGR>
{
    #region SingletonMember
    private static G_GlobalDataMGR m_vInstance = null;
    public static G_GlobalDataMGR a_instance { get { if (m_vInstance == null) { EditorLog("MGR Instance is null !", 1); m_vInstance = ResetMGR(); } return m_vInstance; } }
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initalize()
    {
        base.Initalize();

        m_bLoadComplete = false;
        m_vTABLEs = new G_GlobalDataTable[(int)GT_DataTable.Count];

        EditorLog(ToString() + " Initalize Complete");
        m_bInitComplete = true;
    }

    /*
    protected virtual G_GlobalDataTable GetTableData(GT_DataTable eTableType)
    {
        if (m_vTABLEs == null)
            return null;

        if (eTableType >= GT_DataTable.Count)
            return null;

        if (m_vTABLEs.Length <= (int)eTableType)
            return null;

        if (m_vTABLEs[(int)eTableType] == null)
            m_vTABLEs[(int)eTableType] = new G_GlobalDataTable();

        return m_vTABLEs[(int)eTableType];
    }
    */

    protected virtual void GetTableData(GT_DataTable eType, out G_GlobalDataTable vRetData)
    {
        vRetData = null;
        if (m_vTABLEs == null)
            return;

        if (eType >= GT_DataTable.Count)
            return;

        if (m_vTABLEs.Length <= (int)eType)
            return;

        if (m_vTABLEs[(int)eType] == null)
            m_vTABLEs[(int)eType] = new G_GlobalDataTable();

        vRetData = m_vTABLEs[(int)eType];
    }

    public void ClearAllData()
    {
        if (m_vTABLEs != null && m_vTABLEs.Length > 0)
        {
            Array.Clear(m_vTABLEs, (int)GT_DataTable.Skill, m_vTABLEs.Length - (int)GT_DataTable.Skill);
        }

        G_OverLevelData.ClearStaticValues();
        G_SkillData.ClearStaticValues();
        G_AttendanceData.ClearStaticValues();
        G_RewardData.ClearStaticValues();
        G_BaseStatData.ClearStaticValues();
        G_PropertyStatData.ClearStaticValues();
        G_WingStatData.ClearStaticValues();
        G_EquipmentData.ClearStaticValues();
        G_MissionData.ClearStaticValues();
        G_RewardData.ClearStaticValues();
        G_CurrencyGroupData.ClearStaticValues();
        G_GlobalVariableData.ClearStaticValues();
        G_StartPossessionData.ClearStaticValues();
        G_AbilityData.ClearStaticValues();
        G_ProbabilityData.ClearStaticValues();
        G_StageData.ClearStaticValues();
        G_DungeonData.ClearStaticValues();
        G_LootData.ClearStaticValues();
        G_TreasureHouseRankReward.ClearStaticValues();
        G_ProductData.ClearStaticValues();
        G_PassData.ClearStaticValues();
        G_PassProductData.ClearStaticValues();
        G_SummonData.ClearStaticValues();
        G_OfflineRewardData.ClearStaticValues();
        G_BannerData.ClearStaticValues();
        G_RankRewardData.ClearStaticValues();
        G_TestHallRankRewardData.ClearStaticValues();
        G_ArtifactData.ClearStaticValues();

        GC.Collect();
    }

    public void Init()
    {
        //매니저 초기화 및 생성, 그리고 텍스트 데이터만 우선적으로 로드합니다.
        Initalize();

        StartCoroutine(CreateTextData());
    }

    public void LoadClientData()
    {
        if (!m_bInitComplete)
            return;

        //클라이언트에 사용하는 모든 데이터를 불러옵니다.
        StartCoroutine(CreateClientTableData());
    }

    public IEnumerator CreateTextData()
    {
        //클라이언트 데이터 로드 전 게임에서 사용하는 모든 문자열에 관한 데이터부터 읽어옵니다.
        a_bLoadTextComplete = false;

        List<G_FileData> vFILE_LIST = new List<G_FileData>();
        G_FileData vFILE = new G_FileData("LocalizeText",
          new string[] {
                "Text",
                "Word",
                "Contents",
                "Status",
                "Message",
                "Skill",
                "Item",
                "Shop",
                "Error",
                "Mission",
          },
          new GT_DataTable[] {
                GT_DataTable.Text,
                GT_DataTable.Text,
                GT_DataTable.Text,
                GT_DataTable.Text,
                GT_DataTable.Text,
                GT_DataTable.Text,
                GT_DataTable.Text,
                GT_DataTable.Text,
                GT_DataTable.Text,
                GT_DataTable.Text,
          }
          );
        vFILE_LIST.Add(vFILE);

        yield return StartCoroutine(LoadTables(vFILE_LIST));

        a_bLoadTextComplete = true;
    }

    public IEnumerator CreateClientTableData()
    {
        a_bLoadComplete = false;
        List<G_FileData> vFILE_LIST = new List<G_FileData>();

        string strFileName;

        {
            strFileName = "Skill";

            /*Skill.xlsx*/
            G_FileData vSKILL_FILE = new G_FileData(strFileName,
              new string[] {
                "Skill",
                "SkillShape",
                "SkillLevel",
                "SkillBuff",
                "SkillDebuff",
                "AdBuff"
              },
              new GT_DataTable[] {
                GT_DataTable.Skill,
                GT_DataTable.SkillShape,
                GT_DataTable.SkillLevel,
                GT_DataTable.Buff,
                GT_DataTable.Debuff,
                GT_DataTable.AdBuff,
              }
              );
            vFILE_LIST.Add(vSKILL_FILE);
        }

        {
            strFileName = "Unit";

            /*Unit.xlsx*/
            G_FileData vUNIT_FILE = new G_FileData(strFileName,
              new string[] {
                "Monster",
                "Status",
              },
              new GT_DataTable[] {
                GT_DataTable.Monster,
                GT_DataTable.MonsterStatus,
              }
              );
            vFILE_LIST.Add(vUNIT_FILE);
        }

        {
            strFileName = "Field";

            /*Field.xlsx*/
            // TODO: Data, Contents, Reward 는 사용하지 않음
            G_FileData vFIELD_FILE = new G_FileData(strFileName,
              new string[] {
                "Data",
                "Contents",
                "Reward",
                "Stage",
                "Dungeon",
                "Loot"
              },
              new GT_DataTable[] {
                GT_DataTable.Field,
                GT_DataTable.FieldContents,
                GT_DataTable.FieldReward,
                GT_DataTable.Stage,
                GT_DataTable.Dungeon,
                GT_DataTable.Loot,
              }
              );
            vFILE_LIST.Add(vFIELD_FILE);
        }

        {
            strFileName = "Exp";

            /*Exp.xlsx*/
            G_FileData vEXP_FILE = new G_FileData(strFileName,
              new string[] {
                "Account",
                "ArenaTier",
              },
              new GT_DataTable[] {
                GT_DataTable.ExpAccount,
                GT_DataTable.ExpArenaTier,
              }
              );
            vFILE_LIST.Add(vEXP_FILE);
        }

        {
            strFileName = "Attendance";

            /*Attendance.xlsx*/
            G_FileData vATTENDANCE_FILE = new G_FileData(strFileName,
              new string[] {
                "Attendance",
              },
              new GT_DataTable[] {
                GT_DataTable.Attendance,
              }
              );
            vFILE_LIST.Add(vATTENDANCE_FILE);
        }

        {
            strFileName = "Value";

            /*Value.xlsx*/
            G_FileData vVALUE_FILE = new G_FileData(strFileName,
              new string[] {
                "Currency",
                "CurrencyGroup",
                "ContentsOpen",
                "Item",
                "Grade"
              },
              new GT_DataTable[] {
                GT_DataTable.Currency,
                GT_DataTable.CurrencyGroup,
                GT_DataTable.ContentsOpen,
                GT_DataTable.Item,
                GT_DataTable.Grade,
              }
              );
            vFILE_LIST.Add(vVALUE_FILE);
        }

        {
            strFileName = "Reward";

            /*Reward.xlsx*/
            G_FileData vREWARD_FILE = new G_FileData(strFileName,
              new string[] {
                "Reward",
                "Rank",
                "DragonNestRank",
                "OfflineReward",
                "TestHallRank"
              },
              new GT_DataTable[] {
                GT_DataTable.Reward,
                GT_DataTable.RankReward,
                GT_DataTable.TreasureHouseRankReward,
                GT_DataTable.OfflineReward,
                GT_DataTable.TestHallRankReward,
              }
              );
            vFILE_LIST.Add(vREWARD_FILE);
        }

        {
            strFileName = "Character";

            /*Character.xlsx*/
            G_FileData vCHARACTER_FILE = new G_FileData(strFileName,
              new string[] {
                "Stat",
                "Property",
                "WingStat",
                "Ability",
                "SupportUnit",
                "ExtraGear",
                "OverLevel",
                "Potential",
                "Probability",
                "Equipment",
                "StartPossession",
                "EquipEffect",
                "Costume",
                "Wing",
                "Artifact"
              },
              new GT_DataTable[] {
                GT_DataTable.BaseStat,
                GT_DataTable.PropertyStat,
                GT_DataTable.WingStat,
                GT_DataTable.Ability,
                GT_DataTable.SupportUnit,
                GT_DataTable.ExtraGear,
                GT_DataTable.OverLevel,
                GT_DataTable.Potential,
                GT_DataTable.Probability,
                GT_DataTable.Equipment,
                GT_DataTable.StartPossession,
                GT_DataTable.CharacterEquipEffect,
                GT_DataTable.Costume,
                GT_DataTable.Wing,
                GT_DataTable.Artifact,
              }
              );
            vFILE_LIST.Add(vCHARACTER_FILE);
        }

        {
            strFileName = "Shop";

            /*Shop.xlsx*/
            G_FileData vSHOP_FILE = new G_FileData(strFileName,
              new string[] {
                "Product",
                "Pass",
                "PassProduct",
                "Summon",
                "Banner",
              },
              new GT_DataTable[] {
                GT_DataTable.Product,
                GT_DataTable.Pass,
                GT_DataTable.PassProduct,
                GT_DataTable.Summon,
                GT_DataTable.Banner,
              }
              );
            vFILE_LIST.Add(vSHOP_FILE);
        }

        {
            strFileName = "Mission";

            /*Mission.xlsx*/
            G_FileData vMISSION_FILE = new G_FileData(strFileName,
              new string[] {
                "Mission",
              },
              new GT_DataTable[] {
                GT_DataTable.Mission,
              }
              );
            vFILE_LIST.Add(vMISSION_FILE);
        }

        {
            strFileName = "GlobalVariable";

            /*GlobalVariable.xlsx*/
            G_FileData vGLOBAL_VARIABLE_FILE = new G_FileData(strFileName,
              new string[] {
                "GlobalVariable",
              },
              new GT_DataTable[] {
                GT_DataTable.GlobalVariable,
              }
              );
            vFILE_LIST.Add(vGLOBAL_VARIABLE_FILE);
        }

        yield return StartCoroutine(LoadTables(vFILE_LIST));

        a_bLoadComplete = true;
    }

    public IEnumerator LoadTables(List<G_FileData> vFILEs)
    {
        m_iTotalTableCount = vFILEs.Count;
        m_iCurLoadedTableCount = 0;

        string strTablePATH = string.Empty;
        strTablePATH = m_strTableDataPath;
        for(var enumerator = vFILEs.GetEnumerator(); enumerator.MoveNext();)
        {
            G_FileData vFILE = enumerator.Current;
            m_fCurTableProgress = 0.0f;
            int iFileCount = vFILE.a_eTypes.Length;

            JSONNode vData = G_JSONCtrl.LoadFromTextFile(strTablePATH + vFILE.a_strFileName);
            for (int i = 0; i < iFileCount; ++i)
            {
                G_GlobalDataTable vTable = null;
                GetTableData(vFILE.a_eTypes[i], out vTable);
                if (vTable != null)
                    vTable.Load(vData[vFILE.a_strTableNames[i]], vFILE.a_eTypes[i]);

                m_fCurTableProgress = i / iFileCount;
                yield return null;
            }

            m_fCurTableProgress = 1f;
            m_iCurLoadedTableCount += 1;

            yield return null;
        }
    }

    /*
    public T GetData<T>(GT_DataTable eTableType, int iID) where T : G_GlobalDataClass
    {
        G_GlobalDataTable vTable = GetTableData(eTableType);
        return vTable == null ? null : vTable.GetData<T>(iID);
    }
    */

    public void GetData<T>(GT_DataTable eType, int iID, out T vRetData) where T : G_GlobalDataClass
    {
        vRetData = null;

        G_GlobalDataTable vTable;
        GetTableData(eType, out vTable);
        if (vTable == null)
            return;

        vTable.GetData<T>(iID, out vRetData);
    }

    public string GetLocalizeText(string strKey)
    {
        string strText = string.Empty;

        G_GlobalDataTable vTable;
        GetTableData(GT_DataTable.Text, out vTable);
        if (vTable != null)
        {
            G_LocalizeText vData;
            vTable.GetData<G_LocalizeText>(strKey, out vData);
            if (vData != null)
                return vData.GetLocalize();
            else
                return strKey; //null이면 text key그대로 리턴
        }
        return strText;
    }

    public float GetGlobalVariable(GT_GlobalVariable eType)
    {
        G_GlobalDataTable vTable;
        GetTableData(GT_DataTable.GlobalVariable, out vTable);
        if (vTable != null)
        {
            G_GlobalVariableData vData;
            vTable.GetData<G_GlobalVariableData>((int)eType, out vData);
            if (vData != null)
                return vData.a_fValue;
        }

        return 0.0f;
    }

    public bool GetTable<T>(GT_DataTable eType, out Dictionary<int, G_GlobalDataClass> vDicTable) where T : G_GlobalDataTable
    {
        G_GlobalDataTable vTable;
        GetTableData(eType, out vTable);
        if (vTable == null)
        {
            vDicTable = null;
            return false;
        }
        else
        {
            vDicTable = vTable.GetTable();
            return true;
        }
    }

    /*
    public T GetDataByIndex<T>(GT_DataTable eType, int iIndex) where T : G_GlobalDataClass
    {
        G_GlobalDataTable vTable;
        GetTableData(eType, out vTable);
        Dictionary<int, G_GlobalDataClass> vDicTable = vTable.GetTable();
        List<int> vKeyList = new List<int>(vDicTable.Keys);
        if (iIndex > vKeyList.Count - 1)
            return null;

        return vTable.GetData<T>(vKeyList[iIndex]);
    }
    */

    public void GetFirstData<T>(GT_DataTable eType, out T vRetData) where T : G_GlobalDataClass
    {
        vRetData = null;

        G_GlobalDataTable vTable;
        GetTableData(eType, out vTable);
        if (vTable != null && vTable.GetTable() != null)
        {
            KeyValuePair<int, G_GlobalDataClass> vData = vTable.GetTable().FirstOrDefault();
            if (vData.Value != null)
            {
                vRetData = vData.Value as T;
            }
        }
    }

    public int GetTableDataCount(GT_DataTable eType)
    {
        G_GlobalDataTable vTable;
        GetTableData(eType, out vTable);
        if (vTable == null)
            return 0;

        return vTable.GetTable().Count;
    }

    protected G_GlobalDataTable[] m_vTABLEs = null;

    public bool a_bLoadComplete { get { return m_bLoadComplete; } set { m_bLoadComplete = value; } }
    protected bool m_bLoadComplete = false;

    public int a_iTotalTableCount { get { return m_iTotalTableCount; } set { m_iTotalTableCount = value; } }
    protected int m_iTotalTableCount = 0;

    public int a_iCurLoadedTableCount { get { return m_iCurLoadedTableCount; } set { m_iCurLoadedTableCount = value; } }
    protected int m_iCurLoadedTableCount = 0;

    public float a_fCurTableProgress { get { return m_fCurTableProgress * 100f; } set { m_fCurTableProgress = value; } }
    protected float m_fCurTableProgress = 0.0f;

    public bool a_bLoadTextComplete { get { return m_bLoadTextComplete; } set { m_bLoadTextComplete = value; } }
    protected bool m_bLoadTextComplete = false;

    private bool m_bInitComplete = false;

    #region ConstFilePath
    protected const string m_strResourcesRoot = "/Resources/";
    protected const string m_strTableDataPath = "TableDatas/";
    #endregion

}

/*
  -참조방법
      Dictionary<int, G_GlobalDataClass> vTestTable = null;
      if (G_GlobalDataMGR.a_instance.GetTable<G_GlobalDataTable>(GT_DataTable.TEST_FILE, out vTestTable) == false)
          Debug.LogError("");

      foreach(G_TestClass vTEST in vTestTable.Values) { ... }

      or

      G_TileMapData vData = G_GlobalDataMGR.a_instance.GetData<G_TileMapData>(G_TableType.TileMap, 1234);
  */
