using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FHAachen.XR.Powerwall.Behaviour;
using System.Security.Cryptography.X509Certificates;

/// <summary>
///     Class <c>MenuFuncitons</c> is used to add additional Editor functions to the Unity GUI.
///     <param>applyPosition</param> true if the position of the tracked object should be applied to the selected GameObject.
///     <param>applyRotation</param> true if the rotation of the tracked object should be applied to the selected GameObject.
///     <param>objectName</param> Name of the tracked Object in the Vicon software.
///     <param>objectID</param> ID of the tracked Object in the DTrack2 software.
///     <param>chosenObject</param> Selected GameObject.
///     Author Jan Klemens
/// </summary>
public class MenuFuncitons : EditorWindow {
    private bool applyPosition = true;
    private bool applyRotation = true;
    private string objectName = "";
    private int objectID = 0;
    private GameObject chosenObject = null;

    /// <summary>
    ///     Opens the GUI Window when the MenuItem is selected.
    /// </summary>
    [MenuItem("GameObject/XR/Make GameObject trackable")]
    private static void openTrackableGUI() {
        MenuFuncitons window = (MenuFuncitons)EditorWindow.GetWindow(typeof(MenuFuncitons));
        window.Show();
    }

    /// <summary>
    ///     Defines what happens when the window is oppened.
    /// </summary>
    private void OnGUI() {
        makeTrackableGUI();
    }

    /// <summary>
    ///     Defines the Layout of the Window and sets local variables to inputs.
    /// </summary>
    private void makeTrackableGUI() {
        GUILayout.Label("", EditorStyles.boldLabel);

        chosenObject = EditorGUILayout.ObjectField("GameObject: ", chosenObject, typeof(GameObject), true) as GameObject;

        if(chosenObject != null) {
            objectName = EditorGUILayout.TextField("Name of tracked Object", objectName);
            objectID = EditorGUILayout.IntField("ID of tracked Object", objectID);
            applyPosition = EditorGUILayout.Toggle("Apply Position", applyPosition);
            applyRotation = EditorGUILayout.Toggle("Apply Rotation", applyRotation);
        }

        if (GUILayout.Button("Make Object trackable")) {
            makeTrackable(chosenObject);
            this.Close();
        }
    }

    /// <summary>
    ///     Adds the Component <c>TrackingBody</c> to the selected <paramref name="gameObject"/>
    /// </summary>
    /// <param name="gameObject">Selected <c>GameObject</c></param>
    private void makeTrackable(GameObject gameObject) {
        if(gameObject.GetComponent<TrackingBody>() == null) 
            gameObject.AddComponent<TrackingBody>(); 
        gameObject.GetComponent<TrackingBody>().bodyID = objectID;
        gameObject.GetComponent<TrackingBody>().bodyName = objectName;
        gameObject.GetComponent<TrackingBody>().applyPosition = this.applyPosition;
        gameObject.GetComponent<TrackingBody>().applyRotation = this.applyRotation;
    }
}
