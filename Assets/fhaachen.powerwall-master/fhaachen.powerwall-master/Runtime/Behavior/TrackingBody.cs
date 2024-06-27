using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FHAachen.XR.Powerwall.Tracking;

namespace FHAachen.XR.Powerwall.Behaviour
{
    /// <summary>
    ///     Class <c>TrackingBody</c> is used as a general representation of a tracked object.
    ///     After starting the game, this script will remove itself from a gameobject and replace itself with either the according Vicon or DTrack scipt, 
    ///         while passing the necessary parameters along.
    ///     <param>bodyName</param> Name of the Tracked object (Vicon). 
    ///     <param>bodyID</param> ID of the Tracked object (DTRack). 
    ///     <param>DataStream</param> GameObject that carries an Tracking Datastream. 
    ///     Author Jan Klemens
    /// </summary>
    public class TrackingBody : MonoBehaviour
    {
        [Tooltip("Name by which this object is identified, within the Vicon software")]
        public string bodyName = "Object";
        [Tooltip("ID by which this object is identified, within the DTrack software")]
        public int bodyID = 0;
        private GameObject DataStream;
        [Tooltip("Toggle if the objects position should be tracked")]
        public bool applyPosition = true;
        [Tooltip("Toggle if the objects rotation should be tracked")]
        public bool applyRotation = true;

        [SerializeField]
        public TrackingPositionSettings positionConstraints;

        [SerializeField]
        public TrackingRotationSettings rotationConstraints;

        void Start()
        {
            if(PowerwallBuildSettings.Instance.trackingSystem != TrackingSystem.None)
                StartCoroutine(ScriptSetup());
        }

        ///<summary>
        ///     Coroutine that will replace this script on an gameobject with the according Dtrack/Vicon counterpart (if possible).
        ///</summary>
        IEnumerator ScriptSetup(){
            //prevents script from initializing before Datastream
            yield return new WaitForSeconds(1);

            switch (PowerwallBuildSettings.Instance.trackingSystem) {
                case TrackingSystem.Vicon:
                    ViconDataStreamObject viconClient = Resources.Load<ViconDataStreamObject>("ViconDataStreamObject");
                    if (viconClient == null)
                        Debug.LogError("DataStream is not set up.");
                    else {
                        gameObject.AddComponent<UnityVicon.RBScript>();
                        gameObject.GetComponent<UnityVicon.RBScript>().ObjectName = bodyName;
                        gameObject.GetComponent<UnityVicon.RBScript>().Client = viconClient;
                        gameObject.GetComponent<UnityVicon.RBScript>().applyPosition = this.applyPosition;
                        gameObject.GetComponent<UnityVicon.RBScript>().applyRotation = this.applyRotation;
                        gameObject.GetComponent<UnityVicon.RBScript>().positionConstraints = this.positionConstraints;
                        gameObject.GetComponent<UnityVicon.RBScript>().rotationConstraints = this.rotationConstraints;
                        Destroy(gameObject.GetComponent<TrackingBody>());
                    }
                    break;
                case TrackingSystem.DTrack2:
                    DTrack.DtrackObject DTrackClient = Resources.Load<DTrack.DtrackObject>("DTrackObject");
                    if (DTrackClient == null)
                        Debug.Log("DataStream is not set up.");
                    else {
                        gameObject.AddComponent<DTrack.DTrackReceiver6Dof>();
                        gameObject.GetComponent<DTrack.DTrackReceiver6Dof>().client = DTrackClient;
                        gameObject.GetComponent<DTrack.DTrackReceiver6Dof>().bodyId = bodyID;
                        gameObject.GetComponent<DTrack.DTrackReceiver6Dof>().applyPosition = this.applyPosition;
                        gameObject.GetComponent<DTrack.DTrackReceiver6Dof>().applyRotation = this.applyRotation;
                        Destroy(gameObject.GetComponent<TrackingBody>());
                    }
                    break;
            }

        }
    }
}