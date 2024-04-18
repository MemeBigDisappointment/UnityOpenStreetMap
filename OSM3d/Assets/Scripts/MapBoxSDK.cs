using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class MapBoxSDK : MonoBehaviour
{
    public string accessToken;
    public float centerLatitude = 24.0323f;
    public float centerLongtitude = 49.8382f;
    public float zoom = 15f;
    public int bearing = 0;
    public int pitch = 0;
    public enum style { Light, Dark, Streets, Outdoors, Satellite, SatelliteStreets};
    public style mapStyle = style.Streets;
    public enum resolution { low = 1, high = 2 };
    public resolution mapResolution = resolution.low;

    private int _mapWidth = 400;
    private int _mapHeight = 400;
    private string[] _styleStr = new string[] { "light-v11", "dark-v11", "streets-v12", "outdoors-v12", "satellite-v9", "satellite-streets-v12" };
    private string _url = "";
    private bool _MapisLoading = false;
    private Rect _rect;
    private bool _updateMap = true;

    private string _accessTokenLast;
    private float _centerLatitudeLast = -33.8873f;
    private float _centerLongtitudeLast = 151.2189f;
    private float _zoomLast = 12.0f;
    private int _bearingLast = 0;
    private int _pitchLast = 0;
    private style _mapStyleLast = style.Streets;
    private resolution _mapResolutionLast = resolution.low;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetMapbox());
        _rect = gameObject.GetComponent<RawImage>().rectTransform.rect;
        _mapWidth = (int)Math.Round(_rect.width);
        _mapHeight = (int)Math.Round(_rect.height);
    }

    // Update is called once per frame
    void Update()
    {
        if(_updateMap && (_accessTokenLast != accessToken || !Mathf.Approximately(_centerLatitudeLast, centerLatitude) 
            || !Mathf.Approximately(_centerLongtitudeLast, centerLongtitude) || _zoomLast != zoom  || _bearingLast != bearing || _pitchLast != pitch 
            || _mapStyleLast != mapStyle || _mapResolutionLast!= mapResolution))
        {
            _rect = gameObject.GetComponent<RawImage>().rectTransform.rect;
            _mapWidth = (int)Math.Round(_rect.width);
            _mapHeight = (int)Math.Round(_rect.height);
            StartCoroutine(GetMapbox());
            _updateMap = false;
        }
    }

    IEnumerator GetMapbox()
    {
        _url = "https://api.mapbox.com/styles/v1/mapbox/streets-v12/static/" + centerLatitude.ToString().Replace(',', '.') + "," 
            + centerLongtitude.ToString().Replace(',', '.') + "," + zoom.ToString().Replace(',', '.') + "," + bearing.ToString().Replace(',', '.') + "/" 
            + _mapWidth.ToString() + "x" + _mapHeight.ToString() + "?access_token=" + accessToken;
        _MapisLoading = true;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(_url);
        yield return www.SendWebRequest();
        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("WWW ERROR: " + www.error);
        }
        else
        {
            _MapisLoading = false;
            gameObject.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

            _accessTokenLast = accessToken;
            _centerLatitudeLast = centerLatitude;
            _centerLongtitudeLast = centerLongtitude;
            _zoomLast = zoom;
            _bearingLast = bearing;
            _pitchLast = pitch;
            _mapStyleLast = mapStyle;
            _mapResolutionLast = mapResolution;
            _updateMap = true;

}
    }
}
