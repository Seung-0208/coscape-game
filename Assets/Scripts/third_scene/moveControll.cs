using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveControll : MonoBehaviour
{
    Rigidbody rigidbody3;
    void Start()
    {
        rigidbody3 = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 position = rigidbody3.position;
        rigidbody3.MovePosition(position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");

    }
}
