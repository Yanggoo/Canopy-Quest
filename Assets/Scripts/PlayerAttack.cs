using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float attackLastTime;
    public float attackStartTime;
    private Animator animator;
    private new PolygonCollider2D collider2D;

    private PlayerInputAction controls;

    private void Awake()
    {
        controls = new PlayerInputAction();
        controls.GamePlay.Attack.started += ctx => Attack();
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
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        collider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Attack();
    }

    void Attack()
    {
        //if (Input.GetButtonDown("Attack"))
        {
            animator.SetTrigger("Attack");
            StartCoroutine("StartAttack");
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(attackStartTime);
        collider2D.enabled = true;
        StartCoroutine("DisableHitBox");
    }

    IEnumerator DisableHitBox()
    {
        yield return new WaitForSeconds(attackLastTime);
        collider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject.transform.parent;
        if (enemy && enemy.CompareTag("Enemy") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            enemy.GetComponent<EnemyNew>().TakeDamage(damage);
        }

        if (collision.gameObject.CompareTag("YellowStar"))
        {
        
            Destroy(collision.gameObject);
        }
    }
}
