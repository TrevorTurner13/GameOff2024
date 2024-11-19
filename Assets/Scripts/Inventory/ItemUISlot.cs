using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUISlot : MonoBehaviour
{
    public string itemName;
    public int itemQuantity;
    public Sprite itemSprite;
    public bool isFull;

    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text quantityText;

    public void AddItem(string itemName, int itemQuantity, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.itemQuantity = itemQuantity;
        this.itemSprite = itemSprite;
        isFull = true;

        quantityText.text = itemQuantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
        itemImage.enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
