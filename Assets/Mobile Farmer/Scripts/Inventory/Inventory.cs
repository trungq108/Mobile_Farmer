using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public CropType cropType;
    public Sprite cropIcon;
    public int amount;

    public Item (CropType cropType,Sprite cropIcon ,int amount)
    {
        this.cropType = cropType;
        this.cropIcon = cropIcon;
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
                // Increment the item amount if it exists
                items[i].amount++;
                itemFound = true;
                break; // No need to continue loop if item is found
            }
        }

        // If item wasn't found, add it as a new item
        if (!itemFound)
        {
            items.Add(new Item(cropData.cropType, cropData.cropIcon, 1));
        }

        Debug.Log("Item Count = " + items.Count);

        // Logging items
        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log(items[i] + " " + items[i].cropType.ToString() + " " + items[i].amount);
        }
    }
}
