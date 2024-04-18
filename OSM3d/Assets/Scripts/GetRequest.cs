using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
public class GetRequest : MonoBehaviour
{
    public TMPro.TMP_InputField outputArea;
    public Button getButton;
    void Start()
    {
        getButton.onClick.AddListener(GetData);
    }

    void GetData() => StartCoroutine(GetData_Coroutine());

    IEnumerator GetData_Coroutine()
    {
        outputArea.text = "Loading...";
        string uri = "https://www.openstreetmap.org/api/0.6/map?bbox=24.01315,49.82726,24.01633,49.82846";
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                outputArea.text = request.error;
            }
            else
            {
                byte[] OsmData = request.downloadHandler.data;
                string savePath = Path.Combine(Application.persistentDataPath, "osm_map.osm");
                File.WriteAllBytes(savePath, OsmData);
                outputArea.text = savePath;
            }
        }
    }
}
