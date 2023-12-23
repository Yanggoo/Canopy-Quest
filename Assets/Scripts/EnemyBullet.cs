using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float speed;
    public int damage;
    public float destroyDistance;

    private new Rigidbody2D rigidbody2D;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (transform.position - startPosition).magnitude;
        if (distance > destroyDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& collision.GetType().ToString()== "UnityEngine.CapsuleCollider2D")
        {
            collision.GetComponent<PlayerHealth>().DamagePlayer(damage);
            Destroy(gameObject);
        }
        else if(!collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
