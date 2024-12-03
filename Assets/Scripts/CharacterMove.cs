using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    CameraController cam_controller;
    public float speed = 5f;
    private Rigidbody characterRigidbody;




    void Start() {
        characterRigidbody = GetComponent<Rigidbody>();
        cam_controller = GetComponent<CameraController>();
        if (cam_controller == null) {
            Debug.LogError("CameraController not found!");
        }

    }
    void Update(){
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        // -1 ~ 1
    
        Vector3 velocity = new Vector3(inputX, 0, inputZ);
        int test = GameObject.Find("CameraController").GetComponent<CameraController>().clickCnt;
        if (test%4==0) {
            Debug.Log("is_called");
            velocity = new Vector3(0,0,-inputZ);

        }
        else if (test%4==1) {
            Debug.Log("is_called");
            velocity = new Vector3(inputX,0,0);

        }
        else if (test%4==2) {
            Debug.Log("is_called");
            velocity = new Vector3(0,0,inputZ);

        }
        else {
            Debug.Log("is_called");
            velocity = new Vector3(-inputX,0,0);

        }
        velocity *= speed;
        characterRigidbody.velocity = velocity;
        characterRigidbody.MovePosition(characterRigidbody.position + velocity * Time.fixedDeltaTime);
    }

}