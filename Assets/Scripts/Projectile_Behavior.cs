using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Behavior : MonoBehaviour
{
    private bool isTraveling;
    private float travelCounter;
    public float travelLenght;

    private void Awake()
    {
        isTraveling = true;
    }

    private void Update()
    {
        if (isTraveling)
        {
            if (travelCounter <= 0f)
            {
                travelCounter = travelLenght;
            }
            isTraveling = false;
        }

        if (travelCounter > 0f)
        {
            travelCounter -= Time.fixedDeltaTime;

            if (travelCounter < 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
