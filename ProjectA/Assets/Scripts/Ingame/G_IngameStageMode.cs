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

#if MONSTER_SPAWN_WAVE
        G_GameMGR.a_instance.StartCoroutine(ChangeInagmeMode("Map_Stage_04"));
#else
        G_GameMGR.a_instance.StartCoroutine(ChangeInagmeMode("Map_Stage_03"));
#endif
    ;}

    public override void UpdateMode()
    {
        base.UpdateMode();

        UpdateSpawnMonsters();
    }

#if MONSTER_SPAWN_WAVE

    private void UpdateSpawnMonsters()
    {
        m_fSpawnMonsterTimer += Time.fixedDeltaTime;
        if (m_fSpawnMonsterTimer >= G_Constant.m_fMonsterSpawnDelay)
        {
            m_fSpawnMonsterTimer = 0.0f;
            if (m_iCurrentSpawnCount > 0)
                return;

            if (m_eSpawnHorizontalSide == GT_Direction.Right)
                m_eSpawnHorizontalSide = GT_Direction.Left;
            else
                m_eSpawnHorizontalSide = GT_Direction.Right;

            m_eSpawnVerticalSide = (GT_VerticalPos)UnityEngine.Random.Range(0, 2);

            G_GameMGR.a_instance.StartCoroutine(Spawn());
        }
    }

    private IEnumerator Spawn()
    {
        if (m_vFieldPoint == null)
            yield break;

        float fZPos = m_fDefaultZPos;
        if (m_vFieldPoint.a_vMapTransform != null)
            fZPos = m_vFieldPoint.a_vMapTransform.position.z - 1.0f;

        List<SphereCollider> vSpawnPoints = null;
        m_vFieldPoint.GetSpawnPoints(m_eSpawnHorizontalSide, m_eSpawnVerticalSide, ref vSpawnPoints);

        if (vSpawnPoints != null && vSpawnPoints.Count > 0)
        {
            for (int i = 0; i < G_Constant.m_iSpawnMonsterMaxCount; ++i)
            {
                Vector3 vSpawnPosition = Vector3.zero;
                //SphereCollider vPoint = m_vFieldPoint.a_arraySpawnPoints[Random.Range(0, m_vFieldPoint.a_arraySpawnPoints.Count)];
                SphereCollider vPoint = vSpawnPoints[Random.Range(0, vSpawnPoints.Count)];
                if (vPoint != null)
                {
                    Vector3 vPos = Random.insideUnitSphere * vPoint.radius;
                    Vector3 v2DPos = new Vector3(vPoint.transform.position.x + vPos.x, vPoint.transform.position.y + vPos.y, 0);

                    if (m_vFieldPoint.a_vSpawnLimitMin != null && m_vFieldPoint.a_vSpawnLimitMax != null)
                    {
                        float fClampX = Mathf.Clamp(v2DPos.x, m_vFieldPoint.a_vSpawnLimitMin.transform.position.x, m_vFieldPoint.a_vSpawnLimitMax.transform.position.x);
                        float fClampY = Mathf.Clamp(v2DPos.y, m_vFieldPoint.a_vSpawnLimitMin.transform.position.y, m_vFieldPoint.a_vSpawnLimitMax.transform.position.y);
                        vSpawnPosition = new Vector3(fClampX, fClampY, fZPos);
                    }
                    else
                    {
                        vSpawnPosition = v2DPos;
                    }
                }

                SpawnMonster(vSpawnPosition, (bool bForce) =>
                {

                });

                float fDelay = Random.Range(0.05f, 0.3f);
                yield return new WaitForSeconds(fDelay);
            }
        }
    }

#else

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

            if (m_iSpawnPoint < 3)
                ++m_iSpawnPoint;
            else
                m_iSpawnPoint = 0;

            int iSpawnOnceCount = G_Constant.m_iSpawnOnceCount;
            for (int i = 0; i < iSpawnOnceCount; ++i)
            {
                Vector3 vSpawnPosition = Vector3.zero;
                //SphereCollider vPoint = m_vFieldPoint.a_arraySpawnPoints[Random.Range(0, m_vFieldPoint.a_arraySpawnPoints.Count)];
                SphereCollider vPoint = m_vFieldPoint.a_arraySpawnPoints[m_iSpawnPoint];
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

    private int m_iSpawnPoint = -1;

#endif
}