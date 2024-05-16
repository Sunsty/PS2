using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// ////////////////////////////////// TODO /////////////////////////////// 
/// 
/// - Base Movement -
/// 
///     Left or right movement #DONE#
///     acceleration/decceleration #DONE#
///     Good feeling #DONE#
/// 
/// - Jump -
/// 
///     Only when grounded #DONE#
///     Hold JUMP to go higher 
///     Reset Y velocity #DONE#
///     Good Feeling #DONE#
/// 
/// - Fly system -
///     
///     Gravity affected #DONE#
///     Limited time / Reset on grounded *WIP*
///     Has to jump beforhand / only in the air #DONE#
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
    public float dashCooldown;
    public float dashForce;
    public float maxVelocityX;
    public float maxVelocityY;

    [Header("")]

    float dirX;
    bool isJumping;
    float maxSpeed;

    bool isGrounded;

    bool wantsJumping;
    Vector2 jumpDir;
    bool wantsFlying;
    bool isFlying;
    bool hasJumped;
    float jumpedCounter;
    bool canFly;
    bool isDashing;
    bool wantsDashing;
    float dashCounter;
    bool canDash = true;
    private Vector2 currentVelocity;


    private void FixedUpdate()
    {
        dirX = Input.GetAxis("Horizontal");
        Vector2 test = Vector2.right * dirX * acceleration;

        rb.AddForce(Vector2.right * dirX * acceleration);


        if (isJumping)
        {
            rb.AddForce(jumpDir, ForceMode2D.Impulse);
            isJumping = false;
            hasJumped = true;
        }

        if (isFlying && canFly)
        {
            rb.AddForce(jumpDir * flySpeed);
        }

        float tempClampX = Mathf.Clamp(rb.velocity.x, -maxVelocityX, maxVelocityX);
        float tempClampY = Mathf.Clamp(rb.velocity.y, -maxVelocityY, maxVelocityY);
        rb.velocity = new Vector2(tempClampX, tempClampY);

        if (isDashing)
        {

            if (dashCounter <= 0f)
            {
                dashCounter = dashCooldown;
            }

            if (dirX > 0f)
            {
                rb.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
            }
            else if (dirX < 0f)
            {
                rb.AddForce(Vector2.right * -dashForce, ForceMode2D.Impulse);
            }

            isDashing = false;
            canDash = false;
        }

        if (dashCounter > 0f)
        {

            dashCounter -= Time.fixedDeltaTime;

            if (dashCounter <= 0f)
            {
                canDash = true;
            }
        }
        

        if (rb.velocity.x > 15f)
        {
        rb.velocity = Vector2.SmoothDamp(rb.velocity, new Vector2(maxSpeed, rb.velocity.y), ref currentVelocity, 1f, Mathf.Infinity, Time.fixedDeltaTime);
        }
        
        if (rb.velocity.x < 15f)
        {
        rb.velocity = Vector2.SmoothDamp(rb.velocity, new Vector2(maxSpeed, rb.velocity.y), ref currentVelocity, 1f, Mathf.Infinity, Time.fixedDeltaTime);
        }





        Debug.Log(rb.velocity);
    }

    private void Update()
    {

        ////////////////////////////////////////////////////////////
        
        /// Jump ///

        if (Input.GetKeyDown(KeyCode.Space))
        {
            wantsJumping = true;
        }
        else
        {
            wantsJumping = false;
        }

        /// Fly ///

        if (Input.GetKey(KeyCode.Space))
        {
            wantsFlying = true;
        }
        else
        {
            wantsFlying = false;
        }

        /// Dash ///

        if (Input.GetKeyDown(KeyCode.E) && canDash)
        {
            wantsDashing = true;
        }
        else
        {
            wantsDashing = false;
        }

        ////////////////////////////////////////////////////////////
        
        /// Jump ///

        if (wantsJumping && isGrounded)
        {
            isJumping = true;
        }

        /// Fly ///

        if (wantsFlying)
        {
            isFlying = true;
        }
        else
        {
            isFlying = false;
        }

        /// Dash ///

        if (wantsDashing)
        {
            isDashing = true;
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

        ////////////////////////////////////////////////////////////



    }

}
