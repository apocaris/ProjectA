using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_UnitMainCharacter : G_UnitObject
{
    public override void InitializeObject()
    {
        // 앞, 뒤 이펙트 모션 필요한 데이터 세팅
        if (m_dicEffSortOption != null)
        {
            List<GT_EffectSortType> vList = new List<GT_EffectSortType>
            {
                GT_EffectSortType.Front
            };

            if (!m_dicEffSortOption.ContainsKey(G_Constant.m_strMotion_Atk_1))
                m_dicEffSortOption.Add(G_Constant.m_strMotion_Atk_1, vList);
            if (!m_dicEffSortOption.ContainsKey(G_Constant.m_strMotion_Atk_2))
                m_dicEffSortOption.Add(G_Constant.m_strMotion_Atk_2, vList);
            if (!m_dicEffSortOption.ContainsKey(G_Constant.m_strMotion_Atk_3))
                m_dicEffSortOption.Add(G_Constant.m_strMotion_Atk_3, vList);
            if (!m_dicEffSortOption.ContainsKey(G_Constant.m_strMotion_Atk_4))
                m_dicEffSortOption.Add(G_Constant.m_strMotion_Atk_4, vList);
            if (!m_dicEffSortOption.ContainsKey(G_Constant.m_strMotion_Dash))
                m_dicEffSortOption.Add(G_Constant.m_strMotion_Dash, vList);
            if (!m_dicEffSortOption.ContainsKey(G_Constant.m_strMotion_Skill_1))
                m_dicEffSortOption.Add(G_Constant.m_strMotion_Skill_1, vList);
            if (!m_dicEffSortOption.ContainsKey(G_Constant.m_strMotion_Skill_2))
                m_dicEffSortOption.Add(G_Constant.m_strMotion_Skill_2, vList);
        }
    }

    public override void ResetObject()
    {
        base.ResetObject();

        // 외형
        InitializeSpineShape("MainCharacter");

#if ATTACK_BASE_TIMING

#else
        // 공격 이벤트 등록
        SetSpineEvent();
#endif
    }

    private void SetCharacterSkin()
    {

    }

    public void UpdateCamAnchor(Transform vTransform)
    {
        if (m_vMainCamAnchor == null)
            return;

        m_vMainCamAnchor.transform.localPosition = vTransform.localPosition;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!a_bAIUpdate)
            return;

        m_fTargetTimer += Time.fixedDeltaTime;
        if (m_fTargetTimer >= m_fIntervalTargeting)
        {
            m_fTargetTimer = 0.0f;
            G_FieldMGR.a_instance.GetAttackTarget(ref m_vAttackTarget, transform.position, m_eUnitType);
        }

        if (m_bCheckInitAttackMotion)
        {
            m_fCheckAttackMotionTimer += Time.fixedDeltaTime;
            if (m_fCheckAttackMotionTimer >= m_fCheckAttackMotionTime)
            {
                m_bCheckInitAttackMotion = false;
                m_fCheckAttackMotionTimer = 0.0f;
                m_iNextAttackMotion = 0;
            }
        }
    }

    #region State

#if ATTACK_BASE_TIMING
    protected override void SetState(GT_UnitState eState)
    {
        if (eState != GT_UnitState.Attack && m_eState == eState)
            return;

        m_eState = eState;
        switch (m_eState)
        {
            case GT_UnitState.Idle:
                {
                    SetAnimation(G_Constant.m_strMotion_Character_Idle);
                }
                break;
            case GT_UnitState.Move:
                {
                    SetAnimation(G_Constant.m_strMotion_Character_Run, true, m_fMoveSpeed);
                }
                break;
            case GT_UnitState.Attack:
                {
                    SetAnimation(G_Constant.m_strMotion_Character_Atk_1, false, m_fAttackSpeed);
                }
                break;
            case GT_UnitState.Die:
                {
                    SetAnimation(G_Constant.m_strMotion_Character_Die, false);
                    m_bAlive = false;
                }
                break;
        }
    }
#else
    protected void SetState(GT_UnitState eState, bool bForce = false)
    {
        if (!bForce)
        {
            // 죽는 모션 또는 초기화이면 일단 끊고 변경하려는 모션으로 변경
            if (eState == GT_UnitState.Die)
                m_bSkillMotionProcess = false;

            // 스킬 모션 중에는 다른 모션 들어오지 않게 막음
            if (m_bSkillMotionProcess)
                return;
        }

        if (bForce)
            SetForceState(eState);
        else
            base.SetState(eState);

        if (eState == GT_UnitState.Attack)
        {
            if (m_bCheckInitAttackMotion)
            {
                m_bCheckInitAttackMotion = false;
                m_fCheckAttackMotionTimer = 0.0f;
            }

            string strMotionName = string.Empty;
            switch (m_iNextAttackMotion)
            {
                case 0: strMotionName = "atk_1"; break;
                case 1: strMotionName = "atk_2"; break;
                case 2: strMotionName = "atk_3"; break;
                case 3: strMotionName = "atk_4"; break;
            }

            if (!string.IsNullOrEmpty(strMotionName))
            {
                SetAnimation(strMotionName, false, m_fAttackSpeed, GT_SpineTrackIndex.Character);
            }
        }

        if (m_vDustAnchor != null)
        {
            m_vDustAnchor.SetActive(eState == GT_UnitState.Move);
        }
    }
#endif

    private void SetForceState(GT_UnitState eState)
    {
        if (eState != GT_UnitState.Attack && m_eState == eState)
            return;

        m_eState = eState;

        bool bLoop = false;
        string strMotionName = string.Empty;
        switch (m_eState)
        {
            case GT_UnitState.Create:
                break;
            case GT_UnitState.Idle:
                {
                    bLoop = true;
                    strMotionName = G_Constant.m_strMotion_Idle;
                    m_bSkillMotionProcess = false;
                }
                break;
            case GT_UnitState.Die:
                {
                    bLoop = false;
                    strMotionName = G_Constant.m_strMotion_Die;
                    Die();
                }
                break;
        }

        if (!string.IsNullOrEmpty(strMotionName))
        {
            SetAnimation(strMotionName, bLoop, 1.0f, GT_SpineTrackIndex.Character);
        }
    }

    #endregion

    #region Attack Event

    private TrackEntry m_vTrackEntry;
    private IEnumerator m_enumeratorCheckAnimationEnd = null;

    private void SetSpineEvent()
    {
        if (m_vSpineObject != null && m_vSpineObject.state != null)
            m_vSpineObject.state.Event += HandleAttackEvent;
    }

    public void HandleAttackEvent(TrackEntry vTrackEntry, Spine.Event vEvent)
    {
        if (!a_bAIUpdate)
        {
            SetState(GT_UnitState.Idle);
            return;
        }

        if (vTrackEntry.TrackIndex != (int)GT_SpineTrackIndex.Character)
            return;

        if (m_eState != GT_UnitState.Attack && m_eState != GT_UnitState.Dash_Move && m_eState != GT_UnitState.Dash_Ready)
            return;

        if (!vTrackEntry.Animation.Name.Contains("attack") && !vTrackEntry.Animation.Name.Contains("dash"))
            return;

        if (m_bAttackProcess)
            return;

        switch (vEvent.Data.Name)
        {
            case G_Constant.m_strSpine_Event_Attack_1:
            case G_Constant.m_strSpine_Event_Attack_2:
            case G_Constant.m_strSpine_Event_Attack_3:
            case G_Constant.m_strSpine_Event_Attack_4:
                {
                    if (m_bCheckInitAttackMotion)
                    {
                        switch (m_iNextAttackMotion)
                        {
                            case 0:
                                if (vEvent.Data.Name != G_Constant.m_strSpine_Event_Attack_1)
                                    return;
                                break;
                            case 1:
                                if (vEvent.Data.Name != G_Constant.m_strSpine_Event_Attack_2)
                                    return;
                                break;
                            case 2:
                                if (vEvent.Data.Name != G_Constant.m_strSpine_Event_Attack_3)
                                    return;
                                break;
                            case 3:
                                if (vEvent.Data.Name != G_Constant.m_strSpine_Event_Attack_4)
                                    return;
                                break;
                        }
                    }

                    if (m_vTrackEntry == null)
                        m_vTrackEntry = vTrackEntry;

                    ApplyDamage();

                    if (m_enumeratorCheckAnimationEnd == null)
                    {
                        m_enumeratorCheckAnimationEnd = CheckAttackFinish(FinishAttackNormal);
                        StartCoroutine(m_enumeratorCheckAnimationEnd);
                    }
                }
                break;
        }
    }

    private IEnumerator CheckAttackFinish(Action vOnFinished = null)
    {
        if (m_vTrackEntry != null)
        {
            while (true)
            {
                if (m_vTrackEntry == null || m_vTrackEntry.IsComplete)
                    break;
                if (m_vTrackEntry.Animation == null || (m_vTrackEntry.Animation != null && m_vTrackEntry.Animation.Name.Contains("attack") == false))
                    break;

                yield return null;
            }

            if (vOnFinished != null)
                vOnFinished.Invoke();
        }
    }

    private void FinishAttackNormal()
    {
        bool bIsContinueAtk = false;
        // 공격이 끝났을 때 새로운 타겟을 잡아야 한다.
        if (IsTargetExist() && IsAttackable())
        {
            // 타겟이 계속해서 있는 경우 애니메이션 계속 재생
            SetDirection(m_vAttackTarget.transform.position.x);
            if (m_bAlive)
                bIsContinueAtk = true;
        }
        else
        {
            G_FieldMGR.a_instance.GetAttackTarget(ref m_vAttackTarget, transform.position, a_eUnitType);
            if (m_vAttackTarget != null && m_vAttackTarget.a_bAlive && IsAttackable())
            {
                SetDirection(m_vAttackTarget.transform.position.x);
                if (m_bAlive)
                    bIsContinueAtk = true;
            }
        }

        if (m_enumeratorCheckAnimationEnd != null)
        {
            StopCoroutine(m_enumeratorCheckAnimationEnd);
            m_enumeratorCheckAnimationEnd = null;
        }

        m_vTrackEntry = null;
        if (m_bAlive)
        {
            ++m_iNextAttackMotion;
            if (m_iNextAttackMotion > m_iMaxAttackMotion)
                m_iNextAttackMotion = 0;

            if (bIsContinueAtk)
                SetState(GT_UnitState.Attack);
            else
            {
                SetState(GT_UnitState.Idle);
                m_bCheckInitAttackMotion = true;
            }
        }

        m_bAttackProcess = false;
    }

    #endregion

    #region Calculate Attack

    protected override void ApplyDamage()
    {
        base.ApplyDamage();

        m_bAttackProcess = true;
        G_GameMGR.a_instance.a_vGameScene.CameraShake(0.07f, 0.2f);

        List<G_UnitObject> vTargetList = null;
        G_FieldMGR.a_instance.GetNearbyMonsterList(ref vTargetList, transform.position, m_eUnitType, m_fAttackRange);
        if (vTargetList != null)
        {
            for (int i = 0; i < vTargetList.Count; ++i)
            {
                if (vTargetList[i] != null)
                {
                    if (!vTargetList[i].a_bAlive)
                        continue;

                    switch (vTargetList[i].a_eUnitType)
                    {
                        case GT_UnitType.MainCharacter:
                            {

                            }
                            break;
                        case GT_UnitType.Monster:
                            {
                                ((G_UnitMonster)vTargetList[i]).Hit();
                            }
                            break;
                    }
                }
            }
        }
    }

    #endregion

    #region Animation

    protected override void SetAnimation(string strMotionName, bool bLoop = true, float fSpeed = 1, GT_SpineTrackIndex eTrackIndex = GT_SpineTrackIndex.None)
    {
        if (m_vSpineObject != null)
        {
            if (m_vSpineObject.skeleton != null)
                m_vSpineObject.skeleton.SetToSetupPose();
            if (m_vSpineObject.state != null)
            {
                m_vSpineObject.state.ClearTracks();
                m_vSpineObject.state.SetAnimation((int)eTrackIndex, strMotionName, bLoop);
                m_vSpineObject.state.TimeScale = fSpeed;
            }
        }

        // 관련 이펙트 재생
        if (m_dicEffSortOption.ContainsKey(strMotionName))
        {
            if (m_dicEffSortOption[strMotionName] != null)
            {
                for (int i = 0; i < m_dicEffSortOption[strMotionName].Count; ++i)
                {
                    switch (m_dicEffSortOption[strMotionName][i])
                    {
                        case GT_EffectSortType.Front:
                            {
                                if (m_vFrontVFXSpine != null)
                                {
                                    if (m_vFrontVFXSpine.skeleton != null)
                                        m_vFrontVFXSpine.skeleton.SetToSetupPose();
                                    if (m_vFrontVFXSpine.state != null)
                                    {
                                        m_vFrontVFXSpine.state.ClearTracks();
                                        m_vFrontVFXSpine.state.SetAnimation((int)eTrackIndex, strMotionName, bLoop);
                                        m_vFrontVFXSpine.state.TimeScale = fSpeed;
                                    }
                                }
                            }
                            break;
                        case GT_EffectSortType.Back:
                            {
                                if (m_vBackVFXSpine != null)
                                {
                                    if (m_vBackVFXSpine.skeleton != null)
                                        m_vBackVFXSpine.skeleton.SetToSetupPose();
                                    if (m_vBackVFXSpine.state != null)
                                    {
                                        m_vBackVFXSpine.state.ClearTracks();
                                        m_vBackVFXSpine.state.SetAnimation((int)eTrackIndex, strMotionName, bLoop);
                                        m_vBackVFXSpine.state.TimeScale = fSpeed;
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }
    }

    #endregion

    #region Variable
    private Skin m_vCharacterSkin = new Skin("Custom-Character");

    private float m_fTargetTimer = 0.0f;
    private float m_fCheckAttackMotionTimer = 0.0f;
    private bool m_bCheckInitAttackMotion = false;
    private int m_iNextAttackMotion = 0;
    private int m_iMaxAttackMotion = 3;
    private bool m_bAttackProcess = false;
    protected bool m_bSkillMotionProcess = false;

    private Dictionary<string, List<GT_EffectSortType>> m_dicEffSortOption = new Dictionary<string, List<GT_EffectSortType>>();
    #endregion

    #region Constant
    private float m_fIntervalTargeting = 0.2f;
    #endregion

    [Header("Base Parameter")]
    [SerializeField, Rename("Attack motion check time")]
    protected float m_fCheckAttackMotionTime = 2.0f;

    [Header("Objects")]
    [SerializeField, Rename("Main cam anchor")]
    protected GameObject m_vMainCamAnchor = null;

    [Header("VFX Spines")]
    [SerializeField, Rename("Front VFX spine")]
    protected SkeletonAnimation m_vFrontVFXSpine = null;

    [SerializeField, Rename("Back VFX spine")]
    protected SkeletonAnimation m_vBackVFXSpine = null;
}

