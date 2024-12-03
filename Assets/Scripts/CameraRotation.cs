using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private float rotationSpeed = 300.0f;
    
    
    void Update()
    {
        
        CamOrbit();
    }

    void CamOrbit() {
        Transform mainCameraTransform = Camera.main.transform;
        Debug.Log("is called");
        if(Input.GetKeyDown(KeyCode.Q)) {
            float horizontalInput = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, 90);
            mainCameraTransform.position = new Vector3(2.17f,2.0f,8.59f);
        }
    }
}
