using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleWaveProjectile_Behavior : MonoBehaviour
{
    bool shoot;
    public GameObject projectile;
    public float projectileSpeed;
    public float nbrProjectiles;

    private void Awake()
    {
        shoot = true;
    }

    private void Update()
    {
        if (shoot)
        {
            for (int i = 0; i < nbrProjectiles; i++)
            {
                GameObject projectileClone = Instantiate(projectile, transform.position, Quaternion.Euler(0,0,i * (360/ nbrProjectiles)));
                Quaternion angle = Quaternion.Euler(0, 0, i * (float)(360 / nbrProjectiles));
                Vector2 direction = angle * Vector2.up;
                projectileClone.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            }
            shoot = false;
        }
    }

    public float GetNbrProjectiles()
    {
        return nbrProjectiles;
    }

    public void SetNbrProjectiles(int i)
    {
        nbrProjectiles = i;
    }
}
