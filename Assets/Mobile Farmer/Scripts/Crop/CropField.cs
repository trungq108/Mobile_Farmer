using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropField : MonoBehaviour
{
    [SerializeField] private Transform tileParent;
    [SerializeField] private CropDataSO cropData;
    private List<CropTile> cropTiles = new List<CropTile>();
    private State state;

    private void Awake()
    {
        foreach(Transform t in tileParent)
        {
            cropTiles.Add(t.GetComponent<CropTile>());
        }
        state = State.Empty;
    }

    public void SowCropTiles(Vector3[] collisionPositions)
    {
        for(int i = 0; i < collisionPositions.Length; i++)
        {
            CropTile cropTile = FindNearestSowTile(collisionPositions[i]);

            if(cropTile != null) cropTile.Sow(cropData.DataBase[0]);        // Sow Tile
            else                                                            // Field is Full, Trigger Event to Player to stop seeding
            {
                state = State.Sown;

                FieldSown e = new FieldSown();
                e.cropField = this;
                EventManager.TriggerEvent(e);
            }
        }
    }

    private CropTile FindNearestSowTile(Vector3 collisionPosition)
    {
        float minDistance = 1000f;
        int cropTileIndex = -1;

        for(int i = 0;i < cropTiles.Count;i++)
        {
            if (!cropTiles[i].IsEmpty()) continue;

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

    public void WaterCropTiles(Vector3[] collisionPositions)
    {
        for (int i = 0; i < collisionPositions.Length; i++)
        {
            CropTile cropTile = FindNearestWaterTile(collisionPositions[i]);

            if (cropTile != null) cropTile.Water();                         // Water Tile
            else                                                            // Field is Watered, Trigger Event to Player to stop watering
            {
                state = State.Watered;

                FieldWatered e = new FieldWatered();
                e.cropField = this;
                EventManager.TriggerEvent(e);
            }
        }
    }

    private CropTile FindNearestWaterTile(Vector3 collisionPosition)
    {
        float minDistance = 1000f;
        int cropTileIndex = -1;

        for (int i = 0; i < cropTiles.Count; i++)
        {
            if (!cropTiles[i].IsSown()) continue;

            float distance = Vector3.Distance(collisionPosition, cropTiles[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                cropTileIndex = i;
            }
        }
        if (cropTileIndex == -1) return null;
        else return cropTiles[cropTileIndex];
    }

    public bool IsEmpty() => state == State.Empty;
    public bool IsSown() => state == State.Sown;
    public bool IsWatered() => state == State.Watered;
}
