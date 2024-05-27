using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

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
    [SerializeField] CapsuleCollider2D groundCollider;
    [SerializeField] GameObject player;
    [SerializeField] GameObject rangeBoss;
    [SerializeField] GameObject cameraShake;
    [SerializeField] GameObject[] oWPlatorms;

    [Header("Settings"), Space(10)]

    [SerializeField] float contactDmg;
    [SerializeField, Range(1,4), Space(10)] public int currentPattern;

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

    [SerializeField] GameObject[] pattern3BossWaypoints;
    [SerializeField] GameObject shockWave;

    [Space(10)]

    [SerializeField] float dashCdPattern3;
    [SerializeField] float groundCdPattern3;
    [SerializeField] float speedPattern3;
    [SerializeField] float stompForce;
    [SerializeField] float maxVelocityY;

    int pattern3Count;

    [Header("Private"), Space(10)]

    [HideInInspector] public float clock;
    int targetIndex;

    private void Start()
    {
        pattern1BossWaypoints = GameObject.FindGameObjectsWithTag("Boss 2 Melee Pattern 1 Waypoint").OrderBy(m => m.gameObject.transform.GetSiblingIndex()).ToArray();
        pattern3BossWaypoints = GameObject.FindGameObjectsWithTag("Boss 2 Melee Pattern 3 Waypoint").OrderBy(m => m.gameObject.transform.GetSiblingIndex()).ToArray();
        player = GameObject.FindGameObjectWithTag("Player");

        groundCollider = GetComponent<CapsuleCollider2D>();
        Physics2D.IgnoreCollision(groundCollider, player.GetComponent<Collider2D>());

        oWPlatorms = GameObject.FindGameObjectsWithTag("OW Platform");
        foreach (var item in oWPlatorms)
        {
            Physics2D.IgnoreCollision(item.GetComponent<Collider2D>(), groundCollider);
        }
    }

    private void Update()
    {
        currentPattern %= 4;

        if (currentPattern == 0)
        {
            currentPattern = 1;
        }

        ///////////////////// - Pattern 1 - /////////////////////

        if (currentPattern == 1)
        {
            groundCollider.enabled = false;

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
                rangeBoss.GetComponent<Boss2Range_Patterns>().clock = 0f;
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
            groundCollider.enabled = false;

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

        if (currentPattern == 3)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

            groundCollider.enabled = true;

            if (clock > 0)
            {
                clock -= Time.deltaTime;

                if (clock < 0)
                {
                    pattern3Count++;

                    if (pattern3Count%2 == 0)
                    {
                        targetIndex++;
                        targetIndex %= pattern3BossWaypoints.Length;
                    }
                }
            }

            if (pattern3Count%2 == 0)
            {
                if (clock <= 0)
                {
                    clock = dashCdPattern3; 
                }

                rb.velocity = Vector2.zero;
                float distance = Vector2.Distance(transform.position, pattern3BossWaypoints[targetIndex].transform.position);
                transform.position = (Vector2.MoveTowards(transform.position, pattern3BossWaypoints[targetIndex].transform.position, speedPattern3 * Time.deltaTime * distance));
            }
            else
            {
                if (clock <= 0)
                {
                    clock = groundCdPattern3;
                }

                rb.AddForce(Vector2.down * stompForce);
                float tempClampY = Mathf.Clamp(rb.velocity.y, -maxVelocityY, maxVelocityY);
                rb.velocity = new Vector2(rb.velocity.x, tempClampY);
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Environment" && currentPattern == 3)
        {
            Instantiate(shockWave, new Vector2(transform.position.x, transform.position.y - 5), Quaternion.identity);
        }
    }
}
