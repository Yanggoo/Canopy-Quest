using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionRange;
    public float hitBoxTime;
    public Vector2 startSpeed;
    private new Rigidbody2D rigidbody2D;
    private Animator animator;
    public float delayTime;
    public float destroyBombTime;

    // Start is called before the first frame update
    void Start()

    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rigidbody2D.velocity = transform.right * startSpeed.x + transform.up * startSpeed.y;
        Invoke("Explode", delayTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        animator.SetTrigger("Explode");
        Invoke("DestroyThisBomb", destroyBombTime);
        Invoke("GenerateExplosionRange", hitBoxTime);
    }

    void GenerateExplosionRange()
    {
        Instantiate(explosionRange, transform.position, Quaternion.identity);
    }

    void DestroyThisBomb()
    {
        Destroy(gameObject);
    }
}
