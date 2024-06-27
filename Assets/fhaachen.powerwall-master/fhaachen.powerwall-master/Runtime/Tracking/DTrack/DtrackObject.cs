using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DTrack.DataObjects;
using DTrack.DataObjects.Body;
using DTrack.Parser;
using UnityEngine;

namespace DTrack {
    [CreateAssetMenu(fileName = "DTrackObject", menuName = "ScriptableObjects/DTrackObject")]
    public class DtrackObject : ScriptableObject {
        [Tooltip("Port for incoming DTrack tracking data")]
        public int listenPort = 5000;

        private IPEndPoint EndPoint;
        private UdpClient Client;
        private Thread UpdateThread;
        private Packet CurrentPacket;

        private bool ThreadRunning = false;
        public void Start() {
            try {
                EndPoint = new IPEndPoint(IPAddress.Any, listenPort);
                Client = new UdpClient(EndPoint);
            }
            catch(SocketException sex) {
                Debug.LogError("Exception:" + sex);
                Debug.Log("Please Restart the Application");
            }
            UpdateThread = new Thread(GetPackage);
            ThreadRunning = true;
            UpdateThread.Start();
        }

        public void Stop() {
            if (ThreadRunning) {
                ThreadRunning = false;
                UpdateThread.Abort();
                Client.Close();
            }
        }

        private void GetPackage() {
            while (ThreadRunning) {
                try {
                    if(Client != null) {
                        byte[] result = Client.Receive(ref EndPoint);
                        string rawString = Encoding.UTF8.GetString(result);
                        CurrentPacket = RawParser.Parse(rawString);
                    }
                }
                catch (ObjectDisposedException ex) {
                    Debug.Log("Exception :" + ex);
                }
                catch (ThreadAbortException tex) {
                    if (tex != null)
                        ThreadRunning = false;
                }
            }
        }

        public Vector3 GetPosition(int bodyID) {
            if(ThreadRunning && CurrentPacket != null && CurrentPacket.Body6D != null) {
                try {
                    Body6Dof body;
                    if (CurrentPacket.Body6D.TryGetValue(bodyID - 1, out body))
                        return body.GetPosition();
                    else
                        return Vector3.zero;
                }
                catch (Exception ex) {
                    Debug.Log("Error while receiving Position: " + ex);
                    return Vector3.zero;
                }
            }
            else
                return Vector3.zero;
        }
        public Quaternion GetRotation(int bodyID) {
            if (ThreadRunning && CurrentPacket != null && CurrentPacket.Body6D != null) {
                try {
                    Body6Dof body;
                    if (CurrentPacket.Body6D.TryGetValue(bodyID - 1, out body))
                        return body.GetRotation();
                    else
                        return Quaternion.identity;
                }
                catch (Exception ex) {
                    Debug.Log("Error while receiving Position: " + ex);
                    return Quaternion.identity;
                }
            }
            else
                return Quaternion.identity;
        }
    }
}