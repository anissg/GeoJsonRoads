using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    static private Vector3 posRepere1;
    static private Vector3 posRepere2;
    static private Vector3 latLongRepere1;
    static private Vector3 latLongRepere2;

    public static Vector3 LatLongToVector(double latitude, double longitude, double altitude)
    {
        Vector3 latLongVector = new Vector3((float)latitude, 0f, (float)longitude);

        Vector3 unityVector = new Vector3();
        unityVector.x = (latLongVector.x - latLongRepere1.x) * (posRepere2.x - posRepere1.x) / (latLongRepere2.x - latLongRepere1.x) + posRepere1.x;
        unityVector.y = (float)altitude;
        unityVector.z = (latLongVector.z - latLongRepere1.z) * (posRepere2.z - posRepere1.z) / (latLongRepere2.z - latLongRepere1.z) + posRepere1.z;

        return unityVector;
    }

    public static void setReperes(Vector3 pos1, Vector3 pos2, Vector3 latLong1, Vector3 latLong2)
    {
        posRepere1 = pos1;
        posRepere2 = pos2;
        latLongRepere1 = latLong1;
        latLongRepere2 = latLong2;
    }
}
