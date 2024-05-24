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

    [Header("Settings"), Space(10)]

    [SerializeField, Range(1,3)] int currentPattern;

    [Header("Pattern 1"), Space(10)]

    [SerializeField] float speedPattern1;
    [SerializeField] float orbitRange;
    [SerializeField] float shootCdPattern1;

    [Header("Pattern 2"), Space(10)]



    [Header("Pattern 3"), Space(10)]



    [Header("Private"), Space(10)]

    float clock;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void FixedUpdate()
    {
        ///////////////////// - Pattern 1 - /////////////////////

        if (currentPattern == 1)
        {

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
                    Debug.Log("Shoot");
                }
            }
        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 2 - /////////////////////

        if (currentPattern == 2)
        {

        }

        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 3 - /////////////////////



        /////////////////////////////////////////////////////////
    }
}
