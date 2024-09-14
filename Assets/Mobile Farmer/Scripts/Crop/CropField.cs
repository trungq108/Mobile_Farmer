using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using Random = UnityEngine.Random;

public class CropField : MonoBehaviour
{
    [SerializeField] private Transform tileParent;
    private CropDataSO cropData;
    private List<CropTile> cropTiles = new List<CropTile>();
    private State state;
    private int sownTileIndex = 0;
    private int wateredTileIndex = 0;
    private int harvestedTileIndex = 0;

    private void Awake()
    {
        foreach(Transform t in tileParent)
        {
            cropTiles.Add(t.GetComponent<CropTile>());
        }
        cropData = GameAsset.Instance.CropDataSO;
        state = State.Empty;
    }

    public void SowCropTiles(Vector3[] collisionPositions)
    {
        for(int i = 0; i < collisionPositions.Length; i++)
        {
            CropTile cropTile = FindNearestTile(collisionPositions[i]);

            if (cropTile == null) continue;
            if (!cropTile.IsEmpty()) continue;
            cropTile.Sow(cropData.DataBase[1]);
            sownTileIndex++;
        }

        if (sownTileIndex == cropTiles.Count)
        {
            state = State.Sown;
            FieldSown e = new FieldSown();
            e.cropField = this;
            EventManager.TriggerEvent(e);
        }
    }

    public void WaterCropTiles(Vector3[] collisionPositions)
    {
        for (int i = 0; i < collisionPositions.Length; i++)
        {
            CropTile cropTile = FindNearestTile(collisionPositions[i]);

            if (cropTile == null) continue;
            if (!cropTile.IsSown()) continue;
            cropTile.Water();                         
            wateredTileIndex++;
        }

        if (wateredTileIndex == cropTiles.Count)
        {
                state = State.Watered;
                FieldWatered e = new FieldWatered();
                e.cropField = this;
                EventManager.TriggerEvent(e);
        }
    }

    private CropTile FindNearestTile(Vector3 collisionPosition)
    {
        float minDistance = 1000f;
        int cropTileIndex = -1;
        for(int i = 0;i < cropTiles.Count;i++)
        {
            float distance = Vector3.Distance(collisionPosition, cropTiles[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                cropTileIndex = i;
            }
        }
        if(cropTileIndex == -1) return null;
        else return cropTiles[cropTileIndex];
    }

    public void HarvestingField(Vector3 harvestDamagePoint, float harvestRange)
    {
        for (int i = 0; i < cropTiles.Count; i++)
        {
            CropTile cropTile = cropTiles[i];
            if (cropTile == null) continue;
            if (!cropTile.IsWatered()) continue;

            float distance = Vector3.Distance(cropTile.transform.position, harvestDamagePoint);
            if (distance <= harvestRange)
            {
                HarvestingTile(cropTile);
                harvestedTileIndex++;
            }
        }

        if(harvestedTileIndex == cropTiles.Count)
        {
            ResetCropField();
        }
    }

    public void HarvestingTile(CropTile tile)
    {
        tile.Harvest();
    }

    private void ResetCropField()
    {
        state = State.Empty;
        sownTileIndex = 0;
        wateredTileIndex = 0;
        harvestedTileIndex = 0;

        FieldHarvested e = new FieldHarvested();
        e.cropField = this;
        EventManager.TriggerEvent(e);
    }


    public bool IsEmpty() => state == State.Empty;
    public bool IsSown() => state == State.Sown;
    public bool IsWatered() => state == State.Watered;

    // Debug 
    [Button]
    private void SowCropField()
    {
        for(int i = 0; i < cropTiles.Count; i++)
        {
            cropTiles[i].Sow(cropData.DataBase[Random.Range(0, cropData.DataBase.Length)]);
            sownTileIndex++;
        }
        state = State.Sown;
    }

    [Button]
    private void WaterCropField()
    {
        for (int i = 0; i < cropTiles.Count; i++)
        {
            cropTiles[i].Water();
            wateredTileIndex++;
        }
        state = State.Watered;
    }
}
