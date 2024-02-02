using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Coins : MonoBehaviour
{
    public int coinAmount;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CoinBank.instance.Money(coinAmount);
            Destroy(gameObject);
        }
    }
}
