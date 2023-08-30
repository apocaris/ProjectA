using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_CameraController : MonoBehaviour
{
    private void Start()
    {
        SetWidthHeight();
    }

    private void FixedUpdate()
    {
        ClampCamPos();
    }

    public void SetPlayerTransform(Transform vTransform)
    {
        m_vPlayerTransform = vTransform;
    }

    private void ClampCamPos()
    {
        if (m_vPlayerTransform == null)
            return;

        transform.position = Vector3.Lerp(transform.position, m_vPlayerTransform.position + m_vCameraPosition, Time.deltaTime * m_fCameraMoveSpeed);
        float lx = m_vMapSize.x - m_fWidth;
        float clampX = Mathf.Clamp(transform.position.x, -lx + m_vCenter.x, lx + m_vCenter.x);

        float ly = m_vMapSize.y - m_fHeight;
        float clampY = Mathf.Clamp(transform.position.y, -ly + m_vCenter.y, ly + m_vCenter.y);

        transform.position = new Vector3(clampX, clampY, 0);

        UpdateScreenInfo();
    }

    private void UpdateScreenInfo()
    {
        float fCurWid = Camera.main.orthographicSize * Screen.width / Screen.height;
        if (m_fPreviousWidth != fCurWid)
        {
            //Debug.LogFormat("Previous : {0}, Current : {1}", m_fPreviousWidth, fCurWid);
            SetWidthHeight();
        }
    }

    private void SetWidthHeight()
    {
        m_fHeight = Camera.main.orthographicSize;
        m_fWidth = m_fHeight * Screen.width / Screen.height;

        m_fPreviousWidth = m_fWidth;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(m_vCenter, m_vMapSize * 2);
    }

    private float m_fPreviousWidth = 0.0f;

    [SerializeField]
    private Transform m_vPlayerTransform;
    [SerializeField]
    private Vector3 m_vCameraPosition;

    [SerializeField]
    private Vector2 m_vCenter;
    [SerializeField]
    private Vector2 m_vMapSize;

    [SerializeField]
    private float m_fCameraMoveSpeed;
    private float m_fHeight;
    private float m_fWidth;
}