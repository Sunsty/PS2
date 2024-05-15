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
    private float jumpCoef;

    private void FixedUpdate()
    {
        dirX = Input.GetAxis("Horizontal");
        Vector2 test = Vector2.right * dirX * acceleration;
        rb.AddForce(Vector2.right * dirX * acceleration);

        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * (jumpspeed * jumpCoef), ForceMode2D.Impulse);
            isJumping = false;
        }

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);

        Debug.Log(rb.velocity);
        Debug.Log(test);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpCoef = 1f;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpCoef = 0.5f;
        }
    }
}
