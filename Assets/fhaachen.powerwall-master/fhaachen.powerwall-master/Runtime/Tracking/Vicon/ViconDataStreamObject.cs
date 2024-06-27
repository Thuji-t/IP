using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using ViconDataStreamSDK.CSharp;

[CreateAssetMenu(fileName = "ViconDataStreamObject", menuName = "ScriptableObjects/ViconDataStreamObject")]
public class ViconDataStreamObject : ScriptableObject {
    [Tooltip("The hostname or ip address of the Datastream server.")]
    public string HostName = "localhost";

    [Tooltip("The Datastream port number. Typically 804 for the low latency stream and 801 if off-line review is required.")]
    public string Port = "801";

    [Tooltip("Enter a comma separated list of subjects that are required in the stream. If empty, all subjects will be transmitted.")]
    public string SubjectFilter;

    [Tooltip("Switches to the pre-fetch streaming mode, which will request new frames from the server as required while minimizing latency, rather than all frames being streamed. This can potentially help minimise the disruption of data delivery lags on the network. See the datastream documentation for more details of operation.")]
    public bool UsePreFetch = false;

    private ViconDataStreamSDK.CSharp.Client Client;

    private bool UseLightweightData = true;
    private bool GetFrameThread = true;
    private static bool Connected = false;
    //private bool SubjectFilterSet = false;
    private bool ThreadRunnning = false;
    private Thread UpdateThread;


    public static void OnConnected(bool i_bConnected) {
        Connected = i_bConnected;
    }
    public delegate void ConnectionCallback(bool i_bConnected);
    ConnectionCallback ConnectionHandler = OnConnected;

    public void Start() {
        Client = new ViconDataStreamSDK.CSharp.Client();
        Debug.Log("ViconDataStream starting...");
        Output_GetVersion OGV = Client.GetVersion();
        Debug.Log("Using Datastream version " + OGV.Major + "." + OGV.Minor + "." + OGV.Point + "." + OGV.Revision);
        UpdateThread = new Thread(ConnectClient);
        UpdateThread.Start();
    }
    public void Stop() {
        if (ThreadRunnning) {
            ThreadRunnning = false;
            UpdateThread.Join();
        }
    }


    private void ConnectClient() {
        ThreadRunnning = true;

        // We have to handle the multi-route syntax, which is of the form HostName1:Port;Hostname2:Port
        String CombinedHostnameString = "";
        String[] Hosts = HostName.Split(';');
        foreach (String Host in Hosts) {
            String TrimmedString = Host.Trim();
            String HostWithPort = null;

            // Check whether the hostname already contains a port and add if it doesn't
            if (TrimmedString.Contains(":")) {
                HostWithPort = TrimmedString;
            }
            else {
                HostWithPort = TrimmedString + ":" + Port;
            }

            if (!String.IsNullOrEmpty(CombinedHostnameString)) {
                CombinedHostnameString += ";";
            }

            CombinedHostnameString += HostWithPort;
        }

        Debug.Log("Connecting to " + CombinedHostnameString + "...");

        while (ThreadRunnning && !Client.IsConnected().Connected) {
            Output_Connect OC = Client.Connect(CombinedHostnameString);
            Debug.Log("Connect result: " + OC.Result);
            Thread.Sleep(200);
        }

        if (UsePreFetch)
            Client.SetStreamMode(StreamMode.ClientPullPreFetch);
        else
            Client.SetStreamMode(StreamMode.ServerPush);

        // Get a frame first, to ensure we have received supported type data from the server before
        // trying to determine whether lightweight data can be used.
        Client.GetFrame();

        if (!UseLightweightData || Client.EnableLightweightSegmentData().Result != Result.Success)
            Client.EnableSegmentData();

        Client.SetAxisMapping(Direction.Forward, Direction.Left, Direction.Up);
        ConnectionHandler(true);

        while (GetFrameThread && ThreadRunnning)
            Client.GetFrame();

        ThreadRunnning = false;
    }

    public Output_GetSegmentLocalRotationQuaternion GetSegmentRotation(string SubjectName, string SegmentName) {
        if (Client != null)
            return Client.GetSegmentLocalRotationQuaternion(SubjectName, SegmentName);
        else
            return new Output_GetSegmentLocalRotationQuaternion();
    }
    public Output_GetSegmentLocalTranslation GetSegmentTranslation(string SubjectName, string SegmentName) { 
        if(Client != null)
            return Client.GetSegmentLocalTranslation(SubjectName, SegmentName);
        else
            return new Output_GetSegmentLocalTranslation();
    }
    public Output_GetSegmentStaticScale GetSegmentScale(string SubjectName, string SegmentName) { 
        if( Client != null)
            return Client.GetSegmentStaticScale(SubjectName, SegmentName);
        else
            return new Output_GetSegmentStaticScale();
    }
    public Output_GetSubjectRootSegmentName GetSubjectRootSegmentName(string SubjectName) {
        if(Client != null)
            return Client.GetSubjectRootSegmentName(SubjectName);
        else
            return new Output_GetSubjectRootSegmentName();
    }
    public Output_GetSegmentParentName GetSegmentParentName(string SubjectName, string SegmentName) {
        if(Client == null) 
            return Client.GetSegmentParentName(SubjectName, SegmentName);
        else
            return new Output_GetSegmentParentName();
    }

    /// Returns the local translation for a bone, scaled according to its scale and the scale of the bones above it 
    /// in the heirarchy, apart from the root translation
    public Output_GetSegmentLocalTranslation GetScaledSegmentTranslation(string SubjectName, string SegmentName) {
        double[] OutputScale = new double[3];
        OutputScale[0] = OutputScale[1] = OutputScale[2] = 1.0;

        // Check first whether we have a parent, as we don't wish to scale the root node's position
        Output_GetSegmentParentName Parent = GetSegmentParentName(SubjectName, SegmentName);

        string CurrentSegmentName = SegmentName;
        if (Parent.Result == Result.Success) {

            do {
                // We have a parent. First get our scale, and then iterate through the nodes above us
                Output_GetSegmentStaticScale Scale = GetSegmentScale(SubjectName, CurrentSegmentName);
                if (Scale.Result == Result.Success) {
                    for (uint i = 0; i < 3; ++i) {
                        if (Scale.Scale[i] != 0.0) OutputScale[i] = OutputScale[i] * Scale.Scale[i];
                    }
                }

                Parent = GetSegmentParentName(SubjectName, CurrentSegmentName);
                if (Parent.Result == Result.Success) {
                    CurrentSegmentName = Parent.SegmentName;
                }
            } while (Parent.Result == Result.Success);
        }

        Output_GetSegmentLocalTranslation Translation = GetSegmentTranslation(SubjectName, SegmentName);
        if (Translation.Result == Result.Success) {
            for (uint i = 0; i < 3; ++i) {
                Translation.Translation[i] = Translation.Translation[i] / OutputScale[i];
            }
        }
        return Translation;
    }

    public uint GetFrameNumber() {
        if (Client != null)
            return Client.GetFrameNumber().FrameNumber;
        else
            return 0;
    }
}
