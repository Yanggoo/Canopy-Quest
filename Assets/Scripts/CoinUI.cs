using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public int startCoinQuantity;
    public Text coinQauntity;
    public static int currentCoinQuantity;
    // Start is called before the first frame update
    void Start()
    {
        currentCoinQuantity = startCoinQuantity;
    }

    // Update is called once per frame
    void Update()
    {
        coinQauntity.text = currentCoinQuantity.ToString();
    }
}
