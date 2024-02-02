using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBank : MonoBehaviour
{
    public int bank;
    public Text bankText;
    public static CoinBank instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    void Start()
    {
        bank = PlayerPrefs.GetInt("CoinAmount", 0);
        bankText.text = "X " + bank.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

public void Money(int coinCollected)
    {
        bank += coinCollected;
        bankText.text = "X " + bank.ToString();

        DataManager.instance.CurrentCoins(bank);
        bank = PlayerPrefs.GetInt("CoinAmount");
    }

}

