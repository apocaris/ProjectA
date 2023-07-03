using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_CameraShake : MonoBehaviour
{
    Vector3 originPos;

    void Awake()
    {
        UpdateOriginPos();
    }

    public void UpdateOriginPos()
    {
        originPos = transform.localPosition;
    }

    private void Update()
    {
       
    }

    public IEnumerator Shake(float _amount, float _duration, float fDelay = 0.0f)
    {
        if (fDelay != 0.0f)
            yield return new WaitForSeconds(fDelay);

        float timer = 0;
        while (timer <= _duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * _amount + originPos;

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;
    }

    public IEnumerator ShakeY(float _amount, float _duration, float fDelay = 0.0f)
    {
        if (fDelay != 0.0f)
            yield return new WaitForSeconds(fDelay);

        float timer = 0;
        while (timer <= _duration)
        {
            transform.localPosition = new Vector3(originPos.x, Random.Range(0, _amount) + originPos.y, originPos.z);

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;
    }
}
