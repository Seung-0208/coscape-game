using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSound : MonoBehaviour
{
    private AudioSource audioSource;
    Animator Cam1;
    void Start()
    {
        Cam1 = GameObject.Find("Cam1").GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (Cam1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log("is called from cameraSound");
            audioSource.Play();
        }
    }
}
