using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlatform : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxcollider2D;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxcollider2D = GetComponent<BoxCollider2D>();
    }

    public void DestorySelf()
    {
        Destroy(gameObject);
    }
    public void DisableCollider()
    {
        boxcollider2D.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")&& collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            animator.SetTrigger("Collapse");
        }
    }
}
