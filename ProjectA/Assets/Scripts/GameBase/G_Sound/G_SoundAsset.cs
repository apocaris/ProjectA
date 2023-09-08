using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class G_SoundClip
{
    public AudioClip audioClip = null;
    [Range(0, 1)]
    public float volume = 1.0f;
    [Range(-3, 3)]
    public float pitch = 1.0f;
    public bool loop = false;
    public float delay = 0.0f;
    public float front = 0.0f;
    public float duration = 0.0f;
}

[CreateAssetMenu(menuName = "GSound/Create Sound Asset", order = 1)]

public class G_SoundAsset : ScriptableObject
{
    public string id = string.Empty;
    public bool bgm = false;
    public int channel = 0;

    // --- 3D 사운드 관련 항목 ---
    [Header("3D Sound Components")]
    public bool enabled3DSound = false;

    [Range(0, 1), Tooltip("0일 때 2D, 1에 가까울수록 3D 사운드가 됩니다.")]
    public float spatialBlend = 1.0f;

    public float distance = 10.0f;
    // --- 3D 사운드 관련 항목 ---

    [Space]
    public List<G_SoundClip> sound = new List<G_SoundClip>();

#if UNITY_EDITOR
    private void Awake()
    {
        /*
        if (Selection.objects != null && Selection.objects.Length > 0)
        {
            var _clips = Selection.GetFiltered(typeof(AudioClip), SelectionMode.TopLevel);
            if (null != _clips && 0 != _clips.Length)
            {
                if (null == sound)
                    sound = new List<G_SoundClip>();
                sound.Clear();

                for (int i = 0; i != _clips.Length; ++i)
                {
                    sound.Add(new G_SoundClip()
                    {
                        audioClip = (AudioClip)_clips[i],
                    });
                }
            }
        }*/
    }
#endif

    public void PlaySound()
    {
        G_SoundManager.a_instance.Play(this);
    }
}
