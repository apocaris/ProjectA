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
        StartCoroutine(ChangeInagmeMode("Map_Stage_01"));
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
            if (m_iCurrentSpawnCount >= G_Constant.m_iSpawnMonsterMaxCount)
                return;

            int iSpawnOnceCount = G_Constant.m_iSpawnOnceCount;
            for (int i = 0; i < iSpawnOnceCount; ++i)
            {
                Vector3 vSpawnPosition = Vector3.zero;
                SphereCollider vPoint = m_vFieldPoint.a_arraySpawnPoints[Random.Range(0, m_vFieldPoint.a_arraySpawnPoints.Length)];
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
            }
        }
    }
}
