using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitCollider : MonoBehaviour
{
    private int hitDamage;
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        hitDamage = transform.parent.GetComponent<EnemyNew>().damage;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(hitDamage);
            }
        }
    }
}
