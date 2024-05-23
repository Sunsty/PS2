using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eruption_Behavior : MonoBehaviour
{
    [SerializeField] GameObject firePillar;
    [SerializeField] int eruptionHeight;
    private bool erupt;

    private void Awake()
    {
        erupt = true;
    }

    private void Update()
    {
        if (erupt)
        {
            for (int i = 0; i <= eruptionHeight; i++)
            {
                StartCoroutine(Waiter(i));            
                if (i == eruptionHeight)
                {
                    StartCoroutine(DestroyEnd(eruptionHeight));
                }
            }
            erupt = false;
        }
    }

    IEnumerator Waiter(float i)
    {        
        yield return new WaitForSeconds(i/25f);

        Vector3 offset = new Vector3(0, i*5, 0);
        Instantiate(firePillar, transform.position + offset, Quaternion.identity);

    }

    IEnumerator DestroyEnd(float i) 
    { 
        yield return new WaitForSeconds(i/25f); 

        Destroy(gameObject);
    }
}
