using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    // Public Fields
    public Transform groundCheckCollider;
    public LayerMask groundLayer;

    public float speed = 3;
    public float jumpPower = 10;
    public int totalJump;

    //Private Fields
    Rigidbody2D rb;
    Animator animator;

    float groundCheckRadius = 0.2f;
    float horizontalValue;
    int availableJump;
    bool turnRight;
    bool isGrounded;
    bool multipleJump;
    float jumpCooldown = 0.1f;
    float jumpTimer = 0f;

    void Awake()
    {
        availableJump = totalJump;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        // Increment the jump timer every frame
        jumpTimer += Time.deltaTime;
        horizontalValue = Input.GetAxisRaw("Horizontal");


        if (Input.GetButtonDown("Jump") && jumpTimer >= jumpCooldown)
        {
            Jump();

        }
  

        animator.SetFloat("yVelocity", rb.velocity.y * 5 );
    }

    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue);
    }

 
    void Move(float dir) {
        #region Move & Run
        float xVal = dir * speed * 100 * Time.fixedDeltaTime;
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;

        //turning

        if (turnRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            turnRight = false;
        } else if (!turnRight && dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            turnRight = true;
        }

        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        #endregion

     
    }

    void Jump()
    {
        #region Jump

        if (isGrounded )
        {
            multipleJump = true;
            availableJump--;
            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("Jump", true);
            jumpTimer = 0;

        }
        else
        {
            if (multipleJump && availableJump > 0)
            {
                availableJump--;
                float secondJumpPower = jumpPower - 3;
                rb.velocity = Vector2.up * secondJumpPower;
                jumpTimer = 0;

            }
        }
        #endregion
    }

    void GroundCheck() {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        Collider2D[] collider = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (collider.Length > 0)
        {
            isGrounded = true;

            if (!wasGrounded)
            {
                availableJump = totalJump;
                multipleJump = false;

            }

        }
      
            animator.SetBool("Jump", !isGrounded);

    }


}
