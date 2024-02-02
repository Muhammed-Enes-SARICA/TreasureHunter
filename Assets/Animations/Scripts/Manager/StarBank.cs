using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarBank : MonoBehaviour
{
    public int BankStar;
    public Text starBankText;

    public static StarBank instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        BankStar = PlayerPrefs.GetInt("StarAmount", 0);
        starBankText.text = "X " + BankStar.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        starBankText.text = "X " + BankStar.ToString();
    }
    
    public void Collect(int starCollected) {
        BankStar += starCollected;
        starBankText.text = "X " + BankStar.ToString();

        //DataManager.instance.CurrentStars(BankStar);
        //BankStar = PlayerPrefs.GetInt("StarAmount");
    }
}
