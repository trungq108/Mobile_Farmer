using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTile : MonoBehaviour
{
    [SerializeField] private GameObject corn;
    private State state;

    private void Awake()
    {
        state = State.Empty;
    }

    public void Sow(CropData data)
    {
        state = State.Filled;
        Crop newCrop = Instantiate(data.cropPrefab);
        newCrop.transform.position = transform.position + Vector3.up / 2;
    }

    public bool IsEmpty() => state == State.Empty;
}
