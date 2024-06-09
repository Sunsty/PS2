using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] GameObject[] element;
    [SerializeField] GameObject bossBar;
    [SerializeField] GameObject hud;
    [SerializeField] GameObject mainCamera;
    [SerializeField] Image godModeGO;
    [SerializeField] Image powerModeGO;
    [SerializeField] GameObject player;
    [SerializeField] GameObject difficulty;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] Texture2D cursorTexture;

    [Header("Presentation Difficulty"), Space(20)]

    [SerializeField] float playerHealth1;
    [SerializeField] float playerRegen1;
    [SerializeField] float playerIFrames1;

    [SerializeField] bool cheatAccess1;

    [Header("Normal Difficulty"), Space(20)]

    [SerializeField] float playerHealth2;
    [SerializeField] float playerRegen2;
    [SerializeField] float playerIFrames2;

    [SerializeField] bool cheatAccess2;

    [Header("No Hit Difficulty"), Space(20)]

    [SerializeField] float playerHealth3;
    [SerializeField] float playerRegen3;
    [SerializeField] float playerIFrames3;

    [SerializeField] bool cheatAccess3;

    bool cheatAccess;
    bool godMode;
    bool powerMode;

    void Awake()
    {
        foreach (var item in element)
        {
            DontDestroyOnLoad(item);
        }

        hud = GameObject.Find("HUD");
        bossBar = GameObject.Find("Boss Bar");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.Find("Player");
        difficulty = GameObject.Find("Dropdown");
        godModeGO = GameObject.Find("God Mode").GetComponent<Image>();
        powerModeGO = GameObject.Find("Power Mode").GetComponent<Image>();
    }

    private void Update()
    {
        if (difficulty == null)
        {
            difficulty = GameObject.Find("Dropdown");
        }
        if (difficulty != null)
        {
            dropdown = difficulty.GetComponent<TMP_Dropdown>();
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(2);
            hud.SetActive(true);
            for (int i = 0; i < hud.transform.childCount; i++)
            {
                hud.transform.GetChild(i).gameObject.SetActive(true);
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("Scene Loader").GetComponent<SceneLoader_Behavior>().LoadScene(1);
        }

        if (cheatAccess)
        {
            if (Input.GetKeyDown (KeyCode.Alpha1))
            {
                if (!godMode)
                {
                    godMode = true;
                }
                else
                {
                    godMode = false;
                }
            }
            if (Input.GetKeyDown (KeyCode.Alpha2))
            {
                if (!powerMode)
                {
                    powerMode = true;
                }
                else
                {
                    powerMode = false;
                }
            }

            if (godMode)
            {
                player.GetComponent<Player_Health>().godMode = true;
                godModeGO.color = new Color(255, 255, 255, 255);
            }
            else
            {
                player.GetComponent<Player_Health>().godMode = false;
                godModeGO.color = new Color(255, 255, 255, 0);
            }

            if (powerMode)
            {
                player.GetComponent<Player_Shooting>().powerMode = true;
                powerModeGO.color = new Color(255, 255, 255, 255);
            }
            else
            {
                player.GetComponent<Player_Shooting>().powerMode = false;
                powerModeGO.color = new Color(255, 255, 255, 0);
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (difficulty != null)
            {
                if (dropdown.value == 0)
                {
                    cheatAccess = cheatAccess1;
                    player.GetComponent<Player_Health>().maxHealth = playerHealth1;
                    player.GetComponent<Player_Health>().regenAmount = playerRegen1;
                    player.GetComponent<Player_Health>().iFramesAmount = playerIFrames1;
                }

                if (dropdown.value == 1)
                {
                    cheatAccess = cheatAccess2;
                    player.GetComponent<Player_Health>().maxHealth = playerHealth2;
                    player.GetComponent<Player_Health>().regenAmount = playerRegen2;
                    player.GetComponent<Player_Health>().iFramesAmount = playerIFrames2;
                }

                if (dropdown.value == 2)
                {
                    cheatAccess = cheatAccess3;
                    player.GetComponent<Player_Health>().maxHealth = playerHealth3;
                    player.GetComponent<Player_Health>().regenAmount = playerRegen3;
                    player.GetComponent<Player_Health>().iFramesAmount = playerIFrames3;
                }
            }
            if (dropdown == null)
            {
                cheatAccess = cheatAccess1;
                player.GetComponent<Player_Health>().maxHealth = playerHealth1;
                player.GetComponent<Player_Health>().regenAmount = playerRegen1;
                player.GetComponent<Player_Health>().iFramesAmount = playerIFrames1;
            }
        }

    }
}
