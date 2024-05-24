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
        bossBar = GameObject.FindGameObjectWithTag("Boss Bar");
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
            Debug.Log("Congrats !");
            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(2);
            bossBar.SetActive(false);
            Destroy(gameObject);
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
