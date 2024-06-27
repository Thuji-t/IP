using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


public class Co2DataPoint
{
    public float Celsius;
    public float Humidity;
    public float Co2;
    public string datetime;

    public Co2DataPoint(float celsius, float humidity, float co2, string datetime)
    {
        this.Celsius = celsius;
        this.Humidity = humidity;
        this.Co2 = co2;
        this.datetime = datetime;
    }
}

