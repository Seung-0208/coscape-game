using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReButton : MonoBehaviour
{
    /* private Button myButton;

    void Start() {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(MyFunction);
    } */

    public void MyFunction() {
        SceneManager.LoadScene(0);
    }


}
