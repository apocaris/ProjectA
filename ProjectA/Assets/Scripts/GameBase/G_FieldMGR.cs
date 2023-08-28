using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;

public class G_FieldMGR : G_SimpleMGR<G_FieldMGR>
{
    private static G_FieldMGR m_vInstance = null;
    public static G_FieldMGR a_instance
    {
        get
        {
            if (m_vInstance == null)
            {
                EditorLog("MGR Instance is null", 1);
                m_vInstance = ResetMGR();
            }

            return m_vInstance;
        }
    }

    public override void Initalize()
    {
        base.Initalize();

        if (m_vMonsterPoolObject == null)
            m_vMonsterPoolObject = GameObject.Find(G_Constant.m_strMonsterPool);

        if (!G_GameMGR.a_instance.a_vObjectPools.ContainsKey(GT_PoolType.Monster))
        {
            for (int i = 0; i < G_Constant.m_iMonsterPoolSize; ++i)
            {
                G_GameMGR.a_instance.CreatePoolObject(GT_PoolType.Monster, m_vMonsterPoolObject.transform);
            }
        }

        GameObject vIngameStageMode = Instantiate(LoadResource<GameObject>(G_Constant.m_strBaseObject), transform);
        if (vIngameStageMode != null)
        {
            m_vIngameStageMode = vIngameStageMode.AddComponent<G_IngameStageMode>();
            if (m_vIngameStageMode != null)
            {
                m_vIngameStageMode.name = typeof(G_IngameStageMode).ToString();
                m_vIngameStageMode.ResetIngameMode();
            }
        }
    }

    public void FixedUpdate()
    {
        UpdateMode();
    }

    public void ResetMode()
    {
        // Create Character
        {
            bool bInitialize = true;
            if (m_vMainCharacter == null && G_GameMGR.a_instance.a_vIngameArea != null)
            {
                GameObject vObj = Instantiate(LoadResource<GameObject>(G_Constant.m_strCharacterObject), G_GameMGR.a_instance.a_vIngameArea);
                if (vObj != null)
                {
                    m_vMainCharacter = vObj.GetComponent<G_UnitMainCharacter>();
                    if (m_vMainCharacter != null)
                        bInitialize = false;
                }
            }

            if (m_vMainCharacter == null)
            {
                Debug.LogError("Not found main character unit");
                return;
            }

            if (!bInitialize)
            {
                m_vMainCharacter.InitializeObject();
            }

            m_vMainCharacter.ResetObject();
        }

        {
            // 모든 필드의 오브젝트 초기화
            for (int i = 0; i < m_vMonsterList.Count; ++i)
            {
                if (m_vMonsterList[i] == null)
                    continue;

                switch (m_vMonsterList[i].a_eUnitType)
                {
                    case GT_UnitType.Monster:
                        {
                            G_GameMGR.a_instance.ReturnPoolObject(GT_PoolType.Monster, m_vMonsterList[i].gameObject);
                        }
                        break;
                }
            }
            m_vMonsterList.Clear();
        }

        {
            m_bModeSettingComplete = false;
            m_bLoadModeState = false;
        }

        {
            if (m_vIngameStageMode != null)
                m_vIngameStageMode.ResetIngameMode();
        }
    }

    public void StartMode(GT_FieldType eFieldType)
    {
        ResetMode();

        m_eFieldType = eFieldType;
        switch (eFieldType)
        {
            case GT_FieldType.Stage:
                {
                    if (m_vIngameStageMode != null)
                    {
                        m_vIngameStageMode.StartMode();
                    }
                }
                break;
        }
    }

    public void UpdateMode()
    {
        if (!m_bModeSettingComplete)
            return;

        switch (m_eFieldType)
        {
            case GT_FieldType.Stage:
                {
                    if (m_vIngameStageMode != null)
                        m_vIngameStageMode.UpdateMode();
                }
                break;
            case GT_FieldType.Dungeon:
                {

                }
                break;
        }
    }

    public void GetAttackTarget(ref G_UnitObject vRetTarget, Vector3 vOwnPos, GT_UnitType eOwnUnitType, float fMinAttackRange = 0.0f, bool bRandom = false)
    {
        switch (eOwnUnitType)
        {
            case GT_UnitType.MainCharacter:
                {
                    if (m_vMonsterList == null)
                        return;

                    if (bRandom)
                    {
                        if (m_vMonsterList.Count > 0)
                        {
                            vRetTarget = m_vMonsterList[UnityEngine.Random.Range(0, m_vMonsterList.Count)];
                        }
                    }
                    else
                    {
                        float fMinDis = 0.0f;
                        for (int i = 0; i < m_vMonsterList.Count; ++i)
                        {
                            if (m_vMonsterList[i] == null)
                                continue;

                            if (!m_vMonsterList[i].a_bAlive)
                                continue;

                            float fDis = Vector2.Distance(vOwnPos, m_vMonsterList[i].transform.position);
                            if (fMinDis == 0 || fDis < fMinDis)
                            {
                                fMinDis = fDis;
                                vRetTarget = m_vMonsterList[i];
                            }
                        }
                    }
                }
                break;
            case GT_UnitType.Monster:
                {
                    //float fMinDis = fMinAttackRange;
                    //float fDis = Vector3.Distance(vOwnPos, m_vMainCharacter.transform.position);
                    //if (fMinDis == 0 || fDis < fMinDis)
                    //{
                    //    fMinDis = fDis;
                    //    vRetTarget = m_vMainCharacter;
                    //}

                    vRetTarget = m_vMainCharacter;
                }
                break;
        }
    }

    private List<G_UnitObject> m_vNearbyMonsters = new List<G_UnitObject>();
    public void GetNearbyMonsterList(ref List<G_UnitObject> vRetTargets, Vector3 vOwnPos, GT_UnitType eOwnUnitType, float fRange, int iMaxCount = 0)
    {
        switch (eOwnUnitType)
        {
            case GT_UnitType.MainCharacter:
                {
                    if (m_vNearbyMonsters == null)
                        return;

                    m_vNearbyMonsters.Clear();
                    if (m_vMonsterList == null)
                        return;

                    for (int i = 0; i < m_vMonsterList.Count; ++i)
                    {
                        if (m_vMonsterList[i] == null)
                            continue;

                        float fDis = Vector2.Distance(vOwnPos, m_vMonsterList[i].transform.position);
                        if (fDis <= fRange)
                        {
                            m_vNearbyMonsters.Add(m_vMonsterList[i]);
                            if (iMaxCount > 0)
                            {
                                if (m_vNearbyMonsters.Count >= iMaxCount)
                                    break;
                            }
                        }
                    }
                }
                break;
            case GT_UnitType.Monster:
                {

                }
                break;
        }

        vRetTargets = m_vNearbyMonsters;
    }

    public IEnumerator ChangeMode(string strSceneName)
    {
        m_bLoadModeState = true;
        yield return new WaitForSecondsRealtime(0.35f);

        ResetMode();
        yield return StartCoroutine(ChangeModeAsync(strSceneName, () => {
            ChangeModeEnd();
        }));
    }

    private IEnumerator ChangeModeAsync(string strSceneName, Action vOnComplete)
    {
        if (strSceneName.Equals(m_strLastAddtiveScene))
        {
            m_bModeSettingComplete = true;
            vOnComplete?.Invoke();
            yield break;
        }
        else
            m_vFieldPoint = null;

        if (!string.IsNullOrEmpty(m_strLastAddtiveScene))
        {
            GC.Collect();
            Debug.Log("GC Collect by Scene Upload");

            AsyncOperation vUnloadAsyncOper = SceneManager.UnloadSceneAsync(m_strLastAddtiveScene);
            if (!vUnloadAsyncOper.isDone)
                yield return null;
        }

        yield return new WaitForEndOfFrame();

        m_strLastAddtiveScene = strSceneName;
        AsyncOperation vLoadAsync = SceneManager.LoadSceneAsync(m_strLastAddtiveScene, LoadSceneMode.Additive);
        while (!vLoadAsync.isDone)
            yield return null;

        GameObject[] vFieldObjects = SceneManager.GetSceneByName(m_strLastAddtiveScene).GetRootGameObjects();
        for (int i = 0; i < vFieldObjects.Length; ++i)
        {
            if (vFieldObjects[i].name.Equals(G_Constant.m_strFieldPoints))
            {
                m_vFieldPoint = vFieldObjects[i].GetComponent<G_FieldPoint>();
                break;
            }
        }

        m_bLoadModeState = false;
        m_bModeSettingComplete = true;

        vOnComplete?.Invoke();
    }

    private void ChangeModeEnd()
    {
        if (m_vFieldPoint == null)
            return;

        switch (m_eFieldType)
        {
            case GT_FieldType.Stage:
                {
                    if (m_vIngameStageMode != null)
                        m_vIngameStageMode.UpdateModeInfo(ref m_vFieldPoint);
                }
                break;
            case GT_FieldType.Dungeon:
                {

                }
                break;
        }
    }

    #region Variable
    public GT_FieldType a_eFieldType { get { return m_eFieldType; } }
    private GT_FieldType m_eFieldType = GT_FieldType.Stage;

    private bool m_bLoadModeState = false;
    private string m_strLastAddtiveScene = string.Empty;
    private bool m_bModeSettingComplete = false;
    #endregion

    public G_FieldPoint a_vFieldPoint { get { return m_vFieldPoint; } }
    private G_FieldPoint m_vFieldPoint = null;

    public GameObject a_vMonsterPoolObject { get { return m_vMonsterPoolObject; } }
    private GameObject m_vMonsterPoolObject = null;

    // Ingame Mode
    public G_IngameStageMode a_vIngameStageMode { get { return m_vIngameStageMode; } }
    private G_IngameStageMode m_vIngameStageMode = null;

    public G_UnitMainCharacter a_vMainCharacter { get { return m_vMainCharacter; } }
    private G_UnitMainCharacter m_vMainCharacter = null;

    public List<G_UnitObject> a_vMonsterList { get { return m_vMonsterList; } }
    private List<G_UnitObject> m_vMonsterList = new List<G_UnitObject>();
}
