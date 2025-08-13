using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;//�̵� �ӵ�
    Vector2 moveInput;//�Է��� Ű �� ����
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

    public void OnMoveInput(InputAction.CallbackContext context)//��ǲ�׼ǿ� �Էµ� Ű�� �޾Ƽ� ��
    {
        if(context.phase==InputActionPhase.Performed)//�Էµ� Ű�� �ִٸ�
        {
            moveInput = context.ReadValue<Vector2>();//���� ��ǲ�� ������ ����
        }
        else if(context.phase==InputActionPhase.Canceled)//�Էµ� Ű�� ���ٸ�
        {
            moveInput = Vector2.zero;//���� ��ǲ�� ���Ͱ��� 0���� ��
        }
    }

    private void Move()//������ ����
    {
        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;//�Է� ���� ���� ������ ������
        move *= moveSpeed;//�̵��ӵ���ŭ ������
        move.y = rb.velocity.y;

        rb.velocity = move;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            Debug.Log("����");
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
                Debug.Log("�浹");
                return true;
            }
        }
        return false;
    }
}
