using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinItem : MonoBehaviour
{
    private bool isPlayerInTrashBin;

    private PlayerInputAction controls;

    private void Awake()
    {
        controls = new PlayerInputAction();
        controls.GamePlay.Interact.started += ctx =>
        {
            if (isPlayerInTrashBin)
            {
                if (CoinUI.currentCoinQuantity > 0)
                {
                    TrashBinCoin.coinCurrent++;
                    CoinUI.currentCoinQuantity--;
                    SoundManager.PlayThrowCoinClip();
                }
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
        //if (isPlayerInTrashBin&&Input.GetKeyDown(KeyCode.Y))
        //{
        //    if (CoinUI.currentCoinQuantity > 0)
        //    {
        //        TrashBinCoin.coinCurrent++;
        //        CoinUI.currentCoinQuantity--;
        //        SoundManager.PlayThrowCoinClip();
        //    }
        //}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInTrashBin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInTrashBin = false;
        }
    }
}
