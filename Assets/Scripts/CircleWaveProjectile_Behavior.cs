using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleWaveProjectile_Behavior : MonoBehaviour
{
    bool shoot;
    public GameObject projectile;
    public float projectileSpeed;

    private void Awake()
    {
        shoot = true;
    }

    private void Update()
    {
        if (shoot)
        {
            for (int i = 0; i < 36; i++)
            {
                GameObject projectileClone = Instantiate(projectile, transform.position, new Quaternion(0,0,i * 10,0));
                projectileClone.GetComponent<Rigidbody2D>().velocity = Vector2.right * projectileSpeed;
            }
            shoot = false;
        }
    }
}
