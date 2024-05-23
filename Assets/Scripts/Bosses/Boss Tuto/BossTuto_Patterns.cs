using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] GameObject boss;

    [Header("Settings")]

    [SerializeField, Range(1, 3)] int currentPattern;

    [Header("Pattern 1")]

    [SerializeField] GameObject wall;

    [Header("Pattern 2")]

    [SerializeField] GameObject aimTarget;

    [Header("Pattern 3")]

    [SerializeField] GameObject fullWall;

    [Header("Private")]

    float clock;


    private void Update()
    {
        ///////////////////// - Pattern 1 - /////////////////////



        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 2 - /////////////////////



        /////////////////////////////////////////////////////////

        ///////////////////// - Pattern 3 - /////////////////////



        /////////////////////////////////////////////////////////
    }
}
