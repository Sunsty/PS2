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
///     Dash from side to side
///     Leaving fire trail
/// 
/// - Pattern 3 -
/// 
///     Shoot fireballs up
///     Fireballs fall
///     Eruption from ground
/// 
/// - Pattern 4 -
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
    public GameObject fireBall;
    public GameObject[] pattern1BossWaypoints;
    public GameObject[] pattern2BossWaypoints;
    public GameObject[] pattern3BossWaypoints;

    [Header("Settings")]

    public float contactDmg;
    public int currentPattern;

    [Header("Pattern 1")]

    public float dashCdPattern1;
    public float dashSpeedPattern1;
    public float normalSpeedPattern1;

    [Header("Pattern 2")]

    public float dashCdPattern2;
    public float dashSpeedPattern2;
    public float normalSpeedPattern2;

    [Header("Pattern 3")]

    public float normalSpeedPattern3;

    [Header("Pattern 4")]

    [Header("Private")]

    float clock;
    int targetIndex;
    float moveSpeed;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        ///////////////////// - Pattern 1 - /////////////////////

        if (currentPattern == 1)
        {
            if (clock <= 0)
            {
                clock = dashCdPattern1;
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

            if ((Vector2.Distance(transform.position, pattern1BossWaypoints[1].transform.position) <= 15f && targetIndex == 1) || (Vector2.Distance(transform.position, pattern1BossWaypoints[3].transform.position) <= 15f && targetIndex == 3))
            {
                clock = dashCdPattern1;
                targetIndex += 1;
                targetIndex = targetIndex % 4;
            }

            if (targetIndex == 0 || targetIndex == 2)
            {
                moveSpeed = normalSpeedPattern1;
            }

            if (targetIndex == 1 || targetIndex == 3)
            {
                moveSpeed = dashSpeedPattern1;
            }
        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 2 - /////////////////////

        if (currentPattern == 2)
        {
            if (clock <= 0)
            {
                clock = dashCdPattern1;
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

            if ((Vector2.Distance(transform.position, pattern2BossWaypoints[1].transform.position) <= 15f && targetIndex == 1 )|| ( Vector2.Distance(transform.position, pattern2BossWaypoints[3].transform.position) <= 15f && targetIndex == 3))
            {
                clock = dashCdPattern1;
                targetIndex += 1;
                targetIndex = targetIndex % 4;
            }

            if (targetIndex == 0 || targetIndex == 2)
            {
                moveSpeed = normalSpeedPattern1;
            }

            if (targetIndex == 1 || targetIndex == 3)
            {
                moveSpeed = dashSpeedPattern1;
            }
        }

        /////////////////////////////////////////////////////////
        
        ///////////////////// - Pattern 3 - /////////////////////

        if (currentPattern == 3)
        {

        }

        /////////////////////////////////////////////////////////
        
        ///////////////////// - Pattern 4 - /////////////////////

        if (currentPattern == 4)
        {

        }

        /////////////////////////////////////////////////////////
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            currentPattern += 1;
            currentPattern = currentPattern % 5;

            if (currentPattern == 0)
            {
                currentPattern = 1;
            }
            if (currentPattern == 1)
            {
                targetIndex = 0;
                clock = 0;
            }
            if (currentPattern == 2)
            {
                targetIndex = 0;
                clock = 0;
            }
            if (currentPattern == 3)
            {
                targetIndex = 0;
            }
            if (currentPattern == 4)
            {

            }
        }

        ///////////////////// - Pattern 1 - /////////////////////

        if (currentPattern == 1)
        {
            float distance = Vector2.Distance(transform.position, pattern1BossWaypoints[targetIndex].transform.position);
            transform.position = (Vector2.MoveTowards(transform.position, pattern1BossWaypoints[targetIndex].transform.position, moveSpeed * Time.deltaTime * distance));

            Debug.DrawLine(transform.position, Vector2.MoveTowards(pattern1BossWaypoints[targetIndex].transform.position, transform.position, moveSpeed * Time.deltaTime * distance));
        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 2 - /////////////////////

        if (currentPattern == 2)
        {
            float distance = Vector2.Distance(transform.position, pattern2BossWaypoints[targetIndex].transform.position);
            transform.position = (Vector2.MoveTowards(transform.position, pattern2BossWaypoints[targetIndex].transform.position, moveSpeed * Time.deltaTime * distance));

            if ((transform.position.x % 10f > 0f && transform.position.x % 10f < 0.5f) || (transform.position.x % 10f < 0f && transform.position.x % 10f > -0.5f))
            {
                if (targetIndex == 1 || targetIndex == 3)
                {
                Instantiate(fireBall, new Vector2(transform.position.x,transform.position.y), Quaternion.identity);
                }
            }

            Debug.DrawLine(transform.position, Vector2.MoveTowards(pattern2BossWaypoints[targetIndex].transform.position, transform.position, moveSpeed * Time.deltaTime * distance));
        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 3 - /////////////////////

        if (currentPattern == 3)
        {
            float distance = Vector2.Distance(transform.position, pattern3BossWaypoints[targetIndex].transform.position);
            transform.position = (Vector2.MoveTowards(transform.position, pattern3BossWaypoints[targetIndex].transform.position, normalSpeedPattern3 * Time.deltaTime * distance));

            Debug.DrawLine(transform.position, Vector2.MoveTowards(pattern3BossWaypoints[targetIndex].transform.position, transform.position, normalSpeedPattern3 * Time.deltaTime * distance));
        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 4 - /////////////////////

        if (currentPattern == 4)
        {

        }

        /////////////////////////////////////////////////////////

 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player_Health>().TakeDamage(contactDmg);
        }
    }
}
