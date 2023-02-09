using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    // Public Fields
    public Transform groundCheckCollider;
    public LayerMask groundLayer;
    public AudioSource audioSource;
    public AudioClip[] audioClip;
    public float speed = 3;
    public float jumpPower = 10;
    public int totalJump;
    public int attackCombo;
    public int maxAttackCombo = 3;

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
    bool attackCanDo;

    void Awake()
    {
        availableJump = totalJump;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the jump timer every frame
        jumpTimer += Time.deltaTime;
        horizontalValue = Input.GetAxisRaw("Horizontal");


        if (Input.GetButtonDown("Jump") && jumpTimer >= jumpCooldown)
        {
            Jump();

        }
        if (Input.GetKeyDown(KeyCode.C) && !attackCanDo)
        {
            Attack();

        }



        animator.SetFloat("yVelocity", rb.velocity.y * 5);
    }

    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue);
    }


    void Move(float dir)
    {
        #region Move & Run
        float xVal = dir * speed * 100 * Time.fixedDeltaTime;
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;

        //turning

        if (turnRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            turnRight = false;
        }
        else if (!turnRight && dir > 0)
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

        if (isGrounded)
        {
            multipleJump = true;
            availableJump--;
            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("Jump", true);
            animator.SetFloat("MultipleJump", 0);

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
                animator.SetFloat("MultipleJump", 1);


            }
        }
        #endregion
    }

    void GroundCheck()
    {
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

    void Attack()
    {
        Debug.Log("" + attackCombo);
        attackCanDo = true;
        animator.SetTrigger("" + attackCombo);
        audioSource.clip = audioClip[attackCombo];
        audioSource.Play();
    }
    public void StartCombo()
    {
        attackCanDo = false;
        if (attackCombo < maxAttackCombo)
        {
            attackCombo++;
        }

    }
    public void EndCombo()
    {
        attackCanDo = false;
        attackCombo = 0;
    }

}
