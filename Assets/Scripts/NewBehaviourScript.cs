
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PointCloudLoader : MonoBehaviour
{
    public string filePath = "Assets/Trakt 5_-10.ply";
    public Material pointMaterial;
    public float pointSize = 0.1f;

    void Start()
    {
        LoadPointCloud(filePath);
    }

    void LoadPointCloud(string path)
    {
        List<Vector3> points = new List<Vector3>();
        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {
            string[] tokens = line.Split(' ');
            if (tokens.Length >= 3)
            {
                float x = float.Parse(tokens[0]);
                float y = float.Parse(tokens[1]);
                float z = float.Parse(tokens[2]);
                points.Add(new Vector3(x, y, z));
            }
        }

        foreach (Vector3 point in points)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = point;
            sphere.transform.localScale = Vector3.one * pointSize;
            sphere.GetComponent<Renderer>().material = pointMaterial;
        }
    }
}
