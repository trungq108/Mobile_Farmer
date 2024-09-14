using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public CropType cropType;
    public int amount;

    public Item (CropType cropType ,int amount)
    {
        this.cropType = cropType;
        this.amount = amount;
    }
}

[System.Serializable]
public class Inventory
{
    public List<Item> items = new List<Item>();

    public void TryAddItem(CropData cropData)
    {
        bool itemFound = false;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].cropType == cropData.cropType)
            {
                items[i].amount++;
                InventoryUIManager.Instance.UpdateUIContainers(items[i]);
                itemFound = true;
                break; 
            }
        }
        if (!itemFound)
        {
            items.Add(new Item(cropData.cropType, 1));
        }

        Debug.Log("Item Count = " + items.Count);
        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log(items[i] + " " + items[i].cropType.ToString() + " " + items[i].amount);
        }
    }
}
