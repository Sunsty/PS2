using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    [Header("Imports")]

    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject boss2;
    [SerializeField] GameObject hud;

    [Header("Settings")]

    [SerializeField, Range(1,5)] int cameraBehavior;

    [Header("Camera 1")]

    [SerializeField] float timeOffset1;
    [SerializeField] Vector3 posOffset1;
    [SerializeField, Range(1, 50)] float cameraSize1;

    [Header("Camera 2")]

    [SerializeField] float timeOffset2;
    [SerializeField] Vector3 posOffset2;
    [SerializeField, Range(1, 50)] float cameraSize2;

    [Header("Camera 3")]

    [SerializeField] float timeOffset3;
    [SerializeField] Vector3 posOffset3;
    [SerializeField, Range(1, 50)] float cameraSize3;

    [Header("Camera 4")]

    [SerializeField] float timeOffset4;
    [SerializeField] Vector3 posOffset4;
    [SerializeField, Range(1, 50)] float cameraSize4;

    [Header("Camera 5")]

    [SerializeField] float timeOffset5;
    [SerializeField] Vector3 posOffset5;
    [SerializeField, Range(1, 50)] float cameraSize5;
    
    [Header("Camera 6")]

    [SerializeField] float timeOffset6;
    [SerializeField] Vector3 posOffset6;
    [SerializeField, Range(1, 50)] float cameraSize6;
    
    [Header("Camera 7")]

    [SerializeField] float timeOffset7;
    [SerializeField] Vector3 posOffset7;
    [SerializeField, Range(1, 50)] float cameraSize7;

    [Header("Private")]

    float timeOffset;
    Vector3 posOffset;
    Vector3 velocity;
    Vector3 target;

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");
        boss2 = GameObject.FindGameObjectWithTag("Boss2");


        if (cameraBehavior == 1 && boss != null)
        {
            player.GetComponent<Player_Health>().enabled = true;
            player.GetComponent<Player_Movement>().enabled = true;
            player.GetComponent<Player_Shooting>().enabled = true;

            target = (player.transform.position + boss.transform.position) / 2f;
            timeOffset = timeOffset1;
            posOffset = posOffset1;
            float t = 0;
            t += 2 * Time.deltaTime;
            gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, cameraSize1, t);
            hud.SetActive(true);
        }
        else if (cameraBehavior == 2)
        {
            player.GetComponent<Player_Health>().enabled = true;
            player.GetComponent<Player_Movement>().enabled = true;
            player.GetComponent<Player_Shooting>().enabled = true;

            target = player.transform.position;
            timeOffset = timeOffset2;
            posOffset = posOffset2;
            float t = 0;
            t += 2 * Time.deltaTime;
            gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, cameraSize2, t);
            hud.SetActive(true);
        }
        else if (cameraBehavior == 3)
        {
            player.GetComponent<Player_Health>().enabled = true;
            player.GetComponent<Player_Movement>().enabled = true;
            player.GetComponent<Player_Shooting>().enabled = true;

            target = boss.transform.position;
            timeOffset = timeOffset3;
            posOffset = posOffset3;
            float t = 0;
            t += 2 * Time.deltaTime;
            gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, cameraSize3, t);
            hud.SetActive(true);
        }
        else if (cameraBehavior == 4)
        {
            player.GetComponent<Player_Health>().enabled = false;
            player.GetComponent<Player_Movement>().enabled = false;
            player.GetComponent<Player_Shooting>().enabled = false;

            target = player.transform.position;
            timeOffset = timeOffset4;
            posOffset = posOffset4;
            float t = 0;
            t += 2 * Time.deltaTime;
            gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, cameraSize4, t);
            hud.SetActive(false);
        }
        else if (cameraBehavior == 5)
        {
            player.GetComponent<Player_Health>().enabled = false;
            player.GetComponent<Player_Movement>().enabled = false;
            player.GetComponent<Player_Shooting>().enabled = false;

            target = boss.transform.position;
            timeOffset = timeOffset5;
            posOffset = posOffset5;
            float t = 0;
            t += 2 * Time.deltaTime;
            gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, cameraSize5, t);
            hud.SetActive(false);
        }
        else if (cameraBehavior == 6)
        {
            player.GetComponent<Player_Health>().enabled = true;
            player.GetComponent<Player_Movement>().enabled = true;
            player.GetComponent<Player_Shooting>().enabled = true;

            target = (player.transform.position + boss2.transform.position) / 2f;
            timeOffset = timeOffset6;
            posOffset = posOffset6;
            float t = 0;
            t += 2 * Time.deltaTime;
            gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, cameraSize6, t);
            hud.SetActive(true);
        }
        else if (cameraBehavior == 7)
        {
            player.GetComponent<Player_Health>().enabled = false;
            player.GetComponent<Player_Movement>().enabled = false;
            player.GetComponent<Player_Shooting>().enabled = false;

            target = (boss.transform.position + boss2.transform.position) / 2f;
            timeOffset = timeOffset7;
            posOffset = posOffset7;
            float t = 0;
            t += 2 * Time.deltaTime;
            gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, cameraSize7, t);
            hud.SetActive(false);
        }

        transform.position = Vector3.SmoothDamp(transform.position, target + posOffset, ref velocity, timeOffset);
    }

    public void SwitchCameraBehavior(int index)
    {
        cameraBehavior = index;
    }
}
