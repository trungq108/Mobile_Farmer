using DG.Tweening;
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTile : MonoBehaviour
{
    [SerializeField] private MeshRenderer tileMesh;
    private State state;
    private Crop crop;

    private void Awake()
    {
        state = State.Empty;
    }

    public void Sow(CropData data)
    {
        state = State.Sown;
        crop = LeanPool.Spawn(data.cropPrefab, transform.position, Quaternion.identity, this.transform);
        crop.OnInit(data);
    }

    public void Water()
    {
        state = State.Watered;
        tileMesh.material.DOColor(Color.white * 0.3f, 0.5f);
        crop.Grown();
    }

    public void Harvest()
    {
        state = State.Empty;
        tileMesh.material.DOColor(Color.white, 0.5f);
        crop.HarvestSequence();
        //GameObject newCropHarvest = PoolManager.Instance.SpawnCropHavest(transform.position + Vector3.up, Quaternion.Euler(-90f, 0f, 0f));
        //PoolManager.Instance.DespawnCropHavest(newCropHarvest,2f);
    }

    public bool IsEmpty() => state == State.Empty;
    public bool IsSown() => state == State.Sown;
    public bool IsWatered() => state == State.Watered;
}
