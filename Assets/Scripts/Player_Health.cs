using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{

    [Header("Imports")]

    public GameObject player;
    public Image healthBar;

    [Header("Settings")]

    public float maxHealth;
    public float regenAmount;
    public float iFramesAmount;

    [Header("")]

    float health;
    float clockRegen;
    float counterIFrames;
    bool canTakeDmg = true;
    private bool tookDmg;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        ////////////////////////////////////////////////////////////
        
        if (!canTakeDmg)
        {
            tookDmg = true;
        }

        if (tookDmg)
        {
            if (counterIFrames <= 0f)
            {
                counterIFrames = iFramesAmount;
            }

            tookDmg = false;
        }

        if (counterIFrames > 0f)
        {
            counterIFrames -= Time.deltaTime;

            if (counterIFrames < 0f)
            {
                canTakeDmg = true;
            }
        }

        ////////////////////////////////////////////////////////////

        if (clockRegen <= 0f)
        {
            clockRegen = 5f;
        }

        if (clockRegen > 0f)
        {
        clockRegen -= Time.deltaTime;
            if (clockRegen < 0f)
            {
                health += regenAmount;
            }
        }

        ////////////////////////////////////////////////////////////

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (health <= 0f)
        {
            health = 0f;
            Debug.Log("Dead");
        }

        ////////////////////////////////////////////////////////////

        healthBar.fillAmount = health / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (canTakeDmg)
        {
            health -= damage;
            canTakeDmg = false;
        }
    }
}
