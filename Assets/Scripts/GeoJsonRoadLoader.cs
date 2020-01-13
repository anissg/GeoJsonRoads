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
    [SerializeField] private string resourceFileName;
    private string _json;
    private FeatureCollection _geoJsonFeatureCollection;
    
    void Start()
    {
        _json = Resources.Load<TextAsset>(resourceFileName)?.text;
        _geoJsonFeatureCollection = JsonConvert.DeserializeObject<FeatureCollection>(_json, new GeoJsonConverter());

        
        
    }

    void DrawTroncon(Feature<MultiLineString,Dictionary<string,object>> feature)
    {
        MultiLineString itineraire = feature.Geometry;
        string nom_itineraire = feature.Properties["NOM_ITI"].ToString();
        double largeur_itineraire = (double) feature.Properties["LARGEUR"];
        
        GameObject path = new GameObject(nom_itineraire);
        path.AddComponent<MeshRenderer>();
        path.AddComponent<MeshFilter>();
        path.AddComponent<RoadCreator>();
        PathCreator pathCreator = path.AddComponent<PathCreator>();
        pathCreator.path.points = new List<Vector3>();

        foreach (LineString troncon in itineraire.Coordinates)
        {
            foreach (IPosition position in troncon.Coordinates)
            {
                pathCreator.path.points.Add(new Vector3((float)position.Longitude, (float)position.Altitude, (float)position.Latitude));        
            }
        }
    }
}
