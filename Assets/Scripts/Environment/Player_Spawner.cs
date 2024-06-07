using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Spawner : MonoBehaviour
{
    void Awake()
    {
        GameObject.FindWithTag("Player").GetComponent<Transform>().position = this.transform.position;
        GameObject.FindWithTag("Player").GetComponent<Player_Health>().Heal();
        GameObject.FindWithTag("Camera Shake").transform.position = this.transform.position;
    }
}
