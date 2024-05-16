using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 
/// 
/// ////////////////////////////////// TODO /////////////////////////////// 
/// 
/// - Base Movement -
/// 
///     Left or right movement #DONE#
///     acceleration/decceleration *WIP*
///     Good feeling *WIP*
/// 
/// - Jump -
/// 
///     Only when grounded *WIP*
///     Hold JUMP to go higher *WIP*
///     Reset Y velocity #DONE#
///     Good Feeling *WIP*
/// 
/// - Fly system -
///     
///     Gravity affected
///     Limited time / Reset on grounded
///     Has to jump beforhand / only in the air
///     
/// - Dash -
/// 
///     Independent from - Base Movement -
///     Instant short dash
///     Possible while grounded
///     Possible while in the air
///     Cooldown with feedback
/// 
/// ///////////////////////////////////////////////////////////////////////
/// 
/// </summary>

public class Player_Movement : MonoBehaviour
{

    [Header("Imports")]

    public Rigidbody2D rb;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    [Header("Settings")]

    public float maxVelocity;
    public float acceleration;
    public float jumpspeed;
    public float flyTimeOffset;
    public float flySpeed;

    [Header("")]

    float dirX;
    bool isJumping;
    Vector2 tempClamp;

    public bool isGrounded;

    bool wantsJumping;
    Vector2 jumpDir;
    bool wantsFlying;
    bool isFlying;
    bool hasJumped;
    float jumpedCounter;
    bool canFly;

    private void FixedUpdate()
    {
        dirX = Input.GetAxis("Horizontal");
        Vector2 test = Vector2.right * dirX * acceleration;
        rb.AddForce(Vector2.right * dirX * acceleration);


        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(jumpDir, ForceMode2D.Impulse);
            isJumping = false;
            hasJumped = true;
        }

        if (isFlying && canFly)
        {
            rb.AddForce(jumpDir * flySpeed);
        }
        
        tempClamp = Vector2.ClampMagnitude(new Vector2(rb.velocity.x,0), maxVelocity);
        rb.velocity = new Vector2(tempClamp.x , rb.velocity.y);        

    }

    private void Update()
    {

        ////////////////////////////////////////////////////////////

        if (Input.GetKeyDown(KeyCode.Space))
        {
            wantsJumping = true;
        }
        else
        {
            wantsJumping = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            wantsFlying = true;
        }
        else
        {
            wantsFlying = false;
        }

        ////////////////////////////////////////////////////////////

        if (wantsJumping && isGrounded)
        {
            isJumping = true;
        }

        if (wantsFlying)
        {
            isFlying = true;
        }
        else
        {
            isFlying = false;
        }

        ////////////////////////////////////////////////////////////

        if (rb.velocity.y == 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        ////////////////////////////////////////////////////////////

        jumpDir = new Vector3(dirX * 45, jumpspeed, 0);

        if (hasJumped)
        {
            if (jumpedCounter <= 0f)
            {
                jumpedCounter = flyTimeOffset;
            }

            canFly = false;

            hasJumped = false;
        }

        if (jumpedCounter > 0f)
        {
            jumpedCounter -= Time.deltaTime;

            if (jumpedCounter < 0f)
            {
                canFly = true;
            }
        }

        if (isGrounded)
        {
            jumpedCounter = 0f;
        }
    }

}
