
using AnotherFileBrowser.Windows;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FileBrowserUpdate : MonoBehaviour
{
    public TMPro.TMP_InputField outputArea;

    public void OpenFileBrowser()
    {
        var bp = new BrowserProperties();
        bp.filter = "OpenStreetMap files (*.osm, *.txt) | *.osm; *.txt";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            //Load OSM data from local path with UWR
            StartCoroutine(LoadOSM(path));
        });
    }

    IEnumerator LoadOSM(string path)
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get(path))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                byte[] OsmData = uwr.downloadHandler.data;
                string savePath = Path.Combine(Application.persistentDataPath, "osm_map1.osm");
                File.WriteAllBytes(savePath, OsmData);
                StateNameController.osmPath = savePath;
            }
        }
    }
}
