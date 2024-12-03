using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactions : MonoBehaviour
{
    void Update()
    {
        Invoke("destroy",8f);
    }

    void destroy() {
        Destroy(gameObject);
    }
}
