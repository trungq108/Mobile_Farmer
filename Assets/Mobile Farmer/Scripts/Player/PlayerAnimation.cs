using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void MovementAnim(Vector3 direction)
    {
        if(direction.sqrMagnitude > 0f)
        {
            animator.Play(Constain.Run);
            animator.transform.forward = direction; // Rotate character render to the direction, NOT the character himself
        }
        else animator.Play(Constain.Idle);

    }
}
