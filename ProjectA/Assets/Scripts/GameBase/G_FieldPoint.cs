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
