using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSpike : MonoBehaviour
{
    public GameObject hideSpikeBox;
    private Animator animator;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        DisableSpikeHit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            StartCoroutine("SpikeAttack", time);
        }
    }

    IEnumerator SpikeAttack()
    {
        yield return new WaitForSeconds(time);
        animator.SetTrigger("Attack");
    }

    public void EnableSpikeHit()
    {
        hideSpikeBox.SetActive(true);
    }

    public void DisableSpikeHit()
    {
        hideSpikeBox.SetActive(false);
    }
}
