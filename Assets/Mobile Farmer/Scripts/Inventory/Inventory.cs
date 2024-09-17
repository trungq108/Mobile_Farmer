using System;
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

    public void UpdateInventory(CropData cropData)
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
            Item newItem = new Item(cropData.cropType, 1);
            items.Add(newItem);
            InventoryUIManager.Instance.CreatContainer(newItem);
        }
    }

    public void SellingItem()
    {
        int moneyGet = 0;
        for (int i = 0; i < items.Count; i++)
        {
            moneyGet += items[i].amount * GameAsset.Instance.GetCropPrice(items[i].cropType);
            items[i].amount = 0;
            InventoryUIManager.Instance.UpdateUIContainers(items[i]);
        }
        CurrencyManager.Instance.ChangeCurrency(moneyGet);
    }
}
