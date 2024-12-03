using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class check_ufo_second : MonoBehaviour
{
    private Transform childTransform;
    void Start()
    {
        childTransform = transform.Find("ufo");
    }

    // Update is called once per frame
    void Update()
    {
        if(childTransform==null) {
            Invoke("ChangeScene",1f);
        }
    }
    private void ChangeScene() {
        SceneManager.LoadScene(3);
    }
}
