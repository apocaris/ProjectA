using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_GameScene : G_UIBase
{
    public override void Awake()
    {
        base.Awake();

        if (m_vMainCamera != null)
            m_vIngameCamShake = m_vMainCamera.GetComponent<G_CameraShake>();
        if (m_vUICamera != null)
            m_vUICamShake = m_vUICamera.GetComponent<G_CameraShake>();
    }

    public void Start()
    {
        // 게임 시스템 Init
        if (!G_GameMGR.a_instance.a_bGameInitComplete)
        {
            G_GameMGR.a_instance.Initalize();
            if (G_GameMGR.a_instance.a_bGameInitComplete)
            {
                G_GameMGR.a_instance.StartGameMode(GT_FieldType.Stage);
            }
        }
        else
        {

        }
    }

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
            if (m_vMainCamera != null)
                m_vMainCamera.cullingMask = 0;
            if (m_vFieldCamera != null)
                m_vFieldCamera.cullingMask = 0;
        }
        else
        {
            if (m_vMainCamera != null)
            {
                m_vMainCamera.cullingMask |= 1 << LayerMask.NameToLayer("Default");
                m_vMainCamera.cullingMask |= 1 << LayerMask.NameToLayer("TransparentFX");
                m_vMainCamera.cullingMask |= 1 << LayerMask.NameToLayer("Ignore Raycast");
                m_vMainCamera.cullingMask |= 1 << LayerMask.NameToLayer("Water");
                m_vMainCamera.cullingMask |= 1 << LayerMask.NameToLayer("UI");
                m_vMainCamera.cullingMask |= 1 << LayerMask.NameToLayer("MainObject");
            }

            if (m_vFieldCamera != null)
                m_vFieldCamera.cullingMask |= 1 << LayerMask.NameToLayer("Field");
        }
    }
    #endregion

    public G_CameraShake a_vIngameCamShake { get { return m_vIngameCamShake; } }
    private G_CameraShake m_vIngameCamShake = null;

    public G_CameraShake a_vUICamShake { get { return m_vUICamShake; } }
    private G_CameraShake m_vUICamShake = null;

    [Header("Common")]
    [SerializeField, Rename("UI Root")]
    protected UIRoot m_vUIRoot = null;

    public Camera a_vUICamera { get { return m_vUICamera; } }
    [SerializeField, Rename("UI cam")]
    protected Camera m_vUICamera = null;

    public Camera a_vMainCamera { get { return m_vMainCamera; } }
    [SerializeField, Rename("Main cam")]
    protected Camera m_vMainCamera = null;

    [SerializeField, Rename("Field cam")]
    protected Camera m_vFieldCamera = null;

    [Header("Spawn Pools")]
    [SerializeField, Rename("UI Object Pool")]
    protected Transform m_vUIObjectSpawnPool = null;
}
