using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject cube;
    public Vector3 offset = new Vector3(0, 2, -5);

    // Start is called before the first frame update
    void Start()
    { 
        
        
    }

    // Update is called once per frame
    void Update()
    {

       // transform.position = cube.transform.position; //hier sieht man cube nicht
        transform.position = cube.transform.position + new Vector3(0,2,-5);
       // transform.position = cube.transform.position + new Vector3(0,2,-15); //hier sieht man cube
        //transform.rotation = cube.transform.rotation;
    }
}
