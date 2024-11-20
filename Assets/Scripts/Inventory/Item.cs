using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    public string itemName;
    [SerializeField] private int itemQuantity;
    [SerializeField] private Sprite itemSprite;

    private InventoryManager inventoryManager;

    private PlayerInventory playerInventory;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInventory = collision.gameObject.GetComponent<PlayerInventory>();
            playerInventory.AddItem(this);
            inventoryManager.AddItem(itemName, itemQuantity, itemSprite);

            gameObject.SetActive(false);


        }
    }
}
