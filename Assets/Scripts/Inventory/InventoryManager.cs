using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public ItemUISlot[] itemUISlot;

    //public Item item;

    private void Awake()
    {
       if (instance != null && instance != this)
       {
            Destroy(this);
       } 
       else
        {
            instance = this;    
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(string itemName, int itemQuantity, Sprite itemSprite)
    {
        Debug.Log("itemName = " + itemName + "quantity = " + itemQuantity + "itemSprite = " + itemSprite);
        for (int i = 0; i < itemUISlot.Length; i++)
        {
            if (itemUISlot[i].isFull == false)
            {
 

                itemUISlot[i].AddItem(itemName, itemQuantity, itemSprite);
                return;


            }
        }
    }

    public void RemoveItem(string itemName)
    {
        for (int i = itemUISlot.Length -1; i >= 0; i--)
        {
            if (itemUISlot[i].itemName == itemName)
            {
                itemUISlot[i].RemoveItem();
                break;
            }
        }
    }
}
