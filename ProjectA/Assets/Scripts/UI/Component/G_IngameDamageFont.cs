using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;

public class G_IngameDamageFont : G_UIBase
{
    public void ResetCtrl(BigInteger iDamage, GT_Damage eType, int iOrder, float fMonsterSizeOffset, Action vCallback)
    {
        m_vCallback = vCallback;
        if (m_vDamageLables != null)
        {
            for (int i = 0; i < m_vDamageLables.Count; ++i)
            {
                if (m_vDamageLables[i] != null)
                    m_vDamageLables[i].gameObject.SetActive(false);
            }
        }

        G_GameMGR.a_instance.StartCoroutine(ApplyDamage(iDamage, eType, iOrder, fMonsterSizeOffset));
    }

    private IEnumerator ApplyDamage(BigInteger iDamage, GT_Damage eType, int iOrder, float fMonsterSizeOffset)
    {
        yield return new WaitForSeconds(m_fTimeOffsetByOrder * iOrder);

        if (this == null)
            yield break;

        string strDamage = G_Utils.GetLargeValue(iDamage);
        int iLabelIndex = (int)eType - 1;
        if (iLabelIndex > m_vDamageLables.Count - 1)
            yield break;

        UILabel vTargetLabel = m_vDamageLables[iLabelIndex];
        if (vTargetLabel != null)
        {
            vTargetLabel.gameObject.SetActive(true);
            vTargetLabel.text = strDamage;

            TweenAlpha vTweenAlpha = vTargetLabel.GetComponent<TweenAlpha>();
            if (vTweenAlpha != null)
            {
                vTweenAlpha.ResetToBeginning();
                vTweenAlpha.PlayForward();
            }
        }

        if (m_vTweenPosition != null)
        {
            float fBeginY = fMonsterSizeOffset + (m_fYOffestByOrder * iOrder) + 130.0f;
            m_vTweenPosition.from = new Vector3(0.0f, fBeginY, 0.0f);
            m_vTweenPosition.to = new Vector3(0.0f, fBeginY + m_fYTargetOffest, 0.0f);
            m_vTweenPosition.ResetToBeginning();
            m_vTweenPosition.PlayForward();
        }

        if (m_vTweenScale != null)
        {
            m_vTweenScale.ResetToBeginning();
            m_vTweenScale.PlayForward();
        }
    }

    public void TweenEndCallback()
    {
        m_vCallback?.Invoke();
    }

    #region Variable

    private Action m_vCallback = null;

    #endregion

    [Header("Variables")]
    [SerializeField, Rename("Timeoffset by order")]
    protected float m_fTimeOffsetByOrder = 0.1f;

    [SerializeField, Rename("YOffset by order")]
    protected float m_fYOffsetByOrder = 5.0f;

    [Header("Damage labels")]
    [SerializeField, Rename("Lables")]
    protected List<UILabel> m_vDamageLables = null;

    [Header("FXs")]
    [SerializeField, Rename("Tween position")]
    protected TweenPosition m_vTweenPosition = null;

    [SerializeField, Rename("Tween scale")]
    protected TweenScale m_vTweenScale = null;

    #region Constant

    private float m_fYOffestByOrder = 70.0f;
    private float m_fYTargetOffest = 170.0f;

    #endregion
}
