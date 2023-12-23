using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    private bool canOpen;
    private bool isOpened;
    private Animator animator;

    public GameObject coin;
    public float delayTime;

    private PlayerInputAction controls;

    private void Awake()
    {
        controls = new PlayerInputAction();
        controls.GamePlay.Interact.started += ctx =>
        {
            if (canOpen&&!isOpened)
            {
                isOpened = true;
                animator.SetTrigger("Open");
                Invoke("GetCoin", delayTime);
            }
        };
    }
    private void OnEnable()
    {
        controls.GamePlay.Enable();
    }
    private void OnDisable()
    {
        controls.GamePlay.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (canOpen && Input.GetKeyDown(KeyCode.I))
        //{
        //    isOpened = true;
        //    animator.SetTrigger("Open");
        //    Invoke("GetCoin", delayTime);
        //}
        
    }

    void GetCoin()
    {
        Instantiate(coin, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&&collision.GetType().ToString()== "UnityEngine.CapsuleCollider2D")
        {
            canOpen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = false;
        }
    }
}
