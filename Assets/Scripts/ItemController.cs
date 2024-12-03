using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemController : MonoBehaviour
{
    public float jumpForce = 10f;
    public bool canJump = false;
    PlayController play;

    private AudioSource item_audio;
    public AudioClip item;


    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        play = GetComponent<PlayController>();
        item_audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&gameObject.CompareTag("Item"))
        {
            canJump = true;
            //GameObject.Find("Player").GetComponent<PlayController>().JumpCnt++;
            // item_audio.PlayOneShot(item);
            // item_audio.Play();
            Destroy(gameObject,0f);
        }
        if (other.CompareTag("Player")&&gameObject.CompareTag("UFO"))
        {
            item_audio.Play();
            Destroy(gameObject,item.length);
            Invoke("moving_stop", 0.05f);
            //Invoke("ChangeScene",3f);
        }
    }
    
    void moving_stop() {
        if (SceneManager.GetActiveScene().name == "ice1") {
            GameObject.Find("Player").GetComponent<Ice1Controll>().canMoveLeft = false;
            GameObject.Find("Player").GetComponent<Ice1Controll>().canMoveRight = false;
        }
        if (SceneManager.GetActiveScene().name == "ice2") {
            GameObject.Find("Player").GetComponent<Ice2Controll>().canMoveLeft = false;
            GameObject.Find("Player").GetComponent<Ice2Controll>().canMoveRight = false;
        }
        if (SceneManager.GetActiveScene().name == "ice3") {
            GameObject.Find("Player").GetComponent<Ice3Controll>().canMoveLeft = false;
            GameObject.Find("Player").GetComponent<Ice3Controll>().canMoveRight = false;
        }
    }
}