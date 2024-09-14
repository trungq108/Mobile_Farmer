using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private ParticleSystem cropParticle;
    private CropData cropData;

    public void OnInit(CropData cropData)
    {
        this.cropData = cropData;
        transform.localPosition = Vector3.up / 2;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 1f);
    }

    public void Grown()
    {
        this.transform.DOScale(Vector3.one * 3, 1f);
    }

    public void HarvestSequence()
    {
        cropParticle.Play();
        this.transform.DOScale(Vector3.zero, 1f)
            .OnComplete(() => Harvested());
    }

    private void Harvested()
    {
        OnCropHarvest e = new OnCropHarvest();
        e.cropData = this.cropData;
        EventManager.TriggerEvent(e);
        LeanPool.Despawn(this);
    }
}
