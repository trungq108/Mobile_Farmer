using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAsset : Singleton<GameAsset>
{
    [field: SerializeField] public CropDataSO CropDataSO { get; private set; }

    public Sprite GetCropSprite(CropType cropType)
    {
        Sprite sprite = null;
        for(int i = 0; i < CropDataSO.DataBase.Length; i++)
        {
            if (CropDataSO.DataBase[i].cropType == cropType)
            {
                sprite = CropDataSO.DataBase[i].cropIcon;
            }
        }
        return sprite;
    }

    public int GetCropPrice(CropType cropType)
    {
        int price = 0;
        for (int i = 0; i < CropDataSO.DataBase.Length; i++)
        {
            if (CropDataSO.DataBase[i].cropType == cropType)
            {
                price = CropDataSO.DataBase[i].cropPrice;
            }
        }
        return price;
    }
}
