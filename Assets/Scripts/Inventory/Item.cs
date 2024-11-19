using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private int itemQuantity;
    [SerializeField] private Sprite itemSprite;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inventoryManager.AddItem(itemName, itemQuantity, itemSprite);
            Destroy(gameObject);
        }
    }
}
