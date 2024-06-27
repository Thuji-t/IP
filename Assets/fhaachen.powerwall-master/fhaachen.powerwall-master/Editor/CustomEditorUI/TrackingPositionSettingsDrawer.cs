//using UnityEditor;
//using UnityEditor.UIElements;
//using UnityEngine.UIElements;
//using FHAachen.XR.Powerwall.Tracking;
//using UnityEngine;

//[CustomPropertyDrawer(typeof(TrackingPositionSettings))]
//public class TrackingPositionSettingsDrawer : PropertyDrawer {
//    SerializedProperty xActive;
//    SerializedProperty yActive;
//    SerializedProperty zActive;
//    SerializedProperty scale;
//    SerializedProperty trackingParent;
//    SerializedProperty scaleToParent;

    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    //    xActive = property.FindPropertyRelative("xActive");
    //    yActive = property.FindPropertyRelative("yActive");
    //    zActive = property.FindPropertyRelative("zActive");
    //    scale = property.FindPropertyRelative("scale");
    //    trackingParent = property.FindPropertyRelative("trackingParent");
    //    scaleToParent = property.FindPropertyRelative("scaleToParent");

    //    property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, label.text);
    //    if (property.isExpanded) {
    //        Rect startingRect = EditorGUILayout.BeginHorizontal();

    //        EditorGUILayout.LabelField("Active Position");

    //        Rect labelRect = new Rect(startingRect.position, new Vector2(startingRect.width * 0.4f, startingRect.height));
    //        Rect[] boolRects = new Rect[3];
    //        Rect[] labelRects = new Rect[3];
    //        int rectWidth = 30;
    //        for (int i = 0; i < boolRects.Length; i++) {
    //            boolRects[i] = new Rect(new Vector2(labelRect.position.x + labelRect.width + rectWidth * i, labelRect.position.y), new Vector2(rectWidth, labelRect.height));
    //            labelRects[i] = new Rect(new Vector2(boolRects[i].position.x + rectWidth / 2, boolRects[i].position.y), new Vector2(boolRects[i].width / 2, boolRects[i].height));
    //        }

    //        xActive.boolValue = EditorGUI.Toggle(boolRects[0], xActive.boolValue);
    //        EditorGUI.LabelField(labelRects[0], "X");
    //        yActive.boolValue = EditorGUI.Toggle(boolRects[1], yActive.boolValue);
    //        EditorGUI.LabelField(labelRects[1], "Y");
    //        zActive.boolValue = EditorGUI.Toggle(boolRects[2], zActive.boolValue);
    //        EditorGUI.LabelField(labelRects[2], "Z");

    //        EditorGUILayout.EndHorizontal();

    //        EditorGUILayout.PropertyField(scale);
    //        EditorGUILayout.PropertyField(trackingParent);
    //        if (trackingParent.objectReferenceValue != null)
    //            EditorGUILayout.PropertyField(scaleToParent);
    //    }
    //}

//}
