using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : Singleton<InventoryManager>
{
    public Inventory Inventory { get; private set; } = new Inventory();
    private InventoryUIManager UI;
    private const string saveKey = "PlayerInventory";

    private void OnEnable()
    {
        EventManager.AddListener<OnCropHarvest>(OnCropHarvestCallBack);
        EventManager.AddListener<OnCropSelling>(OnCropSellingCallBack);

    }

    private void OnDisable()
    {
        EventManager.RemoveListener<OnCropHarvest>(OnCropHarvestCallBack);
        EventManager.RemoveListener<OnCropSelling>(OnCropSellingCallBack);

    }

    void Start()
    {
        UI = GetComponent<InventoryUIManager>();
        LoadInventory(); 
    }

    public void LoadInventory()
    {
        Inventory = ES3.Load<Inventory>(saveKey, new Inventory());
        UI.CreatCropUIContainers(Inventory.items);
    }

    public void SaveInventory()
    {
        ES3.Save<Inventory>(saveKey, Inventory);
    }

    private void OnCropHarvestCallBack(OnCropHarvest e)
    {
        Inventory.UpdateInventory(e.cropData);
        SaveInventory();
    }

    private void OnCropSellingCallBack(OnCropSelling e)
    {
        Inventory.SellingItem();
        SaveInventory();
    }

}
