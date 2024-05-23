using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Behavior : MonoBehaviour
{
    [Header("Imports")]



    [Header("Settings")]

    [SerializeField] float fadeCd;
    [SerializeField] float fadeDuration;

    [Header("Private")]

    GameObject player;
    float clockFade;
    float clockCd;
    bool playerOn;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        clockFade = fadeDuration;
    }

    private void Update()
    {

        if (playerOn)
        {
            if (clockFade > 0f)
            {
                clockFade -= Time.deltaTime;

                if (clockFade < 0f )
                {
                    clockCd = fadeCd;
                    Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),player.GetComponent<Collider2D>(), true);
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<PlatformEffector2D>().enabled = false;
                }
            }
        }
        else
        {
            clockFade = fadeDuration;
        }


        if (clockCd > 0f)
        {
            clockCd -= Time.deltaTime;

            if (clockCd < 0f)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), false);
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<PlatformEffector2D>().enabled = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOn = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOn = false;
        }
    }
}
