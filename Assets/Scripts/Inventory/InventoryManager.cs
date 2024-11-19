using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemUISlot[] itemUISlot;
    // Start is called before the first frame update
    void Start()
    {
        
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
}
