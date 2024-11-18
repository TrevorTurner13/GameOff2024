using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] InventoryUI inventoryUI;

    public int inventorySize = 4;

    private void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
    }
}
