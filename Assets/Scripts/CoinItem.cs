using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D triggerbox;
    [SerializeField]
    private float enableTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        triggerbox.enabled = false;
        Invoke("EnableCoin", enableTime);
    }
    void EnableCoin()
    {
        triggerbox.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")&&collision.GetType().ToString()== "UnityEngine.CapsuleCollider2D")
        {
            CoinUI.currentCoinQuantity += 1;
            SoundManager.PlayPickCoinClip();
            Destroy(gameObject);
        }
    }
}
