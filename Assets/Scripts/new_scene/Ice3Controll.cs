using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ice3Controll : MonoBehaviour
{
    public LayerMask collisionLayer;
    float collisionStartTime;
    
    //캐릭터 움직임 제어-----------------------
    public float moveSpeed = 10.0f;
    public bool canMoveLeft = false;
    public bool canMoveRight = false;
    bool isground;
    //----------------------------------------

    PlayController controller; //test
    public Transform character;
    Rigidbody rb;

    AudioSource audiosource;
    public AudioClip scene;

    //끼임 체크---------------------
    public GameObject[] obsModel;
    GameObject warning;
    bool isInsideModel;
    //------------------------------

    public int clickCnt = 0;
    public Transform[] playItem;

    public Transform[] obstacle; //기둥들, 아이템들 정보 (오름차순, 정사각형부터)

    public Transform otherParent;
    public Vector3[] origin_pos; //각 기둥들, 아이템들의 원래 위치값 정보


    //animation test-------------------------------------------------
    Animator Cam1;
    private Vector3 cam1pos;
    private Vector3 cam2pos;
    private Vector3 cam3pos;
    private Vector3 cam4pos;
    private Vector3 curpos;
    int cam1_right = Animator.StringToHash("Cam1");
    //---------------------------------------------------------------

    private float z_cur; private float x_cur;


    private void Start()
    {
        obstacle = new Transform[otherParent.childCount];
        int i = 0;
        foreach(Transform child in otherParent) {
            obstacle[i] = child;
            i++;
        }

        //끼임 경고창----------------------
        warning = GameObject.Find("warning");
        warning.SetActive(false);
        //--------------------------------

        rb = GetComponent<Rigidbody>();
        Cam1 = GameObject.Find("Cam1").GetComponent<Animator>(); //애니메이션test

        

        //카메라 위치 설정
        curpos = GameObject.Find("Cam1").transform.position;
        cam1pos = GameObject.Find("cam1_test").transform.position;
        cam2pos = GameObject.Find("Cam2").transform.position;
        cam3pos = GameObject.Find("Cam3").transform.position;
        cam4pos = GameObject.Find("Cam4").transform.position;

        Debug.Log(cam1pos);
        Debug.Log(cam2pos);
        Debug.Log(cam3pos);
        Debug.Log(cam4pos);

        controller = GetComponent<PlayController>();//test

        //각 기둥들의 원래 위치값 저장
        for (int j=0;j<obstacle.Length;j++) {
            origin_pos[j] = obstacle[j].position;
        }

        z_cur = transform.position.z; x_cur = transform.position.x;
    }



    private void Update()
    {
        // rb.isKinematic = false;
        // if (Cam1.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) rb.isKinematic = true;
        
        Vector3 moveInput = new Vector3(0,0,0); //test
        comeback();
        //카메라 위치 조정--------------------------------------------------------------------------------------
        if (Cam1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && Input.GetKeyDown(KeyCode.W)) {
            curpos=GameObject.Find("Cam1").transform.position;
            Debug.Log(curpos);
            if (curpos==cam1pos) {
                x_cur = transform.position.x; z_cur = transform.position.z;
                Cam1.Play("cam1_right",0,0f);
                curpos = GameObject.Find("Cam2").transform.position;
                Debug.Log(curpos==cam2pos);
                x_cur = transform.position.x; z_cur = transform.position.z;
            }
            else if (curpos==cam2pos) {
                x_cur = transform.position.x; z_cur = transform.position.z;
                Cam1.Play("cam1_right(1)",0,0f);
                curpos = GameObject.Find("Cam3").transform.position;
                Debug.Log(curpos==cam3pos);
                x_cur = transform.position.x; z_cur = transform.position.z;
            }
            else if (curpos==cam3pos) {
                x_cur = transform.position.x; z_cur = transform.position.z;
                Cam1.Play("cam1_right(2)",0,0f);
                curpos = GameObject.Find("Cam4").transform.position; 
                Debug.Log(curpos==cam4pos);
                x_cur = transform.position.x; z_cur = transform.position.z;
            }
            else if (curpos==cam4pos) {
                x_cur = transform.position.x; z_cur = transform.position.z;
                Cam1.Play("cam1_right(3)",0,0f);
                curpos = cam1pos;
                Debug.Log(curpos==cam1pos);
                x_cur = transform.position.x; z_cur = transform.position.z;
            }
        }

        if(Cam1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && Input.GetKeyDown(KeyCode.Q)) {
            curpos=GameObject.Find("Cam1").transform.position;
            Debug.Log(curpos);
            if (curpos==cam1pos) {
                x_cur = transform.position.x; z_cur = transform.position.z;
                Cam1.Play("cam1_left",0,0f);
                curpos = GameObject.Find("Cam4").transform.position;
                Debug.Log(curpos==cam4pos);
                x_cur = transform.position.x; z_cur = transform.position.z;
            }
            else if (curpos==cam4pos) {
                x_cur = transform.position.x; z_cur = transform.position.z;
                Cam1.Play("cam1_left(1)",0,0f);
                curpos = GameObject.Find("Cam3").transform.position;
                Debug.Log(curpos==cam3pos);
                x_cur = transform.position.x; z_cur = transform.position.z;
            }
            else if (curpos==cam3pos) {
                x_cur = transform.position.x; z_cur = transform.position.z;
                Cam1.Play("cam1_left(2)",0,0f);
                curpos = GameObject.Find("Cam2").transform.position;
                Debug.Log(curpos==cam2pos);
                x_cur = transform.position.x; z_cur = transform.position.z;
            }
            else if (curpos==cam2pos) {
                x_cur = transform.position.x; z_cur = transform.position.z;
                Cam1.Play("cam1_left(3)",0,0f);
                curpos = cam1pos;
                Debug.Log(curpos==cam1pos);
                x_cur = transform.position.x; z_cur = transform.position.z;
            }
            
        } 


        //캐릭터의 시선, 움직임 조작----------------------------------------------------------------------------------
        if (Cam1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) 
        {
            RaycastHit hit;

            if (curpos==cam1pos) {
            //캐릭터의 위치 및 시선 변경
            Vector3 newPosition = gameObject.transform.position;
           
            if (playItem[0].transform.rotation == Quaternion.Euler(0, 90, 0)||playItem[0].transform.rotation == Quaternion.Euler(0, -90, 0)||playItem[0].transform.rotation == Quaternion.Euler(0, -270, 0) ||playItem[0].transform.rotation == Quaternion.Euler(0, 270, 0))
            {
                playItem[0].transform.rotation = Quaternion.Euler(0, 0, 0);
                // IsObjectInsideModel();
                // if (isInsideModel) Debug.Log("Raycast Check");
            }
            //키 조작
            if (!canMoveLeft && !canMoveRight)
            {
                if (Physics.Raycast(transform.position, -transform.up, out hit, 2f, collisionLayer)) {
                    moveSpeed = 3.0f;
                    if (Input.GetKey(KeyCode.LeftArrow)) newPosition.x -= moveSpeed * Time.deltaTime;
                    if (Input.GetKey(KeyCode.RightArrow)) newPosition.x += moveSpeed * Time.deltaTime;
                    isground = false;
                }
                else {
                    if (isground) 
                    {moveSpeed = 10.0f;
                    if (Input.GetKeyDown(KeyCode.LeftArrow) && !canMoveLeft) {
                        Debug.Log("ice");
                        canMoveLeft = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow) && !canMoveRight) {
                        Debug.Log("ice");
                        canMoveRight = true;
                    }}
                    else {
                        moveSpeed = 3.0f;
                        if (Input.GetKey(KeyCode.LeftArrow)) newPosition.x -= moveSpeed * Time.deltaTime;
                        if (Input.GetKey(KeyCode.RightArrow)) newPosition.x += moveSpeed * Time.deltaTime;
                    }
                }

                //캐릭터의 시선 변경
                if(Input.GetAxisRaw("Vertical")==1) playItem[0].transform.rotation = Quaternion.Euler(0, 0, 0);
                else if(Input.GetAxisRaw("Vertical")==-1) playItem[0].transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (canMoveLeft) newPosition.x -= moveSpeed * Time.deltaTime;
            if (canMoveRight) newPosition.x += moveSpeed * Time.deltaTime;

            gameObject.transform.position = newPosition;


        }

        else if (curpos==cam4pos) {
            //캐릭터의 위치 및 시선 변경
            Vector3 newPosition = gameObject.transform.position;
            if (playItem[0].transform.rotation == Quaternion.Euler(0, -180, 0) || playItem[0].transform.rotation == Quaternion.Euler(0, 0, 0) || playItem[0].transform.rotation == Quaternion.Euler(0, 180, 0) || playItem[0].transform.rotation == Quaternion.Euler(0, 360, 0))
            {
                playItem[0].transform.rotation = Quaternion.Euler(0, 90, 0);
                // IsObjectInsideModel();
                // if (isInsideModel) Debug.Log("Raycast Check");
            }

            //키 조작
            if (!canMoveLeft && !canMoveRight)
            {
                if (Physics.Raycast(transform.position, -transform.up, out hit, 2f, collisionLayer)) {
                    moveSpeed = 3.0f;
                    if (Input.GetKey(KeyCode.LeftArrow)) newPosition.z += moveSpeed * Time.deltaTime;
                    if (Input.GetKey(KeyCode.RightArrow)) newPosition.z -= moveSpeed * Time.deltaTime;
                    isground = false;
                }
                else {
                    if (isground)
                    {moveSpeed = 10.0f;
                    if (Input.GetKeyDown(KeyCode.LeftArrow) && !canMoveLeft) {
                        canMoveLeft = true;
                    }

                    else if (Input.GetKeyDown(KeyCode.RightArrow) && !canMoveRight) {
                        canMoveRight = true;
                    }}
                    else {
                        moveSpeed = 3.0f;
                    if (Input.GetKey(KeyCode.LeftArrow)) newPosition.z += moveSpeed * Time.deltaTime;
                    if (Input.GetKey(KeyCode.RightArrow)) newPosition.z -= moveSpeed * Time.deltaTime;
                    }
                }

                //캐릭터의 시선 변경
                if(Input.GetAxisRaw("Horizontal")==1) playItem[0].transform.rotation = Quaternion.Euler(0, 90, 0);
                else if(Input.GetAxisRaw("Horizontal")==-1) playItem[0].transform.rotation = Quaternion.Euler(0, 270, 0);
            }

            if (canMoveLeft) newPosition.z += moveSpeed * Time.deltaTime;
            if (canMoveRight) newPosition.z -= moveSpeed * Time.deltaTime;

            gameObject.transform.position = newPosition;
            
        }
        else if (curpos==cam2pos) {
            //캐릭터의 위치 및 시선 변경
            Vector3 newPosition = gameObject.transform.position;

            if (playItem[0].transform.rotation == Quaternion.Euler(0, -180, 0) || playItem[0].transform.rotation == Quaternion.Euler(0, 0, 0) || playItem[0].transform.rotation == Quaternion.Euler(0, 180, 0) || playItem[0].transform.rotation == Quaternion.Euler(0, 360, 0))
            {
                playItem[0].transform.rotation = Quaternion.Euler(0, 90, 0);
                // IsObjectInsideModel();
                // if (isInsideModel) Debug.Log("Raycast Check");
            }

            //키 조작
            if (!canMoveLeft && !canMoveRight)
            {
                if (Physics.Raycast(transform.position, -transform.up, out hit, 2f, collisionLayer)) {
                    moveSpeed = 3.0f;
                    if (Input.GetKey(KeyCode.LeftArrow)) newPosition.z -= moveSpeed * Time.deltaTime;
                    if (Input.GetKey(KeyCode.RightArrow)) newPosition.z += moveSpeed * Time.deltaTime;
                    isground = false;
                }
                else {
                    if (isground)
                    {moveSpeed = 10.0f;
                    if (Input.GetKeyDown(KeyCode.LeftArrow) && !canMoveLeft) {
                        canMoveLeft = true;
                    }

                    else if (Input.GetKeyDown(KeyCode.RightArrow) && !canMoveRight) {
                        canMoveRight = true;
                    }}
                    else {
                        moveSpeed = 3.0f;
                        if (Input.GetKey(KeyCode.LeftArrow)) newPosition.z -= moveSpeed * Time.deltaTime;
                        if (Input.GetKey(KeyCode.RightArrow)) newPosition.z += moveSpeed * Time.deltaTime;
                    }
                }
                //캐릭터의 시선 변경
                if(Input.GetAxisRaw("Horizontal")==1) playItem[0].transform.rotation = Quaternion.Euler(0, 270, 0);
                else if(Input.GetAxisRaw("Horizontal")==-1) playItem[0].transform.rotation = Quaternion.Euler(0, 90, 0);
            }

            if (canMoveLeft) newPosition.z -= moveSpeed * Time.deltaTime;
            if (canMoveRight) newPosition.z += moveSpeed * Time.deltaTime;

            gameObject.transform.position = newPosition;
        }
        else {
            //캐릭터의 위치 및 시선 변경
            Vector3 newPosition = gameObject.transform.position;

            if (playItem[0].transform.rotation == Quaternion.Euler(0, 90, 0)||playItem[0].transform.rotation == Quaternion.Euler(0, -90, 0)||playItem[0].transform.rotation == Quaternion.Euler(0, -270, 0) ||playItem[0].transform.rotation == Quaternion.Euler(0, 270, 0))
            {
                playItem[0].transform.rotation = Quaternion.Euler(0,-180, 0);
                // IsObjectInsideModel();
                // if (isInsideModel) Debug.Log("Raycast Check");
            }

            //키 조작
            if (!canMoveLeft && !canMoveRight)
            {
                if (Physics.Raycast(transform.position, -transform.up, out hit, 2f, collisionLayer)) {
                    moveSpeed = 3.0f;
                    if (Input.GetKey(KeyCode.LeftArrow)) newPosition.x += moveSpeed * Time.deltaTime;
                    if (Input.GetKey(KeyCode.RightArrow)) newPosition.x -= moveSpeed * Time.deltaTime;
                    isground = false;
                }
                else {
                    if (isground)
                    {moveSpeed = 10.0f;
                    if (Input.GetKeyDown(KeyCode.LeftArrow) && !canMoveLeft) {
                        canMoveLeft = true;
                    }

                    else if (Input.GetKeyDown(KeyCode.RightArrow) && !canMoveRight) {
                        canMoveRight = true;
                    }}
                    else {
                        moveSpeed = 3.0f;
                        if (Input.GetKey(KeyCode.LeftArrow)) newPosition.x += moveSpeed * Time.deltaTime;
                        if (Input.GetKey(KeyCode.RightArrow)) newPosition.x -= moveSpeed * Time.deltaTime;
                    }
                }
                //캐릭터의 시선 변경
                if(Input.GetAxisRaw("Vertical")==1) playItem[0].transform.rotation = Quaternion.Euler(0, 180, 0);
                else if(Input.GetAxisRaw("Vertical")==-1) playItem[0].transform.rotation = Quaternion.Euler(0, 0, 0);
                
            }

            if (canMoveLeft) newPosition.x += moveSpeed * Time.deltaTime;
            if (canMoveRight) newPosition.x -= moveSpeed * Time.deltaTime;

            gameObject.transform.position = newPosition;
            //rb.MovePosition(newPosition);
            //시선변경
        }
        movingcheck();
        }
        IsObjectInsideModel();
        SetCamera();
        Invoke("check_fall",3f);
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
                //road2
                Vector3 outPosition = gameObject.transform.position;
                Vector3 newPosition;

                //Vector3 prev = Vector3.zero;
                for (int i=0;i<obstacle.Length;i++) { //기둥들 위치 변경
                    
                    newPosition = obstacle[i].position;
                    if (i==obstacle.Length-2) newPosition.x = playItem[0].position.x - 0.695f;
                    else newPosition.x = playItem[0].position.x; //캐릭터 위치의 z값으로 위치를 맞춤
                    obstacle[i].position = newPosition;
                }
                if (x_cur+0.2f <= gameObject.transform.position.x || x_cur-0.2f >= gameObject.transform.position.x || isInsideModel) { //**튕겨나가기
                    warning.SetActive(true);
                    comeback();
                    outPosition.x += 3f;
                    gameObject.transform.position = outPosition;
                }

            }
            else if (Cam1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f&&curpos==cam4pos) {
                Debug.Log("cam4pos");
                Vector3 outPosition = gameObject.transform.position;
                Vector3 newPosition;
                for (int i=0;i<obstacle.Length;i++) {
                    newPosition = obstacle[i].position;
                    if (i==obstacle.Length-2) newPosition.x = playItem[0].position.x - 0.695f;
                    else newPosition.x = playItem[0].position.x;
                    obstacle[i].position = newPosition;
                }
                if (x_cur+0.2f <= gameObject.transform.position.x || x_cur-0.2f >= gameObject.transform.position.x || isInsideModel) { //**튕겨나가기
                    warning.SetActive(true);
                    comeback();
                    outPosition.x -= 3f;
                    gameObject.transform.position = outPosition;
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
                Vector3 outPosition = gameObject.transform.position;
                Vector3 newPosition;
                for (int i=0;i<obstacle.Length;i++) {
                    newPosition = obstacle[i].position;
                    if (i==obstacle.Length-3 || i==obstacle.Length-2) newPosition.z = playItem[0].position.z + 4.078f;
                    else newPosition.z = playItem[0].position.z;
                    obstacle[i].position = newPosition;
                }
                if (z_cur+0.1f <= gameObject.transform.position.z || z_cur-0.1f >= gameObject.transform.position.z || isInsideModel) { //**튕겨나가기
                    warning.SetActive(true);
                    comeback();
                    outPosition.z += 3f;
                    gameObject.transform.position = outPosition;
                }
            }
            else if (Cam1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f&&curpos==cam1pos) {
                Debug.Log("cam1pos");
                Vector3 outPosition = gameObject.transform.position;
                Vector3 newPosition;
                for (int i=0;i<obstacle.Length;i++) {
                    newPosition = obstacle[i].position;
                    if (i==obstacle.Length-3 || i==obstacle.Length-2) newPosition.z = playItem[0].position.z + 4.078f;
                    else newPosition.z = playItem[0].position.z;
                    obstacle[i].position = newPosition;
                }
                if (z_cur+0.1f <= gameObject.transform.position.z || z_cur-0.1f >= gameObject.transform.position.z || isInsideModel) { //**튕겨나가기
                    warning.SetActive(true);
                    comeback();
                    outPosition.z -= 3f;
                    gameObject.transform.position = outPosition;
                }
            }
        }
    }
    void check_fall() {
        RaycastHit hit;
        if (rb.velocity.y<-10f) {
            if (warning.activeSelf) Invoke("LoadScene",2f);
            else if (Physics.Raycast(transform.position, transform.up, out hit, collisionLayer)) {
                warning.SetActive(true);
                Invoke("LoadScene", 3f);
            }
            else LoadScene();
        }
    }

    private void LoadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void IsObjectInsideModel()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.forward, out hit)) {
            if (collisionStartTime == 0f) collisionStartTime = Time.time;
            Debug.DrawRay(transform.position, -transform.forward*hit.distance, Color.red);

            if (Time.time-collisionStartTime >= 0.5f) {
                isInsideModel = true;
            }
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            if (collisionStartTime == 0f) collisionStartTime = Time.time;
            Debug.DrawRay(transform.position, transform.forward*hit.distance, Color.red);
        
            if (Time.time-collisionStartTime >= 0.5f) {
                isInsideModel = true;
            }
        }
        else {
            collisionStartTime = 0f;
            Debug.DrawRay(transform.position, transform.forward*100f, Color.green);
            Debug.DrawRay(transform.position, -transform.forward*100f, Color.green);
            isInsideModel = false;
        }
    }

    void movingcheck() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.right, out hit, 0.1f, collisionLayer)) {
            canMoveLeft=false; canMoveRight = false;
        }
        if (Physics.Raycast(transform.position, -transform.up, out hit, 2f, collisionLayer)) {
            Invoke("stopMoving", 0.02f);
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("cube")) {
            canMoveLeft=false; canMoveRight=false;
        }
        else if (other.gameObject.CompareTag("ground")) {
            isground = true;
        }        
    }

    void OncollisionExit(Collision other) {
        if (other.gameObject.CompareTag("ground")) isground = false;
    }

    void stopMoving() {
        canMoveLeft = false; canMoveRight = false;
    }
}
