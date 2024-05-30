using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Health : MonoBehaviour
{
    [Header("Imports")]

    [SerializeField] GameObject bossBar;
    [SerializeField] GameObject hud;


    [Header("Settings")]

    [SerializeField] float maxHealth;
    [SerializeField] int patternToGo;

    [Header("")]

    float health;
    Image healthBar;

    private void Start()
    {
        health = maxHealth;

        hud = GameObject.Find("HUD");

        bossBar = hud.transform.Find("Boss Bar").gameObject;
        bossBar.SetActive(true);
        healthBar = GameObject.Find("Boss Health").GetComponent<Image>();
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
            if (GetComponent<Boss2Melee_Patterns>() == null && GetComponent<Boss2Range_Patterns>() == null)
            {
                GetComponent<Boss1_Patterns>().currentPattern = patternToGo;
            }
            else if (GetComponent<Boss1_Patterns>() == null)
            {
                GameObject.Find("Boss Melee").GetComponent<Boss2Melee_Patterns>().currentPattern = patternToGo;
                GetComponent<Boss2Range_Patterns>().currentPattern = patternToGo;
            }

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
