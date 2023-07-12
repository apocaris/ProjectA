using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class G_PlayParticle : G_Object
{
    public void UpdateSpeed(float fValue)
    {

    }

    public void PlayParticle()
    {
        if(m_vParticle != null)
        {
            m_vParticle.Clear();
            m_vParticle.Simulate(0);
            m_vParticle.Play();
        }
    }

    public void StopParticle()
    {
        if(m_vParticle != null)
        {
            m_vParticle.Clear();
            m_vParticle.Stop();
        }
    }

    public void ResetOnFinish(float fDelay, System.Action vOnFinish)
    {
        m_vOnFinish = vOnFinish;

        m_enumeratorOnFinishProcess = ResetOnFinishProcess(fDelay, m_vOnFinish);
        G_FieldMGR.a_instance.StartCoroutine(m_enumeratorOnFinishProcess);
    }

    private IEnumerator ResetOnFinishProcess(float fDelay, System.Action vOnFinish)
    {
        yield return new WaitForSeconds(fDelay);

        if (vOnFinish != null)
            vOnFinish.Invoke();

        m_enumeratorOnFinishProcess = null;
    }

    public void ForceInvokeOnFinish()
    {
        if(m_enumeratorOnFinishProcess != null)
            G_FieldMGR.a_instance.StopCoroutine(m_enumeratorOnFinishProcess);
        m_enumeratorOnFinishProcess = null;

        if (m_vOnFinish != null)
            m_vOnFinish.Invoke();
    }

    public void ResetRenderQueue(UIWidget vViewBack)
    {
        if (null != vViewBack)
        {
            Material[] arrayParticleMat = GetParticleMaterials();

            vViewBack.onRender += _mat =>
            {
                for (int i = 0; i != arrayParticleMat.Length; ++i)
                    arrayParticleMat[i].renderQueue = _mat.renderQueue;
            };
        }
    }

    public void ResetRenderQueueOnTop(UIWidget vViewBack)
    {
        if (null != vViewBack)
        {
            Material[] arrayParticleMat = GetParticleMaterials();

            vViewBack.onRender += _mat =>
            {
                for (int i = 0; i != arrayParticleMat.Length; ++i)
                    arrayParticleMat[i].renderQueue = _mat.renderQueue + 3;
            };
        }
    }

    public Material[] GetParticleMaterials()
    {
        List<Material> vMatList = new List<Material>();
        ParticleSystem[] arrayParticles = m_vParticle.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < arrayParticles.Length; ++i)
        {
            Renderer vRenderer = arrayParticles[i].GetComponent<Renderer>();
            vMatList.Add(vRenderer.material);
        }

        return vMatList.ToArray();
    }

    // 이영주 추가
    public void CompleteUnityAnimation(GameObject obj)
    {
        if (obj != null)
            obj.SetActive(false);
    }

    private System.Action m_vOnFinish = null;
    private IEnumerator m_enumeratorOnFinishProcess = null;

    [SerializeField, Rename("Particle")]
    protected ParticleSystem m_vParticle = null;
}
