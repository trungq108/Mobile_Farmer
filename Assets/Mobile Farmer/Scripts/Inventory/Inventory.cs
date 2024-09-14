using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

[System.Serializable]
public class Item
{
    public CropType cropType;
    public int amount;

    public Item (CropType cropType, int amount)
    {
        this.cropType = cropType;
        this.amount = amount;
    }
}

[System.Serializable]
public class Inventory
{
    public List<Item> items = new List<Item>();

    public void TryAddItem(CropType cropType)
    {
        if(items.Count == 0 )
            items.Add(new Item(cropType, 0));

        bool isNewItem = false;
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].cropType == cropType)
            {
                items[i].amount++;
                isNewItem = false;
                break;                
            }
            else
            {
                isNewItem = true;
                continue;
            }
        }
        if(isNewItem == true)
        {
            Item newItem = new Item(cropType, 1);
            items.Add(newItem);
        }

        Debug.Log("Item Count = " + items.Count);   
        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log(items[i] + " " + items[i].cropType.ToString() + " " + items[i].amount);            
        }

    }
}
