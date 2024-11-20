using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public List<Item> items;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Item item)
    {
        items.Add(item);

    }

    public void RemoveItem(string itemName)
    {

        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].itemName == itemName)
            {
                items.Remove(items[i]);

                break;
            }
        }
    }

    public bool CheckForKeycard()
    {
        bool hasKeycard = false;

        foreach (Item item in items)
        {
            if (item.isKeycard)
            {
                hasKeycard = true;
                break;
            }
        }

        return hasKeycard;
    }

}
