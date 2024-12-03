using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(PlayController))] //test

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    PlayController controller; //test
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
    public Camera camera4;
    public Transform character;
    Rigidbody rb;



    public int clickCnt = 0;
    public Transform[] playItem;

    public Transform[] obstacle; //기둥들, 아이템들 정보 (오름차순, 정사각형부터)

    public Vector3[] origin_pos; //각 기둥들, 아이템들의 원래 위치값 정보
    public Transform otherParent;


    //animation test-------------------------------------------------
    Animator Cam1;
    private Vector3 cam1pos;
    private Vector3 cam2pos;
    private Vector3 cam3pos;
    private Vector3 cam4pos;
    private Vector3 curpos;
    int cam1_right = Animator.StringToHash("Cam1");
    //---------------------------------------------------------------

    private void Start()
    {
        obstacle = new Transform[otherParent.childCount];
        int i = 0;
        foreach(Transform child in otherParent) {
            obstacle[i] = child;
            i++;
        }
        rb = GetComponent<Rigidbody>();
        Cam1 = GameObject.Find("Cam1").GetComponent<Animator>(); //애니메이션test
        //카메라 위치 설정
        curpos = GameObject.Find("Cam1").transform.position;
        cam1pos = GameObject.Find("Cam1").transform.position;
        cam2pos = GameObject.Find("Cam2").transform.position;
        cam3pos = GameObject.Find("Cam3").transform.position;
        cam4pos = GameObject.Find("Cam4").transform.position;

        // 초기 화면 설정
        camera1.enabled = true;
        camera2.enabled = false;
        camera3.enabled = false;
        camera4.enabled = false;

        Debug.Log(cam1pos);
        Debug.Log(cam2pos);
        Debug.Log(cam3pos);
        Debug.Log(cam4pos);

        controller = GetComponent<PlayController>();//test

        //각 기둥들의 원래 위치값 저장
        for (int j=0;j<obstacle.Length;j++) {
            origin_pos[j] = obstacle[j].position;
        }
    }



    private void Update()
    {
        rb.isKinematic = false;
        if (Cam1.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) rb.isKinematic = true;

        Vector3 moveInput = new Vector3(0,0,0); //test
        comeback();
        if (Cam1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && Input.GetKeyDown(KeyCode.W)) {
            curpos=GameObject.Find("Cam1").transform.position;
            Debug.Log(curpos);
            
            if (curpos==cam1pos) {
                Debug.Log("1");
                Cam1.Play("cam1_right",0,0f);
                curpos = GameObject.Find("Cam2").transform.position;
                Debug.Log(curpos==cam2pos);
            }
            else if (curpos==cam2pos) {
                Debug.Log("2");
                Cam1.Play("cam1_right(1)",0,0f);
                curpos = GameObject.Find("Cam3").transform.position;
                Debug.Log(curpos==cam3pos);
            }
            else if (curpos==cam3pos) {
                Debug.Log("3");
                Cam1.Play("cam1_right(2)",0,0f);
                curpos = GameObject.Find("Cam4").transform.position; 
                Debug.Log(curpos==cam4pos);
            }
            else if (curpos==cam4pos) {
                Debug.Log("4");
                Cam1.Play("cam1_right(3)",0,0f);
                curpos = cam1pos;
                Debug.Log(curpos==cam1pos);
            }
        }
        if(Cam1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && Input.GetKeyDown(KeyCode.Q)) {
            curpos=GameObject.Find("Cam1").transform.position;
            Debug.Log(curpos);
            if (curpos==cam1pos) {
                Debug.Log("1");
                Cam1.Play("cam1_left",0,0f);
                curpos = GameObject.Find("Cam4").transform.position;
                Debug.Log(curpos==cam4pos);
            }
            else if (curpos==cam4pos) {
                Debug.Log("4");
                Cam1.Play("cam1_left(1)",0,0f);
                curpos = GameObject.Find("Cam3").transform.position;
                Debug.Log(curpos==cam3pos);
            }
            else if (curpos==cam3pos) {
                Debug.Log("3");
                Cam1.Play("cam1_left(2)",0,0f);
                curpos = GameObject.Find("Cam2").transform.position;
                Debug.Log(curpos==cam2pos);
            }
            else if (curpos==cam2pos) {
                Debug.Log("2");
                Cam1.Play("cam1_left(3)",0,0f);
                curpos = cam1pos;
                Debug.Log(curpos==cam1pos);
            }
        } 
        SetCamera();
        Invoke("check)fall",3f);
        //캐릭터의 시선, 위치 변경 및 움직임 조작----------------------------------------------------------------------------------
        if (Cam1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {if (curpos==cam2pos) {
            //캐릭터의 위치 변경
            Vector3 newPosition = gameObject.transform.position;
            //newPosition.z = -0.07f;
            if (playItem[0].transform.rotation == Quaternion.Euler(0, 90, 0)||playItem[0].transform.rotation == Quaternion.Euler(0, -90, 0)||playItem[0].transform.rotation == Quaternion.Euler(0, -270, 0) ||playItem[0].transform.rotation == Quaternion.Euler(0, 270, 0))
            playItem[0].transform.rotation = Quaternion.Euler(0, 0, 0);
            //키 조작
            if (Input.GetKey(KeyCode.LeftArrow)) {
                newPosition.x -= moveSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.RightArrow)) {
                newPosition.x += moveSpeed * Time.deltaTime;
            }
            gameObject.transform.position = newPosition;
            //캐릭터의 시선 변경
            if(Input.GetAxisRaw("Vertical")==1) playItem[0].transform.rotation = Quaternion.Euler(0, 0, 0);
            else if(Input.GetAxisRaw("Vertical")==-1) playItem[0].transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (curpos==cam1pos) {
            //위치변경
            Vector3 newPosition = gameObject.transform.position;
            //newPosition.x = 0f;
            if (playItem[0].transform.rotation == Quaternion.Euler(0, -180, 0) || playItem[0].transform.rotation == Quaternion.Euler(0, 0, 0) || playItem[0].transform.rotation == Quaternion.Euler(0, 180, 0) || playItem[0].transform.rotation == Quaternion.Euler(0, 360, 0))
            playItem[0].transform.rotation = Quaternion.Euler(0, 90, 0);

            if (Input.GetKey(KeyCode.LeftArrow)) {
                newPosition.z += moveSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.RightArrow)) {
                newPosition.z -= moveSpeed * Time.deltaTime;
            }
            gameObject.transform.position = newPosition;
            //시선
            if(Input.GetAxisRaw("Horizontal")==1) playItem[0].transform.rotation = Quaternion.Euler(0, 90, 0);
            else if(Input.GetAxisRaw("Horizontal")==-1) playItem[0].transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else if (curpos==cam3pos) {
            //위치변경
            Vector3 newPosition = gameObject.transform.position;
            //newPosition.x = 3.93f;
            if (playItem[0].transform.rotation == Quaternion.Euler(0, -180, 0) || playItem[0].transform.rotation == Quaternion.Euler(0, 0, 0) || playItem[0].transform.rotation == Quaternion.Euler(0, 180, 0) || playItem[0].transform.rotation == Quaternion.Euler(0, 360, 0))
            playItem[0].transform.rotation = Quaternion.Euler(0, 90, 0);
            if (Input.GetKey(KeyCode.LeftArrow)) {
                newPosition.z -= moveSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.RightArrow)) {
                newPosition.z += moveSpeed * Time.deltaTime;
            }
            gameObject.transform.position = newPosition;
            //시선
            if(Input.GetAxisRaw("Horizontal")==1) playItem[0].transform.rotation = Quaternion.Euler(0, 270, 0);
            else if(Input.GetAxisRaw("Horizontal")==-1) playItem[0].transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else {
            //캐릭터의 위치 변경
            Vector3 newPosition = gameObject.transform.position;
            //newPosition.z = 3.93f;
            if (playItem[0].transform.rotation == Quaternion.Euler(0, 90, 0)||playItem[0].transform.rotation == Quaternion.Euler(0, -90, 0)||playItem[0].transform.rotation == Quaternion.Euler(0, -270, 0) ||playItem[0].transform.rotation == Quaternion.Euler(0, 270, 0))
            playItem[0].transform.rotation = Quaternion.Euler(0, 0, 0);
            if (Input.GetKey(KeyCode.LeftArrow)) {
                newPosition.x += moveSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.RightArrow)) {
                newPosition.x -= moveSpeed * Time.deltaTime;
            }
            gameObject.transform.position = newPosition;
            //시선변경
            if(Input.GetAxisRaw("Vertical")==1) playItem[0].transform.rotation = Quaternion.Euler(0, 180, 0);
            else if(Input.GetAxisRaw("Vertical")==-1) playItem[0].transform.rotation = Quaternion.Euler(0, 0, 0);
        }}
        check_fall();
        //SetCamera();
    }


    //기둥 다시 원래대로
    void comeback() {
        for (int i =0;i<obstacle.Length;i++) {
            obstacle[i].position = origin_pos[i];
        }
    }

    // 2D 아이템 오브젝트 벡터값 변경 + 기둥들 위치 변경
    void SetCamera()
    {

        
        if (curpos==cam2pos || curpos == cam4pos)
        {
            for (int i=1 ;i<playItem.Length;i++) {
                if(playItem[i] != null) {
                playItem[i].localEulerAngles = new Vector3(0,180,0);
                }
            }
            if (Cam1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f&&curpos==cam2pos) {
                Debug.Log("cam2pos");
                Vector3 newPosition;
                for (int i=0;i<obstacle.Length;i++) {
                    newPosition = obstacle[i].position;
                    newPosition.z = playItem[0].position.z;
                    obstacle[i].position = newPosition;
                }
            }
            else if (Cam1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f&&curpos==cam4pos) {
                Debug.Log("cam4pos");
                Vector3 newPosition;
                for (int i=0;i<obstacle.Length;i++) {
                    newPosition = obstacle[i].position;
                    newPosition.z = playItem[0].position.z;
                    obstacle[i].position = newPosition;
                }
            }
        }
        else if (curpos == cam1pos || curpos == cam3pos)
        {
            for (int i=1 ;i<playItem.Length;i++) {
                if(playItem[i] != null) {
                    
                playItem[i].localEulerAngles = new Vector3(0,90,0);
                }
            }
            if (Cam1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f&&curpos==cam3pos) {
                Debug.Log("cam3pos");
                Vector3 newPosition;
                for (int i=0;i<obstacle.Length;i++) {
                    newPosition = obstacle[i].position;
                    newPosition.x = playItem[0].position.x;
                    obstacle[i].position = newPosition;
                }

            }
            else if (Cam1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f&&curpos==cam1pos) {
                Debug.Log("cam1pos");
                Vector3 newPosition;
                for (int i=0;i<obstacle.Length;i++) {
                    newPosition = obstacle[i].position;
                    newPosition.x = playItem[0].position.x;
                    obstacle[i].position = newPosition;
                }
            }
        }
    }
    void check_fall() {
        if (rb.velocity.y<-10f) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
}