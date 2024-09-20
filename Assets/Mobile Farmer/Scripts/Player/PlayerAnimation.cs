using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject wateringCan;
    [SerializeField] private GameObject scythe;

    private int sowAnimLayer = 1;
    private int waterAnimLayer = 2;
    private int havestAnimLayer = 3;

    private void Awake()
    {
        wateringCan.SetActive(false);
        scythe.SetActive(false);
    }

    public void MovementAnim(Vector3 direction)
    {
        if(direction.sqrMagnitude > 0f)
        {
            animator.Play("Run");
            animator.transform.forward = direction; // Rotate character render to the direction, NOT the character himself
        }
        else animator.Play("Idle");
    }

    public void PlaySowAnim()
    {
        animator.SetLayerWeight(sowAnimLayer, 1);
    }

    public void StopSowAnim()
    {
        animator.SetLayerWeight(sowAnimLayer, 0);
    }

    public void PlayWaterAnim()
    {
        animator.SetLayerWeight(waterAnimLayer, 1);
        wateringCan.SetActive(true);
    }

    public void StopWaterAnim()
    {
        animator.SetLayerWeight(waterAnimLayer, 0);
        wateringCan.SetActive(false);
    }

    public void PlayHarvest()
    {
        animator.SetLayerWeight(havestAnimLayer, 1);
        scythe.SetActive(true);
    }

    public void StopPlayHarvest()
    {
        animator.SetLayerWeight(havestAnimLayer, 0);
        scythe.SetActive(false);
    }


}
