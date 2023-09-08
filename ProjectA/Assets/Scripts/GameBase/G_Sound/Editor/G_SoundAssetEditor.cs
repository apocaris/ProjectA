using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(G_SoundAsset))]

public class G_SoundAssetEditor : Editor
{
    SerializedProperty id;
    SerializedProperty bgm;
    SerializedProperty channel;
    SerializedProperty enabled3DSound;
    SerializedProperty spatialBlend;
    SerializedProperty distance;
    SerializedProperty sound;
    ReorderableList soundReorderableList;

    private void OnEnable()
    {
        id = serializedObject.FindProperty("id");
        bgm = serializedObject.FindProperty("bgm");
        channel = serializedObject.FindProperty("channel");
        enabled3DSound = serializedObject.FindProperty("enabled3DSound");
        spatialBlend = serializedObject.FindProperty("spatialBlend");
        distance = serializedObject.FindProperty("distance");

        // Step 1 "link" the SerializedProperties to the properties of YourOtherClass
        sound = serializedObject.FindProperty("sound");

        // Step 2 setup the ReorderableList
        soundReorderableList = new ReorderableList(serializedObject, sound)
        {
            // Can your objects be dragged an their positions changed within the List?
            draggable = false,

            // Can you add elements by pressing the "+" button?
            displayAdd = true,

            // Can you remove Elements by pressing the "-" button?
            displayRemove = true,

            // Make a header for the list
            drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Sound Clips");
            },

            // Now to the interesting part: Here you setup how elements look like
            drawElementCallback = (rect, index, active, focused) =>
            {
                // Get the currently to be drawn element from YourList
                var element = sound.GetArrayElementAtIndex(index);

                // Get the elements Properties into SerializedProperties
                var audioClip = element.FindPropertyRelative("audioClip");
                var volume = element.FindPropertyRelative("volume");
                var pitch = element.FindPropertyRelative("pitch");
                var loop = element.FindPropertyRelative("loop");
                var front = element.FindPropertyRelative("front");
                var delay = element.FindPropertyRelative("delay");
                var duration = element.FindPropertyRelative("duration");

                if (0 == duration.floatValue && null != audioClip.objectReferenceValue)
                    duration.floatValue = ((AudioClip)audioClip.objectReferenceValue).length;

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), audioClip);
                rect.y += EditorGUIUtility.singleLineHeight;

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), volume);
                rect.y += EditorGUIUtility.singleLineHeight;

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), pitch);
                rect.y += EditorGUIUtility.singleLineHeight;

                if (!bgm.boolValue)
                {
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), loop);
                    rect.y += EditorGUIUtility.singleLineHeight;

                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), delay);
                    rect.y += EditorGUIUtility.singleLineHeight;

                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), front);
                    rect.y += EditorGUIUtility.singleLineHeight;

                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), duration);
                    rect.y += EditorGUIUtility.singleLineHeight;
                }
            },

            // And since we have more than one line (default) you'll have to configure 
            // how tall your elements are. Luckyly in your example it will always be exactly
            // 3 Lines in each case. If not you would have to change this.
            // In some cases it becomes also more readable if you use one more Line as spacer between the elements
            //elementHeight = EditorGUIUtility.singleLineHeight * (bgm.boolValue ? 3f : 7f),

            //alternatively if you have different heights you would use e.g.
            elementHeightCallback = index =>
            {
                if (bgm.boolValue)
                    return EditorGUIUtility.singleLineHeight * 3;
                else
                    return EditorGUIUtility.singleLineHeight * 7;
            },

            // optional: Set default Values when adding a new element
            // (otherwise the values of the last list item will be copied)
            onAddCallback = list =>
            {
                // The new index will be the current List size ()before adding
                var index = list.serializedProperty.arraySize;

                // Since this method overwrites the usual adding, we have to do it manually:
                // Simply counting up the array size will automatically add an element
                list.serializedProperty.arraySize++;
                list.index = index;
                var element = list.serializedProperty.GetArrayElementAtIndex(index);

                // again link the properties of the element in SerializedProperties
                var audioClip = element.FindPropertyRelative("audioClip");
                var volume = element.FindPropertyRelative("volume");
                var pitch = element.FindPropertyRelative("pitch");
                var loop = element.FindPropertyRelative("loop");
                var front = element.FindPropertyRelative("front");
                var delay = element.FindPropertyRelative("delay");
                var duration = element.FindPropertyRelative("duration");

                // and set default values
                audioClip.objectReferenceValue = null;
                volume.floatValue = 1.0f;
                pitch.floatValue = 1.0f;
                loop.boolValue = false;
                front.floatValue = 0.0f;
                delay.floatValue = 0.0f;
                duration.floatValue = 0.0f;
            }
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(id);
        if (id.stringValue == string.Empty)
            id.stringValue = target.name;

        EditorGUILayout.PropertyField(bgm);
        if (bgm.boolValue)
            EditorGUILayout.PropertyField(channel);

        EditorGUILayout.PropertyField(enabled3DSound);
        if (enabled3DSound.boolValue)
        {
            EditorGUILayout.PropertyField(spatialBlend);
            EditorGUILayout.PropertyField(distance);
        }

        GUILayout.Space(20);
        if (bgm.boolValue && 1 < sound.arraySize)
            EditorGUILayout.HelpBox("BGM must have only 1 sound clip", MessageType.Warning);
        soundReorderableList.elementHeight = EditorGUIUtility.singleLineHeight * (bgm.boolValue ? 3.5f : 5.5f);
        soundReorderableList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();

        GUILayout.Space(10);

        if (GUILayout.Button("Play Sound"))
        {
            if (Application.isPlaying)
                ((G_SoundAsset)target).PlaySound();
            else
                EditorUtility.DisplayDialog("Play Sound", "Can only be used at runtime!", "OK");
        }
    }
}