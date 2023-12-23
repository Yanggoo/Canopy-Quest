using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : MonoBehaviour
{

    public float speed;
    public int damage;
    public float rotateSpeed;
    public float tuning;

    private new Rigidbody2D rigidbody2D;
    private Transform playerTransform;
    private Transform sickleTransform;
    private Vector2 startSpeed;
    private bool isReturn;
    public float selfDestroyTime;

    private CameraShake camerashake;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = transform.right * speed;
        startSpeed = rigidbody2D.velocity;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sickleTransform = GetComponent<Transform>();
        camerashake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
        Invoke("DestroySelf", selfDestroyTime);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed);
        Vector2 direction = new Vector2(playerTransform.position.x - transform.position.x, playerTransform.position.y - transform.position.y).normalized;
        if (Vector2.Dot(rigidbody2D.velocity, direction) > 0.0f|| Vector2.Dot(rigidbody2D.velocity, startSpeed) < 0.0f)
        {
           rigidbody2D.velocity = (rigidbody2D.velocity.magnitude + startSpeed.magnitude * Time.deltaTime) * direction;
        }
        else
        {
            rigidbody2D.velocity = rigidbody2D.velocity - startSpeed * Time.deltaTime;
        }
        if (Mathf.Abs(transform.position.x - playerTransform.position.x) < 0.5f)
        {
            Destroy(gameObject);
        }
        
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject.transform.parent;
        if (enemy && enemy.CompareTag("Enemy") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            enemy.GetComponent<EnemyNew>().TakeDamage(damage);
        }
    }
}
