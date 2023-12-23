using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNew : ItemGenerator, IDamageable, IEnemyMovable
{
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public Rigidbody2D rigidBody { get; set; }
    public bool isFacingRight { get; set; } = true;

    public bool isWithAttackRange { get; set; } = false;
    public bool isWithChaseRange { get; set; } = false;

    [SerializeField] private EnemyIdleSOBase enemyIdleBase;
    [SerializeField] private EnemyJumpSOBase enemyJumpeBase;
    [SerializeField] private EnemyAttackSOBase enemyAttackBase;
    [SerializeField] private EnemyChaseSOBase enemyChaseBase;
    [SerializeField]
    private PolygonCollider2D hitCollider;

    public bool canJump;
    public Collider2D foot;
    public int weight;
    public int height;
    public float idleSpeed;
    public float chaseSpeed;
    public int damage;
    public float flashTime;
    public GameObject bloodEffect;
    public GameObject floatPoint;
    public int health;
    public BoxCollider2D headDetec;
    public BoxCollider2D kneeDetec;
    public Animator animator;
    public float jumpYspeed = 4f;
    public bool attackWithCollide = false;
    private bool inCamera = false;

    private SpriteRenderer spriteRender;
    private Color oringinalColor;

    public EnemyIdleSOBase enemyIdleBaseInstace { get; set; }
    public EnemyJumpSOBase enemyJumpeBaseInstace { get; set; }
    public EnemyAttackSOBase enemyAttackBaseInstace { get; set; }
    public EnemyChaseSOBase enemyChaseBaseInstace { get; set; }


    #region Animation Triggers

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSound
    }
    #endregion

    public EnemyStateMachine stateMachine { get; set; }
    public EnemyIdleState idleState { get; set; }
    public EnemyChaseState chaseState { get; set; }
    public EnemyAttackState attackState { get; set; }
    public EnemyJumpState jumpState { get; set; }


    private void Awake()
    {
        enemyIdleBaseInstace = Instantiate(enemyIdleBase);
        enemyJumpeBaseInstace = Instantiate(enemyJumpeBase);
        enemyAttackBaseInstace = Instantiate(enemyAttackBase);
        enemyChaseBaseInstace = Instantiate(enemyChaseBase);

        animator = GetComponent<Animator>();

        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);
        jumpState = new EnemyJumpState(this, stateMachine);

    }
    // Start is called before the first frame update
    private void Start()
    {
        CurrentHealth = MaxHealth;
        rigidBody = GetComponent<Rigidbody2D>();
        enemyIdleBaseInstace.Initialize(gameObject, this);
        enemyJumpeBaseInstace.Initialize(gameObject, this);
        enemyAttackBaseInstace.Initialize(gameObject, this);
        enemyChaseBaseInstace.Initialize(gameObject, this);
        stateMachine.Initialize(idleState);

        if (canJump)
        {
            foot = GetComponent<BoxCollider2D>();
        }

        spriteRender = GetComponent<SpriteRenderer>();
        oringinalColor = spriteRender.color;


    }

    private void Update()
    {
        if (!inCamera)
            return;
        if (health <= 0)
        {
            Destroy(gameObject);
            GenerateItems(false);
            return;
        }

        stateMachine.currentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        if (!inCamera)
            return;
        stateMachine.currentEnemyState.PhysicsUpdate();
    }

    private void LateUpdate()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        if(screenPoint.x > -0.5 && screenPoint.x < 1.5 && screenPoint.y > -0.5 && screenPoint.y < 1.5 && screenPoint.z > 0)
        {
            inCamera = true;
        }
        else
        {
            inCamera = false;
        }
    }


    #region HP Management
    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0.0f)
        {
            Die();
        }
    }

    public void Die()
    {
    }

    #endregion HP Management


    #region Movement Management
    public void MoveEnemy(Vector2 velocity)
    {
        //throw new System.NotImplementedException();
    }

    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        if (isFacingRight && velocity.x < 0.0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180.0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
        }
        else if (!isFacingRight && velocity.x > 0.0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0.0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
        }
    }
    #endregion Movement Management

    private void AniamtionTriggerEvent(AnimationTriggerType triggerType)
    {
        stateMachine.currentEnemyState.AnimationTriggerEvent(triggerType);
    }


    public bool isOnGround()
    {
        return foot.IsTouchingLayers(LayerMask.GetMask("Ground")) || foot.IsTouchingLayers(LayerMask.GetMask("MovingPlatform")) || foot.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform")) || foot.IsTouchingLayers(LayerMask.GetMask("DestroyableLayer")) || foot.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }

    public void TakeDamage(int damageGot)
    {
        GameObject gb = Instantiate(floatPoint, transform.position, Quaternion.identity);
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damageGot.ToString();
        health -= damageGot;
        FlashColor(flashTime);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        GameController.cameraShake.Shake();
    }
    void FlashColor(float time)
    {
        spriteRender.color = Color.red;
        Invoke("ResetColor", time);
    }
    void ResetColor()
    {
        spriteRender.color = oringinalColor;
    }

    public void Flip()
    {
        bool doesPlayerHasXAxisSpeed = Mathf.Abs(rigidBody.velocity.x) > 0.01f;
        if (doesPlayerHasXAxisSpeed)
        {
            if (rigidBody.velocity.x > 0.01f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (rigidBody.velocity.x < -0.01f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    public bool HeadDectObstacle()
    {
        return headDetec.IsTouchingLayers(LayerMask.GetMask("Ground")) || headDetec.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform")) || headDetec.IsTouchingLayers(LayerMask.GetMask("DestroyableLayer"));
    }

    public bool KneeDectObstacle()
    {
        return kneeDetec.IsTouchingLayers(LayerMask.GetMask("Ground")) || kneeDetec.IsTouchingLayers(LayerMask.GetMask("DestroyableLayer"));
    }

    public void SetHitColliderActive()
    {
        hitCollider.enabled = true;
    }
    public void SetHitColliderDeactive()
    {
        hitCollider.enabled = false;
    }





}
