using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Co2Room
{
    public Dictionary<String, Co2Sensor> Co2Sensors = new Dictionary<String, Co2Sensor>();
    public String Name;

    public Co2Room(string name, Dictionary<string, Co2Sensor> sensors) { 
        this.Name = name;
        Co2Sensors = sensors;
    }
}

