using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile_Behavior : MonoBehaviour
{
    [SerializeField] GameObject eruption;

    [SerializeField] bool additionalBehaviorEruption;

    [SerializeField] float projectileDmg;
    [SerializeField] float lifeSpan;
    float clock;

    private void Awake()
    {
        clock = lifeSpan;
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
