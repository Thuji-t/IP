using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BimsimRoom
{
    public Dictionary<String, BimsimSensor> Sensors = new Dictionary<String, BimsimSensor>();
    public String Name;

    public BimsimRoom(string name, Dictionary<string, BimsimSensor> sensors) { 
        this.Name = name;
        Sensors = sensors;

    }
}

