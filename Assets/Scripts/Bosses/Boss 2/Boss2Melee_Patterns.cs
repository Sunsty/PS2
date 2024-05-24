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
    [Header("Imports")]

    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject bossBar;
    [SerializeField] GameObject mainCamera;

    [Header("Settings")]

    [SerializeField] float speed;
    [SerializeField, Range(1,4)] int currentPattern;

    [Header("Pattern 1")]

    [SerializeField] GameObject[] pattern1BossWaypoints;

    [SerializeField] float dashCdPattern1;
    [SerializeField] float dashSpeed;
    [SerializeField] int maxPattern1Count;

    int pattern1Count;

    [Header("Pattern 2")]



    [Header("Pattern 3")]



    [Header("Private")]

    float clock;
    int targetIndex;

    private void Start()
    {
        pattern1BossWaypoints = GameObject.FindGameObjectsWithTag("Boss 2 Melee Pattern 1 Waypoint").OrderBy(m => m.gameObject.transform.GetSiblingIndex()).ToArray();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        bossBar = GameObject.FindGameObjectWithTag("Boss Bar");
    }

    private void FixedUpdate()
    {

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
                    targetIndex++;
                    targetIndex = targetIndex % pattern1BossWaypoints.Length;
                }
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



        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 3 - /////////////////////



        /////////////////////////////////////////////////////////
    }

    private void Update()
    {
        ///////////////////// - Pattern 1 - /////////////////////

        if (currentPattern == 1)
        {
            float distance = Vector2.Distance(transform.position, pattern1BossWaypoints[targetIndex].transform.position);
            transform.position = (Vector2.MoveTowards(transform.position, pattern1BossWaypoints[targetIndex].transform.position, dashSpeed * Time.deltaTime * distance));

            Debug.DrawLine(transform.position, Vector2.MoveTowards(pattern1BossWaypoints[targetIndex].transform.position, transform.position, dashSpeed * Time.deltaTime * distance));
        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 2 - /////////////////////



        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 3 - /////////////////////



        /////////////////////////////////////////////////////////
    }
}
