using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public Inventory inventory = new Inventory();
    private InventoryUIManager UI;
    private string saveKey = "PlayerInventory";

    private void OnEnable()
    {
        EventManager.AddListener<OnCropHarvest>(OnCropHarvestCallBack);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<OnCropHarvest>(OnCropHarvestCallBack);
    }

    void Start()
    {
        UI = GetComponent<InventoryUIManager>();
        LoadInventory(); // Load inventory when the game starts
    }

    public void LoadInventory()
    {
        if (ES3.KeyExists(saveKey))
        {
            inventory = ES3.Load<Inventory>(saveKey);
            Debug.Log("Inventory loaded.");
            Debug.Log(inventory.items.Count);
        }
        else
        {
            Debug.Log("No save data found, starting with an empty inventory.");
        }
        UI.CreaCropUIContainers();
    }

    public void SaveInventory()
    {
        ES3.Save(saveKey, inventory);
        Debug.Log("Inventory saved.");
    }

    private void OnCropHarvestCallBack(OnCropHarvest e)
    {
        inventory.TryAddItem(e.cropData);
        SaveInventory();
    }

    // DEBUG
    [Button] private void ResetInventory()
    {
        ES3.DeleteFile();
    }
}
