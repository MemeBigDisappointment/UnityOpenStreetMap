using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuController : MonoBehaviour
{
    public TMP_InputField TMP_InputField; 
    private void Update()
    {
        if(TMP_InputField)
        TMP_InputField.text = StateNameController.osmPath;
    }

    public void OpenNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void OpenStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenDocumentation()
    {
        Application.OpenURL("https://docs.unity3d.com/ScriptReference/index.html");
    }
}
