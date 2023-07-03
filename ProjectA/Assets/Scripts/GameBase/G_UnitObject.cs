using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_UnitObject : G_Object
{
    protected virtual void Awake() { }

    protected virtual void Start() { }

    protected virtual void FixedUpdate()
    {
        if (!m_bAlive)
            return;

        if (!m_bAIUpdate)
            return;

        if (m_eState != GT_UnitState.Die)
            CheckAttackEndTimer();

        switch (m_eState)
        {
            case GT_UnitState.Create:
                {
                    SetState(GT_UnitState.Idle);
                }
                break;
            case GT_UnitState.Idle:
                {
                    CheckAssailable();
                }
                break;
            case GT_UnitState.Move:
                {
                    CheckMoveState();
                }
                break;
            case GT_UnitState.Die:
                break;
        }
    }

    public virtual void InitializeObject() { }

    public virtual void ResetObject()
    {
        SetState(GT_UnitState.Create);
        ApplyDirection(GT_Direction.Right);
        m_vAttackTarget = null;
        m_bProcessingAttack = false;
        m_fAttackDelayTimer = 0.0f;
        m_bAlive = true;
        m_bAIUpdate = true;

        m_fRangeRandomDistance = UnityEngine.Random.Range(0.0f, 0.15f);
    }

    protected virtual void InitializeSpineShape(string strName)
    {
        if (m_vSpineObject == null)
            return;

        switch (m_eUnitType)
        {
            case GT_UnitType.MainCharacter:
                {
                    m_vSpineObject.skeletonDataAsset = LoadSkeletonResource($"Spine/MainCharacter/{strName}/{strName}_SkeletonData");
                    m_vSpineObject.initialSkinName = "default";
                }
                break;
            case GT_UnitType.Monster:
                {
                    m_vSpineObject.skeletonDataAsset = LoadSkeletonResource($"Spine/Monster/{strName}/{strName}_SkeletonData");
                    m_vSpineObject.initialSkinName = "default";
                }
                break;
        }

        try
        {
            if (Time.timeScale != 0)
                m_vSpineObject.Initialize(true);
        }
        catch
        {

        }
    }

    protected virtual void SetState(GT_UnitState eState)
    {
        if (eState != GT_UnitState.Attack && m_eState == eState)
            return;

        m_eState = eState;
        switch (m_eState)
        {
            case GT_UnitState.Idle:
                {
                    SetAnimation(G_Constant.m_strMotion_Idle);
                }
                break;
            case GT_UnitState.Move:
                {
                    SetAnimation(G_Constant.m_strMotion_Move, true, m_fMoveSpeed);
                }
                break;
            case GT_UnitState.Attack:
                {
                    SetAnimation(G_Constant.m_strMotion_Attack, false, m_fAttackSpeed);
                }
                break;
            case GT_UnitState.Die:
                {
                    SetAnimation(G_Constant.m_strMotion_Die, false);
                    Die();
                }
                break;
        }
    }

    protected virtual void SetAnimation(string strMotionName, bool bLoop = true, float fSpeed = 1.0f, GT_SpineTrackIndex eTrackIndex = GT_SpineTrackIndex.None)
    {
        if (m_vSpineObject == null)
            return;

        if (m_vSpineObject.skeleton != null)
            m_vSpineObject.skeleton.SetToSetupPose();
        if (m_vSpineObject.state != null)
        {
            m_vSpineObject.state.ClearTracks();
            m_vSpineObject.state.SetAnimation((int)m_eUnitType, strMotionName, bLoop);

            float fAniTime = m_vSpineObject.state.GetCurrent((int)m_eUnitType).AnimationEnd;
            float fFIS = 1f / fAniTime;
            float fPlayTime = fAniTime * fFIS / fSpeed;
            m_vSpineObject.state.TimeScale = (fAniTime / fPlayTime);
        }
    }

    protected virtual void CheckAssailable()
    {
        if (!m_bAlive)
            return;

        if (IsAssailableRange())
        {
            Attack();
        }
        else
        {
            if (IsTargetExist() && m_fMoveSpeed > 0.0f)
            {
                SetState(GT_UnitState.Move);
            }
            else
            {
                SetState(GT_UnitState.Idle);
            }
        }
    }

    #region Move
    protected virtual void CheckMoveState()
    {
        if (!m_bAlive)
            return;

        if (!IsTargetExist())
        {
            SetState(GT_UnitState.Idle);
            return;
        }

        float fDes = m_fMoveSpeed * Time.fixedDeltaTime;
        SetDirection(m_vAttackTarget.transform.position.x);

        transform.position = Vector3.MoveTowards(transform.position, m_vAttackTarget.transform.position, fDes);

        if (Vector3.Distance(transform.position, m_vAttackTarget.transform.position) <= m_fAttackRange - m_fRangeRandomDistance)
            CheckAssailable();
    }
    #endregion

    #region Direction
    protected void ApplyDirection(GT_Direction eDirection)
    {
        m_eDirection = eDirection;

        if (m_vSpineObject != null && m_vSpineObject.skeleton != null)
        {
            switch (m_eDirection)
            {
                case GT_Direction.Left:
                    {
                        if (m_eUnitType == GT_UnitType.MainCharacter)
                            m_vAnchor.transform.localEulerAngles = m_vLeftRot;
                        else
                            m_vAnchor.transform.localEulerAngles = m_vRightRot;
                    }
                    break;
                case GT_Direction.Right:
                    {
                        if (m_eUnitType == GT_UnitType.MainCharacter)
                            m_vAnchor.transform.localEulerAngles = m_vRightRot;
                        else
                            m_vAnchor.transform.localEulerAngles = m_vLeftRot;
                    }
                    break;
            }
        }
    }

    protected void SetDirection(float fPosX)
    {
        float fPosition = fPosX - transform.position.x;
        if (fPosition > 0)
        {
            ApplyDirection(GT_Direction.Right);
        }
        else
        {
            ApplyDirection(GT_Direction.Left);
        }
    }
    #endregion

    #region Attack
    protected bool IsTargetExist()
    {
        if (m_vAttackTarget == null)
            return false;

        return m_vAttackTarget.a_bAlive;
    }

    protected virtual bool IsAssailableRange()
    {
        if (m_vAttackTarget == null)
            return false;

        if (m_fAttackRange == 0.0f)
            return false;

        return Vector3.Distance(transform.position, m_vAttackTarget.transform.position) <= m_fAttackRange;
    }

    protected virtual void Attack()
    {
        if (m_bProcessingAttack)
            return;

        StartCoroutine(StartAttack());
    }

    protected virtual IEnumerator StartAttack(float fApplyDelay = 0.6f, Action vReturnOnFX = null)
    {
        if (m_vSpineObject == null)
            yield break;

        if (!m_bAlive)
            yield break;

        if (!m_vAttackTarget.a_bAlive)
            yield break;

        m_bProcessingAttack = true;

        if (IsTargetExist())
        {
            SetDirection(m_vAttackTarget.transform.position.x);
        }

        SetState(GT_UnitState.Attack);

        // attackspeed 100 = 1s 로 맞추기 위한 값
        float fFIS = 1f / m_vSpineObject.state.GetCurrent((int)m_eUnitType).AnimationEnd;
        float fPlayTime = m_vSpineObject.state.GetCurrent((int)m_eUnitType).AnimationEnd * fFIS / m_fAttackSpeed;
        float fAttackTime = fPlayTime * fApplyDelay;
        yield return new WaitForSeconds(fAttackTime);

        if (!m_bAlive)
            yield break;

        vReturnOnFX?.Invoke();

        ApplyDamage();

        yield return new WaitForSeconds(fPlayTime - fAttackTime);

        SetState(GT_UnitState.Idle);
    }

    protected virtual void ApplyDamage()
    {

    }

    public bool IsAttackable()
    {
        bool bAttack = false;
        if (m_vAttackTarget != null && m_fAttackRange > 0.0f)
        {
            Vector3 vTargetPos = m_vAttackTarget.transform.position;
            bAttack = Vector3.Distance(transform.position, vTargetPos) < m_fAttackRange;
        }

        return bAttack;
    }

    public virtual void Hit()
    {
        if (!m_bAlive)
            return;

        SetState(GT_UnitState.Die);
    }

    public virtual void Die()
    {
        m_bAlive = false;
    }

    protected virtual void CheckAttackEndTimer()
    {
        if (m_bProcessingAttack && m_eState != GT_UnitState.Attack)
        {
            m_fAttackDelayTimer -= Time.fixedDeltaTime;
            if (m_fAttackDelayTimer <= 0.0f)
            {
                m_fAttackDelayTimer = m_fAttackCoolTime / m_fAttackSpeed;
                m_bProcessingAttack = false;
            }
        }
    }
    #endregion

    #region Variables
    protected GT_UnitState m_eState = GT_UnitState.Create;
    protected GT_Direction m_eDirection = GT_Direction.Right;

    // 이미 공격 중인지 ?
    protected bool m_bProcessingAttack = false;
    protected float m_fAttackDelayTimer = 0.0f;
    private float m_fRangeRandomDistance = 0.0f;

    public bool a_bAlive { get { return m_bAlive; } }
    protected bool m_bAlive = false;

    public bool a_bAIUpdate 
    { 
        get { return m_bAIUpdate; } 
        set
        {
            m_bAIUpdate = value;
            
        }
    }
    protected bool m_bAIUpdate = false;

    public G_UnitObject a_vAttackTarget { get { return m_vAttackTarget; } }
    protected G_UnitObject m_vAttackTarget = null;

    public GT_UnitType a_eUnitType { get { return m_eUnitType; } }
    #endregion

    [Header("Objects")]
    [SerializeField, Rename("Anchor")]
    protected GameObject m_vAnchor = null;

    [SerializeField, Rename("Spine Animation")]
    protected SkeletonAnimation m_vSpineObject = null;

    [SerializeField, Rename("Dust Anchor")]
    protected GameObject m_vDustAnchor = null;

    [Header("Base Parameter")]
    [SerializeField, Rename("Attack Range")]
    protected float m_fAttackRange = 1.0f;

    [SerializeField, Rename("Attack Timing")]
    protected float m_fAttackTiming = 0.6f;

    [SerializeField, Rename("Attack CoolTime")]
    protected float m_fAttackCoolTime = 0.3f;

    [SerializeField, Rename("Attack Speed")]
    protected float m_fAttackSpeed = 1.0f;

    [SerializeField, Rename("Move Speed")]
    protected float m_fMoveSpeed = 1.0f;

    [Header("Unit Data")]
    [SerializeField, Rename("Unit Type")]
    protected GT_UnitType m_eUnitType = GT_UnitType.None;

    [SerializeField, Rename("Unit Size")]
    protected float m_fUnitSize = 1.0f;

    #region Constant
    protected Vector3 m_vRightRot = new Vector3(0.0f, 0.0f, 0.0f);
    protected Vector3 m_vLeftRot = new Vector3(0.0f, 180.0f, 0.0f);
    #endregion
}
