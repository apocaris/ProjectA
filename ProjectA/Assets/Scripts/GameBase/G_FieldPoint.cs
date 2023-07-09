using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_FieldPoint : G_Object
{
    public Transform a_vCameraTransform { get { return m_vCameraTransform; } }
    public Transform a_vCharacterPoint { get { return m_vCharacterPoint; } }
    public Transform a_vBossPoint { get { return m_vBossPoint; } }
    public SphereCollider[] a_arraySpawnPoints { get { return m_arraySpawnPoints; } }
    public Transform a_vSpawnLimitMin { get { return m_vSpawnLimitMin; } }
    public Transform a_vSpawnLimitMax { get { return m_vSpawnLimitMax; } }

    private void OnDrawGizmos()
    {
        if (m_vSpawnLimitMin != null && m_vSpawnLimitMax != null)
        {
            Gizmos.color = Color.yellow;
            if (m_vSpawnLimitMin != null && m_vSpawnLimitMax != null)
            {
                float fWidth = m_vSpawnLimitMax.transform.position.x - m_vSpawnLimitMin.transform.position.x;
                float fHeight = m_vSpawnLimitMax.transform.position.y - m_vSpawnLimitMin.transform.position.y;
                m_vLimitArea = new Vector3(fWidth, fHeight, 0);

                float fCenterX = m_vSpawnLimitMin.transform.position.x + (fWidth / 2);
                float fCenterY = m_vSpawnLimitMin.transform.position.y + (fHeight / 2);
                m_vLimitAreaCenter = new Vector3(fCenterX, fCenterY, 0);
            }

            Gizmos.DrawWireCube(m_vLimitAreaCenter, m_vLimitArea);
        }
    }

    private Vector3 m_vLimitAreaCenter;
    private Vector3 m_vLimitArea;

    [SerializeField, Rename("Camera Transform")]
    protected Transform m_vCameraTransform = null;

    [SerializeField, Rename("Character Point")]
    protected Transform m_vCharacterPoint = null;

    [SerializeField, Rename("Boss Point")]
    protected Transform m_vBossPoint = null;

    [SerializeField, Rename("Spawn Point")]
    protected SphereCollider[] m_arraySpawnPoints = null;

    [SerializeField, Rename("Spawn Limit Min")]
    protected Transform m_vSpawnLimitMin = null;

    [SerializeField, Rename("Spawn Limit Max")]
    protected Transform m_vSpawnLimitMax = null;
}
