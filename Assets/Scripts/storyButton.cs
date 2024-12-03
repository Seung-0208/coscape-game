using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class storyButton : MonoBehaviour
{
    /* private Button myButton;

    void Start() {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(MyFunction);
    } */

    public void MyFunction() {
        SceneManager.LoadScene(5);
    }


}
