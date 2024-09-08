using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] private PlayerAnimation anim;
    [SerializeField] private ParticleSystem seedParticle;
    [SerializeField] private LayerMask cropLayerMask;

    private CropField currentCropField;

    private void OnEnable()
    {
        EventManager.AddListener<SowSeed>(SowSeedCallBack);
        EventManager.AddListener<FieldFulled>(FieldFulledCallBack);

    }

    private void OnDisable()
    {
        EventManager.RemoveListener<SowSeed>(SowSeedCallBack);
        EventManager.RemoveListener<FieldFulled>(FieldFulledCallBack);

    }

    //private void Update()
    //{
    //    Debug.DrawRay(transform.position + Vector3.up, Vector3.down * 5, Color.red);
    //    if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, 5f, cropLayerMask))
    //    {
    //        Sowing();
    //    }
    //    else StopSowing();
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            currentCropField = other.GetComponent<CropField>();
            if (currentCropField.IsEmpty()) Sowing();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            StopSowing();
            currentCropField = null;
        }
    }

    public void Sowing()
    {
        anim.PlaySowAnim();
    }

    public void StopSowing()
    {
        anim.StopSowAnim();
    }

    public void ThrowSeed()
    {
        seedParticle.Play();
    }

    public void SowSeedCallBack(SowSeed e)
    {
        Vector3[] collisionPositions = e.collisionPositions;
        if(currentCropField != null)
        {
            currentCropField.FillCropTiles(collisionPositions);
        }
    }

    private void FieldFulledCallBack(FieldFulled e)
    {
        StopSowing();
    }
}
