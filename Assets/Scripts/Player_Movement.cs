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
///     Good feeling
/// 
/// - Jump -
/// 
///     Only when grounded
///     Hold JUMP to go higher *WIP*
///     Reset Y velocity #DONE#
///     Good Feeling
/// 
/// - Fly system -
///     
///     Gravity affected
///     Limited time?
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
    public Rigidbody2D rb;

    public float maxVelocity;
    public float acceleration;
    public float jumpspeed;
    
    public float dirX;
    private bool isJumping;
    private Vector2 tempClamp;

    public bool isGrounded;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    private bool wantsJumping;

    private void FixedUpdate()
    {
        dirX = Input.GetAxis("Horizontal");
        Vector2 test = Vector2.right * dirX * acceleration;
        rb.AddForce(Vector2.right * dirX * acceleration);

        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpspeed, ForceMode2D.Impulse);
            isJumping = false;
        }

        tempClamp = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
        rb.velocity = new Vector2(tempClamp.x , rb.velocity.y);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            wantsJumping = true;
        }
        else
        {
            wantsJumping = false;
        }

        if (wantsJumping && isGrounded)
        {
            isJumping = true;
        }

        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);    
        
    }

}
