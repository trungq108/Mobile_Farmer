using Sirenix.OdinInspector;
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
    public CropType cropType;
    public GameObject cropPrefab;
    public Sprite cropIcon;
    public int cropPrice;
}
