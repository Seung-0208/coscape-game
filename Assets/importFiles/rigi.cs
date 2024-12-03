using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rigi : MonoBehaviour
{
    public float moveSpeed = 5f; // ĳ������ �̵� �ӵ�

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal"); // ����ڰ� ���� �¿� ����Ű �Է°� �޾ƿ���
        float yInput = Input.GetAxis("Vertical"); // ����ڰ� ���� ���� ����Ű �Է°� �޾ƿ���

        Vector2 movement = new Vector2(xInput, yInput) * moveSpeed; // �Է°��� �̵� �ӵ��� ���Ͽ� �̵� ���� �����
        rb2d.velocity = movement; // Rigidbody2D�� velocity �Ӽ��� �̵� ���͸� �Ҵ��Ͽ� ĳ���� �̵�
    }
}