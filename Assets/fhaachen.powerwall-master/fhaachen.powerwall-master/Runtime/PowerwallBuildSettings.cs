using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;
using System;

namespace FHAachen.XR.Powerwall
{
    public enum TrackingSystem {
        None = 0,
        Vicon = 1,
        DTrack2 = 2
    };
    public enum CameraMode {
        Stereo,
        Mono
    };

    [Serializable]
    public struct TrackingSettings {
        public string host;
        public int port;
        public string HMD_Name;
        public string LeftHand_Name;
        public string RightHand_Name;

        public TrackingSettings(string host, int port, string HMD_Name, string LeftHand_Name, string RightHand_Name) {
            this.host = host;
            this.port = port;
            this.HMD_Name = HMD_Name;
            this.LeftHand_Name = LeftHand_Name;
            this.RightHand_Name = RightHand_Name;
        }
    }


    /// <summary>
    ///     Build-time settings for FH Aachen Powerwall provider.
    /// </summary>
    [XRConfigurationData("FH-Aachen Powerwall", PowerwallBuildSettings.BuildSettingsKey)]
    public class PowerwallBuildSettings : ScriptableObject
    {
        public const string BuildSettingsKey = "xr.sdk.powerwall.settings";


        public CameraMode cameraMode = CameraMode.Stereo;
        public TrackingSystem trackingSystem = TrackingSystem.Vicon;
        [SerializeField] public TrackingSettings ViconSettings = new TrackingSettings("localhost", 801, "HMD", "LeftHand", "RightHand");
        [SerializeField] public TrackingSettings DTrackSettings = new TrackingSettings("localhost", 5000, "1", "2", "3");
        public bool initDataStream = false;
        public bool AntiJitter = true;
        public float AntiJitterThreshold = 0.00005f;

        /// <summary>
        ///     Runtime access to build settings.
        /// </summary>
        public static PowerwallBuildSettings Instance
        {
            get
            {
                PowerwallBuildSettings settings = null;
                
#if UNITY_EDITOR
                    UnityEngine.Object obj = null;
                    UnityEditor.EditorBuildSettings.TryGetConfigObject(BuildSettingsKey, out obj);
                    if (obj == null || !(obj is PowerwallBuildSettings))
                        return null;
                    settings = (PowerwallBuildSettings) obj;
#else
                    settings = s_RuntimeInstance;
                    if (settings == null)
                        settings = new PowerwallBuildSettings();
#endif
                
                return settings;
            }
        }

#if !UNITY_EDITOR
        /// <summary>
        ///     Static instance that will hold the runtime asset instance we created in our build process.
        /// </summary>
        public static PowerwallBuildSettings s_RuntimeInstance = null;

        void OnEnable()
        {
            s_RuntimeInstance = this;
        }
#endif

    }
}