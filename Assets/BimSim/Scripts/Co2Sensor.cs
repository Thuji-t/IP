using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Co2Sensor
{
    public List<Co2DataPoint> Data = new List<Co2DataPoint>();
    public String SerialNumber;

    public Co2Sensor(string serial_number, List<Co2DataPoint> data)
    {
        this.SerialNumber = serial_number;
        this.Data = data;
    }
}

