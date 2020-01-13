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
    [SerializeField] GameObject repere1;
    [SerializeField] GameObject repere2;
    [SerializeField] Vector3 scale;
    [SerializeField] Material material;

    [SerializeField] private string resourceFileName;
    private string _json;
    private FeatureCollection _geoJsonFeatureCollection;

    void Start()
    {
        _json = Resources.Load<TextAsset>(resourceFileName)?.text;
        _geoJsonFeatureCollection = JsonConvert.DeserializeObject<FeatureCollection>(_json, new GeoJsonConverter());

        Utils.setReperes(repere1.transform.position, repere2.transform.position, new Vector3(4.85f, 0f, 45.68f), new Vector3(5f, 0f, 45.74f));

        GameObject roads = new GameObject("Routes");
        roads.transform.eulerAngles = new Vector3(90, roads.transform.eulerAngles.y, roads.transform.eulerAngles.z);

        GameObject road = DrawTroncon(_geoJsonFeatureCollection.Features[0]);
        road.transform.parent = roads.transform;
    }

    GameObject DrawTroncon(Feature feature)
    {
        MultiLineString itineraire = feature.Geometry as MultiLineString;
        string nom_itineraire = feature.Properties["NOM_ITI"].ToString();
        double largeur_itineraire = (double) feature.Properties["LARGEUR"];
        
        GameObject road = new GameObject(nom_itineraire);
        RoadCreator roadCreator = road.AddComponent<RoadCreator>();
        roadCreator.spacing = 0.5f;
        roadCreator.material = material;
        roadCreator.tiling = 5;
        Path path = road.GetComponent<Path>();
        path.points = new List<Vector3>();

        foreach (LineString troncon in itineraire.Coordinates)
        {
            foreach (IPosition position in troncon.Coordinates)
            {
                path.points.Add(Utils.LatLongToVector(position.Latitude, position.Longitude, position.Altitude.Value));        
            }
        }

        return road;
    }
}
