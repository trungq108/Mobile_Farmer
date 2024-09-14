using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class PoolManager : Singleton<PoolManager>
{
    [Header("Crop Harvest Pool Element")]
    [SerializeField] private GameObject cropParticlePrefab;


    public Crop SpawnCrop(Crop crop, Vector3 position, Quaternion quaternion, Transform parent)
    {
        return LeanPool.Spawn(crop, position, quaternion, parent);
    }

    public void DespawnCrop(Crop crop,float timeDelay)
    {
        LeanPool.Despawn(crop, timeDelay);
    }

    //public GameObject SpawnCropHavest(Vector3 position, Quaternion quaternion)
    //{
    //    return LeanPool.Spawn(cropParticlePrefab, position, quaternion);
    //}

    //public void DespawnCropHavest(GameObject despawnObject, float timeDelay)
    //{
    //    LeanPool.Despawn(despawnObject, timeDelay);
    //}
}
