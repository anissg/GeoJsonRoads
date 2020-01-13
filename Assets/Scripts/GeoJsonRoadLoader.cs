﻿using System.Collections;
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

    [SerializeField] private string resourceFileName;
    private string _json;
    private FeatureCollection _geoJsonFeatureCollection;

    void Start()
    {
        _json = Resources.Load<TextAsset>(resourceFileName)?.text;
        _geoJsonFeatureCollection = JsonConvert.DeserializeObject<FeatureCollection>(_json, new GeoJsonConverter());

        Utils.setReperes(repere1.transform.position, repere2.transform.position, new Vector3(4.85f, 0f, 45.68f), new Vector3(5f, 0f, 45.74f));
        
        
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
