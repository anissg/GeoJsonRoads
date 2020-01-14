using System.Collections;
using System.Collections.Generic;
using GeoJSON.Net;
using GeoJSON.Net.Converters;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

public class GeoJsonRoadLoader : MonoBehaviour
{
    [SerializeField] float scale;
    [SerializeField] Material material;
    [SerializeField] private string resourceFileName;
    [SerializeField] private Vector3 refPoint;
    private string _json;
    private FeatureCollection _geoJsonFeatureCollection;
    private double altitude;

    void Start()
    {
        _json = Resources.Load<TextAsset>(resourceFileName)?.text;
        _geoJsonFeatureCollection = JsonConvert.DeserializeObject<FeatureCollection>(_json, new GeoJsonConverter());

        GameObject roads = new GameObject("Routes");

        foreach (Feature troncon in _geoJsonFeatureCollection.Features)
        {
            GameObject road = DrawTroncon(troncon);
            road.transform.parent = roads.transform;
        }

    }

    GameObject DrawTroncon(Feature feature)
    {
        MultiLineString itineraire = feature.Geometry as MultiLineString;
        string id_itineraire = feature.Properties["ID"].ToString();
        double largeur_itineraire = (double) feature.Properties["LARGEUR"];
        
        GameObject road = new GameObject(id_itineraire);
        RoadCreator roadCreator = road.AddComponent<RoadCreator>();
        roadCreator.spacing = 1f;
        roadCreator.roadWidth = (float)largeur_itineraire;
        roadCreator.material = material;
        roadCreator.tiling = 2;
        Path path = road.GetComponent<Path>();
        //path.AutoSetControlPoints = true;
        path.points = new List<Vector3>();

        foreach (LineString troncon in itineraire.Coordinates)
        {
            foreach (IPosition position in troncon.Coordinates)
            {
                if (position.Altitude.HasValue)
                    if (position.Altitude.Value < 250)
                        altitude = position.Altitude.Value;

                path.AddSegment(GeoUtils.GeoToWorldPosition(position.Latitude, position.Longitude,
                    altitude, refPoint, scale));
            }
        }

        return road;
    }
}
