using System.Collections.Generic;
using UnityEngine;
using FHAachen.XR.Powerwall.Behaviour;

namespace FHAachen.XR.Powerwall
{
    /// <summary>
    ///     Class <c>Prefabmanager</c> controlls the prefabs that will be initialized at runtime.
    /// </summary>
    public class Prefabmanager : MonoBehaviour
    {
        /// <summary>
        ///     Creates an instance of the datastream prefab at Runtime when the option is selected in the plugin settings.
        /// </summary>
        //[RuntimeInitializeOnLoadMethod]
        //public static void createDataStream() {
        //    if (PowerwallBuildSettings.Instance.trackingSystem == TrackingSystem.Vicon)
        //        Debug.Log("Tracking System: Vicon");
        //    else if (PowerwallBuildSettings.Instance.trackingSystem == TrackingSystem.DTrack2)
        //        Debug.Log("Tracking System: DTrack2");
        //    else
        //        Debug.Log("No Tracking System selected");

        //    if (PowerwallBuildSettings.Instance.trackingSystem != TrackingSystem.None && PowerwallBuildSettings.Instance.initDataStream)
        //        GameObject.Instantiate((GameObject)Resources.Load("GeneralTracking/TrackingDataStream"));
        //}

        /// <summary>
        ///     Replaces the Main Camera in the scene with an instance of the StereoCamera prefab when DisplayMode is set to Stereo in the plugin settings.
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        public static void replaceCamera() {
            if (PowerwallBuildSettings.Instance.cameraMode != CameraMode.Mono) {
                Debug.Log("RenderMode: Stereo");

                GameObject stereoCameraPrefab = (GameObject)Resources.Load("StereoCamera");
                GameObject stereoCamera = GameObject.Instantiate(stereoCameraPrefab);
                stereoCamera.name = "StereoCamera";
                stereoCamera.transform.parent = Camera.main.transform.parent;
                stereoCamera.transform.localPosition = Camera.main.transform.localPosition;
                stereoCamera.transform.localRotation = Camera.main.transform.localRotation;
                stereoCamera.transform.localScale = Camera.main.transform.localScale;

                /*
                List<Transform> childList = new List<Transform>();
                foreach (Transform child in Camera.main.transform)
                    childList.Add(child);
                foreach (Transform child in childList)
                    child.parent = stereoCamera.transform;

                DestroyImmediate(Camera.main.gameObject);
                */
            }
            else 
                Debug.Log("RenderMode: Mono");
        }
    }
}