using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerAnimation anim;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = MobileJoystick.Instance.GetMoveVector().normalized;
        direction.z = direction.y;
        direction.y = 0;
        if(RaycheckDirection(direction * 0.1f))
        {
            Vector3 movementVector = direction * moveSpeed * Time.deltaTime;
            controller.Move(movementVector);
        }
        anim.MovementAnim(direction);
    }

    private bool RaycheckDirection(Vector3 checkDirection)
    {
        return Physics.Raycast(rayPoint.position + checkDirection, Vector3.down, 5f, groundLayerMask);
    }
}
