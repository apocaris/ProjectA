using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class G_GameMGR : G_SimpleMGR<G_GameMGR>
{
    private static G_GameMGR m_vInstance = null;
    public static G_GameMGR a_instance { get
        {
            if (m_vInstance == null)
            {
                EditorLog("MGR Instance is null", 1);
                m_vInstance = ResetMGR();
            }

            return m_vInstance;
        }
    }


    protected override void Awake()
    {
        base.Awake();
        m_vGameVersion = Application.version;
    }

    private void FixedUpdate()
    {
        
    }

    public override void Initalize()
    {
        base.Initalize();

        m_vGameScene = GetComponent<G_GameScene>();
        if (m_vGameScene == null)
        {
            // GameScene 은 GameMGR 과 함께 있어야 함. 세팅부터 잘못됨.
            Debug.LogError("Game init error : Not found G_GameScene");
            return;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(G_Constant.m_strIngameScene));
        G_FieldMGR.a_instance.Initalize();
        m_bGameInitComplete = true;
    }

    public void StartGameMode(GT_FieldType eFieldType)
    {
        G_FieldMGR.a_instance.StartMode(eFieldType);
    }

    #region Object Pooling
    public GameObject CreatePoolObject(GT_PoolType eType, Transform vParent)
    {
        if (vParent == null)
            return null;

        if (!m_vObjectPools.ContainsKey(eType))
            m_vObjectPools.Add(eType, new Queue<GameObject>());

        GameObject vObject = null;
        switch (eType)
        {
            case GT_PoolType.Monster:
                {
                    vObject = Instantiate(LoadResource<GameObject>(G_Constant.m_strMonsterObject), vParent);
                }
                break;
        }

        if (vObject != null)
            vObject.SetActive(false);

        m_vObjectPools[eType].Enqueue(vObject);
        return vObject;
    }

    public GameObject GetPoolObject(GT_PoolType eType, Transform vParent)
    {
        if (m_vObjectPools[eType].Count > 0)
        {
            return m_vObjectPools[eType].Dequeue();
        }

        return CreatePoolObject(eType, vParent);
    }

    public void ReturnPoolObject(GT_PoolType eType, GameObject vObject)
    {
        if (vObject != null)
        {
            vObject.SetActive(false);
            m_vObjectPools[eType].Enqueue(vObject);
        }
    }

    public void ResetPool(GT_PoolType eType)
    {
        if (m_vObjectPools != null)
        {
            if (m_vObjectPools.ContainsKey(eType))
            {
                while (m_vObjectPools[eType].Count > 0)
                {
                    GameObject vObj = m_vObjectPools[eType].Dequeue();
                    if (vObj != null)
                        Destroy(vObj);
                }
            }
        }
    }
    #endregion

    public string a_vGameVersion { get { return m_vGameVersion; } }
    private string m_vGameVersion = string.Empty;

    public bool a_bGameInitComplete { get { return m_bGameInitComplete; } }
    private bool m_bGameInitComplete = false;

    public G_GameScene a_vGameScene { get { return m_vGameScene; } }
    private G_GameScene m_vGameScene = null;

    //Pool -------------------------------------------
    public Dictionary<GT_PoolType, Queue<GameObject>> a_vObjectPools { get { return m_vObjectPools; } }
    private Dictionary<GT_PoolType, Queue<GameObject>> m_vObjectPools = new Dictionary<GT_PoolType, Queue<GameObject>>();
}
