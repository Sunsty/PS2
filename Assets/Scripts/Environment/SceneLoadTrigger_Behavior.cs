using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger_Behavior : MonoBehaviour
{
    [SerializeField] int scene;

    GameObject bossBar;

    private void Awake()
    {
        bossBar = GameObject.Find("Boss Bar");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossBar.SetActive(true);
            SceneManager.LoadScene(scene);
        }
    }
}
