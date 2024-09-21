using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerAnimation anim;
    [SerializeField] private Transform model;
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
        anim.MovementAnim(direction);

        if (direction.magnitude <= 0) return;
        if (!RaycheckDirection(direction * 0.1f)) return;
        controller.Move(direction * moveSpeed * Time.deltaTime);
        model.transform.forward = direction;
    }

    private bool RaycheckDirection(Vector3 checkDirection)
    {
        return Physics.Raycast(rayPoint.position + checkDirection, Vector3.down, 5f, groundLayerMask);
    }

    public void SetTreeModePosition(Vector3 newPosition, Vector3 treePosition)
    {
        transform.position = newPosition;
        model.transform.forward = treePosition - transform.position;
    }
}
