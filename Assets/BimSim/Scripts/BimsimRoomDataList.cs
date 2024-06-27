using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class BimsimRoomDataList : MonoBehaviour
{
    public BimsimManager BimsimManager;
    public string RoomName = "H217";
    [SerializeField]
    private TMPro.TMP_Text _lastFetched;
    [SerializeField]
    private TMPro.TMP_Text _dataView;
    [SerializeField]
    private TMPro.TMP_Text _fetching;

    [SerializeField]
    private RawImage _bufferWheel;

    private float rotationSpeed = -360f; // Degrees per second


    private void Awake()
    {
        _bufferWheel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _lastFetched.text = "Last fetch: " + BimsimManager.lastFetch.ToString();

        BimsimManager.FetchCompleted += (sender, args) => { on_fetch_complete(); };
        BimsimManager.Fetching += (sender, args) => { on_fetching(); };

        //Bufferwheel
        if (!_bufferWheel.gameObject.activeSelf) { return; }
        // Rotate the image around the Z-axis
        _bufferWheel.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime); 
    }

    public void FillView()
    {
        if (BimsimManager.BimsimData == null) { print("no bimsim data.");  return; }

        //fill rooms
        string txt = "";
        foreach (var room in BimsimManager.BimsimData.Rooms.Where(r => r.Name == this.RoomName))
        {
            string head_display = "";
            int head_display_count = 0;

            foreach (var sensor in room.Sensors.Values)
            {
                foreach (var d in sensor.Data)
                {
                    if (head_display_count > 10) { break; }
                    head_display += "{datetime: " + d.datetime +
                        ", celsius: " + d.Celsius +
                        ", humidity: " + d.Humidity +
                        ", air pressure: " + d.AirPressure +
                        "} \n";
                    head_display_count++;
                }
            }


            txt += "example data: \n";
            txt += head_display + "\n";
        }
        _dataView.text = txt;
    }

    public void ClearView()
    {
        _dataView.text = "";
    }

    private void on_fetch_complete()
    {
        // fill fetching
        _fetching.text = "fetching complete!";
        _bufferWheel.gameObject.SetActive(false);
    }

    private void on_fetching()
    {
        _fetching.text = "currently fecthing data ...";
        _bufferWheel.gameObject.SetActive(true);
    }
}
