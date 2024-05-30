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
    [SerializeField] GameObject player;
    [SerializeField] GameObject respawn;
    [SerializeField] GameObject sceneLoadTrigger;

    [Header("Settings")]

    [SerializeField, Range(0, 8)] public int currentPattern;

    [Header("Speech Bubbles"), Space(10)]

    [SerializeField] GameObject speechBubble0;
    [SerializeField] GameObject speechBubble1;
    [SerializeField] GameObject speechBubble2;
    [SerializeField] GameObject speechBubble3;
    [SerializeField] GameObject speechBubble4;

    [SerializeField] float speechOffset;

    bool speechBubbleSpawned;

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
        bossBar = GameObject.Find("Boss Bar");
        player = GameObject.Find("Player");

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

        sceneLoadTrigger.SetActive(false);
    }


    private void Update()
    {
        if (!player.GetComponent<Player_Health>().IsFullLife() && currentPattern == 2)
        {
            player.GetComponent<Player_Health>().Heal();
            GameObject[] activeWalls = GameObject.FindGameObjectsWithTag("Boss Tuto Pattern 1 Wall");
            foreach (GameObject wall in activeWalls)
            {
                Destroy(wall);
            }
            currentPattern--;

            player.transform.position = respawn.transform.position;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        if (!player.GetComponent<Player_Health>().IsFullLife() && currentPattern == 4)
        {
            player.GetComponent<Player_Health>().Heal();
            foreach (GameObject target in activeAimTargets)
            {
                Destroy(target);
            }
            currentPattern--;

            player.transform.position = respawn.transform.position;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        if (!player.GetComponent<Player_Health>().IsFullLife() && currentPattern == 6)
        {
            player.GetComponent<Player_Health>().Heal();
            GameObject[] activeWalls = GameObject.FindGameObjectsWithTag("Boss Tuto Pattern 3 Wall");
            foreach (GameObject wall in activeWalls)
            {
                Destroy(wall);
            }
            currentPattern--;

            player.transform.position = respawn.transform.position;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        bossBar.SetActive(false);

        ///////////////////// - Pattern Cine 0 - /////////////////////

        if (currentPattern == 0)
        {
            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(5);

            if (!speechBubbleSpawned)
            {
                InstantiateSpeech(speechBubble0, speechOffset);
                speechBubbleSpawned = true;
            }
        }

        //////////////////////////////////////////////////////////////
        
        ///////////////////// - Pattern Cine 1 - /////////////////////

        if (currentPattern == 1)
        {
            if (GameObject.FindGameObjectWithTag("Speech Bubble") ==  null)
            {
                speechBubbleSpawned = false;
            }

            clock = 0f;
            wallCountPattern1 = 0;

            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(5);

            if (!speechBubbleSpawned)
            {
                InstantiateSpeech(speechBubble1, speechOffset);
                speechBubbleSpawned = true;
            }
        }

        //////////////////////////////////////////////////////////////

        ///////////////////// - Pattern 1 - /////////////////////

        if (currentPattern == 2)
        {
            speechBubbleSpawned = false;

            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(2);

            bossBar.SetActive(false);

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
            clock = 0f;
            wavesCount = 0;

            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(5);

            if (!speechBubbleSpawned)
            {
                InstantiateSpeech(speechBubble2, speechOffset);
                speechBubbleSpawned = true;
            }
        }

        //////////////////////////////////////////////////////////////

        ///////////////////// - Pattern 2 - /////////////////////

        if (currentPattern == 4)
        {
            speechBubbleSpawned = false;

            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(2);

            bossBar.SetActive(false);

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
            clock = 0f;
            wallCountPattern3 = 0;

            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(5);

            if (!speechBubbleSpawned)
            {
                InstantiateSpeech(speechBubble3, speechOffset);
                speechBubbleSpawned = true;
            }
        }

        //////////////////////////////////////////////////////////////

        ///////////////////// - Pattern 3 - /////////////////////

        if (currentPattern == 6)
        {
            speechBubbleSpawned = false;

            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(2);

            bossBar.SetActive(false);

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

            if (!speechBubbleSpawned)
            {
                InstantiateSpeech(speechBubble4, speechOffset);
                speechBubbleSpawned = true;
            }
        }

        //////////////////////////////////////////////////////////////
        
        //////////////////// - Pattern Free Cam - ////////////////////

        if (currentPattern == 8)
        {
            sceneLoadTrigger.SetActive(true);

            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(2);
            this.enabled = false;
        }

        //////////////////////////////////////////////////////////////
    }

    private void InstantiateSpeech(GameObject speechBubble, float offset)
    {
        Instantiate(speechBubble, new Vector3(transform.position.x, transform.position.y + offset, transform.position.z), Quaternion.identity, transform);
    }
}
