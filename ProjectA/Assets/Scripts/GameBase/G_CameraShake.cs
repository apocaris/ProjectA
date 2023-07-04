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

    #region 2D

    public void Shake2D(float _amount, float _duration, float fDelay = 0.0f)
    {
        m_fShakeAmount = _amount;
        m_bIsShake = true;
        InvokeRepeating("BeginShake", fDelay, 0.01f);
        Invoke("StopShake", _duration);
    }

    private void BeginShake()
    {
        Vector3 vPos = transform.localPosition;

        float fOffsetX = Random.value * m_fShakeAmount * 1.8f - m_fShakeAmount;
        float fOffsetY = Random.value * m_fShakeAmount * 1.8f - m_fShakeAmount;

        vPos.x += fOffsetX;
        vPos.y += fOffsetY;

        transform.localPosition = vPos;
    }

    private void StopShake()
    {
        CancelInvoke("BeginShake");
        //transform.localPosition = originPos;
        m_bIsShake = false;
    }

    private float m_fShakeAmount = 0.0f;

    public bool a_bIsShake { get { return m_bIsShake; } }
    private bool m_bIsShake = false;

    #endregion
}
