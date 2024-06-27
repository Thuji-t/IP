using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;
#endif


namespace FHAachen.XR.Powerwall {
    /// <summary>
    ///     Loader for FH Aachen Powerwall Package.
    /// </summary>
    public class PowerwallLoader : XRLoaderHelper {
        private static List<XRInputSubsystemDescriptor> s_InputSubsystemDescriptors = new List<XRInputSubsystemDescriptor>();
        private static List<XRDisplaySubsystemDescriptor> s_DisplaySubsystemDescriptors = new List<XRDisplaySubsystemDescriptor>();
        private ViconDataStreamObject viconDataStreamObject;
        private DTrack.DtrackObject dtrackObject;

        /// <summary>
        ///     Initializes the input subsystem.
        /// </summary>
        /// <returns>true if the subsystem was initialized sucessfully</returns>
        public override bool Initialize() {
            CreateSubsystem<XRInputSubsystemDescriptor, XRInputSubsystem>(s_InputSubsystemDescriptors, "PowerwallTracking");

            if(PowerwallBuildSettings.Instance.initDataStream) {
                switch (PowerwallBuildSettings.Instance.trackingSystem) {
                    case TrackingSystem.Vicon:
                        viconDataStreamObject = Resources.Load<ViconDataStreamObject>("ViconDataStreamObject");
                        break;
                    case TrackingSystem.DTrack2:
                        dtrackObject = Resources.Load<DTrack.DtrackObject>("DTrackObject");
                        break;
                }
            }

#if ENABLE_INPUT_SYSTEM
            InputSystem.RegisterLayout<PowerwallController>(
                matches: new InputDeviceMatcher()
                    .WithInterface(XRUtilities.InterfaceMatchAnyVersion)
                    .WithProduct("PowerwallLeftController"));
            InputSystem.RegisterLayout<PowerwallController>(
                matches: new InputDeviceMatcher()
                    .WithInterface(XRUtilities.InterfaceMatchAnyVersion)
                    .WithProduct("PowerwallRightController"));
#endif

            return true;
        }

        /// <summary>
        ///     Starts the input subsystem.
        /// </summary>
        /// <returns>true if the subsystem was started sucessfully</returns>
        public override bool Start() {
            Powerwall.SetTrackingSystem(PowerwallBuildSettings.Instance.trackingSystem);
            switch (PowerwallBuildSettings.Instance.trackingSystem) {
                case TrackingSystem.Vicon:
                    Powerwall.SetHost(PowerwallBuildSettings.Instance.ViconSettings.host, PowerwallBuildSettings.Instance.ViconSettings.port.ToString());
                    Powerwall.SetHMDName(PowerwallBuildSettings.Instance.ViconSettings.HMD_Name);
                    Powerwall.SetLeftControllerName(PowerwallBuildSettings.Instance.ViconSettings.LeftHand_Name);
                    Powerwall.SetRightControllerName(PowerwallBuildSettings.Instance.ViconSettings.RightHand_Name);
                    break;
                case TrackingSystem.DTrack2:
                    Powerwall.SetHost(PowerwallBuildSettings.Instance.DTrackSettings.host, PowerwallBuildSettings.Instance.DTrackSettings.port.ToString());
                    Powerwall.SetHMDName(PowerwallBuildSettings.Instance.DTrackSettings.HMD_Name);
                    Powerwall.SetLeftControllerName(PowerwallBuildSettings.Instance.DTrackSettings.LeftHand_Name);
                    Powerwall.SetRightControllerName(PowerwallBuildSettings.Instance.DTrackSettings.RightHand_Name);
                    break;
            }
            Powerwall.SetAntiJitterActive(PowerwallBuildSettings.Instance.AntiJitter);
            Powerwall.SetAntiJitterThreshold(PowerwallBuildSettings.Instance.AntiJitterThreshold);

            StartSubsystem<XRInputSubsystem>();

            if (PowerwallBuildSettings.Instance.initDataStream) {
                switch (PowerwallBuildSettings.Instance.trackingSystem) {
                    case TrackingSystem.Vicon:
                        viconDataStreamObject.HostName = PowerwallBuildSettings.Instance.ViconSettings.host;
                        viconDataStreamObject.Port = PowerwallBuildSettings.Instance.ViconSettings.port.ToString();
                        viconDataStreamObject.Start();
                        break;
                    case TrackingSystem.DTrack2:
                        dtrackObject.listenPort = PowerwallBuildSettings.Instance.DTrackSettings.port;
                        dtrackObject.Start();
                        break;
                }
            }

            return true;
        }

        /// <summary>
        ///     Stops the input subsystem.
        /// </summary>
        /// <returns>true if the subsystem was stoped sucessfully</returns>
        public override bool Stop() {
            if (PowerwallBuildSettings.Instance.initDataStream) {
                switch (PowerwallBuildSettings.Instance.trackingSystem) {
                    case TrackingSystem.Vicon:
                        viconDataStreamObject.Stop();
                        break;
                    case TrackingSystem.DTrack2:
                        dtrackObject.Stop();
                        break;
                }
            }

            StopSubsystem<XRInputSubsystem>();
            return true;
        }

        /// <summary>
        ///     Deinitializes the input subsystem.
        /// </summary>
        /// <returns>true if the subsystem was deinitialized sucessfully</returns>
        public override bool Deinitialize() {
            DestroySubsystem<XRInputSubsystem>();
            return true;
        }
    }
}
