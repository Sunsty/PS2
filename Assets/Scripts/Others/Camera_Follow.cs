using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;

    float timeOffset;
    Vector3 posOffset;
    Vector3 velocity;
    Vector3 target;
    [SerializeField, Range(1,3)] int cameraBehavior;

    [Header("Camera 1")]

    public float timeOffset1;
    public Vector3 posOffset1;

    [Header("Camera 2")]

    public float timeOffset2;
    public Vector3 posOffset2;

    [Header("Camera 3")]

    public float timeOffset3;
    public Vector3 posOffset3;



    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");


        if (cameraBehavior == 1)
        {
            target = (player.transform.position + boss.transform.position) / 2f;
            timeOffset = timeOffset1;
            posOffset = posOffset1;
        }
        else if (cameraBehavior == 2)
        {
            target = player.transform.position;
            timeOffset = timeOffset2;
            posOffset = posOffset2;
        }
        else if (cameraBehavior == 3)
        {
            target = boss.transform.position;
            timeOffset = timeOffset3;
            posOffset = posOffset3;
        }
        transform.position = Vector3.SmoothDamp(transform.position, target + posOffset, ref velocity, timeOffset);
    }

    public void SwitchCameraBehavior(int index)
    {
        cameraBehavior = index;
    }
}
