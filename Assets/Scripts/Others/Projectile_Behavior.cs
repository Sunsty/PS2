using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Behavior : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public GameObject[] oWPlatorms;
    
    public float travelLenght;
    public float projectileDmg;

    private bool isTraveling;
    private float travelCounter;

    private void Awake()
    {
        isTraveling = true;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        oWPlatorms = GameObject.FindGameObjectsWithTag("OW Platform");
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
                DestroyAnim();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss") || collision.CompareTag("Boss2"))
        {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss_Health>().TakeDamage(projectileDmg);
            DestroyAnim();
        }


        if (collision.CompareTag("Aim Target"))
        {
            collision.gameObject.GetComponent<AimTarget_Health>().TakeDamage(projectileDmg);
            DestroyAnim();
        }

        if (collision.CompareTag("Shield"))
        {
            DestroyAnim();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Environment")
        {
            transform.rotation = Quaternion.Euler(0,0,-90);
            transform.position = new Vector3(transform.position.x,transform.position.y + 2f,transform.position.z);
            DestroyAnim();
        }
    }

    private void DestroyAnim()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        animator.SetBool("Dying", true);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
