using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CropData", menuName = "ScriptableObject / CropData", order = 0)]
public class CropDataSO : ScriptableObject
{
    [TableList]
    public CropData[] DataBase;
}

[System.Serializable]
public struct CropData
{
    public Crop cropPrefab;
    public string cropName;
    public int cropPrice;
    public int cropSellPrice;
}
