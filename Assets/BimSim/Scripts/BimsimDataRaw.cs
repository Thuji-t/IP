using System.Collections.Generic;
using System.Numerics;

public class BimsimDataRaw
{
    public Dictionary<int, string> DateTime;
    //public Dictionary<string, int> Age;
    public Dictionary<int, float> Celsius;
    public Dictionary<int, float> Air_Pressure;
    public Dictionary<int, float> Humidity;
    public Dictionary<int, string> Serial_Number;
    public Dictionary<int, string> Room;

    public BimsimDataRaw()
    {

    }
}