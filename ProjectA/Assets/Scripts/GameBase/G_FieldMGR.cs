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

        if (!G_GameMGR.a_instance.a_vObjectPools.ContainsKey(GT_Pool.Monster))
        {
            for (int i = 0; i < G_Constant.m_iMonsterPoolSize; ++i)
            {
                G_GameMGR.a_instance.CreatePoolObject(GT_Pool.Monster, m_vMonsterPoolObject.transform);
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
            // Summon characters by class
            if (m_dicCharacters != null && G_GameMGR.a_instance.a_vIngameArea != null)
            {
                for (GT_UnitClass eClass = GT_UnitClass.Axe; eClass <= GT_UnitClass.TwoSword; ++eClass)
                {
                    bool bInitialize = true;
                    G_UnitMainCharacter vCharacter = null;

                    if (!m_dicCharacters.ContainsKey(eClass))
                    {
                        GameObject vObj = Instantiate(LoadResource<GameObject>(G_Constant.m_strCharacterObject), G_GameMGR.a_instance.a_vIngameArea);
                        if (vObj != null)
                        {
                            vCharacter = vObj.GetComponent<G_UnitMainCharacter>();
                            if (vCharacter != null)
                                m_dicCharacters.Add(eClass, vCharacter);
                            bInitialize = false;
                        }
                    }
                    else
                    {
                        vCharacter = m_dicCharacters[eClass];
                    }

                    if (vCharacter != null)
                    {
                        if (!bInitialize)
                        {
                            vCharacter.InitializeObject();
                            if (G_GameMGR.a_instance.a_vGameScene != null && G_GameMGR.a_instance.a_vGameScene.a_vMainCamera != null)
                            {
                                if (eClass == GT_UnitClass.TwoSword)
                                {
                                    G_CameraController vCtrl = G_GameMGR.a_instance.a_vGameScene.a_vMainCamera.GetComponent<G_CameraController>();
                                    if (vCtrl != null && vCtrl.a_vPlayerTransform == null)
                                        vCtrl.SetPlayerTransform(vCharacter.transform);
                                }
                            }
                        }

                        vCharacter.ResetObject(eClass);

                        if (m_vRepCharacter == null && eClass == GT_UnitClass.TwoSword)
                        {
                            vCharacter.SetRepresentative();
                            m_vRepCharacter = vCharacter;
                        }
                    }
                }
            }
        }

        {
            // 모든 필드의 오브젝트 초기화
            for (int i = 0; i < m_vMonsterList.Count; ++i)
            {
                if (m_vMonsterList[i] == null)
                    continue;

                switch (m_vMonsterList[i].a_eUnitType)
                {
                    case GT_Unit.Monster:
                        {
                            G_GameMGR.a_instance.ReturnPoolObject(GT_Pool.Monster, m_vMonsterList[i].gameObject);
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

    public void StartMode(GT_Field eFieldType)
    {
        ResetMode();

        m_eFieldType = eFieldType;
        switch (eFieldType)
        {
            case GT_Field.Stage:
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
            case GT_Field.Stage:
                {
                    if (m_vIngameStageMode != null)
                        m_vIngameStageMode.UpdateMode();
                }
                break;
            case GT_Field.Dungeon:
                {

                }
                break;
        }
    }

    public void NotifyAttackTarget(G_UnitMainCharacter vSource)
    {
        if (vSource == null)
            return;

        G_UnitObject vTarget = vSource.a_vAttackTarget;
        if (vTarget == null)
            return;

        foreach (G_UnitMainCharacter vCharacter in m_dicCharacters.Values)
        {
            if (vCharacter == null)
                continue;
            if (vCharacter == vSource)
                continue;

            vCharacter.SetAttackTarget(ref vTarget);
        }
    }

    public void NotifyDashCharacter(G_UnitMainCharacter vSource)
    {
        if (vSource == null)
            return;


        Vector3 vTargetPos = vSource.a_vDesDashPos;
        foreach (G_UnitMainCharacter vCharacter in m_dicCharacters.Values)
        {
            if (vCharacter == null)
                continue;
            if (vCharacter == vSource)
                continue;

            float fRand = UnityEngine.Random.Range(0.05f, 1.0f);
            vTargetPos += new Vector3(0.0f, fRand, 0.0f);
            ClampingLimitPosition(ref vTargetPos);
            vCharacter.SetDashDesPos(ref vTargetPos);
        }
    }

    public void GetAttackTarget(ref G_UnitObject vRetTarget, Vector3 vOwnPos, GT_Unit eOwnUnitType, float fMinAttackRange = 0.0f, bool bRandom = false)
    {
        switch (eOwnUnitType)
        {
            case GT_Unit.MainCharacter:
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
            case GT_Unit.Monster:
                {
                    if (m_dicCharacters != null)
                    {
                        float fMinDis = fMinAttackRange;
                        foreach (G_UnitMainCharacter vTarget in m_dicCharacters.Values)
                        {
                            if (vTarget == null)
                                continue;

                            float fDis = Vector3.Distance(vOwnPos, vTarget.transform.position);
                            if (fMinDis == 0 || fDis < fMinDis)
                            {
                                fMinDis = fDis;
                                vRetTarget = vTarget;
                                break;
                            }
                        }
                    }
                }
                break;
        }
    }

    private List<G_UnitObject> m_vNearbyMonsters = new List<G_UnitObject>();
    public void GetNearbyMonsterList(ref List<G_UnitObject> vRetTargets, Vector3 vOwnPos, GT_Unit eOwnUnitType, float fRange, int iMaxCount = 0)
    {
        switch (eOwnUnitType)
        {
            case GT_Unit.MainCharacter:
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
            case GT_Unit.Monster:
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
            case GT_Field.Stage:
                {
                    if (m_vIngameStageMode != null)
                        m_vIngameStageMode.UpdateModeInfo(ref m_vFieldPoint);
                }
                break;
            case GT_Field.Dungeon:
                {

                }
                break;
        }
    }

    private void ClampingLimitPosition(ref Vector3 vTargetPos)
    {
        if (m_vFieldPoint == null)
            return;

        if (m_vFieldPoint.a_vSpawnLimitMin != null && m_vFieldPoint.a_vSpawnLimitMax != null)
        {
            float fClampX = Mathf.Clamp(vTargetPos.x, m_vFieldPoint.a_vSpawnLimitMin.transform.position.x, m_vFieldPoint.a_vSpawnLimitMax.transform.position.x);
            float fClampY = Mathf.Clamp(vTargetPos.y, m_vFieldPoint.a_vSpawnLimitMin.transform.position.y, m_vFieldPoint.a_vSpawnLimitMax.transform.position.y);
            vTargetPos = new Vector3(fClampX, fClampY, 0);
        }
    }

    #region Variable
    public GT_Field a_eFieldType { get { return m_eFieldType; } }
    private GT_Field m_eFieldType = GT_Field.Stage;

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

    //public G_UnitMainCharacter a_vMainCharacter { get { return m_vMainCharacter; } }
    //private G_UnitMainCharacter m_vMainCharacter = null;

    public G_UnitMainCharacter a_vRepCharacter { get { return m_vRepCharacter; } }
    private G_UnitMainCharacter m_vRepCharacter = null;

    public Dictionary<GT_UnitClass, G_UnitMainCharacter> a_dicCharacters { get { return m_dicCharacters; } }
    private Dictionary<GT_UnitClass, G_UnitMainCharacter> m_dicCharacters = new Dictionary<GT_UnitClass, G_UnitMainCharacter>(); 

    public List<G_UnitObject> a_vMonsterList { get { return m_vMonsterList; } }
    private List<G_UnitObject> m_vMonsterList = new List<G_UnitObject>();
}
