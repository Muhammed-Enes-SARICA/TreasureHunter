using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public GameObject[] itemInStore;
    public GameObject shopPanel;

    public bool sellItem;
    Inventory inventory;
    
    
    void Start()
    {
        inventory = GetComponent<Inventory>();
        setUpShop();

    }

    
    void Update()
    {
        
    }

    public void setUpShop()
    {
        for (int i = 0; i <itemInStore.Length; i++)
        {
           GameObject itemToSell= Instantiate(itemInStore[i],inventory.slots[i].transform.position,Quaternion.identity);
            itemToSell.transform.SetParent(inventory.slots[i].transform, false);
            itemToSell.transform.localPosition = new Vector3(0, 0, 0);
            itemToSell.name = itemToSell.name.Replace("(Clone)", "");
        }
    }

    public void IsSellingItems()
    {
        sellItem = !sellItem;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            shopPanel.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            shopPanel.SetActive(false);
        }
    }
}
