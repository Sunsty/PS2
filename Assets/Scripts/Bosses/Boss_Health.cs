using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Health : MonoBehaviour
{
    [Header("Imports")]

    [SerializeField] GameObject bossBar;

    [Header("Settings")]

    [SerializeField] float maxHealth;

    [Header("")]

    float health;
    Image healthBar;
    GameObject mainCamera;

    private void Start()
    {
        health = maxHealth;
        healthBar = GameObject.FindGameObjectWithTag("Boss Bar").GetComponentInChildren<Image>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        bossBar = GameObject.Find("Boss Bar");
    }

    private void Update()
    {        

        ////////////////////////////////////////////////////////////

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (health <= 0f)
        {
            health = 0f;
            GetComponent<Boss1_Patterns>().currentPattern = 5;
            bossBar.SetActive(false);
        }

        ////////////////////////////////////////////////////////////

        healthBar.fillAmount = health / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public float GetHealth()
    {
        return health;
    }
}
