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

        

        //_geoJsonObject
    }
}
