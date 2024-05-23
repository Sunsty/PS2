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

    [Header("Private")]

    float clock;
    List<GameObject> wall;

    private void Awake()
    {
        for (int i = 0; i <= height; i++)
        {
            GameObject currentProjectileClone = Instantiate(projectile, transform.position + new Vector3(0, i * 2, 0), Quaternion.identity);
            wall.Add(currentProjectileClone);
        }
    }

    private void Update()
    {
        //////////////////////////////////////
        
        transform.position = Vector3.MoveTowards(transform.position, endWaypoint.transform.position, speed);

        foreach (GameObject item in wall)
        {
            item.transform.position = new Vector3(transform.position.x, 0, 0);
        }

        if (transform.position == endWaypoint.transform.position)
        {
            Destroy(gameObject);
        }
    }
}
