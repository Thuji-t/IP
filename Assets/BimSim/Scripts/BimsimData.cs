using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;


public class BimsimData
{
    public List<BimsimRoom> Rooms = new List<BimsimRoom>();
    public BimsimData(List<BimsimRoom> rooms) { 
        this.Rooms = rooms;
    }

    public static BimsimData fromJson(string dataString)
    {
        BimsimDataRaw rawData = JsonConvert.DeserializeObject<BimsimDataRaw>(dataString);

        Dictionary<string, BimsimRoom> rooms = new Dictionary<string, BimsimRoom>();
        Dictionary<string, BimsimSensor> sensors = new Dictionary<string, BimsimSensor>();


        for (int i = 0; i < rawData.Celsius.Count; i++)
        {
            if (!rooms.ContainsKey(rawData.Room[i]))
                rooms[rawData.Room[i]] = new BimsimRoom(rawData.Room[i], new Dictionary<string, BimsimSensor>());

            if (!sensors.ContainsKey(rawData.Serial_Number[i]))
                sensors[rawData.Serial_Number[i]] = new BimsimSensor(rawData.Serial_Number[i], new List<BimsimDataPoint>());

            sensors[rawData.Serial_Number[i]].Data.Add(new BimsimDataPoint(rawData.Celsius[i], rawData.Humidity[i], rawData.Air_Pressure[i], rawData.DateTime[i]));
            rooms[rawData.Room[i]].Sensors[rawData.Serial_Number[i]] = sensors[rawData.Serial_Number[i]];
        }

        BimsimData bimsimData = new BimsimData(rooms.Values.ToList());
        return bimsimData;

    }
}
