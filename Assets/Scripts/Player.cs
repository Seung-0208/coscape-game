using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player 스크립트를 오브젝트에 더할 때 PlayerController 스크립트도 같이 더함

[RequireComponent (typeof(PlayController))]
public class Player : MonoBehaviour
{

    //카메라 위치 확인----------------------------------------------------
    private Vector3 cam1pos;
    private Vector3 cam2pos;
    private Vector3 cam3pos;
    private Vector3 cam4pos;
    private Vector3 curpos;
    //-------------------------------------------------------------------

    public float moveSpeed = 5;
    PlayController controller;
    CameraController cam_controller;
    RestartGame restart;

    public Transform character;
    void Start()
    {
        controller = GetComponent<PlayController>();
        cam_controller = GetComponent<CameraController>();
        restart = GetComponent<RestartGame>();

        //위치 할당-------------------------------------------
        curpos = GameObject.Find("Cam1").transform.position;
        cam1pos = GameObject.Find("Cam1").transform.position;
        cam2pos = GameObject.Find("Cam2").transform.position;
        cam3pos = GameObject.Find("Cam3").transform.position;
        cam4pos = GameObject.Find("Cam4").transform.position;
        //---------------------------------------------------
    }

    void Update()
    {
        //GetAxis - 기본 스무딩 적용, GetAxisRaw - 스무딩 적용X
        //Vector3 moveInput = new Vector3(0,0,Input.GetAxisRaw("Vertical"));
        
        Vector3 moveInput = new Vector3(0,0,0);
        
        /* if (cam_controller.camera1.enabled == true) {
            moveInput = new Vector3(0,0,-Input.GetAxisRaw("Vertical"));
        }
        else if (cam_controller.camera2.enabled == true) {
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"),0,0);
        }
        else if (cam_controller.camera3.enabled == true) {
            moveInput = new Vector3(0,0,Input.GetAxisRaw("Vertical"));
        }
        else {
            moveInput = new Vector3(-Input.GetAxisRaw("Horizontal"),0,0);
        } */

        if (curpos==cam2pos) {
            Debug.Log("cam1pos moving");
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"),0,0);
            //moveInput = new Vector3(-Input.GetAxisRaw("Horizontal"),0,0);
        }
        else if (curpos==cam1pos) {
            Debug.Log("cam2pos moving");
            moveInput = new Vector3(0,0,Input.GetAxisRaw("Vertical"));
        }
        else if (curpos==cam3pos) {
            Debug.Log("cam3pos moving");
            moveInput = new Vector3(0,0,Input.GetAxisRaw("Vertical"));
            //moveInput = new Vector3(Input.GetAxisRaw("Horizontal"),0,0);
        }
        else {
            Debug.Log("cam4pos moving");
            moveInput = new Vector3(0,0,Input.GetAxisRaw("Vertical"));
        }
        Vector3 moveVeclocity = moveInput * moveSpeed;
        controller.Move(moveInput);
    }
}