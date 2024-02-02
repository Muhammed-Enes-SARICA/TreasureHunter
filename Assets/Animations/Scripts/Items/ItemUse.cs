using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUse : MonoBehaviour
{
    public int ID;
    public float healtToGive;
    public float manaToGive;
    public float damageToincrease;
    public int coinToGive;

    private void Start()
    {
    }
    public void Use()
    {
        PlayerHealth.instance.currentHealt += healtToGive;


    }
    public void DamageIncrase()
    {

        PlayerController.instance.damage += damageToincrease;


        waitThirtySeconds();


        DecreaseDamage();
    }
    private IEnumerator waitThirtySeconds()
    {
        yield return new WaitForSeconds(30f);


    }
    private void DecreaseDamage()
    {

        PlayerController.instance.damage -= damageToincrease;

    }

    public void  AddCoin(){
        CoinBank.instance.Money(coinToGive);
    }
        
  
}