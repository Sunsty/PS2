using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundWave_Behavior : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float offset;
    [SerializeField] float lifeSpan;

    float clock;
    private int step;

    Vector3 targetUp;
    Vector3 targetDown;

    private void Awake()
    {
        targetUp = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        targetDown = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);
    }

    private void Update()
    {

        if (step == 0)
        {
            float distance = Vector3.Distance(transform.position, targetUp);
            transform.position = Vector3.MoveTowards(transform.position, targetUp, speed*2 * Time.deltaTime * distance);

            if (distance < .5f)
            {
                step++;
            }
        }

        if (step == 1)
        {
            float distance = Vector3.Distance(transform.position, targetDown);
            transform.position = Vector3.MoveTowards(transform.position, targetDown, speed * Time.deltaTime * distance);
            if (distance < .1f)
            {
                step++;
            }
        }

        if (step == 2)
        {
            Destroy(gameObject);
        }
    }
}
