using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : Singleton<InventoryUIManager>
{
    [SerializeField] private Transform containerParent;
    [SerializeField] private CropUIContainer containerPrefab;
    private List<Item> items = new List<Item>();
    private List<CropUIContainer> containers = new List<CropUIContainer>();

    [Button]
    public void CreatCropUIContainers(List<Item> items)
    {
        this.items = items;
        for (int i = 0; i < items.Count; i++)
        {
            CreatContainer(items[i]);
        }
    }

    public void CreatContainer(Item item)
    {
        CropUIContainer container = Instantiate(containerPrefab, containerParent);
        container.Configue(item);
        containers.Add(container);
    }

    public void UpdateUIContainers(Item item)
    {
        for (int i = 0;i < containers.Count;i++)
        {
            if(containers[i].Item.cropType == item.cropType)
            {
                containers[i].Configue(item);
            }
        }
    }
}
