using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTarget_Health : MonoBehaviour
{
    [Header("Imports")]



    [Header("Settings")]

    [SerializeField] float maxHealth;

    [Header("Private")]

    float health;


    private void Awake()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (health < 0f)
        {
            health = 0f;
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
