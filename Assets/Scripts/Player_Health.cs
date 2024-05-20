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

    [Header("")]

    float health;
    float time;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
        {
            TakeDamage(10f);
        }



        ////////////////////////////////////////////////////////////

        if (time <= 0f)
        {
            time = 5f;
        }

        if (time > 0f)
        {
        time -= Time.deltaTime;
            if (time < 0f)
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
        health -= damage;
    }
}
