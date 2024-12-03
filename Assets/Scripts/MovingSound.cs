using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSound : MonoBehaviour
{
    private Rigidbody rb;
    public AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) && !audioSource.isPlaying) {
            audioSource.Play();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
            audioSource.Stop();
        }
    }
}
