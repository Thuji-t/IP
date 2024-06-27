using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FHAachen.XR.Powerwall.Behaviour 
{
    /// <summary>
    ///     Class <c>StereoCamera</c> is used to imitate a steroscopic camera in Unity.
    ///     The resulting output is dependant on the number of connected displays:
    ///         1 Screen: The left subcamera will diplay its view on display 1.
    ///         2 Screen: The left subcamera will diplay its view on display 1 and the right subcamera will display its view on display 2.
    ///         3 Screen: The left subcamera will diplay its view on display 2 and the right subcamera will display its view on display 3.
    ///     <param>eyeDistance</param> Factor that controlls the distance between the subcameras. 
    ///     <param>leftEye</param> The left subcamera. 
    ///     <param>rightEye</param> The right subcamera. 
    ///     Author Jan Klemens
    /// </summary>
    public class StereoCamera : MonoBehaviour
    {    
        public float eyeDistance = 0.2f;   
        private GameObject leftEye = null;
        private GameObject rightEye = null;

        void Start()
        {
            leftEye = GameObject.Find("Camera_Left");
            rightEye = GameObject.Find("Camera_Right");
            initScreens();
            SetEyeDistance(eyeDistance);
        }

        ///<summary>
        ///     Detects the number of connected displays and maps the subcameras to the displays as mentioned above.
        ///</summary>
        private void initScreens(){
            Debug.Log ("displays connected: " + Display.displays.Length);    
            for (int i = 1; i < Display.displays.Length; i++) Display.displays[i].Activate();
            if(Display.displays.Length == 3 ){
                leftEye.GetComponent<Camera>().targetDisplay = 1;
                rightEye.GetComponent<Camera>().targetDisplay = 2;
            }
        }

        ///<summary>
        ///     Places the subcameras acording to the given eye distance.
        ///     <param>eyeDistance</param> The new Distance between the two subcameras.
        ///</summary>
        private void SetEyeDistance(float eyeDistance){
            leftEye.transform.localPosition = new Vector3(-(eyeDistance/2), 0, 0);
            rightEye.transform.localPosition = new Vector3((eyeDistance/2), 0, 0);
        }
    }
}