using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traveling_Behavior : MonoBehaviour
{
    [SerializeField] public bool active;
    [SerializeField] public float speed;
    [SerializeField] Rigidbody2D rb;

    bool endScreen;
    private void Update()
    {
        if (active)
        {
            rb.velocity = Vector3.up * speed;
        }

        if (transform.position.y > 652)
        {
            active = false;
            rb.velocity = Vector3.zero;
            endScreen = true;
        }
    }
}
