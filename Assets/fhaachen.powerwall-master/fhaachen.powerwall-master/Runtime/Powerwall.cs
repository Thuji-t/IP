using System.Runtime.InteropServices;

namespace FHAachen.XR.Powerwall {
    /// <summary>
    ///     Class <c>Powerwall</c> is used to controll the Input Subsystem.
    /// </summary>
    public static class Powerwall {
        /// <summary>
        ///     Sets the <paramref name="Host"/> and <paramref name="Post"/> of the vicon datastream in the input subsystem.
        /// </summary>
        /// <param name="Host">Hostadress of the datastream</param>
        /// <param name="Post">Port of the datastream</param>
        [DllImport("PowerwallTracking")]
        public static extern void SetHost(string Host, string Post);

        /// <summary>
        ///     Sets <paramref name="Name"/> in the input subsystem.
        /// </summary>
        /// <param name="Name">Name of the HMD-object</param>
        [DllImport("PowerwallTracking")]
        public static extern void SetHMDName(string Name);

        /// <summary>
        ///     Sets <paramref name="Name"/> in the input subsystem.
        /// </summary>
        /// <param name="Name">Name of the LeftHand-object</param>
        [DllImport("PowerwallTracking")]
        public static extern void SetLeftControllerName(string Name);

        /// <summary>
        ///     Sets <paramref name="Name"/> in the input subsystem.
        /// </summary>
        /// <param name="Name">Name of the RightHand-object</param>
        [DllImport("PowerwallTracking")]
        public static extern void SetRightControllerName(string Name);

        /// <summary>
        ///     Enables/Disables the tracking capabilities of the input subsystem.
        /// </summary>
        /// <param name="active">Bool(true if tracking active)</param>
        [DllImport("PowerwallTracking", CharSet = CharSet.Ansi)]
        public static extern void SetTrackingSystem(TrackingSystem system);

        /// <summary>
        ///     Enables/Disables the anti jitter capabilities of the input subsystem.
        /// </summary>
        /// <param name="active">Bool(true if anti jitter active)</param>
        [DllImport("PowerwallTracking")]
        public static extern void SetAntiJitterActive(bool active);

        /// <summary>
        ///     Sets <paramref name="value"/> in the input subsystem.
        /// </summary>
        /// <param name="value">Anti Jitter Threshold</param>
        [DllImport("PowerwallTracking")]
        public static extern void SetAntiJitterThreshold(float value);
    }
}

