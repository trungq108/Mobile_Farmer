using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAbility : MonoBehaviour
{
    [Header("Crop Ability")]

    [SerializeField] private PlayerAnimation anim;
    [SerializeField] private PlayerTool tool;
    [SerializeField] private ParticleSystem seedParticle;
    [SerializeField] private ParticleSystem waterParticle;
    [SerializeField] private Transform harvestPoint;
    [SerializeField] [Range(0f, 3f)] private float harvestRange;

    private CropField currentCropField;
    private Tree currentTree;

    [Header("Tree Ability")]
    private Vector2 initMousePos;
    private Vector2 currentMousePos;
    private bool isOnTreeMode = false;
    private bool isDragging = false;
    [SerializeField] private float swipeThreshold;


    private void OnEnable()
    {
        EventManager.AddListener<SowSeed>(SowSeedCallBack);
        EventManager.AddListener<WaterSeed>(WaterSeedCallBack);
        EventManager.AddListener<FieldSown>(FieldSownCallBack);
        EventManager.AddListener<FieldWatered>(FieldWateredCallBack);
        EventManager.AddListener<FieldHarvested>(FieldHarvestedCallBack);
        EventManager.AddListener<ChangeTool>(ChangeToolCallBack);
        EventManager.AddListener<EnterTreeMode>(EnterTreeModeCallBack);
        EventManager.AddListener<ExitTreeMode>(ExitTreeModeCallBack);

    }

    private void OnDisable()
    {
        EventManager.RemoveListener<SowSeed>(SowSeedCallBack);
        EventManager.RemoveListener<WaterSeed>(WaterSeedCallBack);
        EventManager.RemoveListener<FieldSown>(FieldSownCallBack);
        EventManager.RemoveListener<FieldWatered>(FieldWateredCallBack);
        EventManager.RemoveListener<FieldHarvested>(FieldHarvestedCallBack);
        EventManager.RemoveListener<ChangeTool>(ChangeToolCallBack);
        EventManager.RemoveListener<EnterTreeMode>(EnterTreeModeCallBack);
        EventManager.RemoveListener<ExitTreeMode>(ExitTreeModeCallBack);

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
            if (currentCropField.IsWatered() && tool.CurrentTool == Tool.Harvest)
                Harvesting();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            StopSowing();
            StopWatering();
            StopHarvesting();
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

    public void Harvesting() => anim.PlayHarvest();
    public void StopHarvesting() => anim.StopPlayHarvest();
    public void ScytheDamage()
    {
        if(currentCropField != null)
        {
            currentCropField.HarvestingField(harvestPoint.position, harvestRange);
        }
    }

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

    private void FieldHarvestedCallBack(FieldHarvested e)
    {
        if (e.cropField == currentCropField)
            StopHarvesting();
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
                StopHarvesting();
                break;

            case Tool.Sow:
                StopWatering();
                StopHarvesting();
                if (currentCropField.IsEmpty()) Sowing();
                break;

            case Tool.Water:
                StopSowing();
                StopHarvesting();
                if(currentCropField.IsSown()) Watering();
                break;

            case Tool.Harvest:
                StopSowing();
                StopWatering();
                if (currentCropField.IsWatered()) Harvesting();
                break;

        }
    }

    private void Update()
    {
        if(isOnTreeMode)
            HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            isDragging = true;
            initMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            OnSwipe();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnRelease();
        }
    }

    private void OnSwipe()
    {
        currentMousePos = Input.mousePosition;
        Vector2 swipeDelta = currentMousePos - initMousePos;
        if (Mathf.Abs(swipeDelta.x) > swipeThreshold)
        {
            anim.PlayShakeTree();
            currentTree.ShakeTree();
            initMousePos = Input.mousePosition;
        }       
    }

    private void OnRelease()
    {
        isDragging = false;
    }

    private void EnterTreeModeCallBack(EnterTreeMode e)
    {
        currentTree = e.tree;
        isOnTreeMode = true;
        GetComponent<PlayerController>().SetTreeModePosition(e.playerShakeTreePos, e.tree.transform.position);
    }

    private void ExitTreeModeCallBack(ExitTreeMode e)
    {
        currentTree = null;
        isOnTreeMode = false;
    }
}
