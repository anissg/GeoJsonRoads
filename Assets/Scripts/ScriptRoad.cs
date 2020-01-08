using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathCreator))]
[RequireComponent(typeof(RoadCreator))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ScriptRoad : MonoBehaviour
{
    void Start()
    {
        Path path = GetComponent<PathCreator>().path;
        path.points = new List<Vector3>
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 3, -1),
            new Vector3(0, 3, -1),
            new Vector3(0, 3, -1),
            new Vector3(3, 3, 0),
            new Vector3(3, 3, 0)
        };
    }

    void Update()
    {

    }
}
