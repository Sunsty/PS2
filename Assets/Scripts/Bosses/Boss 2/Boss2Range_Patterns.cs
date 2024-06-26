using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

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
    [SerializeField] GameObject meleeBoss;
    [SerializeField] GameObject sceneLoadTrigger;
    [SerializeField] GameObject hud;

    [Header("Settings"), Space(10)]

    [SerializeField, Range(0,4)] public int currentPattern;

    [Header("Speech Bubbles"), Space(10)]

    [SerializeField] GameObject speechBubble1;
    [SerializeField] GameObject speechBubble2;

    [SerializeField] float speechOffset;

    bool speechBubbleSpawned;

    [Header("Pattern 1"), Space(10)]

    [SerializeField] GameObject bulletPattern1;

    [Space(10)]

    [SerializeField] float speedPattern1;
    [SerializeField] float orbitRange;
    [SerializeField] float shootCdPattern1;

    [Header("Pattern 2"), Space(10)]

    [SerializeField] GameObject pattern2BossWaypoint;
    [SerializeField] GameObject wheel;

    [Space(10)]

    [SerializeField] float maxPattern2Duration;
    [SerializeField] float pattern2Speed;

    bool startPattern2;
    GameObject wheelClone;

    [Header("Pattern 3"), Space(10)]

    [SerializeField] GameObject[] pattern3BossWaypoints;
    [SerializeField] GameObject bulletPattern3;

    [Space(10)]

    [SerializeField] float pattern3Speed;
    [SerializeField] float bulletSpeedPattern3;
    [SerializeField] float fireRatePattern3;
    [SerializeField] int maxPattern3Count;

    int pattern3Count;
    int targetIndex;

    [Header("Pattern 4"), Space(10)]

    [SerializeField] GameObject basePos;

    [Space(10)]

    [Header("Private"), Space(10)]

    [HideInInspector] public float clock;

    private void Start()
    {
        pattern3BossWaypoints = GameObject.FindGameObjectsWithTag("Boss 2 Range Pattern 3 Waypoint").OrderBy(m => m.gameObject.transform.GetSiblingIndex()).ToArray();
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        hud = GameObject.Find("HUD");

        bossBar = hud.transform.Find("Boss Bar").gameObject;
        Debug.Log(bossBar);
    }

    private void Update()
    {


        ///////////////////// - Pattern 0 - /////////////////////

        if (currentPattern == 0)
        {
            if (GameObject.FindGameObjectWithTag("Speech Bubble") == null)
            {
                speechBubbleSpawned = false;
            }

            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(7);

            if (!speechBubbleSpawned)
            {
                InstantiateSpeech(speechBubble1, speechOffset);
                speechBubbleSpawned = true;
            }
        }

        /////////////////////////////////////////////////////////

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
                    GameObject bulletClone = Instantiate(bulletPattern1, transform.position, Quaternion.identity);
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
                wheelClone = Instantiate(wheel, transform.position, Quaternion.identity);
                startPattern2 = true;
            }

            transform.rotation = Quaternion.Euler(0, 0, 0);
            float distance = Vector2.Distance(transform.position, pattern2BossWaypoint.transform.position);
            transform.position = (Vector2.MoveTowards(transform.position, pattern2BossWaypoint.transform.position, pattern2Speed * Time.deltaTime * distance));

            if (clock <= 0f)
            {
                clock = maxPattern2Duration;
            }

            if (clock > 0f)
            {
                clock -= Time.deltaTime;

                if (clock < 0f)
                {
                    startPattern2 = false;
                    Destroy(wheelClone);
                    currentPattern++;
                    meleeBoss.GetComponent<Boss2Melee_Patterns>().currentPattern = 3;
                    meleeBoss.GetComponent<Boss2Melee_Patterns>().clock = 0f;
                    meleeBoss.GetComponent<Boss2Melee_Patterns>().targetIndex = 0;
                }
            }
        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 3 - /////////////////////

        if (currentPattern == 3)
        {
            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(6);

            if (targetIndex == 1 || targetIndex == 3)
            {
                transform.position = (Vector2.MoveTowards(transform.position, pattern3BossWaypoints[targetIndex].transform.position, pattern3Speed * Time.deltaTime));

                if (targetIndex == 1)
                {
                    if (clock <= 0f)
                    {
                        clock = fireRatePattern3;
                    }

                    if (clock > 0f)
                    {
                        clock -= Time.deltaTime;

                        if (clock < 0f)
                        {
                            GameObject bulletClone = Instantiate(bulletPattern3, transform.position, Quaternion.identity);
                            bulletClone.GetComponent<Rigidbody2D>().velocity = Vector2.right * -bulletSpeedPattern3;
                        }
                    }
                }

                if (targetIndex == 3)
                {
                    if (clock <= 0f)
                    {
                        clock = fireRatePattern3;
                    }

                    if (clock > 0f)
                    {
                        clock -= Time.deltaTime;

                        if (clock < 0f)
                        {
                            GameObject bulletClone = Instantiate(bulletPattern3, transform.position, Quaternion.identity);
                            bulletClone.GetComponent<Rigidbody2D>().velocity = Vector2.right * bulletSpeedPattern3;
                        }
                    }
                }
            }
            else if(targetIndex == 0)
            {
                transform.position = pattern3BossWaypoints[0].transform.position;
                targetIndex++;
            }
            else if (targetIndex == 2)
            {
                transform.position = pattern3BossWaypoints[2].transform.position;
                targetIndex++;
            }


            if ((Vector2.Distance(transform.position, pattern3BossWaypoints[1].transform.position) <= .1f && targetIndex == 1) || (Vector2.Distance(transform.position, pattern3BossWaypoints[3].transform.position) <= .1f && targetIndex == 3))
            {
                pattern3Count++;
                targetIndex++;
                targetIndex %= 4;
                transform.position = pattern3BossWaypoints[targetIndex].transform.position;
            }

            if (pattern3Count == maxPattern3Count)
            {
                currentPattern = 1;
                clock = 0f;
                pattern3Count = 0;
                targetIndex = 0;
                meleeBoss.GetComponent<Boss2Melee_Patterns>().currentPattern = 1;
                meleeBoss.GetComponent<Boss2Melee_Patterns>().clock = 0f;
            }

        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 4 - /////////////////////

        if (currentPattern == 4)
        {
            Destroy(wheelClone);

            transform.position = basePos.transform.position;

            if (GameObject.FindGameObjectWithTag("Speech Bubble") == null)
            {
                speechBubbleSpawned = false;
            }

            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(7);

            if (!speechBubbleSpawned)
            {
                InstantiateSpeech(speechBubble2, speechOffset);
                speechBubbleSpawned = true;
            }
        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 5 - /////////////////////

        if (currentPattern == 5)
        {
            mainCamera.GetComponent<Camera_Follow>().SwitchCameraBehavior(8);

            GameObject.FindGameObjectWithTag("Traveling").GetComponent<Traveling_Behavior>().active = true;

            Destroy(transform.parent.gameObject);
        }

        /////////////////////////////////////////////////////////
    }

    private void InstantiateSpeech(GameObject speechBubble, float offset)
    {
        Instantiate(speechBubble, new Vector3(transform.position.x, transform.position.y + offset, transform.position.z), Quaternion.identity, transform);
    }
}
