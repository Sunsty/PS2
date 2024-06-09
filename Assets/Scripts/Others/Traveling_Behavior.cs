using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traveling_Behavior : MonoBehaviour
{
    [SerializeField] public bool active;
    [SerializeField] public float speed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject title;
    private void Update()
    {
        if (active)
        {
            rb.velocity = Vector3.up * speed;
        }

        if (transform.position.y > 652)
        {
            rb.velocity = Vector3.zero;
            if (active)
            {
                Instantiate(title, transform.position, Quaternion.identity);
            }

            active = false;

        }
    }
}
