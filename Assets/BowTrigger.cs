using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowTrigger : MonoBehaviour
{
    private bool isPlayerInSign = false;
    private PlayerInputAction controls;
    private PlayerWeaponController weaponController;

    private void Awake()
    {
        weaponController = GameObject.Find("Player").GetComponent<PlayerWeaponController>();
        controls = new PlayerInputAction();
        controls.GamePlay.Interact.started += ctx => {
            if (isPlayerInSign)
            {
                weaponController.EnableBow();
                Destroy(gameObject.transform.parent.gameObject);
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = false;
        }
    }
}
