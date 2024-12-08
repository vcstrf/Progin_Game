using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
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
    [SerializeField] float overheadCheckRadius = 0.2f;
    [SerializeField] private bool canDoubleJump;

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

    void MoveWithInput()
    {
        xInput = Input.GetAxis("Horizontal");

        if (isGrounded)
        {
            canDoubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                canDoubleJump = true;
                Jump();
            }
            else if (canDoubleJump)
            {
                canDoubleJump = false;
                Jump();
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            isCrouching = true;
        }

        if (Input.GetKeyUp(KeyCode.S) && !Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, groundMask))
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

        Flip();
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;

        ApplyFriction();

    }

    void Flip()
    {
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

    void CheckCrouchState()
    {
        bool isObstacleOverhead = Physics2D.Raycast(overheadCheckCollider.position, Vector2.up, overheadCheckRadius, groundMask);
        if (isGrounded)
        {
            if (!Input.GetKey(KeyCode.S) && !isObstacleOverhead)
            {
                isCrouching = false;
                standingCollider.enabled = true;
            }
            else if (Input.GetKey(KeyCode.S) || isObstacleOverhead)
            {
                isCrouching = true;
                standingCollider.enabled = false;
            }
        }
    }

    void FixedUpdate()
    {
        CheckGround();
        MoveWithInput();
        CheckCrouchState();
    }
}
