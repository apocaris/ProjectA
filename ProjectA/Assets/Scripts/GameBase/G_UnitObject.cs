using Spine;
using Spine.Unity;
using System;
using System.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

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

        if (m_vAnchor != null)
            m_vAnchor.transform.localScale = Vector3.one * m_fUnitSize;
    }

    protected virtual void InitializeSpineShape(string strClass, string strName)
    {
        if (m_vSpineObject == null)
            return;

        switch (m_eUnitType)
        {
            case GT_Unit.MainCharacter:
                {
                    m_vSpineObject.skeletonDataAsset = LoadSkeletonResource($"Spine/MainCharacter/{strClass}/{strName}/{strName}_SkeletonData");
                    m_vSpineObject.initialSkinName = "default";
                }
                break;
            case GT_Unit.Monster:
                {
                    m_vSpineObject.skeletonDataAsset = LoadSkeletonResource($"Spine/Monster/{strName}/{strName}_SkeletonData");
                    m_vSpineObject.initialSkinName = "default";
                }
                break;
        }

        try
        {
            if (Time.timeScale != 0)
            {
                m_vSpineObject.timeScale = 1;
                m_vSpineObject.Initialize(true);
            }
        }
        catch
        {
            
        }
    }

    private TrackEntry m_vTrackEntry;
    private IEnumerator m_enumeratorCheckAnimationEnd = null;

    protected virtual void SetState(GT_UnitState eState)
    {
        if (eState != GT_UnitState.Attack && m_eState == eState)
            return;

        m_eState = eState;
        switch (m_eState)
        {
            case GT_UnitState.Idle:
                {
                    SetAnimation(G_Constant.m_strMotion_Idle, true, 1.0f, GetSpineTrackIndex());
                }
                break;
            case GT_UnitState.Move:
                {
                    float fAniSpeed = m_fMoveSpeed;
                    if (m_eUnitType == GT_Unit.MainCharacter)
                    {
                        fAniSpeed = m_fMoveSpeed / 2f;
                        if (fAniSpeed > 1.5f)
                            fAniSpeed = 1.5f;
                    }

                    SetAnimation(G_Constant.m_strMotion_Move, true, fAniSpeed, GetSpineTrackIndex());
                }
                break;
            case GT_UnitState.Attack:
                {
                    if (m_eUnitType != GT_Unit.MainCharacter)
                        SetAnimation(G_Constant.m_strMotion_Attack, false, m_fAttackSpeed, GetSpineTrackIndex());
                }
                break;
            case GT_UnitState.Hit:
                {
                    if (m_eUnitType != GT_Unit.MainCharacter)
                        SetAnimation(G_Constant.m_strMotion_Hit, false, 3.0f, GetSpineTrackIndex());
                }
                break;
            case GT_UnitState.Die:
                {
                    FinishAnimation();
                    SetAnimation(G_Constant.m_strMotion_Die, false, 1.0f, GetSpineTrackIndex());
                    Die();
                }
                break;
        }
    }

    protected virtual void SetAnimation(string strMotionName, bool bLoop = true, float fAniSpeed = 1.0f, GT_SpineTrackIndex eTrackIndex = GT_SpineTrackIndex.None)
    {
        if (m_vSpineObject == null)
            return;

        if (m_vSpineObject.skeleton != null)
            m_vSpineObject.skeleton.SetToSetupPose();
        if (m_vSpineObject.state != null)
        {
            m_vSpineObject.state.ClearTracks();
            m_vTrackEntry = m_vSpineObject.state.SetAnimation((int)eTrackIndex, strMotionName, bLoop);
            if (strMotionName == G_Constant.m_strMotion_Hit)
            {
                if (m_enumeratorCheckAnimationEnd == null)
                {
                    m_enumeratorCheckAnimationEnd = CheckAnimationFinish(FinishAnimation);
                    StartCoroutine(m_enumeratorCheckAnimationEnd);
                }
            }

            float fAniTime = m_vSpineObject.state.GetCurrent((int)eTrackIndex).AnimationEnd;
            float fFIS = 1f / fAniTime;
            float fPlayTime = fAniTime * fFIS / fAniSpeed;
            m_vSpineObject.state.TimeScale = (fAniTime / fPlayTime);
        }
    }

    private IEnumerator CheckAnimationFinish(Action vOnFinished = null)
    {
        if (m_vTrackEntry != null)
        {
            while (true)
            {
                if (m_vTrackEntry == null || m_vTrackEntry.IsComplete)
                    break;
                if (m_vTrackEntry.Animation == null || (m_vTrackEntry.Animation != null && m_vTrackEntry.Animation.Name.Contains("hit") == false))
                    break;

                yield return null;
            }

            if (vOnFinished != null)
                vOnFinished.Invoke();
        }
    }

    protected void FinishAnimation()
    {
        if (m_enumeratorCheckAnimationEnd != null)
        {
            StopCoroutine(m_enumeratorCheckAnimationEnd);
            m_enumeratorCheckAnimationEnd = null;
        }

        SetState(GT_UnitState.Idle);
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

        transform.position = Vector2.MoveTowards(transform.position, m_vAttackTarget.transform.position, fDes);

        if (Vector2.Distance(transform.position, m_vAttackTarget.transform.position) <= m_fAttackRange - m_fRangeRandomDistance)
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
                        //if (m_eUnitType == GT_Unit.MainCharacter)
                        //    m_vAnchor.transform.localEulerAngles = m_vLeftRot;
                        //else
                        //    m_vAnchor.transform.localEulerAngles = m_vRightRot;

                        m_vAnchor.transform.localEulerAngles = m_vLeftRot;
                    }
                    break;
                case GT_Direction.Right:
                    {
                        //if (m_eUnitType == GT_Unit.MainCharacter)
                        //    m_vAnchor.transform.localEulerAngles = m_vRightRot;
                        //else
                        //    m_vAnchor.transform.localEulerAngles = m_vLeftRot;

                        m_vAnchor.transform.localEulerAngles = m_vRightRot;
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
    public void SetAttackTarget(ref G_UnitObject vTarget)
    {
        m_vAttackTarget = vTarget;
    }

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

        return Vector2.Distance(transform.position, m_vAttackTarget.transform.position) <= m_fAttackRange;
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
            bAttack = Vector2.Distance(transform.position, vTargetPos) < m_fAttackRange;
        }

        return bAttack;
    }

    public virtual void Hit()
    {
        if (!m_bAlive)
            return;
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

    protected GT_SpineTrackIndex GetSpineTrackIndex()
    {
        GT_SpineTrackIndex eTrackIndex = GT_SpineTrackIndex.None;
        switch (m_eUnitType)
        {
            case GT_Unit.MainCharacter:
                {
                    eTrackIndex = GT_SpineTrackIndex.Character;
                }
                break;
            case GT_Unit.Monster:
                {
                    eTrackIndex = GT_SpineTrackIndex.Monster;
                }
                break;
        }

        return eTrackIndex;
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

    public GT_Unit a_eUnitType { get { return m_eUnitType; } }
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
    protected GT_Unit m_eUnitType = GT_Unit.None;

    [SerializeField, Rename("Unit Size")]
    protected float m_fUnitSize = 1.0f;

    #region Constant
    protected Vector3 m_vRightRot = new Vector3(0.0f, 0.0f, 0.0f);
    protected Vector3 m_vLeftRot = new Vector3(0.0f, 180.0f, 0.0f);
    #endregion
}
