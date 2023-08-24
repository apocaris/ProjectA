using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class G_UnitMonster : G_UnitObject
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!m_bAlive)
            return;

        if (!m_bAggressive)
        {
            m_fAgroTimer += Time.fixedDeltaTime;
            if (m_fAgroTimer >= 0.2f)
            {
                m_fAgroTimer = 0.0f;
                if (IsTargetExist())
                {
                    float fDistance = Vector2.Distance(a_vAttackTarget.transform.position, transform.position);
                    if (fDistance < m_fAgroRange)
                    {
                        m_bAggressive = true;
                        CheckAssailable();
                    }
                    else
                    {
                        if (m_eState != GT_UnitState.Move && !m_bPatrolArrive)
                            SetState(GT_UnitState.Move);
                    }
                }

                //패트롤 목적지 재설정
                m_fPatrolTimer += Time.fixedDeltaTime;
                if (m_fPatrolTimer >= 1.5f)
                {
                    m_fPatrolTimer = 0.0f;
                    SetPatrolDes();
                    m_bPatrolArrive = false;
                }
            }
        }
    }

    public void ResetObject(Action<bool> vNotifyDie, Action vNotifyReturn)
    {
        base.ResetObject();

        m_vNotifyDie = vNotifyDie;
        m_vNotifyReturn = vNotifyReturn;

        if (m_vSpineObject != null)
        {
            //m_vSpineObject.transform.localScale = Vector3.one;
            m_vSpineObject.transform.localPosition = Vector3.zero;
        }

        if (m_vHitVFXAnchor != null)
        {
            m_vHitVFXAnchor.SetActive(false);
        }

        m_bAggressive = false;
        m_fAgroTimer = 0.0f;
        m_fPatrolTimer = 1.0f;
        m_vSpawnOriginPos = transform.position;
        m_vPatrolTargetPos = transform.position;
        m_bDieTween = false;

        // 임시
        m_iHitCount = 0;

        // 외형
        string strMonster;
        int iMonsterResource = UnityEngine.Random.Range(0, 6);
        switch (iMonsterResource)
        {
            case 1: strMonster = "demon_baphomet_1"; break;
            case 2: strMonster = "demon_wolf_4"; break;
            case 3: strMonster = "baphomet"; break;
            case 4: strMonster = "chicken"; break;
            default:
                {
                    strMonster = "slime";
                }
                break;
        }

        InitializeSpineShape(strMonster);
    }

    protected override void CheckMoveState()
    {
        //어그로 체크
        if (m_bAggressive)
        {
            base.CheckMoveState();
        }
        else
        {
            UpdatePatrolPos();
        }
    }

    private void SetPatrolDes()
    {
        float fAddValueX = UnityEngine.Random.Range(-m_fPatrolRange, m_fPatrolRange);
        float fAddValueY = UnityEngine.Random.Range(-m_fPatrolRange, m_fPatrolRange);
        Vector3 vAdditionalPos = new Vector3(fAddValueX, fAddValueY, 0.0f);
        m_vPatrolTargetPos = m_vSpawnOriginPos + vAdditionalPos;
        if (G_FieldMGR.a_instance != null && G_FieldMGR.a_instance.a_vFieldPoint != null)
        {
            if (G_FieldMGR.a_instance.a_vFieldPoint.a_vSpawnLimitMin != null && G_FieldMGR.a_instance.a_vFieldPoint.a_vSpawnLimitMax != null)
            {
                // 최대, 최소 위치 적용
                float fClampX = Mathf.Clamp(m_vPatrolTargetPos.x, G_FieldMGR.a_instance.a_vFieldPoint.a_vSpawnLimitMin.transform.position.x, G_FieldMGR.a_instance.a_vFieldPoint.a_vSpawnLimitMax.transform.position.x);
                float fClampY = Mathf.Clamp(m_vPatrolTargetPos.y, G_FieldMGR.a_instance.a_vFieldPoint.a_vSpawnLimitMin.transform.position.y, G_FieldMGR.a_instance.a_vFieldPoint.a_vSpawnLimitMax.transform.position.y);

                m_vPatrolTargetPos = new Vector3(fClampX, fClampY, 0);
            }
        }
    }

    private void UpdatePatrolPos()
    {
        if (!a_bAlive)
            return;

        if (!IsTargetExist())
        {
            SetState(GT_UnitState.Idle);
            return;
        }

        //패트롤 처리
        float step = m_fMoveSpeed * Time.fixedDeltaTime; // calculate distance to move
        SetDirection(m_vPatrolTargetPos.x);

        transform.position = Vector2.MoveTowards(transform.position, m_vPatrolTargetPos, step);
        if (Vector2.Distance(transform.position, m_vPatrolTargetPos) <= 0.01f)
        {
            //목적지 도착했으면 Idle
            if (!m_bPatrolArrive)
            {
                if (m_eState != GT_UnitState.Idle)
                {
                    SetState(GT_UnitState.Idle);
                    m_bPatrolArrive = true;
                }
            }
        }
    }

    public void UpdateTarget(G_UnitMainCharacter vTarget)
    {
        m_vAttackTarget = vTarget;
    }

    protected override void CheckAssailable()
    {
        if (m_bAggressive)
        {
            base.CheckAssailable();
        }
    }

    protected override void Attack()
    {
        if (!m_bAlive)
            return;

        base.Attack();
    }

    protected override void ApplyDamage()
    {
        base.ApplyDamage();
    }

    public void Hit(BigInteger iDamage, bool bNomralAttack = false)
    {
        if (bNomralAttack)
        {
            PlayHitAnimation();
        }

        base.Hit();

        ++m_iHitCount;
        if (m_iHitCount >= 3)
        {
            SetState(GT_UnitState.Die);
        }

        if (m_vHitTween != null)
        {
            if (!m_bDieTween)
            {
                if (a_bAlive)
                {
                    m_vHitTween.tweenGroup = 10;
                }
                else
                {
                    m_bDieTween = true;
                    //평타를 통한 사망
                    m_vHitTween.tweenGroup = 20;
                }

                m_vHitTween.Play();
            }
        }
    }

    private IEnumerator WaitForDie()
    {
        yield return new WaitForSeconds(m_vSpineObject.state.GetCurrent((int)GetSpineTrackIndex()).AnimationEnd);

        m_vNotifyReturn?.Invoke();
    }

    public override void Die()
    {
        if (!m_bAlive)
            return;

        base.Die();

        m_vNotifyDie?.Invoke(false);

        StartCoroutine(WaitForDie());
    }

    #region Hit VFX
    private void PlayHitAnimation()
    {
        if (m_vHitVFXAnchor != null)
            m_vHitVFXAnchor.SetActive(true);

        //if (m_vHitVFX_Spine != null)
        //{
        //    if (m_vHitVFX_Spine.skeleton != null)
        //        m_vHitVFX_Spine.skeleton.SetToSetupPose();
        //    if (m_vHitVFX_Spine.state != null)
        //    {
        //        m_vHitVFX_Spine.state.ClearTracks();
        //        m_vHitVFX_Spine.state.SetAnimation(0, G_Constant.m_strMotion_Hit, false);
        //        m_vHitVFX_Spine.state.TimeScale = 1;
        //    }
        //}

        if (m_vHitVFX != null)
        {
            m_vHitVFX.PlayParticle();
        }
    }

    #endregion

    private Action<bool> m_vNotifyDie = null;
    private Action m_vNotifyReturn = null;

    #region Variable
    private float m_fAgroTimer = 0.0f;
    private float m_fPatrolTimer = 0.0f;
    private Vector3 m_vSpawnOriginPos;
    private Vector3 m_vPatrolTargetPos;
    private bool m_bPatrolArrive = false;
    private bool m_bDieTween = false;

    public bool a_bAggressive { get { return m_bAggressive; } }
    private bool m_bAggressive = false;

    // 임시
    private int m_iHitCount = 0;
    #endregion

    #region Constant
    private const float m_fPatrolRange = 1.0f;
    #endregion

    [SerializeField, Rename("AgroRange")]
    protected float m_fAgroRange = 1.0f;

    [SerializeField, Rename("Hit Tween")]
    protected UIPlayTween m_vHitTween = null;

    [Header("Hit VFX")]
    [SerializeField, Rename("Hit VFX Anchor")]
    protected GameObject m_vHitVFXAnchor = null;

    [SerializeField, Rename("Hit VFX Spine")]
    protected SkeletonAnimation m_vHitVFX_Spine = null;

    [SerializeField, Rename("Hit VFX")]
    protected G_PlayParticle m_vHitVFX = null;
}
