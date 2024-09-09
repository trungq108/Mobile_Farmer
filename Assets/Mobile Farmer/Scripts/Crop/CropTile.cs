using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTile : MonoBehaviour
{
    [SerializeField] private GameObject corn;
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
        crop = Instantiate(data.cropPrefab, this.transform);
        crop.transform.localPosition = Vector3.up / 2;
        crop.transform.localRotation = Quaternion.identity;
    }

    public void Water()
    {
        state = State.Watered;
        tileMesh.material.color = Color.white * 0.3f;
        crop.Grown();
    }

    public bool IsEmpty() => state == State.Empty;
    public bool IsSown() => state == State.Sown;
    public bool IsWatered() => state == State.Watered;
}
