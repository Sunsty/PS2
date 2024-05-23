using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Behavior : MonoBehaviour
{
    public GameObject player;
    public GameObject[] oWPlatorms;
    
    public float travelLenght;
    public float projectileDmg;

    private bool isTraveling;
    private float travelCounter;

    private void Awake()
    {
        isTraveling = true;
        player = GameObject.FindGameObjectWithTag("Player");
        oWPlatorms = GameObject.FindGameObjectsWithTag("OW Platform");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        foreach (var item  in oWPlatorms)
        {
            Physics2D.IgnoreCollision(item.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void Update()
    {
        if (isTraveling)
        {
            if (travelCounter <= 0f)
            {
                travelCounter = travelLenght;
            }
            isTraveling = false;
        }

        if (travelCounter > 0f)
        {
            travelCounter -= Time.fixedDeltaTime;

            if (travelCounter < 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<Boss_Health>().TakeDamage(projectileDmg);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Environment")
        {
            Destroy(gameObject);
        }
    }
}
