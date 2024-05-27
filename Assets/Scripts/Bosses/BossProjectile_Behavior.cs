using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile_Behavior : MonoBehaviour
{
    [Header("Imports")]

    [SerializeField] GameObject player;


    [Header("Additionnal Behavior")]

    [SerializeField, Space(10)] bool additionalBehaviorEruption;
    [SerializeField] GameObject eruption;

    [SerializeField, Space(10)] bool additionalBehaviorBoss2;
    [SerializeField] float bulletSpeed;

    Vector3 target;

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

        if (additionalBehaviorBoss2)
        {
            target = player.transform.position;
            gameObject.GetComponent<Rigidbody2D>().AddForce((target - transform.position).normalized * bulletSpeed * 1000f);
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

        if (collision.CompareTag("Shield"))
        {
            Destroy(gameObject);
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
