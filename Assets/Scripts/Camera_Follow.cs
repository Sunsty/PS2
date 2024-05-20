using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;

    public float timeOffset;
    public Vector3 posOffset;

    private Vector3 velocity;
    private Vector3 target;


    void Update()
    {
        target = (player.transform.position + boss.transform.position) / 2f;
        transform.position = Vector3.SmoothDamp(transform.position, target + posOffset, ref velocity, timeOffset);
    }
}
