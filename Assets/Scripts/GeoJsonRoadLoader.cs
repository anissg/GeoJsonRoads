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

    void Start()
    {
        _json = Resources.Load<TextAsset>(resourceFileName)?.text;
        _geoJsonFeatureCollection = JsonConvert.DeserializeObject<FeatureCollection>(_json, new GeoJsonConverter());

        GameObject roads = new GameObject("Routes");
        //roads.transform.eulerAngles = new Vector3(90, roads.transform.eulerAngles.y, roads.transform.eulerAngles.z);

        GameObject road1 = DrawTroncon(_geoJsonFeatureCollection.Features[0]);
        road1.transform.parent = roads.transform;
        //GameObject road2 = DrawTroncon(_geoJsonFeatureCollection.Features[1]);
        //road2.transform.parent = roads.transform;
        //GameObject road3 = DrawTroncon(_geoJsonFeatureCollection.Features[2]);
        //road3.transform.parent = roads.transform;
        GameObject road4 = DrawTroncon(_geoJsonFeatureCollection.Features[3]);
        road4.transform.parent = roads.transform;

        //foreach (Feature troncon in _geoJsonFeatureCollection.Features)
        //{
        //    GameObject road = DrawTroncon(troncon);
        //    road.transform.parent = roads.transform;
        //}

    }

    GameObject DrawTroncon(Feature feature)
    {
        MultiLineString itineraire = feature.Geometry as MultiLineString;
        string nom_itineraire = feature.Properties["NOM_ITI"].ToString();
        double largeur_itineraire = (double) feature.Properties["LARGEUR"];
        
        GameObject road = new GameObject(nom_itineraire);
        RoadCreator roadCreator = road.AddComponent<RoadCreator>();
        roadCreator.spacing = 1f;
        roadCreator.roadWidth = (float)largeur_itineraire;
        roadCreator.material = material;
        roadCreator.tiling = 2;
        Path path = road.GetComponent<Path>();
        path.AutoSetControlPoints = true;
        path.points = new List<Vector3>();

        foreach (LineString troncon in itineraire.Coordinates)
        {
            foreach (IPosition position in troncon.Coordinates)
            {
                path.AddSegment(GeoUtils.GeoToWorldPosition(position.Latitude, position.Longitude, position.Altitude.Value, refPoint, scale));
                //path.points.Add(GeoUtils.GeoToWorldPosition(position.Latitude, position.Longitude, position.Altitude.Value, refPoint, scale));
            }
        }

        return road;
    }
}
