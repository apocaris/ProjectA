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

    private void ClampCamPos()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + cameraPosition, Time.deltaTime * cameraMoveSpeed);
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

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
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;

        m_fPreviousWidth = width;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }

    private float m_fPreviousWidth = 0.0f;

    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Vector3 cameraPosition;

    [SerializeField]
    private Vector2 center;
    [SerializeField]
    private Vector2 mapSize;

    [SerializeField]
    private float cameraMoveSpeed;
    private float height;
    private float width;
}