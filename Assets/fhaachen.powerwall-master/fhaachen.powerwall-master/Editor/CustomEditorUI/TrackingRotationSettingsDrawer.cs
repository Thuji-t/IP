//using UnityEditor;
//using UnityEditor.UIElements;
//using UnityEngine.UIElements;
//using FHAachen.XR.Powerwall.Tracking;
//using UnityEngine;

//[CustomPropertyDrawer(typeof(TrackingRotationSettings))]
//public class TrackingRotationSettingsDrawer : PropertyDrawer {
//    SerializedProperty xActive;
//    SerializedProperty yActive;
//    SerializedProperty zActive;
//    SerializedProperty xInvert;
//    SerializedProperty yInvert;
//    SerializedProperty zInvert;

//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
//        xActive = property.FindPropertyRelative("xActive");
//        yActive = property.FindPropertyRelative("yActive");
//        zActive = property.FindPropertyRelative("zActive");
//        xInvert = property.FindPropertyRelative("xInvert");
//        yInvert = property.FindPropertyRelative("yInvert");
//        zInvert = property.FindPropertyRelative("zInvert");

//        property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, label.text);
//        if (property.isExpanded) {
//            int rectWidth = 30;

//            Rect activeRect = EditorGUILayout.BeginHorizontal();
//            EditorGUILayout.LabelField("Active Rotation");
//            Rect activeLabelRect = new Rect(activeRect.position, new Vector2(activeRect.width * 0.4f, activeRect.height));
//            Rect[] activeToggleRects = new Rect[3];
//            Rect[] activeLabelRects = new Rect[3];
//            for (int i = 0; i < activeToggleRects.Length; i++) {
//                activeToggleRects[i] = new Rect(new Vector2(activeLabelRect.position.x + activeLabelRect.width + rectWidth * i, activeLabelRect.position.y), new Vector2(rectWidth, activeLabelRect.height));
//                activeLabelRects[i] = new Rect(new Vector2(activeToggleRects[i].position.x + rectWidth / 2, activeToggleRects[i].position.y), new Vector2(activeToggleRects[i].width / 2, activeToggleRects[i].height));

//                xActive.boolValue = EditorGUI.Toggle(activeToggleRects[0], xActive.boolValue);
//                EditorGUI.LabelField(activeLabelRects[0], "X");
//                yActive.boolValue = EditorGUI.Toggle(activeToggleRects[1], yActive.boolValue);
//                EditorGUI.LabelField(activeLabelRects[1], "Y");
//                zActive.boolValue = EditorGUI.Toggle(activeToggleRects[2], zActive.boolValue);
//                EditorGUI.LabelField(activeLabelRects[2], "Z");
//            }
//            EditorGUILayout.EndHorizontal();

//            Rect invertRect = EditorGUILayout.BeginHorizontal();
//            EditorGUILayout.LabelField("invert Rotation");
//            Rect invertLabelRect = new Rect(invertRect.position, new Vector2(invertRect.width * 0.4f, invertRect.height));
//            Rect[] invertToggleRects = new Rect[3];
//            Rect[] invertLabelRects = new Rect[3];
//            for (int i = 0; i < invertToggleRects.Length; i++) {
//                invertToggleRects[i] = new Rect(new Vector2(invertLabelRect.position.x + invertLabelRect.width + rectWidth * i, invertLabelRect.position.y), new Vector2(rectWidth, invertLabelRect.height));
//                invertLabelRects[i] = new Rect(new Vector2(invertToggleRects[i].position.x + rectWidth / 2, invertToggleRects[i].position.y), new Vector2(invertToggleRects[i].width / 2, invertToggleRects[i].height));

//                xInvert.boolValue = EditorGUI.Toggle(invertToggleRects[0], xInvert.boolValue);
//                EditorGUI.LabelField(invertLabelRects[0], "X");
//                yInvert.boolValue = EditorGUI.Toggle(invertToggleRects[1], yInvert.boolValue);
//                EditorGUI.LabelField(invertLabelRects[1], "Y");
//                zInvert.boolValue = EditorGUI.Toggle(invertToggleRects[2], zInvert.boolValue);
//                EditorGUI.LabelField(invertLabelRects[2], "Z");
//            }
//            EditorGUILayout.EndHorizontal();
//        }
//    }

//}
