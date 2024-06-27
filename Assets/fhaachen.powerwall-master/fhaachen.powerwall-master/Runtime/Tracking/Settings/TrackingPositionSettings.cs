using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FHAachen.XR.Powerwall.Tracking {
    [Serializable]
    public class TrackingPositionSettings {
        public bool xActive;
        public bool yActive;
        public bool zActive;
        public Vector3 scale;
        [Tooltip("Parent to which this object will be tracked relational to")]
        public Transform trackingParent;
        [Tooltip("Scale of relational tracking")]
        public Vector3 scaleToParent;


        public TrackingPositionSettings() {
            xActive = true;
            yActive = true;
            zActive = true;
            scale = Vector3.one;
            trackingParent = null;
            scaleToParent = Vector3.one;
        }
    }
}