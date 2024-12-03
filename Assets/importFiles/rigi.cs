using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rigi : MonoBehaviour
{
    public float moveSpeed = 5f; // 캐릭터의 이동 속도

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal"); // 사용자가 누른 좌우 방향키 입력값 받아오기
        float yInput = Input.GetAxis("Vertical"); // 사용자가 누른 상하 방향키 입력값 받아오기

        Vector2 movement = new Vector2(xInput, yInput) * moveSpeed; // 입력값에 이동 속도를 곱하여 이동 벡터 만들기
        rb2d.velocity = movement; // Rigidbody2D의 velocity 속성에 이동 벡터를 할당하여 캐릭터 이동
    }
}