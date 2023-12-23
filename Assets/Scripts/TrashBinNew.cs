using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinNew : ItemGenerator
{
    private bool isPlayerInTrashBin=false;

    private PlayerInputAction controls;

    private int coinCurrent=0;
    private int coinMax;
    public TrashBinPanelNew panel;

    public void SetValueAndCoinNeeded(int value,int coin)
    {
        coinMax = coin;
        totalValue = value;
    }
    private void Awake()
    {
        controls = new PlayerInputAction();
        controls.GamePlay.Interact.started += ctx =>
        {
            if (isPlayerInTrashBin)
            {
                if (CoinUI.currentCoinQuantity > 0 && coinCurrent != coinMax)
                {
                    CoinUI.currentCoinQuantity--;
                    coinCurrent++;
                    panel.UpdatePanelInfo((float)coinCurrent / coinMax, coinCurrent + "/" + coinMax);
                    SoundManager.PlayThrowCoinClip();
                    if (coinCurrent == coinMax)
                    {
                        GenerateItems(true);
                    }
                }
            }
        };
    }

    public void Initialize()
    {
        panel.UpdatePanelInfo((float)coinCurrent / coinMax, coinCurrent + "/" + coinMax);
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
