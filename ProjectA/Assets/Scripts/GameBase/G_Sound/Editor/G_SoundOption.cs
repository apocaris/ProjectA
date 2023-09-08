using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class G_SoundOption : EditorWindow
{
    public static G_SoundAsset defaultPressUI = null;
    public static G_SoundPlayer.Trigger defaultTrigger = G_SoundPlayer.Trigger.OnPress;

    [SerializeField] private G_SoundAsset _defaultPressUI = null;
    [SerializeField] private G_SoundPlayer.Trigger _defaultTrigger = G_SoundPlayer.Trigger.OnPress;

    Vector2 _scrollPosition = Vector2.zero;

    [MenuItem("Window/GSound Option")]
    static void Init()
    {
        G_SoundOption window = (G_SoundOption)EditorWindow.GetWindow<G_SoundOption>(true, "GSound Option");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
        
        GUILayout.TextArea("Sound Player default value");

        _defaultPressUI = (G_SoundAsset)EditorGUILayout.ObjectField("Asset", _defaultPressUI, typeof(G_SoundAsset), true);
        defaultPressUI = _defaultPressUI;

        _defaultTrigger = (G_SoundPlayer.Trigger)EditorGUILayout.EnumPopup("Trigger", _defaultTrigger);
        defaultTrigger = _defaultTrigger;

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}
