using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;//이동 속도
    Vector2 moveInput;//입력한 키 값 저장
    Rigidbody rb;

    public float jumpPower = 5f; 
    public LayerMask groundLayerMask;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnMoveInput(InputAction.CallbackContext context)//인풋액션에 입력된 키를 받아서 씀
    {
        if(context.phase==InputActionPhase.Performed)//입력된 키가 있다면
        {
            moveInput = context.ReadValue<Vector2>();//무브 인풋에 내용을 전달
        }
        else if(context.phase==InputActionPhase.Canceled)//입력된 키가 없다면
        {
            moveInput = Vector2.zero;//무브 인풋에 벡터값을 0으로 함
        }
    }

    private void Move()//움직임 구현
    {
        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;//입력 받은 값에 방향을 정해줌
        move *= moveSpeed;//이동속도만큼 곱해줌
        move.y = rb.velocity.y;

        rb.velocity = move;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            Debug.Log("쩜프");
            rb.AddForce(Vector2.up * jumpPower,ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward *0.2f)+(transform.up * 0.01f),Vector3.down),
            new Ray(transform.position + (-transform.forward *0.2f)+(transform.up * 0.01f),Vector3.down),
            new Ray(transform.position + (transform.right *0.2f)+(transform.up * 0.01f),Vector3.down),
            new Ray(transform.position + (-transform.right *0.2f)+(transform.up * 0.01f),Vector3.down)
        };

        for(int i = 0; i<rays.Length; i++)
        {
            if (Physics.Raycast(rays[i],0.1f,groundLayerMask))
            {
                Debug.Log("충돌");
                return true;
            }
        }
        return false;
    }
}
