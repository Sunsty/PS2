using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AimTarget_Behavior : MonoBehaviour
{
    [Header("Imports")]

    [SerializeField] GameObject player;

    [Header("Settings")]

    [SerializeField] float speed;
    [SerializeField] float idleDuration;

    [Header("Private")]

    bool active;
    float clock;
    private bool canMove;

    private void Awake()
    {
        active = true;
        player = GameObject.FindGameObjectWithTag("Player");
        clock = idleDuration;
    }

    private void Update()
    {
        if (active)
        {
            if (clock > 0f)
            {
                clock -= Time.deltaTime;
                transform.position = (Vector2.MoveTowards(transform.position, player.transform.position, (speed / 100) * Time.deltaTime));

                if (clock < 0f)
                {
                    canMove = true;
                }
            }

            if (canMove)
            {
                float distance = Vector2.Distance(transform.position, player.transform.position);
                transform.position = (Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime));
            }
        }
    }
}
