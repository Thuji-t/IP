﻿/* Copyright (c) 2019, Advanced Realtime Tracking GmbH
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 * 
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the distribution.
 * 3. Neither the name of copyright holder nor the names of its contributors
 *    may be used to endorse or promote products derived from this software
 *    without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DTrack.DataObjects;
using DTrack.Parser;
using UnityEngine;

using System.Threading.Tasks;

namespace DTrack
{
    public class DTrack : MonoBehaviour
    {
        [Tooltip("Port for incoming DTrack tracking data")]
        public int listenPort = 5000;
        [Tooltip("Game objects receiving tracking data from DTrack")]
        public GameObject[] receivers = new GameObject[0];

        private IPEndPoint _endPoint;
        private UdpClient _client;
        private Thread _thread;

        private Packet _currentPacket;

        private bool _runReceiveThread = true;

        void Start()
        {
            _endPoint = new IPEndPoint(IPAddress.Any, listenPort);
            _client = new UdpClient(_endPoint);

            _thread = new Thread(() =>
            {
                while (_runReceiveThread)
                {
                    try
                    {
                        var result = _client.Receive(ref _endPoint);
                        var rawString = Encoding.UTF8.GetString(result);
                        _currentPacket = RawParser.Parse(rawString);
                    }
                    catch (ObjectDisposedException ex) 
                    {
                        Debug.Log("Exception :" + ex);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Parsing Error: " + e);
                    }
                }
            });
            _thread.Start();
        }

        public void RegisterTarget(GameObject obj)
        {
            var objs = new List<GameObject>(receivers);
            objs.Add(obj);
            receivers = objs.ToArray();
        }

        public void UnregisterTarget(GameObject obj)
        {
            var objs = new List<GameObject>(receivers);
            objs.Remove(obj);
            receivers = objs.ToArray();
        }

        void OnDestroy()
        {
            _runReceiveThread = false;
            _thread.Abort();
            _client.Close();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_currentPacket != null)
            {
                foreach (var i in receivers)
                {
                    try
                    {
                        i.GetComponent<IDTrackReceiver>().ReceiveDTrackPacket(_currentPacket);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Error passing Packet: " + e);
                    }
                }
            }
        }
    }
}
