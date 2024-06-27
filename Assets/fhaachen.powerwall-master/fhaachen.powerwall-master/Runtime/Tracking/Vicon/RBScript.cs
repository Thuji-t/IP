using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ViconDataStreamSDK.CSharp;
using FHAachen.XR.Powerwall.Tracking;

namespace UnityVicon {
    public class RBScript : MonoBehaviour {
        public string ObjectName = "";
        public ViconDataStreamObject Client;

        public bool applyPosition = true;
        public bool applyRotation = true;

        public TrackingPositionSettings positionConstraints;
        public TrackingRotationSettings rotationConstraints;

        private Quaternion m_LastGoodRotation;
        private Vector3 m_LastGoodPosition;
        private bool m_bHasCachedPose = false;

        private Vector3 lastPosition = new Vector3(0, 0, 0);
        private Quaternion initialRotation;

        public RBScript() {
        }

        void Start() {
            initialRotation = gameObject.transform.localRotation;
        }

        void Update() {

            Output_GetSubjectRootSegmentName OGSRSN = Client.GetSubjectRootSegmentName(ObjectName);
            string SegRootName = OGSRSN.SegmentName;

            // UNITY-49 - Don't apply root motion to parent object
            Transform Root = transform;
            if (Root == null) {
                throw new Exception("fbx doesn't have root");
            }

            Output_GetSegmentLocalRotationQuaternion ORot = Client.GetSegmentRotation(ObjectName, SegRootName);
            Output_GetSegmentLocalTranslation OTran = Client.GetSegmentTranslation(ObjectName, SegRootName);
            if (ORot.Result == Result.Success && OTran.Result == Result.Success && !OTran.Occluded) {
                // Input data is in Vicon co-ordinate space; z-up, x-forward, rhs.
                // We need it in Unity space, y-up, z-forward lhs
                //           Vicon Unity
                // forward    x     z
                // up         z     y
                // right     -y     x
                // See https://gamedev.stackexchange.com/questions/157946/converting-a-quaternion-in-a-right-to-left-handed-coordinate-system


                if (applyRotation) {
                    float rotX = -(float)ORot.Rotation[0];
                    float rotY = -(float)ORot.Rotation[2];
                    float rotZ = -(float)ORot.Rotation[1];

                    if (!rotationConstraints.xActive)
                        rotX = 0;
                    else if(rotationConstraints.xInvert)
                        rotX *= -1;
                    if (!rotationConstraints.yActive)
                        rotY = 0;
                    else if(rotationConstraints.yInvert)
                        rotY *= -1;
                    if (!rotationConstraints.zActive)
                        rotZ = 0;
                    else if(rotationConstraints.zInvert)
                        rotZ *= -1;

                    Root.localRotation = new Quaternion(rotX, rotY, rotZ, (float)ORot.Rotation[3]);
                    Root.localRotation *= initialRotation;
                }

                if (applyPosition) {
                    Vector3 updatePosition = new Vector3((float)OTran.Translation[0] * 0.001f * positionConstraints.scale.x, (float)OTran.Translation[2] * 0.001f * positionConstraints.scale.y, (float)OTran.Translation[1] * 0.001f * positionConstraints.scale.z);

                    if (positionConstraints.trackingParent != null && positionConstraints.scaleToParent.x != 0 && positionConstraints.scaleToParent.y != 0 && positionConstraints.scaleToParent.z != 0) {
                        Vector3 diff = updatePosition - positionConstraints.trackingParent.localPosition;
                        diff = new Vector3(diff.x * positionConstraints.scaleToParent.x, diff.y * positionConstraints.scaleToParent.y, diff.z * positionConstraints.scaleToParent.z);
                        updatePosition = positionConstraints.trackingParent.localPosition + diff;
                    }

                    if (!positionConstraints.xActive)
                        updatePosition = new Vector3(0, updatePosition.y, updatePosition.z);
                    if (!positionConstraints.yActive)
                        updatePosition = new Vector3(updatePosition.x, 0, updatePosition.z);
                    if (!positionConstraints.zActive)
                        updatePosition = new Vector3(updatePosition.x, updatePosition.y, 0);

                    Root.localPosition += updatePosition - lastPosition;
                    lastPosition = updatePosition;
                }


                m_LastGoodPosition = Root.localPosition;
                m_LastGoodRotation = Root.localRotation;
                m_bHasCachedPose = true;
            }
            else {
                if (m_bHasCachedPose) {
                    Root.localRotation = m_LastGoodRotation;
                    Root.localPosition = m_LastGoodPosition;
                }
            }

        }
    } //end of program
}// end of namespace

