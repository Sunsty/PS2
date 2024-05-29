using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 
/// ////////////////////////////////// TODO /////////////////////////////// 
/// 
/// - Pattern 1 -
/// 
///     Projectile Walls
///     Platforms
///     
/// - Pattern 2 -
/// 
///     Aiming Targets
/// 
/// - Pattern 3 -
/// 
///     Full wall ( dash through )
/// 
/// ///////////////////////////////////////////////////////////////////////
/// 
/// </summary>


public class BossTuto_Patterns : MonoBehaviour
{
    [Header("Imports")]

    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject bossBar;

    [Header("Settings")]

    [SerializeField, Range(1, 8)] int currentPattern;

    [Header("Speech Bubbles"), Space(10)]

    [SerializeField] GameObject speechBubble1;
    [SerializeField] GameObject speechBubble2;
    [SerializeField] GameObject speechBubble3;
    [SerializeField] GameObject speechBubble4;

    [Header("Pattern 1")]

    [SerializeField] GameObject wallUp;
    [SerializeField] float downOffset;
    [SerializeField] GameObject wallDown;
    [SerializeField] float upOffset;
    [SerializeField] float wallOffsetPattern1;
    [SerializeField] int maxWallCountPattern1;

    int wallCountPattern1;
    GameObject[] activeWallsPattern1;

    [Header("Pattern 2")]

    [SerializeField] GameObject aimTarget;
    [SerializeField] GameObject[] aimTargetWaypoints;

    [SerializeField, Space(10)] List<int> spawnPattern1;
    [SerializeField, Space(10)] List<int> spawnPattern2;
    [SerializeField, Space(10)] List<int> spawnPattern3;
    [SerializeField, Space(10)] List<int> spawnPattern4;
    [SerializeField, Space(10)] List<int> spawnPattern5;
    [SerializeField, Space(10)] List<int> spawnPattern6;

    [SerializeField, Space(20)] List<int>[] spawnPatterns;

    [SerializeField] int maxWavesCount;
    int wavesCount;

    [SerializeField] GameObject[] activeAimTargets;

    [Header("Pattern 3")]

    [SerializeField] GameObject wallFull;
    [SerializeField] float fullOffset;
    [SerializeField] float wallOffsetPattern3;
    [SerializeField] int maxWallCountPattern3;

    int wallCountPattern3;
    GameObject[] activeWallsPattern3;

    [Header("Private")]

    float clock;


    private void Start()
    {
        bossBar = GameObject.FindGameObjectWithTag("Boss Bar");

        bossBar.SetActive(false);

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        aimTargetWaypoints = GameObject.FindGameObjectsWithTag("Aim Target Waypoint").OrderBy(m => m.gameObject.transform.GetSiblingIndex()).ToArray();
        spawnPatterns = new List<int>[6];

        spawnPatterns[0] = spawnPattern1;
        spawnPatterns[1] = spawnPattern2;
        spawnPatterns[2] = spawnPattern3;
        spawnPatterns[3] = spawnPattern4;
        spawnPatterns[4] = spawnPattern5;
        spawnPatterns[5] = spawnPattern6;
    }


    private void Update()
    {
        currentPattern = currentPattern % 8;

        ///////////////////// - Pattern Cine 1 - /////////////////////

        if (currentPattern == 1)
        {
            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(5);

            if (Input.GetKeyDown(KeyCode.F))
            {
                currentPattern++;
            }
        }

        //////////////////////////////////////////////////////////////

        ///////////////////// - Pattern 1 - /////////////////////

        if (currentPattern == 2)
        {
            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(2);

            if (clock <= 0)
            {
                clock = wallOffsetPattern1;
            }

            if (clock > 0)
            {
                clock -= Time.deltaTime;

                if (clock < 0)
                {
                    wallCountPattern1++;
                    if (wallCountPattern1 <= maxWallCountPattern1)
                    {
                        if (wallCountPattern1 % 2 == 0)
                        {
                            Instantiate(wallDown, transform.position + new Vector3(0, upOffset, 0), Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(wallUp, transform.position - new Vector3(0, downOffset, 0), Quaternion.identity);
                        }
                    }
                }
            }

            activeWallsPattern1 = GameObject.FindGameObjectsWithTag("Boss Tuto Pattern 1 Wall");

            if (wallCountPattern1 >= maxWallCountPattern1 && activeWallsPattern1.Length == 0)
            {
                currentPattern++;
                clock = 0;
                wallCountPattern1 = 0;
            }
        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern Cine 2 - /////////////////////

        if (currentPattern == 3)
        {
            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(5);

            if (Input.GetKeyDown(KeyCode.F))
            {
                currentPattern++;
            }
        }

        //////////////////////////////////////////////////////////////

        ///////////////////// - Pattern 2 - /////////////////////

        if (currentPattern == 4)
        {
            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(2);

            if (activeAimTargets.Length == 0)
            {            
                foreach (var waypointIndex in spawnPatterns[wavesCount])
                {
                    Instantiate(aimTarget, aimTargetWaypoints[waypointIndex].transform.position, Quaternion.identity);
                }
                
                wavesCount++;
            }

            activeAimTargets = GameObject.FindGameObjectsWithTag("Aim Target");

            if (wavesCount >= maxWavesCount && activeAimTargets.Length == 0)
            {            
                currentPattern++;
            }
        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern Cine 3 - /////////////////////

        if (currentPattern == 5)
        {
            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(5);

            if (Input.GetKeyDown(KeyCode.F))
            {
                currentPattern++;
            }
        }

        //////////////////////////////////////////////////////////////

        ///////////////////// - Pattern 3 - /////////////////////

        if (currentPattern == 6)
        {
            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(2);

            if (clock <= 0)
            {
                clock = wallOffsetPattern3;
            }

            if (clock > 0)
            {
                clock -= Time.deltaTime;

                if (clock < 0)
                {
                    wallCountPattern3++;
                    if (wallCountPattern3 <= maxWallCountPattern3)
                    {
                    Instantiate(wallFull, transform.position - new Vector3(0, fullOffset, 0), Quaternion.identity);
                    }
                }
            }

            activeWallsPattern3 = GameObject.FindGameObjectsWithTag("Boss Tuto Pattern 3 Wall");

            if (wallCountPattern3 >= maxWallCountPattern3 && activeWallsPattern3.Length == 0)
            {
                currentPattern++;
                clock = 0;
                wallCountPattern3 = 0;
            }
        }

        /////////////////////////////////////////////////////////

        //////////////////// - Pattern Cine End - ////////////////////

        if (currentPattern == 7)
        {
            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(5);

            if (Input.GetKeyDown(KeyCode.F))
            {
                mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(2);
                this.enabled = false;
            }
        }

        //////////////////////////////////////////////////////////////
    }
}
