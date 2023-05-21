using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_GameMGR : G_SimpleMGR<G_GameMGR>
{
    private static G_GameMGR m_vInstance = null;
    public static G_GameMGR a_instance { get
        {
            if (m_vInstance == null)
            {
                EditorLog("MGR Instance is null", 1);
                m_vInstance = ResetMGR();
            }

            return m_vInstance;
        }
    }


    protected override void Awake()
    {
        base.Awake();
        m_vGameVersion = Application.version;
    }

    private void FixedUpdate()
    {
        
    }



    public string a_vGameVersion { get { return m_vGameVersion; } }
    private string m_vGameVersion = string.Empty;
}
