using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam_Behavior : MonoBehaviour
{
    [SerializeField] float contactDmg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player_Health>().TakeDamage(contactDmg);
        }
    }
}
