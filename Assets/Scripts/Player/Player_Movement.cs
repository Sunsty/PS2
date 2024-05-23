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
///     Glide when out of fuel #DONE#
///     
/// - Dash -
/// 
///     Independent from - Base Movement - #DONE#
///     Instant short dash #DONE#
///     Possible while grounded #DONE# 
///     Possible while in the air #DONE#
///     Cooldown #DONE#
/// 
/// ///////////////////////////////////////////////////////////////////////
/// 
/// </summary>

public class Player_Movement : MonoBehaviour
{

    [Header("Imports")]

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheckLeft;
    [SerializeField] Transform groundCheckRight;
    [SerializeField] Image fuelBar;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Player_Health player_Health;

    [Header("Settings")]

    [SerializeField] float acceleration;
    [SerializeField] float jumpspeed;
    [SerializeField] float flyTimeOffset;
    [SerializeField] float flySpeed;
    [SerializeField] float dashLenght;
    [SerializeField] float dashForce;
    [SerializeField] float maxVelocityX;
    [SerializeField] float maxVelocityY;
    [SerializeField] float maxFlyingFuel;
    [SerializeField] float downLenght;
    [SerializeField] float glideCoef;
    [SerializeField] float dashCooldown;

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
    float tempClamp;
    private bool hasDashCd = true;
    private bool hasDashed;
    private float dashCdCounter;


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

        if (isFlying && canFly && flyingFuel < 0f)
        {
            tempClamp = glideCoef;
        }
        else
        {
            tempClamp = 1;
        }

        ////////////////////////////////////////////////////////////

        float tempClampX = Mathf.Clamp(rb.velocity.x, -maxVelocityX, maxVelocityX);
        float tempClampY = Mathf.Clamp(rb.velocity.y, -maxVelocityY * tempClamp, maxVelocityY);
        rb.velocity = new Vector2(tempClampX, tempClampY);

        ////////////////////////////////////////////////////////////

        if (isDashing)
        {

            if (dashCounter <= 0f)
            {
                dashCounter = dashLenght;
            }

            if (dirX > 0f)
            {
                tempForce = dashForce;
            }
            else if (dirX < 0f)
            {
                tempForce = -dashForce;
            }

            player_Health.InitDash(dashLenght);

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
                hasDashed = true;
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
        
        if (hasDashed)
        {
            if (dashCdCounter <= 0f)
            {
                dashCdCounter = dashCooldown;
            }

            hasDashCd = false;

            hasDashed = false;
        }

        if (dashCdCounter > 0f)
        {
            dashCdCounter -= Time.fixedDeltaTime;

            if ( dashCdCounter < 0f)
            {
                hasDashCd = true;
            }
        }
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

        if (wantsDashing && dirX != 0 && hasDashCd)
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
