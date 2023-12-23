using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float doublejumpSpeed;
    private Rigidbody2D rigidBody2D;
    private Animator animator;
    private BoxCollider2D feetCollider;
    private CircleCollider2D bodyCollider;
    private bool isOnGround;
    private bool canDoubleJump;
    private bool isOnOneWayPlatform;
    public float restoreTime;
    private bool isLadder;
    private bool isClimbing;
    private bool isJumping;
    private bool isFalling;
    private bool isDoubleJumping;
    private bool isDoubleFalling;
    public float climbSpeed;
    private float playerGravity;

    private PlayerInputAction controls;
    private Vector2 move;

    private void Awake()
    {
        controls = new PlayerInputAction();
        controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += ctx => move = Vector2.zero;
        controls.GamePlay.Jump.started += ctx => Jump();
    }

    private void OnEnable()
    {
        controls.GamePlay.Enable();
    }
    private void OnDisable()
    {
        controls.GamePlay.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        feetCollider = GetComponent<BoxCollider2D>();
        playerGravity = rigidBody2D.gravityScale;
        bodyCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.isGameActive)
        {
            Flip();
            Run();
            //Jump();
            CheckAirStatus();
            Climb();
            CheckIsOnGround();
            CheckLadder();
            OneWayPlatformCheck();
            CheckAirStatus();
            SwitchAnimation();
        }
    }

    void Flip()
    {
        bool doesPlayerHasXAxisSpeed = Mathf.Abs(rigidBody2D.velocity.x) > 0.01f;
        if (doesPlayerHasXAxisSpeed)
        {
            if(rigidBody2D.velocity.x > 0.01f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (rigidBody2D.velocity.x < -0.01f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    void Run()
    {
        //float moveDirection = Input.GetAxis("Horizontal");
        //Vector2 playerVelocity = new Vector2(moveDirection * runSpeed, rigidBody2D.velocity.y);
        //rigidBody2D.velocity = playerVelocity;
        //bool doesPlayerHasXAxisSpeed = Mathf.Abs(rigidBody2D.velocity.x) > Mathf.Epsilon;
        //animator.SetBool("Run", doesPlayerHasXAxisSpeed);

        Vector2 playerVelocity = new Vector2(move.x * runSpeed, rigidBody2D.velocity.y);
        rigidBody2D.velocity = playerVelocity;
        bool doesPlayerHasXAxisSpeed = Mathf.Abs(rigidBody2D.velocity.x) > Mathf.Epsilon;
        animator.SetBool("Run", doesPlayerHasXAxisSpeed);

    }

    void CheckIsOnGround()
    {
        isOnGround = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))|| feetCollider.IsTouchingLayers(LayerMask.GetMask("MovingPlatform")) || feetCollider.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform")) || feetCollider.IsTouchingLayers(LayerMask.GetMask("DestroyableLayer")) || (!bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")) && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")));
        isOnOneWayPlatform = feetCollider.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
    } 

    void Jump()
    {
        //if (Input.GetButtonDown("Jump"))
        //{
            if (isOnGround)
            {
                animator.SetBool("Jump", true);
                Vector2 jumpVelocity = new Vector2(0.0f, jumpSpeed);
                rigidBody2D.velocity = Vector2.up * jumpVelocity;
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    animator.SetBool("DoubleJump", true);
                    Vector2 doubleJumpVelocity = new Vector2(0.0f, doublejumpSpeed);
                    rigidBody2D.velocity = Vector2.up * doubleJumpVelocity;
                    canDoubleJump = false;
                }
            }
        //}
    }

    //void Attack()
    //{
    //    if (Input.GetButtonDown("Attack"))
    //    {
    //        animator.SetTrigger("Attack");
    //    }
    //}

    void SwitchAnimation()
    {
        animator.SetBool("Idle", false);
        if (animator.GetBool("Jump"))
        {
            if (rigidBody2D.velocity.y < 0.01f)
            {
                animator.SetBool("Jump", false);
                animator.SetBool("Fall", true);
            }
        }else if (isOnGround)
        {
            animator.SetBool("Fall", false);
            animator.SetBool("Idle", true);
        }

        if (animator.GetBool("DoubleJump"))
        {
            if (rigidBody2D.velocity.y < 0.01f)
            {
                animator.SetBool("DoubleJump", false);
                animator.SetBool("DoubleFall", true);
            }
        }else if (isOnGround)
        {
            animator.SetBool("DoubleFall", false);
            animator.SetBool("Idle", true);
        }
        if (rigidBody2D.velocity.magnitude < 0.01f)
            animator.SetBool("Idle", true);
    }

    void OneWayPlatformCheck()
    {
        if (isOnGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }

        //float moveY = Input.GetAxis("Vertical");
        if(isOnOneWayPlatform && move.y < -0.01f)
        {
            gameObject.layer = LayerMask.NameToLayer("OneWayPlatform");
            Invoke("RestorePlayerLayer", restoreTime);
        }
    }

    void RestorePlayerLayer()
    {
        if(!isOnGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    void CheckLadder()
    {
        isLadder = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))|| bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));

    }
    void Climb()
    {
        if (isLadder)
        {
            rigidBody2D.gravityScale = 0.0f;
            float moveY = move.y;
            if (Mathf.Abs(moveY) > 0.5f&& (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))|| moveY<0.0f))
            {
                animator.SetBool("IsClimbing", true);
                animator.SetBool("Jump", false);
                animator.SetBool("Fall", false);
                animator.SetBool("DoubleFall", false);
                animator.SetBool("DoubleJump", false);
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, moveY * climbSpeed);
            }
            else
            {
                animator.SetBool("IsClimbing", false);
                if (isJumping || isFalling || isDoubleFalling || isDoubleJumping)
                {
                    rigidBody2D.gravityScale = playerGravity ;
                }
                else
                {
                    rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, 0.0f);
                }
            }
        }
        else
        {
            animator.SetBool("IsClimbing", false);
            rigidBody2D.gravityScale = playerGravity;
        }
    }
    void CheckAirStatus()
    {
        isJumping = animator.GetBool("Jump");
        isFalling = animator.GetBool("Fall");
        isDoubleJumping = animator.GetBool("DoubleJump");
        isDoubleFalling = animator.GetBool("DoubleFall");
        isClimbing = animator.GetBool("IsClimbing");
    }
}
