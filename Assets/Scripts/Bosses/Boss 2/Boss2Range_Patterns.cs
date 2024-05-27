using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

/// <summary>
/// 
/// /////////////////////// - TODO - ///////////////////////
/// 
/// - Pattern 1 -
/// 
///     Basic Projectiles
/// 
/// - Pattern 2 -
/// 
///     Go in center
///     Star like turning projectile shape 
/// 
/// - Pattern 3 -
/// 
///     
/// 
/// ////////////////////////////////////////////////////////
/// 
/// </summary>

public class Boss2Range_Patterns : MonoBehaviour
{
    [Header("Imports"), Space(10)]

    [SerializeField] GameObject player;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject bossBar;

    [Header("Settings"), Space(10)]

    [SerializeField, Range(1,3)] public int currentPattern;

    [Header("Pattern 1"), Space(10)]

    [SerializeField] GameObject bullet;
    [SerializeField] float speedPattern1;
    [SerializeField] float orbitRange;
    [SerializeField] float shootCdPattern1;

    [Header("Pattern 2"), Space(10)]

    [SerializeField] GameObject pattern2BossWaypoint;
    [SerializeField] GameObject wheel;

    [Space(10)]

    [SerializeField] float pattern2Speed;

    bool startPattern2;

    [Header("Pattern 3"), Space(10)]



    [Header("Private"), Space(10)]

    float clock;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        bossBar = GameObject.FindGameObjectWithTag("Boss Bar");
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
            bossBar.SetActive(true);

            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(2);

            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = (transform.position - player.transform.position).normalized * orbitRange + player.transform.position;
            transform.RotateAround(player.transform.position, Vector3.forward, Time.deltaTime * speedPattern1);

            if (clock <= 0)
            {
                clock = shootCdPattern1;
            }

            if (clock > 0)
            {
                clock -= Time.deltaTime;

                if (clock < 0)
                {
                    GameObject bulletClone = Instantiate(bullet, transform.position, Quaternion.identity);
                }
            }
        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 2 - /////////////////////

        if (currentPattern == 2)
        {
            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(1);

            if (!startPattern2 && Vector2.Distance(transform.position, pattern2BossWaypoint.transform.position) <= 0.1f)
            {
                GameObject wheelClone = Instantiate(wheel, transform.position, Quaternion.identity);
                startPattern2 = true;
            }

            transform.rotation = Quaternion.Euler(0, 0, 0);
            float distance = Vector2.Distance(transform.position, pattern2BossWaypoint.transform.position);
            transform.position = (Vector2.MoveTowards(transform.position, pattern2BossWaypoint.transform.position, pattern2Speed * Time.deltaTime * distance));
        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 3 - /////////////////////



        /////////////////////////////////////////////////////////
    }
}
