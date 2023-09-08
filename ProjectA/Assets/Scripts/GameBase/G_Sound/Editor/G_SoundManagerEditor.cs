using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(G_SoundManager))]

public class G_SoundManagerEditor : Editor
{
    ReorderableList assetsReorderableList;
    SerializedProperty soundAssets;

    private void OnEnable()
    {
        soundAssets = serializedObject.FindProperty("soundAssets");

        assetsReorderableList = new ReorderableList(serializedObject, soundAssets)
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
                EditorGUI.LabelField(rect, "Sound Assets");
            },

            // Now to the interesting part: Here you setup how elements look like
            drawElementCallback = (rect, index, active, focused) =>
            {
                // Get the currently to be drawn element from YourList
                var element = soundAssets.GetArrayElementAtIndex(index);
                
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element);
            },

            // And since we have more than one line (default) you'll have to configure 
            // how tall your elements are. Luckyly in your example it will always be exactly
            // 3 Lines in each case. If not you would have to change this.
            // In some cases it becomes also more readable if you use one more Line as spacer between the elements
            elementHeight = EditorGUIUtility.singleLineHeight,
        };
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        /*
        serializedObject.Update();

        //assetsReorderableList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Add Selected Assets"))
        {
            G_SoundManager _manager = (G_SoundManager)target;
            if (null == _manager.soundAssets)
                _manager.soundAssets = new List<G_SoundAsset>();

            var _objects = UnityEditor.Selection.GetFiltered(typeof(G_SoundAsset), UnityEditor.SelectionMode.DeepAssets);
            foreach (var _object in _objects)
            {
                G_SoundAsset _asset = (G_SoundAsset)_object;
                int _index = _manager.soundAssets.FindIndex((_data) => null == _data || _data.id == _asset.id);
                if (-1 == _index)
                    _manager.soundAssets.Add(_asset);
                else
                    _manager.soundAssets[_index] = _asset;
            }
        }
        */
    }
}
