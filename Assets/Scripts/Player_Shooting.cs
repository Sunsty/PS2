using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    [Header("Imports")]

    public GameObject bullet;
    public Transform firePoint;

    [Header("Settings")]

    public float bulletSpeed = 50;
    public float fireRate = 0.01f;

    [Header("")]

    Vector2 lookDirection;
    float lookAngle;
    private bool isShooting = true;
    private float shootingCounter;
    private bool wantsToShot;

    private void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);

        if (Input.GetMouseButton(0))
        {
            wantsToShot = true;
        }
        else
        {
            wantsToShot = false;
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
