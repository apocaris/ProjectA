using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_IngameStageMode : G_IngameModeBase
{
    public override void ResetIngameMode()
    {
        base.ResetIngameMode();


    }

    public override void StartMode()
    {
        base.StartMode();

        ResetIngameMode();
        G_GameMGR.a_instance.StartCoroutine(ChangeInagmeMode("Map_Stage_02"));
    }

    public override void UpdateMode()
    {
        base.UpdateMode();

        UpdateSpawnMonsters();
    }

    private void UpdateSpawnMonsters()
    {
        if (m_vFieldPoint == null)
            return;

        m_fSpawnMonsterTimer += Time.fixedDeltaTime;
        if (m_fSpawnMonsterTimer >= G_Constant.m_fMonsterSpawnDelay)
        {
            m_fSpawnMonsterTimer = 0.0f;
            //if (m_iCurrentSpawnCount >= G_Constant.m_iSpawnMonsterMaxCount)
            //    return;
            if (m_iCurrentSpawnCount > 0)
                return;

            if (m_vSpawnSide == GT_Direction.Right)
                m_vSpawnSide = GT_Direction.Left;
            else
                m_vSpawnSide = GT_Direction.Right;

            //int iSpawnOnceCount = G_Constant.m_iSpawnOnceCount;
            G_GameMGR.a_instance.StartCoroutine(Spawn());
        }
    }

    private IEnumerator Spawn()
    {
        //int iSpawnOnceCount = G_Constant.m_iSpawnMonsterMaxCount;
        for (int i = 0; i < G_Constant.m_iSpawnMonsterMaxCount; ++i)
        {
            Vector3 vSpawnPosition = Vector3.zero;
            //SphereCollider vPoint = m_vFieldPoint.a_arraySpawnPoints[Random.Range(0, m_vFieldPoint.a_arraySpawnPoints.Count)];
            SphereCollider vPoint = null;
            switch (m_vSpawnSide)
            {
                case GT_Direction.Left:
                    {
                        vPoint = m_vFieldPoint.a_arrayLeftSpawnPoints[Random.Range(0, m_vFieldPoint.a_arrayLeftSpawnPoints.Count)];
                    }
                    break;
                case GT_Direction.Right:
                    {
                        vPoint = m_vFieldPoint.a_arrayRightSpawnPoints[Random.Range(0, m_vFieldPoint.a_arrayRightSpawnPoints.Count)];
                    }
                    break;
            }

            if (vPoint != null)
            {
                Vector3 vPos = Random.insideUnitSphere * vPoint.radius;
                Vector3 v2DPos = new Vector3(vPoint.transform.position.x + vPos.x, vPoint.transform.position.y + vPos.y, 0);

                if (m_vFieldPoint.a_vSpawnLimitMin != null && m_vFieldPoint.a_vSpawnLimitMax != null)
                {
                    float fClampX = Mathf.Clamp(v2DPos.x, m_vFieldPoint.a_vSpawnLimitMin.transform.position.x, m_vFieldPoint.a_vSpawnLimitMax.transform.position.x);
                    float fClampY = Mathf.Clamp(v2DPos.y, m_vFieldPoint.a_vSpawnLimitMin.transform.position.y, m_vFieldPoint.a_vSpawnLimitMax.transform.position.y);
                    vSpawnPosition = new Vector3(fClampX, fClampY, 0);
                }
                else
                {
                    vSpawnPosition = v2DPos;
                }
            }

            SpawnMonster(vSpawnPosition, (bool bForce) =>
            {

            });

            float fDelay = UnityEngine.Random.Range(0.1f, 0.5f);
            yield return new WaitForSeconds(fDelay);
        }
    }
}
