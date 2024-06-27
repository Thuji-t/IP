using System.Linq;
using UnityEngine;

public class DrillProxyReceiverSingleton : ScriptableObject
{
    private static DrillProxyReceiver instance;
    public static DrillProxyReceiver Instance
    {
        get
        {
            // Still no instance
            if (instance == null) instance = CreateDefault();
            return instance;
        }
    }
    public static DrillProxyReceiver CreateDefault()
    {
        ConnectionData credentials = new ConnectionData();
        return new DrillProxyReceiver(credentials);
    }
}