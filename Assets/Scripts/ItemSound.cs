using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSound : MonoBehaviour
{
    public AudioClip soundEffect;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            audioSource.Play();  // 아이템 먹는 소리 재생
        }
    }
}
