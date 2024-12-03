using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UFOcontroller : MonoBehaviour
{
    void Start() {
        Debug.Log("UFO controller!");
    }
    private void OntriggerEnter(Collider other) {
        
        if (other.CompareTag("Player")) {
            Destroy(gameObject);
            Debug.Log("UFO check trigger");
            SceneManager.LoadScene(2);
            
        }
    }
}
