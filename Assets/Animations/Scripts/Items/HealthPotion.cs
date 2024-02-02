using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public float healtToGive;

    GameManagerTwo gameManager;
    Inventory inventory;
    public GameObject itemToAdd;
    public int itemAmount;

    void Start()
    {
        gameManager = GameManagerTwo.instance;
        inventory = gameManager.GetComponent<Inventory>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            inventory.CheckSlotsAvailableity(itemToAdd,itemToAdd.name,itemAmount);
            //collision.GetComponent<PlayerHealth>().currentHealt += healtToGive;
            Destroy(gameObject);


         }

    }
}
