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
    private int cameraBehavior = 1;

    void Update()
    {
        if (cameraBehavior == 1)
        {
            target = (player.transform.position + boss.transform.position) / 2f;
        }
        else if (cameraBehavior == 2)
        {
            target = player.transform.position;
        }
        transform.position = Vector3.SmoothDamp(transform.position, target + posOffset, ref velocity, timeOffset);
    }

    public void SwitchCameraBehavior(int index)
    {
        cameraBehavior = index;
    }
}
