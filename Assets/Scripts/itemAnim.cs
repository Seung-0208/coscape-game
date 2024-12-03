using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemAnim : MonoBehaviour
{
    public float speed = 2f; //이동 속도
    public float amplitude = 0.5f; //애니메이션 진폭
    public float frequency = 1f; //애니메이션 주파수

    private Vector3 startPosition; //시작 위치

    void Start()
    {
        startPosition = transform.position; //현재 위치를 받아옴
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;

        Vector3 position = startPosition;
        position.y += offset;

        transform.position = position;
    }
}
