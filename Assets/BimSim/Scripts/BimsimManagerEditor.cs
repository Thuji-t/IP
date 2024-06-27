#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(BimsimManager))]
public class BimsimManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BimsimManager myTarget = (BimsimManager)target;

        base.OnInspectorGUI();
        if (EditorGUILayout.DropdownButton(new GUIContent("Force refresh of data", "invoke a refresh of bimsim data via BimsimManager"), FocusType.Passive))
        {
            Debug.Log("button pressed");
            myTarget.RefreshDataAsync();
        };
    }
}
#endif