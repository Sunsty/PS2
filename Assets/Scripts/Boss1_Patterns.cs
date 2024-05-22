using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject circleWaveProjectile;
    [SerializeField] GameObject[] pattern1BossWaypoints;
    [SerializeField] GameObject[] pattern2BossWaypoints;
    [SerializeField] GameObject[] pattern3BossWaypoints;

    [Header("Settings")]

    [SerializeField] float contactDmg;
    [SerializeField] [Range(1, 4)] int currentPattern;

    [Header("Pattern 1")]

    [SerializeField] float dashCdPattern1;
    [SerializeField] float dashSpeedPattern1;
    [SerializeField] float normalSpeedPattern1;

    [Header("Pattern 2")]

    [SerializeField] GameObject fireBall;

    [SerializeField] float dashCdPattern2;
    [SerializeField] float dashSpeedPattern2;
    [SerializeField] float normalSpeedPattern2;

    [Header("Pattern 3")]

    [SerializeField] float normalSpeedPattern3;
    [SerializeField] float wavesCd;
    [SerializeField] int baseNbrProjectile;

    [Header("Pattern 4")]

    [Header("Private")]

    float clock;
    int targetIndex;
    float moveSpeed;
    private float clockWaves;
    private int currentWave;
    private int nbrProjectile;
    private float clockTrail;

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

            if (clockTrail <= 0f)
            {
                clockTrail = 0.2f;
            }

            if (clockTrail > 0f)
            {
                clockTrail -= Time.deltaTime;
                if (clockTrail < 0f)
                {
                    if (targetIndex == 1 || targetIndex == 3)
                    {
                    Instantiate(fireBall, new Vector2(transform.position.x,transform.position.y), Quaternion.identity);
                    }
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

            if (clockWaves <= 0f)
            {
                clockWaves = wavesCd;
            }

            if (clockWaves > 0f)
            {
                clockWaves -= Time.deltaTime;
                if (clockWaves < 0f)
                {
                    GameObject circleWaveProjectileClone = Instantiate(circleWaveProjectile, transform.position, Quaternion.Euler(0,0,0));
                    currentWave += 1;
                    currentWave = currentWave % 3;
                    nbrProjectile = baseNbrProjectile + currentWave;
                    circleWaveProjectileClone.GetComponent<CircleWaveProjectile_Behavior>().SetNbrProjectiles(nbrProjectile);
                }
            }

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
