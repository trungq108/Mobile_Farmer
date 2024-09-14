using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public Inventory inventory = new Inventory();
    private InventoryUIManager UI;
    private const string saveKey = "PlayerInventory";

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
        LoadInventory(); 
    }

    public void LoadInventory()
    {
        inventory = ES3.Load<Inventory>(saveKey, new Inventory());
        UI.CreatCropUIContainers(inventory.items);
    }

    public void SaveInventory()
    {
        ES3.Save(saveKey, inventory);
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
