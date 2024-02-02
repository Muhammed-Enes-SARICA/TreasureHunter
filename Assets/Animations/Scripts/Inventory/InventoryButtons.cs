using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtons : MonoBehaviour
{
    GameManagerTwo gameManager;
    Inventory inventory;

    void Start()
    {
        gameManager = GameManagerTwo.instance;
        inventory = gameManager.GetComponent<Inventory>();
    }

   public void UseItem()
    {
        inventory.useInventoryItems(gameObject.name);
    }
}
