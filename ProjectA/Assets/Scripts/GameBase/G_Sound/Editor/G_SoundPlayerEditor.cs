using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(G_SoundPlayer))]
public class G_SoundPlayerEditor : Editor
{
    SerializedProperty triggerInfo;
    ReorderableList triggerReorderableList;

    private void OnEnable()
    {
        //
        // Step 1 "link" the SerializedProperties to the properties of YourOtherClass
        triggerInfo = serializedObject.FindProperty("triggerInfo");

        // Step 2 setup the ReorderableList
        triggerReorderableList = new ReorderableList(serializedObject, triggerInfo)
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
                EditorGUI.LabelField(rect, "Triggers Info");
            },

            // Now to the interesting part: Here you setup how elements look like
            drawElementCallback = (rect, index, active, focused) =>
            {
                // Get the currently to be drawn element from YourList
                var element = triggerInfo.GetArrayElementAtIndex(index);

                // Get the elements Properties into SerializedProperties
                var trigger = element.FindPropertyRelative("trigger");
                var asset = element.FindPropertyRelative("asset");
                var talker = element.FindPropertyRelative("talker");
                var chaseTalker = element.FindPropertyRelative("chaseTalker");

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), trigger);
                rect.y += EditorGUIUtility.singleLineHeight;

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), asset);
                rect.y += EditorGUIUtility.singleLineHeight;

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), talker);
                rect.y += EditorGUIUtility.singleLineHeight;

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), chaseTalker);
                rect.y += EditorGUIUtility.singleLineHeight;
            },

            // And since we have more than one line (default) you'll have to configure 
            // how tall your elements are. Luckyly in your example it will always be exactly
            // 3 Lines in each case. If not you would have to change this.
            // In some cases it becomes also more readable if you use one more Line as spacer between the elements
            elementHeight = EditorGUIUtility.singleLineHeight * 4.5f,

            //alternatively if you have different heights you would use e.g.
            //elementHeightCallback = index =>
            //{
            //    var element = YourList.GetArrayElementAtIndex(index);
            //    var Enum = element.FindPropertyRelative("Enum");

            //    switch ((YourClass.YourEnum)Enum.intValue)
            //    {
            //        case YourClass.YourEnum.first:
            //            return EditorGUIUtility.singleLineHeight * 3;

            //        case YourClass.YourEnum.second:
            //            return EditorGUIUtility.singleLineHeight * 5;

            //            default:
            //                return EditorGUIUtility.singleLineHeight;
            //    }
            //}

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
                var trigger = element.FindPropertyRelative("trigger");
                var asset = element.FindPropertyRelative("asset");
                var talker = element.FindPropertyRelative("talker");
                var chaseTalker = element.FindPropertyRelative("chaseTalker");

                // and set default values
                if (0 == index)
                {
                    trigger.enumValueIndex = (int)G_SoundOption.defaultTrigger;
                    asset.objectReferenceValue = G_SoundOption.defaultPressUI;
                    talker.objectReferenceValue = null;
                    chaseTalker.boolValue = false;
                }
                else
                {
                    trigger.enumValueIndex = 0;
                    asset.objectReferenceValue = null;
                    talker.objectReferenceValue = null;
                    chaseTalker.boolValue = false;
                }
            }
        };
        if (0 == triggerInfo.arraySize)
        {
            triggerReorderableList.onAddCallback.Invoke(triggerReorderableList);
            serializedObject.ApplyModifiedProperties();
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        triggerReorderableList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }
}
