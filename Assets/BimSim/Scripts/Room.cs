using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public string Name;
    public BimsimRoomDataList bimsimRoomDataList;
    public Co2RoomDataList co2RoomDataList;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Player entered room " + Name);
            bimsimRoomDataList.FillView();
            co2RoomDataList.FillView();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Player left room " + Name);
            bimsimRoomDataList.ClearView();
            co2RoomDataList.ClearView();
        }
    }
}
