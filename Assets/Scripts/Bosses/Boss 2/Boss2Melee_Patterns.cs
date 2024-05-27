using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 
/// /////////////////////// - TODO - ///////////////////////
/// 
/// - Pattern 1 -
/// 
///     Succesives Dashes with little windup
/// 
/// - Pattern 2 -
/// 
///     Turning around RANGE
/// 
/// - Pattern 3 -
/// 
/// 
/// 
/// ////////////////////////////////////////////////////////
/// 
/// </summary>

public class Boss2Melee_Patterns : MonoBehaviour
{
    [Header("Imports"), Space(10)]

    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject player;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject rangeBoss;

    [Header("Settings"), Space(10)]

    [SerializeField] float contactDmg;
    [SerializeField, Range(1,4), Space(10)] int currentPattern;

    [Header("Pattern 1"), Space(10)]

    [SerializeField] GameObject[] pattern1BossWaypoints;

    [Space(10)]

    [SerializeField] float dashCdPattern1;
    [SerializeField] float dashSpeed;
    [SerializeField] int maxPattern1Count;

    int pattern1Count;

    [Header("Pattern 2"), Space(10)]

    [SerializeField] GameObject pattern2BossWaypoint;

    [Space(10)]

    [SerializeField] float speedPattern2;
    [SerializeField] float dashSpeedPattern2;
    [SerializeField] float orbitRange;
    [SerializeField] float dashCdPattern2;

    int accelerate;
    float currentSpeed;

    [Header("Pattern 3"), Space(10)]



    [Header("Private"), Space(10)]

    float clock;
    int targetIndex;

    private void Start()
    {
        pattern1BossWaypoints = GameObject.FindGameObjectsWithTag("Boss 2 Melee Pattern 1 Waypoint").OrderBy(m => m.gameObject.transform.GetSiblingIndex()).ToArray();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {

        ///////////////////// - Pattern 1 - /////////////////////


        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 2 - /////////////////////



        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 3 - /////////////////////



        /////////////////////////////////////////////////////////
    }

    private void Update()
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
                clock -= Time.deltaTime;

                if (clock < 0)
                {
                    pattern1Count++;
                    targetIndex++;
                    targetIndex = targetIndex % pattern1BossWaypoints.Length;
                }
            }

            if (pattern1Count == maxPattern1Count)
            {
                currentPattern = 2;
                rangeBoss.GetComponent<Boss2Range_Patterns>().currentPattern = 2;
                clock = 0f;
                pattern1Count = 0;
                targetIndex = 0;
            }

            float distance = Vector2.Distance(transform.position, pattern1BossWaypoints[targetIndex].transform.position);
            transform.position = (Vector2.MoveTowards(transform.position, pattern1BossWaypoints[targetIndex].transform.position, dashSpeed * Time.deltaTime * distance));

            Debug.DrawLine(transform.position, Vector2.MoveTowards(pattern1BossWaypoints[targetIndex].transform.position, transform.position, dashSpeed * Time.deltaTime * distance));
        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 2 - /////////////////////

        if (currentPattern == 2)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = (transform.position - pattern2BossWaypoint.transform.position).normalized * orbitRange + pattern2BossWaypoint.transform.position;
            transform.RotateAround(pattern2BossWaypoint.transform.position, Vector3.forward, Time.deltaTime * currentSpeed);

            if (clock <= 0)
            {
                clock = dashCdPattern2;
            }

            if (clock > 0)
            {
                clock -= Time.deltaTime;

                if (clock < 0)
                {
                    accelerate++;
                    accelerate = accelerate % 6;
                }
            }

            if (accelerate == 5)
            {
                currentSpeed = dashSpeedPattern2;
            }
            else
            {
                currentSpeed = speedPattern2;
            }
        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 3 - /////////////////////



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
