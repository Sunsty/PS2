using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// ////////////////////////////////// TODO /////////////////////////////// 
/// 
/// - Base Movement -
/// 
///     Left or right movement #DONE#
///     acceleration/decceleration #DONE#
///     Good feeling #DONE#
///     DOWN to go off a platform #DONE#
/// 
/// - Jump -
/// 
///     Only when grounded #DONE#
///     Reset Y velocity #DONE#
///     Good Feeling #DONE#
/// 
/// - Fly system -
///     
///     Gravity affected #DONE#
///     Limited time / Reset on grounded #DONE# 
///         FOR DISPLAY : |flyingFuel| |maxFlyingFuel|
///     Has to jump beforhand / only in the air #DONE#
///     Glide when out of fuel
///     
/// - Dash -
/// 
///     Independent from - Base Movement - #DONE#
///     Instant short dash #DONE#
///     Possible while grounded #DONE# 
///     Possible while in the air #DONE#
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
    public Image fuelBar;

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
    public float maxFlyingFuel;
    public float downLenght;

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
    Vector2 currentVelocity;
    float tempForce;
    bool clampOverwrite;
    float flyingFuel;
    private bool wantsDown;
    private bool wentThrough;
    private bool hasDown;
    private float downCounter;

    private void FixedUpdate()
    {
        ////////////////////////////////////////////////////////////

        dirX = Input.GetAxis("Horizontal");
        Vector2 test = Vector2.right * dirX * acceleration;

        rb.AddForce(Vector2.right * dirX * acceleration);

        ////////////////////////////////////////////////////////////

        if (isJumping)
        {
            rb.AddForce(jumpDir, ForceMode2D.Impulse);
            isJumping = false;
            hasJumped = true;
        }

        ////////////////////////////////////////////////////////////

        if (isFlying && canFly && flyingFuel > 0f)
        {
            flyingFuel -= Time.fixedDeltaTime;
            rb.AddForce(jumpDir * flySpeed);
        }

        ////////////////////////////////////////////////////////////

        /// - GLIDE - ///

        ////////////////////////////////////////////////////////////

        float tempClampX = Mathf.Clamp(rb.velocity.x, -maxVelocityX, maxVelocityX);
        float tempClampY = Mathf.Clamp(rb.velocity.y, -maxVelocityY, maxVelocityY);
        rb.velocity = new Vector2(tempClampX, tempClampY);

        ////////////////////////////////////////////////////////////

        if (isDashing)
        {

            if (dashCounter <= 0f)
            {
                dashCounter = dashCooldown;
            }

            if (dirX > 0f)
            {
                tempForce = dashForce;
            }
            else if (dirX < 0f)
            {
                tempForce = -dashForce;
            }

            isDashing = false;
            canDash = false;
        }

        if (dashCounter > 0f)
        {
            clampOverwrite = true;

            rb.AddForce(Vector2.right * tempForce * dashCounter, ForceMode2D.Impulse);

            dashCounter -= Time.fixedDeltaTime;

            if (dashCounter <= 0f)
            {
                clampOverwrite = false;
                canDash = true;
            }
        }
        
        if (!clampOverwrite)
        {
            if (rb.velocity.x > 15f)
            {
            rb.velocity = Vector2.SmoothDamp(rb.velocity, new Vector2(maxSpeed, rb.velocity.y), ref currentVelocity, 1f, Mathf.Infinity, Time.fixedDeltaTime);
            }
        
            if (rb.velocity.x < 15f)
            {
            rb.velocity = Vector2.SmoothDamp(rb.velocity, new Vector2(maxSpeed, rb.velocity.y), ref currentVelocity, 1f, Mathf.Infinity, Time.fixedDeltaTime);
            }
        }

        ////////////////////////////////////////////////////////////

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

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            wantsDashing = true;
        }
        else
        {
            wantsDashing = false;
        }

        /// Down ///
        
        if (Input.GetKey(KeyCode.S))
        {
            wantsDown = true;
        }
        else
        {
            wantsDown = false;
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

        if (wantsDashing && dirX != 0)
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
            flyingFuel = maxFlyingFuel;
        }

        ////////////////////////////////////////////////////////////

        fuelBar.fillAmount = flyingFuel / maxFlyingFuel;

        ////////////////////////////////////////////////////////////

        if (wantsDown)
        {
            GameObject[] platforms = GameObject.FindGameObjectsWithTag("OW Platform");
            foreach (GameObject plat in platforms)
            {
                plat.GetComponent<Collider2D>().enabled = false;
            }

            hasDown = true;
        }

        else if (!wantsDown && !wentThrough)
        {
            GameObject[] platforms = GameObject.FindGameObjectsWithTag("OW Platform");
            foreach (GameObject plat in platforms)
            {
                plat.GetComponent<Collider2D>().enabled = true;
            }
        }

        if (hasDown)
        {
            if (downCounter <= 0f)
            {
                downCounter = downLenght;
            }

            wentThrough = true;

            hasDown = false;
        }

        if (downCounter > 0f)
        {
            downCounter -= Time.deltaTime;

            if (downCounter < 0f)
            {
                wentThrough = false;
            }
        }
    }
}
