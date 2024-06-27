using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


public class BimsimDataPointRaw
{
    public float Celsius;
    public float Humidity;
    public float AirPressure;
    public string datetime;
    public string serial_number;
    public string room;

    public BimsimDataPointRaw(float celsius, float humidity, float air_pressure, string datetime, string serial_number, string room)
    {
        this.Celsius = celsius;
        this.Humidity = humidity;
        this.AirPressure = air_pressure;
        this.datetime = datetime;
        this.serial_number = serial_number;
        this.room = room;
    }
}

