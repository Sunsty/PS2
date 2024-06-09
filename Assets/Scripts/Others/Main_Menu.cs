using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public void LoadScene(int scene)
    {
        GameObject.Find("Scene Loader").GetComponent<SceneLoader_Behavior>().LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
