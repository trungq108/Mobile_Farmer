using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] private PlayerAnimation anim;
    [SerializeField] private PlayerTool tool;
    [SerializeField] private ParticleSystem seedParticle;
    [SerializeField] private ParticleSystem waterParticle;

    private CropField currentCropField;

    private void OnEnable()
    {
        EventManager.AddListener<SowSeed>(SowSeedCallBack);
        EventManager.AddListener<WaterSeed>(WaterSeedCallBack);
        EventManager.AddListener<FieldSown>(FieldSownCallBack);
        EventManager.AddListener<FieldWatered>(FieldWateredCallBack);

        EventManager.AddListener<ChangeTool>(ChangeToolCallBack);

    }

    private void OnDisable()
    {
        EventManager.RemoveListener<SowSeed>(SowSeedCallBack);
        EventManager.RemoveListener<WaterSeed>(WaterSeedCallBack);
        EventManager.RemoveListener<FieldSown>(FieldSownCallBack);
        EventManager.RemoveListener<FieldWatered>(FieldWateredCallBack);
        EventManager.RemoveListener<ChangeTool>(ChangeToolCallBack);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            currentCropField = other.GetComponent<CropField>();
            if (currentCropField.IsEmpty() && tool.CurrentTool == Tool.Sow)
                Sowing();
            if (currentCropField.IsSown() && tool.CurrentTool == Tool.Water)
                Watering();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            StopSowing();
            StopWatering();
            currentCropField = null;
        }
    }

    public void Sowing() => anim.PlaySowAnim();
    public void StopSowing() => anim.StopSowAnim();
    public void ThrowSeed() => seedParticle.Play();

    public void Watering() => anim.PlayWaterAnim();
    public void StopWatering()
    {
        anim.StopWaterAnim();
        waterParticle.Stop();
    } 
    public void ThrowWater() => waterParticle.Play();

    public void SowSeedCallBack(SowSeed e)
    {
        Vector3[] collisionPositions = e.collisionPositions;
        if (currentCropField != null)
        {
            currentCropField.SowCropTiles(collisionPositions);
        }
    }

    public void WaterSeedCallBack(WaterSeed e)
    {
        Vector3[] collisionPositions = e.collisionPositions;
        if (currentCropField != null)
        {
            currentCropField.WaterCropTiles(collisionPositions);
        }
    }

    private void FieldSownCallBack(FieldSown e)
    {
        if (e.cropField == currentCropField)
            StopSowing();
    }

    private void FieldWateredCallBack(FieldWatered e)
    {
        if (e.cropField == currentCropField)
            StopWatering();
    }

    private void ChangeToolCallBack(ChangeTool e)
    {
        if (currentCropField == null) return;

        Tool toolChange = e.toolChange;
        switch (toolChange)
        {
            case Tool.Empty:
                StopSowing();
                StopWatering();
                break;

            case Tool.Sow:
                StopWatering();
                if (currentCropField.IsEmpty()) Sowing();
                break;

            case Tool.Water:
                StopSowing();
                if(currentCropField.IsSown()) Watering();
                break;

            case Tool.Harvest:
                StopSowing();
                StopWatering();
                break;
        }

    }
}
