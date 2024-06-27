using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BimsimSensor
{
    public List<BimsimDataPoint> Data = new List<BimsimDataPoint>();
    public String SerialNumber;

    public BimsimSensor(string serial_number, List<BimsimDataPoint> data)
    {
        this.SerialNumber = serial_number;
        this.Data = data;
    }
}

