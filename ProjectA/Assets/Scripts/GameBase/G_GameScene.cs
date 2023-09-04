using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;

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
                InitializeUI();
                G_GameMGR.a_instance.StartGameMode(GT_Field.Stage);
            }
        }
        else
        {

        }
    }

    #region UI Initialize

    public void InitializeUI()
    {
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
        if (m_vMainCamera == null)
            return;
        if (vAnchor == null)
            return;

        float fHeight = vAnchor.GetHeight();

        Vector3 vAnchorCenter = new Vector3(vAnchor.transform.position.x, vAnchor.transform.position.y + (fHeight / 2), 0);
        Vector3 vDamagePos = m_vUICamera.ViewportToWorldPoint(m_vMainCamera.WorldToViewportPoint(vAnchorCenter));
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

    #region Variables

    // The number of damage labels currently being drawn on the screen.
    private List<GameObject> m_vDamageFonts = new List<GameObject>();

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
