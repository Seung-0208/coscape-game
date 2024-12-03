using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInside : MonoBehaviour
{
    public GameObject[] model; // 3D 모델 오브젝트

    private void Update()
    {
        // 게임 오브젝트가 3D 모델 안에 있는지 여부 판단
        bool isInsideModel = IsObjectInsideModel();

        if (isInsideModel)
        {
            Debug.Log("게임 오브젝트가 3D 모델 안에 있습니다.");
        }
    }

    private bool IsObjectInsideModel()
    {
        // 게임 오브젝트와 3D 모델 사이의 충돌 여부 체크
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            for (int i=0;i<model.Length;i++)
            {if (hit.collider.gameObject == model[i])
            {
                return true; // 충돌한 객체가 3D 모델이면 true 반환
            }}
        }

        return false; // 충돌한 객체가 3D 모델이 아니면 false 반환
    }
}
