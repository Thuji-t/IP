using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


public class Co2DataPointRaw
{
    public float Celsius;
    public float Humidity;
    public float Co2;
    public string datetime;
    public string serial_number;
    public string room;

    public Co2DataPointRaw(float celsius, float humidity, float co2, string datetime, string serial_number, string room)
    {
        this.Celsius = celsius;
        this.Humidity = humidity;
        this.Co2 = co2;
        this.datetime = datetime;
        this.serial_number = serial_number;
        this.room = room;
    }
}

