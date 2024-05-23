using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile_Behavior : MonoBehaviour
{
    [Header("Imports")]

    [SerializeField] GameObject player;


    [Header("Additionnal Behavior")]

    [SerializeField] bool additionalBehaviorEruption;
    [SerializeField] GameObject eruption;

    [Header("Settings")]

    [SerializeField] float projectileDmg;
    [SerializeField] float lifeSpan;

    float clock;
    GameObject[] oWPlatorms;



    private void Awake()
    {
        clock = lifeSpan;

        player = GameObject.FindGameObjectWithTag("Player");
        oWPlatorms = GameObject.FindGameObjectsWithTag("OW Platform");
        foreach (var item in oWPlatorms)
        {
            Physics2D.IgnoreCollision(item.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void Update()
    {
        if (clock > 0f)
        {
            clock -= Time.deltaTime;

            if (clock < 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player_Health>().TakeDamage(projectileDmg);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Environment")
        {
            Instantiate(eruption, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
