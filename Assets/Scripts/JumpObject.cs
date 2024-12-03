using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpObject : MonoBehaviour
{
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 80, 0));
    }
}
