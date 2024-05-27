using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Platform_Behavior : MonoBehaviour
{
    [Header("Imports"), Space(10)]



    [Header("Settings"), Space(10)]

    [SerializeField, Space(5)] bool fade;
    [SerializeField] float fadeCd;
    [SerializeField] float fadeDuration;

    [Space(10)]

    [SerializeField, Space(5)] bool powerUp;
    [SerializeField] float bonusDmg;

    [Space(10)]

    [SerializeField, Space(5)] bool shield;
    [SerializeField] GameObject shieldGmO;

    bool shieldUp;
    GameObject shieldClone;

    [Header("Private"), Space(10)]

    GameObject player;
    float clockFade;
    float clockCd;
    bool playerOn;
    float baseProjectileDmg;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        clockFade = fadeDuration;
        baseProjectileDmg = player.GetComponent<Player_Shooting>().projectileDmg;
    }

    private void Update()
    {
        if (fade)
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

        if (powerUp)
        {
            if (playerOn)
            {
                player.GetComponent<Player_Shooting>().projectileDmg = bonusDmg;
            }
            else
            {
                player.GetComponent<Player_Shooting>().projectileDmg = baseProjectileDmg;
            }
        }

        if (shield)
        {
            if (playerOn)
            {
                if (!shieldUp)
                {
                    shieldClone = Instantiate(shieldGmO, transform.position, Quaternion.identity);
                    shieldUp = true;
                }
            }
            else
            {
                if (shieldUp)
                {
                    Destroy(shieldClone);
                    shieldUp = false;
                }
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
