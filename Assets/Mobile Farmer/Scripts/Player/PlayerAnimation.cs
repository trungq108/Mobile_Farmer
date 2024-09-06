using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private string currentAnim = "";
    private int sowAnimLayer = 1;


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


    public void ChangAnim(string nextAnim)
    {
        if (currentAnim != nextAnim)
        {
            animator.ResetTrigger(nextAnim);
            currentAnim = nextAnim;
            animator.SetTrigger(currentAnim);
        }
    }
}
