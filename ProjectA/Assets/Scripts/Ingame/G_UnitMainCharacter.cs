using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Random = UnityEngine.Random;

public class G_UnitMainCharacter : G_UnitObject
{
    public override void InitializeObject()
    {
        m_bRep = false;
    }

    public void ResetObject(GT_UnitClass eClass)
    {
        base.ResetObject();

        //스킨 관련 정리
        //if (!IsNull(m_vSkinRuntimeMaterial))
        //    Destroy(m_vSkinRuntimeMaterial);
        //if (!IsNull(m_vSkinRuntimeAtlas))
        //    Destroy(m_vSkinRuntimeAtlas);
        //AtlasUtilities.ClearCache();

        m_eClass = eClass;

        string strClass = string.Empty;
        string strResource = string.Empty;
        switch (m_eClass)
        {
            case GT_UnitClass.Axe:
                {
                    strClass = G_Constant.m_strClassAxe;
                    strResource = "axe_1";
                    m_fAttackSpeed = 1.1f;
                    m_fAttackRange = 1.6f;
                    m_fMoveSpeed = 1.3f;
                }
                break;
            case GT_UnitClass.Spear:
                {
                    strClass = G_Constant.m_strClassSpear;
                    strResource = "spear_1";
                    m_fAttackSpeed = 1.3f;
                    m_fAttackRange = 2.1f;
                    m_fMoveSpeed = 2f;
                }
                break;
            case GT_UnitClass.TwoSword:
                {
                    strClass = G_Constant.m_strClassTwoSword;
                    strResource = "two_sword_1";
                    m_fAttackSpeed = 2.0f;
                    m_fAttackRange = 1.0f;
                    m_fMoveSpeed = 3f;
                }
                break;
        }

        // 외형
        InitializeSpineShape(strClass, strResource);

#if ATTACK_BASE_TIMING

#else
        // 공격 이벤트 등록
        SetSpineEvent();
#endif
    }

    private void SetCharacterSkin()
    {
        UpdateSkin("costume_3", "weapon_2");
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


        /*
        if (m_bRep)
        {
            m_bSetTargetEnemy = false;
            if (m_eState != GT_UnitState.Die && m_eState != GT_UnitState.Attack)
            {
                if (m_eState != GT_UnitState.Dash_Move && m_eState != GT_UnitState.Dash_Ready)
                {
                    m_bSetTargetEnemy = true;
                }
            }

            if (m_bSetTargetEnemy)
            {
                m_fTargetTimer += Time.fixedDeltaTime;
                if (m_fTargetTimer >= m_fIntervalTargeting)
                {
                    m_fTargetTimer = 0.0f;
                    G_FieldMGR.a_instance.GetAttackTarget(ref m_vAttackTarget, transform.position, m_eUnitType);
                    // Once the target is set, notify other characters of the target as well.
                    G_FieldMGR.a_instance.NotifyAttackTarget(this);
                    //if (m_eState != GT_UnitState.Dash_Ready && m_eState != GT_UnitState.Dash_Move)
                    //{
                    //    CheckDashDistance();
                    //}
                }
            }
        }
        */

        m_bSetTargetEnemy = false;
        if (m_eState != GT_UnitState.Die && m_eState != GT_UnitState.Attack)
        {
            if (m_eState != GT_UnitState.Dash_Move && m_eState != GT_UnitState.Dash_Ready)
            {
                m_bSetTargetEnemy = true;
            }
        }

        if (m_bSetTargetEnemy)
        {
            m_fTargetTimer += Time.fixedDeltaTime;
            if (m_fTargetTimer >= m_fIntervalTargeting)
            {
                m_fTargetTimer = 0.0f;
                G_FieldMGR.a_instance.GetAttackTarget(ref m_vAttackTarget, transform.position, m_eUnitType);
                //if (m_eState != GT_UnitState.Dash_Ready && m_eState != GT_UnitState.Dash_Move)
                //{
                //    CheckDashDistance();
                //}
            }
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

        #region Dash 처리
        switch (m_eState)
        {
            case GT_UnitState.Dash_Move:
                {
                    UpdateDashPos();
                }
                break;
        }
        #endregion
    }

    #region Skin

    private void UpdateSkin(string strCloth, string strGauntlet)
    {
        if (m_vSpineObject != null)
        {
            Skeleton vSkeleton = m_vSpineObject.Skeleton;
            SkeletonData vSkeletonData = vSkeleton.Data;

            // FindSkin return값은 start등에서 1회 호출 후 재활용 가능함
            m_vCharacterSkin = new Skin("character-base");
            m_vCharacterSkin.AddSkin(vSkeletonData.FindSkin(strCloth));
            m_vCharacterSkin.AddSkin(vSkeletonData.FindSkin(strGauntlet));

            vSkeleton.SetSkin(m_vCharacterSkin);
            vSkeleton.SetSlotsToSetupPose();

            OptimizeSkin();
        }
    }

    private void OptimizeSkin()
    {
        if (m_vSpineObject != null)
        {
            if (m_vSpineObject.Skeleton != null)
            {
                // Create a repacked skin.
                Skin vPreviousSkin = m_vSpineObject.Skeleton.Skin;
                // Note: GetRepackedSkin()에 의해 반환된 재질 및 텍스처는 'new Texture2D()'처럼 동작하므로 제거해야 합니다.
                if (!IsNull(m_vSkinRuntimeMaterial))
                    Destroy(m_vSkinRuntimeMaterial);
                if (!IsNull(m_vSkinRuntimeAtlas))
                    Destroy(m_vSkinRuntimeAtlas);
                if (m_vSpineObject.SkeletonDataAsset != null)
                {
                    if (m_vSpineObject.SkeletonDataAsset.atlasAssets != null && m_vSpineObject.SkeletonDataAsset.atlasAssets.Length > 0)
                    {
                        if (vPreviousSkin != null)
                        {
                            Skin vRepackedSkin = vPreviousSkin.GetRepackedSkin("Repacked skin", m_vSpineObject.SkeletonDataAsset.atlasAssets[0].PrimaryMaterial, out m_vSkinRuntimeMaterial, out m_vSkinRuntimeAtlas);
                            vPreviousSkin.Clear();

                            // Use the repacked skin.
                            m_vSpineObject.Skeleton.Skin = vRepackedSkin;
                            m_vSpineObject.Skeleton.SetSlotsToSetupPose();
                            m_vSpineObject.AnimationState.Apply(m_vSpineObject.Skeleton);

                            //리소스 정리
                            AtlasUtilities.ClearCache();
                            Resources.UnloadUnusedAssets();
                        }
                    }
                }
            }
        }
    }

    #endregion

    #region State

    public void SetRepresentative()
    {
        m_bRep = true;
    }

    public void SetDashDesPos(ref Vector3 vTargetPos)
    {
        m_vDesDashPos = vTargetPos;
        SetState(GT_UnitState.Dash_Ready);
    }

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
        {
            switch (eState)
            {
                case GT_UnitState.Dash_Ready:
                    {
                        if (m_eState != GT_UnitState.Dash_Ready)
                        {
                            m_eState = GT_UnitState.Dash_Ready;
                            StartCoroutine(PlayDash());
                        }
                    }
                    break;
                case GT_UnitState.Dash_Move:
                    {
                        if (m_eState != GT_UnitState.Dash_Move)
                        {
                            m_eState = GT_UnitState.Dash_Move;
                            m_fOrgMoveSpeed = m_fMoveSpeed;
                            m_fMoveSpeed = m_fDashMoveSpeed;
                        }
                    }
                    break;
                default:
                    {
                        base.SetState(eState);
                    }
                    break;
            }
        }

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
                case 0: strMotionName = G_Constant.m_strMotion_Atk_1; break;
                case 1: strMotionName = G_Constant.m_strMotion_Atk_2; break;
                case 2: strMotionName = G_Constant.m_strMotion_Atk_3; break;
                case 3: strMotionName = G_Constant.m_strMotion_Atk_4; break;
            }

            if (!string.IsNullOrEmpty(strMotionName))
            {
                SetAnimation(strMotionName, false, m_fAttackSpeed, GT_SpineTrackIndex.Character);
            }
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

    #region Attack

    protected override void Attack()
    {
        if (m_bProcessingAttack)
            return;

        StartCoroutine(StartAttack());
    }

    protected override IEnumerator StartAttack(float fApplyDelay = 0.6F, Action vReturnOnFX = null)
    {
        if (!m_bAlive)
            yield break;

        if (!m_vAttackTarget.a_bAlive)
            yield break;

        if (m_eState == GT_UnitState.Attack)
            yield break;

        if (IsTargetExist())
            SetDirection(m_vAttackTarget.transform.position.x);
        SetState(GT_UnitState.Attack);

        m_bProcessingAttack = true;
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

        if (!vTrackEntry.Animation.Name.Contains("atk") && !vTrackEntry.Animation.Name.Contains("dash"))
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

                    ApplyAttackVFX(vEvent.Data.Name);

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
                if (m_vTrackEntry.Animation == null || (m_vTrackEntry.Animation != null && m_vTrackEntry.Animation.Name.Contains("atk") == false))
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
            G_FieldMGR.a_instance.GetAttackTarget(ref m_vAttackTarget, transform.position, m_eUnitType);
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
        G_GameMGR.a_instance.a_vGameScene.CameraShake(0.04f, 0.12f);

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

                    m_vDamageValues.Clear();
                    m_vDamageTypes.Clear();

                    GT_Damage eType = GT_Damage.Normal;
                    BigInteger iDamage;

                    // For test
                    {
                        iDamage = (BigInteger)Random.Range(1, 1000000);
                        if (iDamage >= 790000 && iDamage < 794000)
                            eType = GT_Damage.Critical;
                        else if (iDamage >= 794001 && iDamage < 900000)
                            eType = GT_Damage.SuperCritical;
                        else if (iDamage >= 900001)
                            eType = GT_Damage.HyperCritical;
                    }

                    m_vDamageTypes.Add(eType);
                    m_vDamageValues.Add(iDamage);

                    G_GameMGR.a_instance.a_vGameScene.ShowDamage(ref m_vDamageTypes, ref m_vDamageValues, vTargetList[i]);

                    switch (vTargetList[i].a_eUnitType)
                    {
                        case GT_Unit.MainCharacter:
                            {

                            }
                            break;
                        case GT_Unit.Monster:
                            {
                                ((G_UnitMonster)vTargetList[i]).Hit(1, true);
                            }
                            break;
                    }
                }
            }
        }
    }

    private void ApplyAttackVFX(string strEventName)
    {
        int iVFXIndex = G_Utils.GetNumbersInString(strEventName);
        if (iVFXIndex < 0)
            return;

        iVFXIndex -= 1;
        G_PlayParticle vVFX = null;
        switch (m_eClass)
        {
            case GT_UnitClass.Axe:
                {
                    if (m_vAxeVFXs != null && m_vAxeVFXs.Count > 0)
                    {
                        if (iVFXIndex < m_vAxeVFXs.Count)
                            vVFX = m_vAxeVFXs[iVFXIndex];
                    }
                }
                break;
            case GT_UnitClass.Spear:
                {
                    if (m_vSpearVFXs != null && m_vSpearVFXs.Count > 0)
                    {
                        if (iVFXIndex < m_vSpearVFXs.Count)
                            vVFX = m_vSpearVFXs[iVFXIndex];
                    }
                }
                break;
            case GT_UnitClass.TwoSword:
                {
                    if (m_vTwoswordVFXs != null && m_vTwoswordVFXs.Count > 0)
                    {
                        if (iVFXIndex < m_vTwoswordVFXs.Count)
                            vVFX = m_vTwoswordVFXs[iVFXIndex];
                    }
                }
                break;
        }

        if (vVFX != null)
            vVFX.PlayParticle();
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
        /*
        if (m_dicEffSortOption.ContainsKey(strMotionName))
        {
            if (m_dicEffSortOption[strMotionName] != null)
            {
                for (int i = 0; i < m_dicEffSortOption[strMotionName].Count; ++i)
                {
                    switch (m_dicEffSortOption[strMotionName][i])
                    {
                        case GT_EffectSort.Front:
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
                        case GT_EffectSort.Back:
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
        */
    }

    #endregion

    #region Dash

    // 목표 위치 설정
    private void CheckDashDistance()
    {
        m_vDesDashPos = Vector3.zero;
        if (m_eState == GT_UnitState.Idle || m_eState == GT_UnitState.Move)
        {
            // 타켓을 잡은 몬스터와 일정 거리 이상이면 대시 바로 발동
            if (!IsTargetExist())
                return;

            float fDis = Vector2.Distance(transform.position, m_vAttackTarget.transform.position);
            if (fDis <= m_fAttackRange + m_fDashApplyDistance)
                return;

            // 타켓의 위치를 기준으로 공격 범위를 뺀 위치가 대시 도착 위치
            // 나보다 왼쪽이냐
            if (transform.position.x > m_vAttackTarget.transform.position.x)
            {
                m_vDesDashPos = new Vector3(m_vAttackTarget.transform.position.x + (m_fAttackRange + 1.0f), m_vAttackTarget.transform.position.y, 0);
            }
            // 나보다 오른쪽이냐
            else
            {
                m_vDesDashPos = new Vector3(m_vAttackTarget.transform.position.x - (m_fAttackRange + 1.0f), m_vAttackTarget.transform.position.y, 0);
            }

            SetState(GT_UnitState.Dash_Ready);
            // Notifies dash usage information to other characters
            G_FieldMGR.a_instance.NotifyDashCharacter(this);
        }
    }

    private IEnumerator PlayDash()
    {
        if (m_vSpineObject == null)
        {
            SetState(GT_UnitState.Idle);
            yield break;
        }

        SetAnimation(G_Constant.m_strMotion_Idle);
        yield return new WaitForSeconds(0.05f);

        if (m_fOrgTimescale == 0.0f)
            m_fOrgTimescale = m_vSpineObject.timeScale;

        if (m_fDashMotionSpeed <= 1.0f)
            m_fDashMotionSpeed = 1.0f;

        SetAnimation(G_Constant.m_strMotion_Move, false, m_fDashMotionSpeed, GT_SpineTrackIndex.Character);
        //yield return new WaitForSeconds((13f / 30f) / m_fDashMotionSpeed);

        if (m_vAttackTarget != null)
            SetDirection(m_vAttackTarget.transform.position.x);
        SetState(GT_UnitState.Dash_Move);
    }

    private void UpdateDashPos()
    {
        if (!m_bAlive)
            return;

        if (!IsTargetExist())
        {
            SetState(GT_UnitState.Idle);
            return;
        }

        float fStep = m_fMoveSpeed * Time.fixedDeltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, m_vDesDashPos, fStep);
        if (transform.position == m_vDesDashPos)
            StopDash();
    }

    private void StopDash()
    {
        m_vDesDashPos = Vector3.zero;
        if (m_fOrgMoveSpeed > 0.0f)
            m_fMoveSpeed = m_fOrgMoveSpeed;
        if (m_fOrgTimescale > 0.0f)
        {
            if (m_vSpineObject != null)
                m_vSpineObject.timeScale = m_fOrgTimescale;
        }
        SetState(GT_UnitState.Move);
    }

    #endregion

    #region Variable
    private Skin m_vCharacterSkin = null;
    private Material m_vSkinRuntimeMaterial = null;
    private Texture2D m_vSkinRuntimeAtlas = null;
    private GT_UnitClass m_eClass = GT_UnitClass.None;

    private float m_fTargetTimer = 0.0f;
    private float m_fCheckAttackMotionTimer = 0.0f;
    private bool m_bCheckInitAttackMotion = false;
    private int m_iNextAttackMotion = 0;
    private int m_iMaxAttackMotion = 2;
    private bool m_bAttackProcess = false;
    protected bool m_bSkillMotionProcess = false;
    private float m_fOrgTimescale = 0.0f;
    private float m_fOrgMoveSpeed = 0.0f;
    private bool m_bSetTargetEnemy = false;

    public Vector3 a_vDesDashPos { get { return m_vDesDashPos; } }
    private Vector3 m_vDesDashPos = Vector3.zero;

    public bool a_bRep { get { return m_bRep; } }
    private bool m_bRep = false;

    private List<BigInteger> m_vDamageValues = new List<BigInteger>();
    private List<GT_Damage> m_vDamageTypes = new List<GT_Damage>();

    #endregion

    #region Constant
    private float m_fIntervalTargeting = 0.2f;
    #endregion

    [Header("Base Parameter")]
    [SerializeField, Rename("Attack motion check time")]
    protected float m_fCheckAttackMotionTime = 2.0f;

    [SerializeField, Rename("Dash move speed")]
    protected float m_fDashMoveSpeed = 1.0f;

    [SerializeField, Rename("Dash play time scale")]
    protected float m_fDashMotionSpeed = 1.5f;

    [SerializeField, Rename("Dash apply distance")]
    protected float m_fDashApplyDistance = 1.5f;

    [Header("Objects")]
    [SerializeField, Rename("Main cam anchor")]
    protected GameObject m_vMainCamAnchor = null;

    [Header("Attack VFXs")]
    [SerializeField, Rename("VFX anchor")]
    protected GameObject m_vVFXAnchor = null;

    [SerializeField]
    protected List<G_PlayParticle> m_vAxeVFXs = null;

    [SerializeField]
    protected List<G_PlayParticle> m_vSpearVFXs = null;

    [SerializeField]
    protected List<G_PlayParticle> m_vTwoswordVFXs = null;
}

