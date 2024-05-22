using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile_Behavior : MonoBehaviour
{
    public float lifeSpan;
    float clock;
    private void Awake()
    {
        clock = lifeSpan;
    }

    private void Update()
    {
        if (clock > 0f)
        {
            clock -= Time.deltaTime;

            if (clock < 0f)
            {
                Destroy(gameObject);
            }
        }

    }
}
