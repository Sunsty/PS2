using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel_Behavior : MonoBehaviour
{
    [Header("Imports"), Space(10)]

    [SerializeField] GameObject beam;

    [Header("Settings"), Space(10)]

    [SerializeField] float nbrOfBeams;
    [SerializeField] float rotationSpeed;

    [Header("Private"), Space(10)]

    float clock;

    private void Awake()
    {
        for (int i = 0; i < nbrOfBeams; i++)
        {
            GameObject projectileClone = Instantiate(beam, transform.position, Quaternion.Euler(0, 0, i * (360 / nbrOfBeams)), transform);
        }
    }

    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime * rotationSpeed);
    }

}
