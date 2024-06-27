using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FHAachen.XR.Powerwall.Tracking {
    [Serializable]
    public class TrackingRotationSettings {
        public bool xActive;
        public bool yActive;
        public bool zActive;
        public bool xInvert;
        public bool yInvert;
        public bool zInvert;

        public TrackingRotationSettings() {
            this.xActive = true;
            this.yActive = true;
            this.zActive = true;
            this.xInvert = false;
            this.yInvert = false;
            this.zInvert = false;
        }
    }
}