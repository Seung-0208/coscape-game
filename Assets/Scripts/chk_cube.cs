using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class chk_cube : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Cube")) {
            if (transform.position.y>other.transform.position.y)
                SceneManager.LoadScene(0);
        }
    }
}
