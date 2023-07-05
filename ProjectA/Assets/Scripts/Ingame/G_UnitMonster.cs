using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;

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
                    float fDistance = Vector3.Distance(a_vAttackTarget.transform.position, transform.position);
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
        m_vAgroTargetPos = transform.position;
        m_bDieTween = false;

        // 외형
        InitializeSpineShape("MOB_MONKEY_01");
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
        m_vAgroTargetPos = m_vSpawnOriginPos + vAdditionalPos;
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
        SetDirection(m_vAgroTargetPos.x);

        transform.position = Vector3.MoveTowards(transform.position, m_vAgroTargetPos, step);
        if (Vector3.Distance(transform.position, m_vAgroTargetPos) <= 0.01f)
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

        if (m_vHitVFX != null)
        {
            if (m_vHitVFX.skeleton != null)
                m_vHitVFX.skeleton.SetToSetupPose();
            if (m_vHitVFX.state != null)
            {
                m_vHitVFX.state.ClearTracks();
                m_vHitVFX.state.SetAnimation(0, G_Constant.m_strMotion_Hit, false);
                m_vHitVFX.state.TimeScale = 1;
            }
        }
    }

    #endregion

    private Action<bool> m_vNotifyDie = null;
    private Action m_vNotifyReturn = null;

    #region Variable
    private float m_fAgroTimer = 0.0f;
    private float m_fPatrolTimer = 0.0f;
    private Vector3 m_vSpawnOriginPos;
    private Vector3 m_vAgroTargetPos;
    private bool m_bPatrolArrive = false;
    private bool m_bDieTween = false;

    public bool a_bAggressive { get { return m_bAggressive; } }
    private bool m_bAggressive = false;
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
    protected SkeletonAnimation m_vHitVFX = null;
}
