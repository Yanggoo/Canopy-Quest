using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public int damage;
    public float destroyDistance;

    private new Rigidbody2D rigidbody2D;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = transform.right * speed;
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
        var enemy = collision.gameObject.transform.parent;
        if (enemy&& enemy.CompareTag("Enemy") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            enemy.GetComponent<EnemyNew>().TakeDamage(damage);
        }
    }
}
