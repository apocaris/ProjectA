using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * Author : 최신
 * Date : 2019-04-07
 * Desc : 주기적인 객체 갱신이 필요없는 참조형 매니저 최상위 객체
 */

public class G_SimpleMGR<T> : G_Object where T : G_Object
{
    protected G_SimpleMGR() { }

    protected virtual void Awake() { }

    protected virtual void OnDestroy() { }

    protected virtual void Initalize() { }

    public static T ResetMGR()
    {
        T vInstance = (T)FindObjectOfType(typeof(T));

        if (vInstance == null)
        {
            GameObject vNewInstance = Instantiate(Resources.Load(G_Constant.m_strBaseInstance)) as GameObject;
            vInstance = vNewInstance.AddComponent<T>();
        }

        vInstance.name = typeof(T).ToString();
        DontDestroyOnLoad(vInstance.gameObject);

        EditorLog(typeof(T).ToString() + " Manager Reset Complete");

        return vInstance;
    }
}
