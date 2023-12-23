using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTriggerCheck:MonoBehaviour
{
    [SerializeField]
    private EnemyNew owner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            owner.isWithChaseRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            owner.isWithChaseRange = false;
        }
    }
}
