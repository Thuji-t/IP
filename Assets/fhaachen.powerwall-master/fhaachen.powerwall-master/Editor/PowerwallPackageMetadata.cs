#if XR_MGMT_320
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.XR.Management.Metadata;
using UnityEngine;

namespace FHAachen.XR.Powerwall.Editor
{
    /// <summary>
    ///     Class <c>PowerwallMetadata</c> defines the Metadata of the powerwall plugin. 
    /// </summary>
    internal class PowerwallMetadata : IXRPackage
    {
        /// <summary>
        ///     Class <c>PowerwallPackageMetadata</c> defines the internal Unity Package metadata for the powerwall plugin.
        /// </summary>
        private class PowerwallPackageMetadata : IXRPackageMetadata
        {
            public string packageName => "FH Aachen Powerwall XR Plugin";
            public string packageId => "fh-aachen.powerwall.xrplugin";
            public string settingsType => "FHAachen.XR.Powerwall.PowerwallBuildSettings";

            private static readonly List<IXRLoaderMetadata> s_LoaderMetadata = new List<IXRLoaderMetadata>() { new PowerwallLoaderMetadata() };
            public List<IXRLoaderMetadata> loaderMetadata => s_LoaderMetadata;
        }

        /// <summary>
        ///     Class <c>PowerwallLoaderMetadata</c> defines the Loader of the powerwall plugin.
        /// </summary>
        private class PowerwallLoaderMetadata : IXRLoaderMetadata
        {
            public string loaderName => "Powerwall Loader";
            public string loaderType => "FHAachen.XR.Powerwall.PowerwallLoader";

            private static readonly List<BuildTargetGroup> s_SupportedBuildTargets = new List<BuildTargetGroup>()
            {
                BuildTargetGroup.Standalone,
            };
            public List<BuildTargetGroup> supportedBuildTargets => s_SupportedBuildTargets;
        }

        private static IXRPackageMetadata s_Metadata = new PowerwallPackageMetadata();
        public IXRPackageMetadata metadata => s_Metadata;

        /// <summary>
        ///     Initializes the Powerwall setting instance.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool PopulateNewSettingsInstance(ScriptableObject obj)
        {
            var settings = obj as PowerwallBuildSettings;
            if (settings != null)
            {
                settings.cameraMode = CameraMode.Stereo;
                settings.trackingSystem = TrackingSystem.None;
                settings.initDataStream = false;
                settings.AntiJitter = true;
                settings.AntiJitterThreshold = 0.00005f;

                settings.ViconSettings = new TrackingSettings(
                    "localhost", 
                    801, 
                    "HMD", 
                    "LeftHand", 
                    "RightHand");
                settings.DTrackSettings = new TrackingSettings(
                    "localhost", 
                    5000, 
                    "1", 
                    "2", 
                    "3");
                return true;
            }

            return false;
        }
    }
}
#endif