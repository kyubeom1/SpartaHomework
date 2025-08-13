using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed=5f;
    Vector2 moveInput;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase==InputActionPhase.Performed)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else if(context.phase==InputActionPhase.Canceled)
        {
            moveInput = Vector2.zero;
        }
    }

    private void Move()
    {
        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;
        move *= moveSpeed;
        move.y = rb.velocity.y;

        rb.velocity = move;
    }
}
