using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Android;
using Vector3 = UnityEngine.Vector3;

public class G_GameScene : G_UIBase
{
    public override void Awake()
    {
        base.Awake();

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //문화권 시간 고정
        System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

        Application.targetFrameRate = 60;

        if (a_vMainCamera != null)
        {
            m_vIngameCamShake = a_vMainCamera.GetComponent<G_CameraShake>();

            float fAspect = a_vMainCamera.aspect;
            int iWidth = (int)(G_Constant.m_iScreenResoultion);
            int iHeight = (int)(G_Constant.m_iScreenResoultion * fAspect);

            Screen.SetResolution(iWidth, iHeight, true);
        }
            
        if (m_vUICamera != null)
            m_vUICamShake = m_vUICamera.GetComponent<G_CameraShake>();

#if UNITY_ANDROID
        //Android의 경우 지속가능한 성능 옵션 활성화 
        AndroidDevice.SetSustainedPerformanceMode(true);
#endif
    }

    public void Start()
    {
        // 게임 시스템 Init
        if (!G_GameMGR.a_instance.a_bGameInitComplete)
        {
            G_GameMGR.a_instance.Initalize();
            if (G_GameMGR.a_instance.a_bGameInitComplete)
            {
                InitializeUI();
                G_GameMGR.a_instance.StartGameMode(GT_Field.Stage);
            }
        }
        else
        {

        }
    }

    public void FixedUpdate()
    {
        if (G_GameMGR.a_instance.a_bGameInitComplete)
        {
            if (m_vAlwaysUpdates != null && m_vAlwaysUpdates.Count > 0)
            {
                for (int i = 0; i < m_vAlwaysUpdates.Count; ++i)
                {
                    try
                    {
                        if (m_vAlwaysUpdates[i] != null)
                            m_vAlwaysUpdates[i].Execute();
                    }
                    catch (Exception e)
                    {
                        G_Utils.DebugLog(e.ToString(), 2);
                    }
                }
            }
        }
    }

    #region UI Initialize

    public void InitializeUI()
    {
        // All UI Initialize
        if (m_vUIFixedMain != null)
            m_vUIFixedMain.Initialize();

        // Filling the Initial Damage Font Pool
        if (!G_GameMGR.a_instance.a_vObjectPools.ContainsKey(GT_Pool.DamageFont))
        {
            for (int i = 0; i < G_Constant.m_iDamageFontPoolSize; ++i)
            {
                G_GameMGR.a_instance.CreatePoolObject(GT_Pool.DamageFont, m_vUIObjectSpawnPool);
            }
        }
    }

    #endregion

    #region Damage Font

    public void ShowDamage(ref List<GT_Damage> vTypes, ref List<BigInteger> vValues, G_UnitObject vAnchor)
    {
        if (m_vUICamera == null)
            return;
        if (a_vMainCamera == null)
            return;
        if (vAnchor == null)
            return;

        float fHeight = vAnchor.GetHeight();

        Vector3 vAnchorCenter = new Vector3(vAnchor.transform.position.x, vAnchor.transform.position.y + (fHeight / 2), vAnchor.transform.position.z);
        Vector3 vDamagePos = m_vUICamera.ViewportToWorldPoint(a_vMainCamera.WorldToViewportPoint(vAnchorCenter));
        vDamagePos.z = 0.0f;

        for (int i = 0; i < vValues.Count; ++i)
        {
            if (m_vDamageFonts.Count > G_Constant.m_iDamageFontPoolSize)
            {
                GameObject vOld = m_vDamageFonts[0];
                if (vOld != null)
                {
                    G_GameMGR.a_instance.ReturnPoolObject(GT_Pool.DamageFont, vOld);
                    m_vDamageFonts.Remove(vOld);
                }
            }

            GameObject vNew = G_GameMGR.a_instance.GetPoolObject(GT_Pool.DamageFont, m_vUIObjectSpawnPool);
            if (vNew != null)
            {
                vNew.transform.position = vDamagePos;
                m_vDamageFonts.Add(vNew);
                G_IngameDamageFont vCtrl = vNew.GetComponent<G_IngameDamageFont>();
                if (vCtrl != null)
                {
                    vCtrl.ResetCtrl(vValues[i], vTypes[i], i, vAnchor.a_fUnitSize, () =>
                    {
                        G_GameMGR.a_instance.ReturnPoolObject(GT_Pool.DamageFont, vCtrl.gameObject);
                        m_vDamageFonts.Remove(vCtrl.gameObject);
                    });
                }
            }
        }
    }

    #endregion

    #region Cam setting
    public void CameraShake(float fAmount, float fDuration, float fDelay = 0.0f)
    {
        if (m_vIngameCamShake != null)
        {
            m_vIngameCamShake.Shake2D(fAmount, fDuration, fDelay);
        }
    }
    public void UpdateCamState(bool bState)
    {
        if (!bState)
        {
            if (a_vMainCamera != null)
                a_vMainCamera.cullingMask = 0;
            if (m_vFieldCamera != null)
                m_vFieldCamera.cullingMask = 0;
        }
        else
        {
            if (a_vMainCamera != null)
            {
                a_vMainCamera.cullingMask |= 1 << LayerMask.NameToLayer("Default");
                a_vMainCamera.cullingMask |= 1 << LayerMask.NameToLayer("TransparentFX");
                a_vMainCamera.cullingMask |= 1 << LayerMask.NameToLayer("Ignore Raycast");
                a_vMainCamera.cullingMask |= 1 << LayerMask.NameToLayer("Water");
                a_vMainCamera.cullingMask |= 1 << LayerMask.NameToLayer("UI");
                a_vMainCamera.cullingMask |= 1 << LayerMask.NameToLayer("MainObject");
            }

            if (m_vFieldCamera != null)
                m_vFieldCamera.cullingMask |= 1 << LayerMask.NameToLayer("Field");
        }
    }
    #endregion

    #region AlwaysUpdate

    public void AddAlwaysUpdate(ref EventDelegate vEvent)
    {
        if (vEvent == null)
            return;
        if (m_vAlwaysUpdates == null)
            return;

        if (m_vAlwaysUpdates.Contains(vEvent))
            return;

        m_vAlwaysUpdates.Add(vEvent);
    }

    #endregion

    #region Variables

    // The number of damage labels currently being drawn on the screen.
    private List<GameObject> m_vDamageFonts = new List<GameObject>();

    // -----------------------------------------------
    // Features that must always be updated regardless of power saving mode

    private List<EventDelegate> m_vAlwaysUpdates = new List<EventDelegate>();

    #endregion

    public G_CameraShake a_vIngameCamShake { get { return m_vIngameCamShake; } }
    private G_CameraShake m_vIngameCamShake = null;

    public G_CameraShake a_vUICamShake { get { return m_vUICamShake; } }
    private G_CameraShake m_vUICamShake = null;

    [Header("Common")]
    [SerializeField, Rename("UI Root")]
    private UIRoot m_vUIRoot = null;

    public Camera a_vUICamera { get { return m_vUICamera; } }
    [SerializeField, Rename("UI cam")]
    private Camera m_vUICamera = null;

    public Camera a_vMainCamera { 
        get
        {
#if CAM_PERSPECTIVE
            return m_vMainCamera_pers;
#else
            return m_vMainCamera_ortho;
#endif
        }
    }
    [SerializeField, Rename("Main cam : ortho")]
    private Camera m_vMainCamera_ortho = null;

    [SerializeField, Rename("Main cam : pers")]
    private Camera m_vMainCamera_pers = null;

    [SerializeField, Rename("Field cam")]
    private Camera m_vFieldCamera = null;

    [Header("Spawn Pools")]
    [SerializeField, Rename("UI Object Pool")]
    private Transform m_vUIObjectSpawnPool = null;

    [Header("Main UI's")]
    [SerializeField, Rename("Main Panel")]
    private GameObject m_vMainPanel = null;

    [SerializeField, Rename("Fixed main")]
    private G_UIFixedMain m_vUIFixedMain = null;
}
