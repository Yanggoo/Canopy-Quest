using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    protected bool isPlayerInSign;
    protected PlayerInputAction controls;
    protected void Awake()
    {
        controls = new PlayerInputAction();
        controls.GamePlay.Interact.started += ctx => {
            if (isPlayerInSign)
            {
                //dialogBoxText.text = text;
                //dialogBox.SetActive(true);
                //if (screenChange != null)
                //    screenChange.ChangeImage();
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
