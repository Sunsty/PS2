using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// ////////////////////////////////// TODO /////////////////////////////// 
/// 
/// - Pattern 1 -
/// 
///     Slow wind up dash
///     Fire trail?
/// 
/// - Pattern 2 -
/// 
///     Shoot fireballs up
///     Fireballs fall
///     Eruption from ground
/// 
/// - Pattern 3 -
/// 
///     Fire shockwaves
/// 
/// ///////////////////////////////////////////////////////////////////////
/// 
/// </summary>
/// 
public class Boss1_Patterns : MonoBehaviour
{
    [Header("Imports")]

    public Rigidbody2D rb;
    public GameObject[] pattern1BossWaypoints;

    [Header("Settings")]

    public float dashCd;
    public float dashSpeed;
    public float normalSpeed;

    [Header("")]

    float clock;
    int targetIndex;
    float moveSpeed;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (clock <= 0)
        {
            clock = dashCd;
        }

        if (clock > 0)
        {
            clock -= Time.fixedDeltaTime;

            if (clock < 0)
            {
                targetIndex += 1;
                targetIndex = targetIndex % 4;
            }
        }

        if(( Vector2.Distance(transform.position, pattern1BossWaypoints[1].transform.position) <= 15f && targetIndex == 1 )|| ( Vector2.Distance(transform.position, pattern1BossWaypoints[3].transform.position) <= 15f && targetIndex == 3 ))
        {
            clock = dashCd;
            targetIndex += 1;
            targetIndex = targetIndex % 4;
        }

        if (targetIndex == 0 || targetIndex == 2)
        {
            moveSpeed = normalSpeed;
        }

        if (targetIndex == 1 || targetIndex == 3)
        {
            moveSpeed = dashSpeed;
        }
    }

    private void Update()
    {        
        float distance = Vector2.Distance(transform.position, pattern1BossWaypoints[targetIndex].transform.position);
        transform.position = (Vector2.MoveTowards(transform.position, pattern1BossWaypoints[targetIndex].transform.position, moveSpeed * Time.deltaTime * distance));

        Debug.DrawLine(transform.position, Vector2.MoveTowards(pattern1BossWaypoints[targetIndex].transform.position, transform.position, moveSpeed * Time.deltaTime * distance));   
    }

}
