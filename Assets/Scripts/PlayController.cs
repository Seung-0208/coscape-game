using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(Rigidbody))]
public class PlayController : MonoBehaviour
{
    Vector3 velocity;
    Rigidbody myRigidbody;
    private AudioSource audioSource;
    public AudioSource item;
    public AudioClip ufo;
    public AudioClip scene;

    //점프 코드 구현-----------------------------------------------------
    public float jumpForce = 200f;
    public float jumpSpeedBoost = 5f;
    public float fallSpeedBoost = 10f;

    public bool isGrounded = true;
    public int JumpCnt = 0;

    public float rotationForce = 2f;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    private float jumpTime = 0f;
    
    public GameObject[] itemCnt_image;

    //------------------------------------------------------------------



    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

        //점프아이템 개수 ui 이미지
        for (int i=0; i<itemCnt_image.Length; i++) {
            itemCnt_image[i].SetActive(false);
        }
    }

    public void Move(Vector3 _movearrow) {
        myRigidbody.MovePosition(myRigidbody.position + _movearrow * 10.0f * Time.deltaTime);
    }

    //여기서부터는 점프 코드 구현을 위해 존재-----------------------------
    void Update() {

        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetKeyDown(KeyCode.E) && JumpCnt>0) {
            myRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpTime = Time.time;
            JumpCnt--;
            
        }

        if (Input.GetKey(KeyCode.E)&&((Input.GetKey(KeyCode.LeftArrow))||Input.GetKey(KeyCode.RightArrow))) {
            if (SceneManager.GetActiveScene().name == "bridge") GameObject.Find("Player").GetComponent<ThirdControll>().moveSpeed = 1.0f;
            else if (SceneManager.GetActiveScene().name == "elevate1") GameObject.Find("Player").GetComponent<Ele1Controller>().moveSpeed = 1.0f;
            else if (SceneManager.GetActiveScene().name == "elevate2") GameObject.Find("Player").GetComponent<Ele2Controller>().moveSpeed = 2.0f;
            else if (SceneManager.GetActiveScene().name == "trap") GameObject.Find("Player").GetComponent<trapController>().moveSpeed = 1.0f;
            else if (SceneManager.GetActiveScene().name == "ice1") GameObject.Find("Player").GetComponent<Ice1Controll>().moveSpeed = 1.0f;
            else if (SceneManager.GetActiveScene().name == "ice2") GameObject.Find("Player").GetComponent<Ice2Controll>().moveSpeed = 1.0f;
            else if (SceneManager.GetActiveScene().name == "ice3") GameObject.Find("Player").GetComponent<Ice3Controll>().moveSpeed = 1.0f;
        }
        else {
            if (SceneManager.GetActiveScene().name == "bridge") GameObject.Find("Player").GetComponent<ThirdControll>().moveSpeed = 3.0f;
            else if (SceneManager.GetActiveScene().name == "elevate1") GameObject.Find("Player").GetComponent<Ele1Controller>().moveSpeed = 3.0f;
            else if (SceneManager.GetActiveScene().name == "elevate2") GameObject.Find("Player").GetComponent<Ele2Controller>().moveSpeed = 3.0f;
            else if (SceneManager.GetActiveScene().name == "trap") GameObject.Find("Player").GetComponent<trapController>().moveSpeed = 3.0f;
            else if (SceneManager.GetActiveScene().name == "ice1") GameObject.Find("Player").GetComponent<Ice1Controll>().moveSpeed = 10.0f;
            else if (SceneManager.GetActiveScene().name == "ice2") GameObject.Find("Player").GetComponent<Ice2Controll>().moveSpeed = 10.0f;
            else if (SceneManager.GetActiveScene().name == "ice3") GameObject.Find("Player").GetComponent<Ice3Controll>().moveSpeed = 10.0f;
        }

        //if (Input.GetKey(KeyCode.E) && JumpCnt>0) GameObject.Find("Player").GetComponent<ThirdControll>().moveSpeed = 1.0f;
        //else GameObject.Find("Player").GetComponent<ThirdControll>().moveSpeed = 3.0f;

        
        //GameObject.Find("Player").GetComponent<ThirdControll>().moveSpeed = 3.0f;

        for (int i=0; i<itemCnt_image.Length; i++) {
            if (i==JumpCnt) {
                itemCnt_image[i].SetActive(true);
            }
            else {
                itemCnt_image[i].SetActive(false);
            }
        }

    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Item")) {
            JumpCnt++;
            item.Play();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

}
