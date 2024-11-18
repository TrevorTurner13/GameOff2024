using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryItemUI itemPrefab;
    [SerializeField] private RectTransform controlPanel;

    List<InventoryItemUI> listOfItemsUI = new List<InventoryItemUI>();

    // Start is called before the first frame update
    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            InventoryItemUI itemUI = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            itemUI.transform.SetParent(controlPanel);
            listOfItemsUI.Add(itemUI);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
