using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Behavior : MonoBehaviour
{
    [Header("Imports")]

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject endWaypoint;

    [Header("Settings")]

    [SerializeField] float speed;
    [SerializeField] float duration;
    [SerializeField] float height;
    [SerializeField] bool invert;

    [Header("Private")]

    float clock;
    List<GameObject> wall;
    int invertCoef;

    private void Awake()
    {
        if (invert)
        {
            invertCoef = -1;
        }
        else
        {
            invertCoef = 1;
        }
        wall = new List<GameObject>();
        for (int i = 0; i <= height; i++)
        {
            GameObject currentProjectileClone = Instantiate(projectile, transform.position + new Vector3(0, invertCoef * i * 2, 0), Quaternion.identity, transform);
            wall.Add(currentProjectileClone);
        }
    }

    private void Update()
    {
        //////////////////////////////////////
        
        transform.position = Vector3.MoveTowards(transform.position, endWaypoint.transform.position, speed);

        foreach (GameObject item in wall)
        {
            item.transform.position = new Vector3(transform.position.x, item.transform.position.y, 0);
        }

        if (transform.position == endWaypoint.transform.position)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
