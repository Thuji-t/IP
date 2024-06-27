using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


public class BimsimDataPoint
{
    public float Celsius;
    public float Humidity;
    public float AirPressure;
    public string datetime;

    public BimsimDataPoint(float celsius, float humidity, float air_pressure, string datetime)
    {
        this.Celsius = celsius;
        this.Humidity = humidity;
        this.AirPressure = air_pressure;
        this.datetime = datetime;
    }
}

