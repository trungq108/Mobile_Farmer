using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] private Transform containerParent;
    [SerializeField] private CropUIContainer containerPrefab;

    [Button]
    public void CreaCropUIContainers()
    {
        List<Item> items = InventoryManager.Instance.inventory.items;
        for (int i = 0; i < items.Count; i++)
        {
            CropUIContainer container = Instantiate(containerPrefab, containerParent);
            container.Configue(items[i]);
        }
    }

    public void UpdateUIContainers(CropData cropData)
    {
        

    }
}
