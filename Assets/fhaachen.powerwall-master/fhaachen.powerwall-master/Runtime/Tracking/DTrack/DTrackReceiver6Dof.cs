/* Copyright (c) 2019, Advanced Realtime Tracking GmbH
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
using DTrack.DataObjects;
using DTrack.DataObjects.Body;
using UnityEngine;

namespace DTrack
{
    public class DTrackReceiver6Dof : MonoBehaviour, IDTrackReceiver
    {
        [Tooltip("Enter Body ID as seen in DTrack")]
        public int bodyId;
        [Tooltip("Current value of DTrack frame counter")]
        public long frame;

        [Tooltip("Update position of this 6dof target")]
        public bool applyPosition = true;
        [Tooltip("Update rotation of this 6dof target")]
        public bool applyRotation = true;

        private Vector3 lastPosition = new Vector3(0, 0, 0);
        private Quaternion initialRotation;

        public DtrackObject client = null;

        public void ReceiveDTrackPacket(Packet packet) {
            throw new NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {
            initialRotation = gameObject.transform.localRotation;
        }

        // Update is called once per frame
        void Update()
        {
            if (applyPosition) {
                transform.localPosition += client.GetPosition(bodyId) - lastPosition;
                lastPosition = client.GetPosition(bodyId);
            }

            if (applyRotation) 
                transform.rotation = client.GetRotation(bodyId) * initialRotation;
        }
    }
}
