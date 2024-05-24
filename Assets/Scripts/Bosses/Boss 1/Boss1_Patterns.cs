using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
 
public class Boss1_Patterns : MonoBehaviour
{
    [Header("Imports")]

    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject bossBar;

    [Header("Settings")]

    [SerializeField] float contactDmg;
    [SerializeField] [Range(0, 4)] int currentPattern;

    [Header("Pattern 1")]

    [SerializeField] GameObject[] pattern1BossWaypoints;

    [SerializeField] float dashCdPattern1;
    [SerializeField] float dashSpeedPattern1;
    [SerializeField] float normalSpeedPattern1;
    [SerializeField] int maxPattern1Count;

    int pattern1Count;

    [Header("Pattern 2")]

    [SerializeField] GameObject fireBall;
    [SerializeField] GameObject[] pattern2BossWaypoints;

    [SerializeField] float dashCdPattern2;
    [SerializeField] float dashSpeedPattern2;
    [SerializeField] float normalSpeedPattern2;
    [SerializeField] int maxPattern2Count;

    int pattern2Count;

    [Header("Pattern 3")]

    [SerializeField] GameObject circleWaveProjectile;
    [SerializeField] GameObject pattern3BossWaypoint;

    [SerializeField] float normalSpeedPattern3;
    [SerializeField] float wavesCd;
    [SerializeField] int baseNbrProjectile;
    [SerializeField] float pattern3Duration;

    int pattern3Count;

    [Header("Pattern 4")]

    [SerializeField] GameObject cinematicBossWaypoint;

    [SerializeField] float secondPhaseHealth;

    [Header("Enraged Stats")]

    [SerializeField] float dashCdPattern1Enraged;
    [SerializeField] float dashSpeedPattern1Enraged;
    [SerializeField] float normalSpeedPattern1Enraged;
    [SerializeField] int maxPattern1CountEnraged;

    [SerializeField, Space(10)] float dashCdPattern2Enraged;
    [SerializeField] float dashSpeedPattern2Enraged;
    [SerializeField] float normalSpeedPattern2Enraged;
    [SerializeField] int maxPattern2CountEnraged;

    [SerializeField, Space(10)] float normalSpeedPattern3Enraged;
    [SerializeField] float wavesCdEnraged;
    [SerializeField] int baseNbrProjectileEnraged;
    [SerializeField] float pattern3DurationEnraged;

    bool enraged;

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
        pattern1BossWaypoints = GameObject.FindGameObjectsWithTag("Boss 1 Pattern 1 Waypoint").OrderBy(m => m.gameObject.transform.GetSiblingIndex()).ToArray();
        pattern2BossWaypoints = GameObject.FindGameObjectsWithTag("Boss 1 Pattern 2 Waypoint").OrderBy(m => m.gameObject.transform.GetSiblingIndex()).ToArray();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        bossBar = GameObject.FindGameObjectWithTag("Boss Bar");
    }


    private void FixedUpdate()
    {
        ///////////////////// - Pattern 0 - /////////////////////

        if (currentPattern == 0)
        {
            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(5);

            if (Input.GetKeyDown(KeyCode.F))
            {
                currentPattern = 1;
            }
        }

        ///////////////////// - Pattern 1 - /////////////////////

        if (currentPattern == 1)
        {
            bossBar.SetActive(true);

            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(1);

            if (clock <= 0)
            {
                clock = dashCdPattern1;
            }

            if (clock > 0)
            {
                clock -= Time.fixedDeltaTime;

                if (clock < 0)
                {
                    pattern1Count++;
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

            if (pattern1Count == maxPattern1Count)
            {
                currentPattern = 2;
                clock = 0f;
                pattern1Count = 0;
                targetIndex = 0;
            }
        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 2 - /////////////////////

        if (currentPattern == 2)
        {
            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(1);

            if (clock <= 0)
            {
                clock = dashCdPattern1;
            }

            if (clock > 0)
            {
                clock -= Time.fixedDeltaTime;

                if (clock < 0)
                {
                    pattern2Count++;
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

            if (pattern2Count == maxPattern2Count)
            {
                currentPattern = 3;
                clock = 0f;
                targetIndex = 0;
                pattern2Count = 0;
            }
        }

        /////////////////////////////////////////////////////////
        
        ///////////////////// - Pattern 3 - /////////////////////

        if (currentPattern == 3)
        {
            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(1);

            if (clock <= 0)
            {
                clock = pattern3Duration;
            }

            if (clock > 0)
            {
                clock -= Time.fixedDeltaTime;

                if (clock < 0)
                {
                    clockWaves = 0f;
                    currentPattern = 1;
                    clock = 0f;
                }
            }
        }

        /////////////////////////////////////////////////////////

        float health = gameObject.GetComponent<Boss_Health>().GetHealth();

        if (health <= secondPhaseHealth && !enraged)
        {
            currentPattern = 4;
            enraged = true;
        }

        ///////////////////// - Pattern 4 - /////////////////////

        if (currentPattern == 4)
        {
            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(5);
        }

        /////////////////////////////////////////////////////////
    }

    private void Update()
    {

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
            if (targetIndex == 0 || targetIndex == 2)
            {
                float distance = Vector2.Distance(transform.position, pattern2BossWaypoints[targetIndex].transform.position);
                transform.position = (Vector2.MoveTowards(transform.position, pattern2BossWaypoints[targetIndex].transform.position, moveSpeed * Time.deltaTime * distance));
            }
            else if (targetIndex == 1 || targetIndex == 3)
            {
                transform.position = (Vector2.MoveTowards(transform.position, pattern2BossWaypoints[targetIndex].transform.position, moveSpeed * Time.deltaTime * 70f));
            }

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
            Debug.DrawLine(transform.position, Vector2.MoveTowards(pattern3BossWaypoint.transform.position, transform.position, normalSpeedPattern3 * Time.deltaTime));
        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 3 - /////////////////////

        if (currentPattern == 3)
        {
            float distance = Vector2.Distance(transform.position, pattern3BossWaypoint.transform.position);
            transform.position = (Vector2.MoveTowards(transform.position, pattern3BossWaypoint.transform.position, normalSpeedPattern3 * Time.deltaTime * distance));

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
            Debug.DrawLine(transform.position, Vector2.MoveTowards(pattern3BossWaypoint.transform.position, transform.position, normalSpeedPattern3 * Time.deltaTime));
        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Cinematic - /////////////////////

        if (currentPattern == 4)
        {
            float distance = Vector2.Distance(transform.position, cinematicBossWaypoint.transform.position);
            transform.position = (Vector2.MoveTowards(transform.position, cinematicBossWaypoint.transform.position, normalSpeedPattern3 * Time.deltaTime * distance));

            if (Input.GetKeyDown(KeyCode.F))
            {
                currentPattern = 1;
                clock = 0f;
            }
        }

        /////////////////////////////////////////////////////////

        if (enraged)
        {
            dashCdPattern1 = dashCdPattern1Enraged;
            dashSpeedPattern1 = dashSpeedPattern1Enraged;
            maxPattern1Count = maxPattern1CountEnraged;

            dashCdPattern2 = dashCdPattern2Enraged;
            dashSpeedPattern2 = dashSpeedPattern2Enraged;
            normalSpeedPattern2 = normalSpeedPattern2Enraged;
            maxPattern2Count = maxPattern2CountEnraged;

            normalSpeedPattern3 = normalSpeedPattern3Enraged;
            wavesCd = wavesCdEnraged;
            baseNbrProjectile = baseNbrProjectileEnraged;
            pattern3Duration = pattern3DurationEnraged;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player_Health>().TakeDamage(contactDmg);
        }
    }
}
