using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class G_CameraController_perspective : MonoBehaviour
{
    private void Start()
    {
        UpdateLimitSize();
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
        float clampX = Mathf.Clamp(transform.position.x, m_vMoveMin.x, m_vMoveMax.x);
        float clampY = Mathf.Clamp(transform.position.y, m_vMoveMin.y, m_vMoveMax.y);
        transform.position = new Vector3(clampX, clampY);
    }

    private void UpdateLimitSize()
    {
        float fMinX = m_vMoveMin.x;
        if (fMinX < 0.0f)
            fMinX *= -1;
        float fMaxX = m_vMoveMax.x;
        if (fMaxX < 0.0f)
            fMaxX *= -1;

        float fMinY = m_vMoveMin.x;
        if (fMinY < 0.0f)
            fMinY *= -1;
        float fMaxY = m_vMoveMax.x;
        if (fMaxY < 0.0f)
            fMaxY *= -1;

        m_vLimitSize = new Vector3(fMinX + fMaxX, fMinY + fMaxY, 0.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        UpdateLimitSize();
        Gizmos.DrawWireCube(m_vCenter, m_vLimitSize);
    }

    private Vector3 m_vLimitSize;

    public Transform a_vPlayerTransform { get { return m_vPlayerTransform; } }
    private Transform m_vPlayerTransform;

    [SerializeField]
    private Vector3 m_vCameraPosition;

    [SerializeField, Rename("Min")]
    private Vector2 m_vMoveMin;

    [SerializeField, Rename("Max")]
    private Vector2 m_vMoveMax;

    [SerializeField]
    private Vector3 m_vCenter;

    [SerializeField]
    private float m_fCameraMoveSpeed;
}
