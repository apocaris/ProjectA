using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_IngameModeBase : G_Object
{
    public virtual void StartMode() { }
    public virtual void UpdateMode() { }

    public virtual void ResetIngameMode()
    {
        m_vFieldPoint = null;
        m_iCurrentSpawnCount = 0;
        m_fSpawnMonsterTimer = 0.0f;

        int iRand = UnityEngine.Random.Range(0, 2);
        m_eSpawnHorizontalSide = (GT_Direction)iRand;
    }

    public virtual IEnumerator ChangeInagmeMode(string strName)
    {
        yield return StartCoroutine(G_FieldMGR.a_instance.ChangeMode(strName));
    }

    public virtual void UpdateModeInfo(ref G_FieldPoint vFieldPoint)
    {
        if (vFieldPoint == null)
            return;

        m_vFieldPoint = vFieldPoint;
        if (m_vFieldPoint != null && G_FieldMGR.a_instance.a_dicCharacters != null)
        {
            if (m_vFieldPoint.a_vCharacterPoints.Count == G_FieldMGR.a_instance.a_dicCharacters.Count)
            {
                foreach (KeyValuePair<GT_UnitClass, G_UnitMainCharacter> vData in G_FieldMGR.a_instance.a_dicCharacters)
                {
                    if (vData.Value == null)
                        continue;

                    int iTargetPosIdx = (int)vData.Key - 1;
                    if (iTargetPosIdx >= 0 && iTargetPosIdx < m_vFieldPoint.a_vCharacterPoints.Count)
                    {
                        Transform vTarget = m_vFieldPoint.a_vCharacterPoints[iTargetPosIdx];
                        if (vTarget != null)
                        {
                            vData.Value.transform.position = vTarget.position;
                        }
                    }
                }
            }
        }
    }

    protected GameObject SpawnMonster(Vector3 vPosition, Action<bool> vOnDie)
    {
        GameObject vObject = G_GameMGR.a_instance.GetPoolObject(GT_PoolType.Monster, G_FieldMGR.a_instance.a_vMonsterPoolObject.transform);
        if (vObject != null)
        {
            G_UnitMonster vMonster = vObject.GetComponent<G_UnitMonster>();
            if (vMonster != null)
            {
                vMonster.gameObject.SetActive(true);
                vMonster.transform.position = vPosition;

                vMonster.ResetObject((bool bForce) =>
                {
                    if (vMonster != null)
                    {
                        G_FieldMGR.a_instance.a_vMonsterList.Remove(vMonster);
                        --m_iCurrentSpawnCount;
                    }

                    vOnDie?.Invoke(bForce);
                }, () =>
                {
                    G_GameMGR.a_instance.ReturnPoolObject(GT_PoolType.Monster, vMonster.gameObject);
                });

                //2021.06.23 스폰시 타겟 설정. 어그로는 패트롤 후 일정 범위 안에 캐릭터 들어오면 인식
                G_UnitObject vTargetUnit = null;
                G_FieldMGR.a_instance.GetAttackTarget(ref vTargetUnit, vMonster.transform.position, vMonster.a_eUnitType);
                if (vTargetUnit != null)
                {
                    if (vTargetUnit.a_eUnitType == GT_Unit.MainCharacter)
                        vMonster.UpdateTarget((G_UnitMainCharacter)vTargetUnit);
                }

                ++m_iCurrentSpawnCount;
                G_FieldMGR.a_instance.a_vMonsterList.Add(vMonster);
            }
        }

        return vObject;
    }

    protected G_FieldPoint m_vFieldPoint = null;
    protected GT_Direction m_eSpawnHorizontalSide = GT_Direction.Left;
    protected GT_VerticalPos m_eSpawnVerticalSide = GT_VerticalPos.Top;

    protected float m_fSpawnMonsterTimer = 0.0f;
    protected int m_iCurrentSpawnCount = 0;

    public bool a_bStageEnd { get { return m_bStageEnd; } }
    protected bool m_bStageEnd = false;
}
