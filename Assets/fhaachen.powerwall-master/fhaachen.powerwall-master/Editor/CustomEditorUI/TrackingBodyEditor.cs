//using UnityEngine;
//using UnityEditor;
//using FHAachen.XR.Powerwall.Behaviour;

//[CustomEditor(typeof(TrackingBody))]
//[CanEditMultipleObjects]
//public class TrackingBodyEditor : Editor {
//    SerializedProperty bodyName;
//    SerializedProperty bodyID;

//    SerializedProperty applyPosition;
//    SerializedProperty applyRotation;

//    SerializedProperty positionConstraints;
//    SerializedProperty rotationConstraints;

//    void OnEnable() {
//        bodyName = serializedObject.FindProperty("bodyName");
//        bodyID = serializedObject.FindProperty("bodyID");
//        applyPosition = serializedObject.FindProperty("applyPosition");
//        applyRotation = serializedObject.FindProperty("applyRotation");
//        positionConstraints = serializedObject.FindProperty("positionConstraints");
//        rotationConstraints = serializedObject.FindProperty("rotationConstraints");
//    }

//    public override void OnInspectorGUI() {
//        serializedObject.Update();

//        EditorGUILayout.LabelField("Body Identification:", EditorStyles.boldLabel);
//        EditorGUILayout.PropertyField(bodyName);
//        EditorGUILayout.PropertyField(bodyID);

//        EditorGUILayout.LabelField("Position & Rotation Settings:", EditorStyles.boldLabel);
//        EditorGUILayout.PropertyField(applyPosition);
//        EditorGUILayout.PropertyField(applyRotation);
//        EditorGUILayout.PropertyField(positionConstraints);
//        EditorGUILayout.PropertyField(rotationConstraints);

//        serializedObject.ApplyModifiedProperties();
//    }
//}