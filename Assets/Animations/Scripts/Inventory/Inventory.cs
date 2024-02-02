using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public GameObject[] slots;
    //public GameObject[] backPack;
    bool isInstantiated;
    public ItemList itemList;
    public Dictionary<string, int> inventoryItems = new Dictionary<string, int>();

    TextMeshProUGUI amountText;
    void Start()
    {
        if (itemList != null)
        {
            DataToInventory();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckSlotsAvailableity(GameObject itemToAdd,string itemName,int itemAmount)
    {
        isInstantiated = false;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount>0){
                slots[i].GetComponent<Slots>().isUsed=true;
            }

            else if(!isInstantiated && !slots[i].GetComponent<Slots>().isUsed)
            {
               
                if (!inventoryItems.ContainsKey(itemName))
                {
                   GameObject item= Instantiate(itemToAdd, slots[i].transform.position, Quaternion.identity);
                    item.transform.SetParent(slots[i].transform,false);
                    item.transform.localPosition = new Vector3(0, 0, 0);
                    item.name = item.name.Replace("(Clone)","");
                    isInstantiated = true;
                    inventoryItems.Add(itemName,itemAmount);
                    amountText = slots[i].GetComponentInChildren<TextMeshProUGUI>();
                    amountText.text = itemAmount.ToString();
                    break;
                }
                else
                {
                    for (int j = 0; j < slots.Length; j++)
                    {
                        if (slots[j].transform.GetChild(0).gameObject.name == itemName)
                        {
                            inventoryItems[itemName] += itemAmount;
                            amountText = slots[j].GetComponentInChildren<TextMeshProUGUI>();
                            amountText.text = inventoryItems[itemName].ToString();
                            break;
                        }
                    }break;
                }

            }
        }
    }
    

    public void useInventoryItems(string itemName)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].GetComponent<Slots>().isUsed)
            {
                continue;
            }

            if (slots[i].transform.GetChild(0).gameObject.name == itemName)
            {
                inventoryItems[itemName]--;
                amountText = slots[i].GetComponentInChildren<TextMeshProUGUI>();
                amountText.text = inventoryItems[itemName].ToString();

                if (inventoryItems[itemName] <= 0)
                {
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    slots[i].GetComponent<Slots>().isUsed = false;
                    inventoryItems.Remove(itemName);
                    ReorganizedInv();
                }
                break;
            }
        }
    }

    public void ReorganizedInv()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].GetComponent<Slots>().isUsed)
            {
                for (int j = i+1; j < slots.Length; j++)
                {
                    if (slots[j].GetComponent<Slots>().isUsed)
                    {
                        Transform itemMove = slots[j].transform.GetChild(0).transform;
                        itemMove.transform.SetParent(slots[i].transform, false);
                        itemMove.transform.localPosition = new Vector3(0, 0, 0);
                        slots[i].GetComponent<Slots>().isUsed = true;
                        slots[j].GetComponent<Slots>().isUsed = false;
                        break;
                    }
                }
            }
        }
    }
    public void DataToInventory()
    {
        for (int i = 0; i < GameData.instance.saveData.addID.Count; i++) 
        {
            for (int j = 0; j < itemList.items.Count; j++)
            {
                if (itemList.items[j].ID == GameData.instance.saveData.addID[i])
                {
                    CheckSlotsAvailableity(itemList.items[j].gameObject, GameData.instance.saveData.inventoryItemsName[i], GameData.instance.saveData.inventoryItemsAmount[i]);


                }
            }
        }
    }
    public void InventoryToData()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetComponent<Slots>().isUsed)
            {
                if (!GameData.instance.saveData.addID.Contains(slots[i].GetComponentInChildren<ItemUse>().ID))
                {
                    GameData.instance.saveData.addID.Add(slots[i].GetComponentInChildren<ItemUse>().ID);
                    GameData.instance.saveData.inventoryItemsName.Add(slots[i].GetComponentInChildren<ItemUse>().name);
                    GameData.instance.saveData.inventoryItemsAmount.Add(inventoryItems[slots[i].GetComponentInChildren<ItemUse>().name]);

                }
            }
        }
    }
}
