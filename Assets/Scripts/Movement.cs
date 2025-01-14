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

    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing;
    [SerializeField] private float dashingPower = 40f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 0.7f;

/*    [SerializeField] Collider2D standingCollider;
    [SerializeField] Transform overheadCheckCollider;*/
    [SerializeField] bool StartFacingLeft;
    [SerializeField] bool CutsceneNeeded;
    [SerializeField] float overheadCheckRadius = 0.2f;
    [SerializeField] private int airJumpCounter = 0;
    [SerializeField] private int maxAirJumps = 1;
    [SerializeField] private TrailRenderer trail;

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

    void Update()
    {
        GetInput();
        CheckGround();
        MoveWithInput();
/*        CheckCrouchState();*/
    }

    void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpSpeed);
    }

    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal");

        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
                airJumpCounter = 0;
            }
            else if (airJumpCounter < maxAirJumps)
            {
                Jump();
                airJumpCounter++;
            }
        }

/*        if (Input.GetKeyDown(KeyCode.S))
        {
            isCrouching = true;
        }

        if (Input.GetKeyUp(KeyCode.S) && !Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, groundMask))
        {
            isCrouching = false;
        }*/

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }

        if (Input.GetMouseButton(1) && canDash && Mathf.Abs(body.velocity.x) > 1.5f)
        {
            StartCoroutine(Dash());
        }
    }

    void MoveWithInput()
    {
        if (isGrounded && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)))
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            isGrounded = false;
        }

        float xVal = xInput * walkSpeed;

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
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;

        if (isGrounded)
        {
            airJumpCounter = 0;
        }

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

/*    void CheckCrouchState()
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
    }*/

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        body.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        trail.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trail.emitting = false;
        body.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
