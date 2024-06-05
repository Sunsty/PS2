using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    [Header("Imports")]

    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;
    [SerializeField] Animator animator;

    [Header("Settings")]

    [SerializeField] float bulletSpeed = 50;
    [SerializeField] float fireRate = 0.01f;
    [SerializeField] public float projectileDmg;

    [Header("")]

    Vector2 lookDirection;
    float lookAngle;
    bool isShooting = true;
    float shootingCounter;
    bool wantsToShot;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bullet.GetComponent<Projectile_Behavior>().projectileDmg = projectileDmg;

        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);

        if (Input.GetMouseButton(0))
        {
            wantsToShot = true;
            animator.SetBool("IsShooting", true);
        }
        else
        {
            wantsToShot = false;
            animator.SetBool("IsShooting", false);
        }

        if (isShooting && wantsToShot)
        {
            if (shootingCounter <= 0f)
            {
                shootingCounter = fireRate;
            }

            GameObject bulletClone = Instantiate(bullet);
            bulletClone.transform.position = firePoint.position;
            bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

            bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;

            isShooting = false;
        }

        if (shootingCounter > 0f)
        {
            shootingCounter -= Time.deltaTime;

            if (shootingCounter < 0f)
            {
                isShooting = true;
            }
        }
    }
}
