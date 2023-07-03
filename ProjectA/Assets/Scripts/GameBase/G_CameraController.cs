using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_CameraController : MonoBehaviour
{
    private void Awake()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }

    public void LateUpdate()
    {
        if (target == null)
            return;

        if (transform.position != target.position)
        {
            Vector3 vTargetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            vTargetPosition.x = Mathf.Clamp(vTargetPosition.x, minposition.x, maxposition.x);
            vTargetPosition.y = Mathf.Clamp(vTargetPosition.y, minposition.y, maxposition.y);

            transform.position = Vector3.Lerp(transform.position, vTargetPosition, smoothing);
        }
    }

    public Transform target = null;
    public float smoothing = 0.1f;
    public Vector2 minposition;
    public Vector2 maxposition;
}