using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{

    [Header("Imports")]

    [SerializeField] GameObject player;
    [SerializeField] Image healthBar;

    [Header("Settings")]

    [SerializeField] float maxHealth;
    [SerializeField] float regenAmount;
    [SerializeField] float iFramesAmount;

    [Header("")]

    float health;
    float clockRegen;
    float counterIFrames;
    bool canTakeDmg = true;
    bool tookDmg;
    private SpriteRenderer sprite;
    private bool initIFrames;
    private float clockDashIFrames;
    private float dashIFramesLenght;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    private void Update()
    {
        ////////////////////////////////////////////////////////////
        
        if (!canTakeDmg && clockDashIFrames <= 0f)
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

            if (counterIFrames % 0.3f >= 0.15f)
            {
                sprite.color = new Color(255, 255, 255, 0);
            }
            else
            {
                sprite.color = new Color(255, 255, 255, 255);
            }

            if (counterIFrames < 0f)
            {
                canTakeDmg = true;
            }
        }

        ////////////////////////////////////////////////////////////
        
        if (initIFrames)
        {
            clockDashIFrames = dashIFramesLenght;
            canTakeDmg = false;
            initIFrames = false;
        }

        if (clockDashIFrames > 0f)
        {
            clockDashIFrames -= Time.deltaTime;

            if (clockDashIFrames < 0f)
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

    public void InitDash(float duration)
    {
        dashIFramesLenght = duration;
        initIFrames = true;
    }
}
