using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Health : MonoBehaviour
{
    [Header("Imports")]

    public GameObject boss;


    [Header("Settings")]

    public float maxHealth;

    [Header("")]

    float health;
    Image healthBar;
    GameObject mainCamera;

    private void Start()
    {
        health = maxHealth;
        healthBar = GameObject.FindGameObjectWithTag("Boss Bar").GetComponentInChildren<Image>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
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
            Destroy(boss);
        }

        ////////////////////////////////////////////////////////////

        healthBar.fillAmount = health / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
