using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(ItemGenerator))]
public class ItemGeneratorEditor : Editor
{
    SerializedProperty itemList;

    void OnEnable()
    {
        itemList = serializedObject.FindProperty("itemList");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(itemList, true);

        serializedObject.ApplyModifiedProperties();

        ItemGenerator generator = (ItemGenerator)target;
    }
}
#endif
