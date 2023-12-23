using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionRange : MonoBehaviour
{
    public int damage;
    public float destroyTime;
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject.transform.parent;
        if (enemy && enemy.CompareTag("Enemy") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            enemy.GetComponent<EnemyNew>().TakeDamage(damage);
        }
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
            }

        }
    }
}
