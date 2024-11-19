using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item item = new Item("Item Name", 1);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Inventory.instance.AddItem(item);
            Destroy(gameObject);
        }
    }
}
