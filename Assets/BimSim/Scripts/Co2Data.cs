using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;


public class Co2Data
{
    public List<Co2Room> Rooms = new List<Co2Room>();
    public Co2Data(List<Co2Room> rooms) { 
        this.Rooms = rooms;
    }

    public static Co2Data fromJson(string dataString)
    {
        Co2DataRaw rawData = JsonConvert.DeserializeObject<Co2DataRaw>(dataString);

        Dictionary<string, Co2Room> rooms = new Dictionary<string, Co2Room>();
        Dictionary<string, Co2Sensor> sensors = new Dictionary<string, Co2Sensor>();


        for (int i = 0; i < rawData.Celsius.Count; i++)
        {
            if (!rooms.ContainsKey(rawData.Room[i]))
                rooms[rawData.Room[i]] = new Co2Room(rawData.Room[i], new Dictionary<string, Co2Sensor>());

            if (!sensors.ContainsKey(rawData.Serial_Number[i]))
                sensors[rawData.Serial_Number[i]] = new Co2Sensor(rawData.Serial_Number[i], new List<Co2DataPoint>());

            sensors[rawData.Serial_Number[i]].Data.Add(new Co2DataPoint(rawData.Celsius[i], rawData.Humidity[i], rawData.Co2[i], rawData.DateTime[i]));
            rooms[rawData.Room[i]].Co2Sensors[rawData.Serial_Number[i]] = sensors[rawData.Serial_Number[i]];
        }

        Co2Data bimsimData = new Co2Data(rooms.Values.ToList());
        return bimsimData;

    }
}
