using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public GameObject[] element;
    public GameObject bossBar;

    void Awake()
    {
        foreach (var item in element)
        {
            DontDestroyOnLoad(item);
        }

        bossBar = GameObject.Find("Boss Bar");
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Scene 0")
        {
            SceneManager.LoadScene(1);
        }

        if (SceneManager.GetActiveScene().name == "Boss Scene 3")
        {
            bossBar.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
    }
}
