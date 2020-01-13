using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoUtils
{
    private const int EarthRadius = 6378137; 
    private const double OriginShift = 2 * Mathf.PI * EarthRadius / 2;

    public static Vector3 GeoToWorldPosition(double latitude, double longitude, double altitude, Vector3 refPoint, float scale = 1)
    {
        float posx = (float) (longitude * OriginShift / 180);
        float posy = Mathf.Log(Mathf.Tan((float)(90 + latitude) * Mathf.PI / 360)) / (Mathf.PI / 180);
        posy = (float) (posy * OriginShift / 180);
        return new Vector3((posx - refPoint.x) * scale, (float)altitude, (posy - refPoint.z) * scale);
    }
}
