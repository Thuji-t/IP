using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FHAachen.XR.Powerwall.Behaviour 
{
    /// <summary>
    ///     Class <c>TrackingBody</c> is used as a general representation of a Tracking-DataStream.
    ///     After starting the game, this script will remove itself from a gameobject and replace itself with either the according Vicon or DTrack scipt, 
    ///         while passing the necessary parameters along.
    ///     <param>host</param> IP-Adress at which the data will be expected (Vicon). 
    ///     <param>port</param> Port at which the data will be expected (DTRack/Vicon). 
    ///     Author Jan Klemens
    /// </summary>
    public class TrackingDataStream : MonoBehaviour
    {
        void Start()
        {
            switch (PowerwallBuildSettings.Instance.trackingSystem) {
                case TrackingSystem.Vicon:
                    gameObject.AddComponent<ViconDataStreamClient>();
                    gameObject.GetComponent<ViconDataStreamClient>().HostName = PowerwallBuildSettings.Instance.ViconSettings.host;
                    gameObject.GetComponent<ViconDataStreamClient>().Port = PowerwallBuildSettings.Instance.ViconSettings.port.ToString();
                    Destroy(gameObject.GetComponent<TrackingDataStream>());
                    break;
                case TrackingSystem.DTrack2:
                    gameObject.AddComponent<DTrack.DTrack>();
                    gameObject.GetComponent<DTrack.DTrack>().listenPort = PowerwallBuildSettings.Instance.DTrackSettings.port;
                    Destroy(gameObject.GetComponent<TrackingDataStream>());
                    break;
            }
        }
    }
}