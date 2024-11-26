using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    /*public Animator animator;*/

    public float walkSpeed;
    public float jumpSpeed;

    public float crouchSpeedModifier = 0.5f;
    public float sprintSpeedModifier = 1.7f;

    public bool isCrouching;
    public bool isSprinting;

    [SerializeField] Collider2D standingCollider;
    [SerializeField] Transform overheadCheckCollider;
    [SerializeField] bool StartFacingLeft;
    [SerializeField] bool CutsceneNeeded;

    const float overheadCheckRadius = 0.2f;

    public Rigidbody2D body;
    public Collider2D groundCheck;
    bool facingRight = true;

    public LayerMask groundMask;
    float xInput;

    public bool isGrounded;

    public PhysicsMaterial2D zeroFrictionMaterial;
    public PhysicsMaterial2D groundedFrictionMaterial;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        if (StartFacingLeft)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpSpeed);
    }

    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal");
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            /*animator.SetBool("isJumping", true);*/
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            isCrouching = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }
    }

    void Update()
    {
        GetInput();
    }

    void MoveWithInput()
    {
        if (isGrounded && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)))
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            isGrounded = false;
        }

        float xVal = xInput * walkSpeed * 100 * Time.deltaTime;

        if (isSprinting)
        {
            xVal *= sprintSpeedModifier;
        }
        if (isCrouching)
        {
            xVal *= crouchSpeedModifier;
        }

        body.velocity = new Vector2(xVal, body.velocity.y);

        if (facingRight && xInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            facingRight = false;
        }
        else if (!facingRight && xInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            facingRight = true;
        }

        /*animator.SetFloat("xVelocity", Mathf.Abs(body.velocity.x));*/
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
        /*animator.SetBool("isJumping", !isGrounded);*/
        ApplyFriction();
    }
    void ApplyFriction()
    {
        if (isGrounded)
        {
            body.sharedMaterial = groundedFrictionMaterial;
        }
        else
        {
            body.sharedMaterial = zeroFrictionMaterial;
        }
    }
    void Crouch(bool isCrouching)
    {
        if (isCrouching)
        {
            if (Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, groundMask))
            {
                standingCollider.enabled = false; 
            }
            else
            {
                standingCollider.enabled = true; 
            }
        }
        else
        {
            standingCollider.enabled = true;
        }
        /*animator.SetBool("isCrouching", isCrouching);*/
    }


    public void SetSpeed(float newSpeed, float newJump)
    {
        walkSpeed = newSpeed;
        jumpSpeed = newJump;
    }

    public void ResetSpeed()
    {
        walkSpeed = 2.5f;
        jumpSpeed = 20f;
    }

    void FixedUpdate()
    {
        CheckGround();
        MoveWithInput();
        Crouch(isCrouching);
    }
}