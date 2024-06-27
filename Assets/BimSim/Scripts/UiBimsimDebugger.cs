using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiBimsimDebugger : MonoBehaviour
{
    public BimsimManager DrillProxyDebugger;

    [SerializeField]
    private TMPro.TMP_Text _lastFetched;
    [SerializeField]
    private TMPro.TMP_Text _rooms;
    [SerializeField]
    private TMPro.TMP_Text _fetching;
    [SerializeField]
    private bool Co2;

    [SerializeField]
    private RawImage _bufferWheel;

    private float rotationSpeed = -360f; // Degrees per second

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _lastFetched.text = "Last fetch: " + DrillProxyDebugger.lastFetch.ToString();

        // Rotate the image around the Z-axis
        _bufferWheel.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        DrillProxyDebugger.FetchCompleted += (sender, args) => { on_fetch_complete(); };
        DrillProxyDebugger.Fetching += (sender, args) => { on_fetching(); };
    }

    private void on_fetch_complete() {
        //fill rooms
        string txt = "";

        if (this.Co2)
        {
            foreach (var room in DrillProxyDebugger.Co2Data.Rooms)
            {
                int roomDatapointCount = 0;
                foreach (var sensor in room.Co2Sensors.Values)
                {
                    roomDatapointCount = roomDatapointCount + sensor.Data.Count;
                }
                txt += room.Name + ": " + room.Co2Sensors.Count.ToString() + " sensors, " + roomDatapointCount.ToString() + " datapoints" + '\n';
            }
            
        }
        else
        {
            foreach (var room in DrillProxyDebugger.BimsimData.Rooms)
            {
                int roomDatapointCount = 0;
                foreach (var sensor in room.Sensors.Values)
                {
                    roomDatapointCount = roomDatapointCount + sensor.Data.Count;
                }
                txt += room.Name + ": " + room.Sensors.Count.ToString() + " sensors, " + roomDatapointCount.ToString() + " datapoints" + '\n';
            }
        }
        _rooms.text = txt;

        // fill fetching
        _fetching.text = "fetching complete!";
        _bufferWheel.gameObject.SetActive(false);
    }

    private void on_fetching() {
        _fetching.text = "currently fecthing data ...";
        _bufferWheel.gameObject.SetActive(true);
    }
}