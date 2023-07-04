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
                    Vector3 v2DPos = new Vector3(vPos.x, vPos.y, 0);
                    vSpawnPosition = vPoint.transform.position + vPos;
                }

                SpawnMonster(vSpawnPosition, (bool bForce) =>
                {

                });
            }
        }
    }
}
