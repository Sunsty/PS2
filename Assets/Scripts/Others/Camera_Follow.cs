using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    [Header("Imports")]

    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;
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

    [Header("Private")]

    float timeOffset;
    Vector3 posOffset;
    Vector3 velocity;
    Vector3 target;

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");


        if (cameraBehavior == 1)
        {
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
            target = boss.transform.position;
            timeOffset = timeOffset5;
            posOffset = posOffset5;
            float t = 0;
            t += 2 * Time.deltaTime;
            gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, cameraSize5, t);
            hud.SetActive(false);
        }

        transform.position = Vector3.SmoothDamp(transform.position, target + posOffset, ref velocity, timeOffset);
    }

    public void SwitchCameraBehavior(int index)
    {
        cameraBehavior = index;
    }
}
