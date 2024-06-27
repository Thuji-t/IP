using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;

using UnityEngine;
//using UnityEngine.Networking.Types;
using UnityEngine.Rendering;

public class BimsimManager : MonoBehaviour
{
    private DrillProxyReceiver Receiver;
    private string DataJson;
    public BimsimData BimsimData;
    public Co2Data Co2Data;
    public DateTime lastFetch = new DateTime();
    public EventHandler Fetching;
    public EventHandler FetchCompleted;
    public bool CO2;

    [SerializeField]
    private int refreshDataAfterSeconds = 300;
    [SerializeField]
    private bool refreshData = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (refreshData)
        {
            InvokeRepeating("RefreshDataAsync", 1, refreshDataAfterSeconds);
        }
        
    }

    public BimsimManager()
    {
        print("i did get built");
    }

    public async void RefreshDataAsync() {       
        try
        {
            this.Fetching.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            print(e);
        }
        

        string exampleQuery = prepare_query().Replace("\n", "").Replace("\r", ""); ;

        var result = await DrillProxyReceiverSingleton.Instance.RawQueryAsync(exampleQuery);
        this.DataJson = result;
        
        if (this.CO2) 
        { 
            this.Co2Data = Co2Data.fromJson(this.DataJson);
        }
        else 
        { 
            this.BimsimData = BimsimData.fromJson(this.DataJson);
        }
        
        lastFetch = DateTime.Now;

        try
        {
            this.FetchCompleted.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e) { print(e);}       
    }

    private string prepare_query() { 
        
        if (this.CO2 )
        {
            return @"SELECT co2, 
                humidity, 
                celsius, 
                source as serial_number, 
                `datetime`, 
                l.room 
                FROM dfs.bimsim.`bimsim_co2_data_current` as d 
                JOIN dfs.bimsim.sensor_locations as l 
                ON l.sensor = d.source 
                WHERE `version` LIKE '3.0' 
                AND `datetime` >= '2023-07-01 00:00:00' 
                ORDER BY `datetime` DESC 
                LIMIT 10000";
        }
        else
        {
            return @"SELECT d.celsius, 
            d.`datetime`, 
            d.humidity, 
            d.pressure as air_pressure, 
            l.room, 
            d.source as serial_number 
            FROM dfs.bimsim.bimsim_sensor_locations as l 
            JOIN dfs.bimsim.bimsim_calculated_data_v2 as d 
            ON l.`sensor` = d.`source` 
            WHERE `datetime` > '2023-07-01'  
            order by `datetime` DESC 
            LIMIT 10000
            ".Replace("\n", "").Replace("\r", ""); ;
        }
    }
}
