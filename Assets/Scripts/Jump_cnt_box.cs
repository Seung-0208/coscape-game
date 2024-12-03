using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Jump_cnt_box : MonoBehaviour
{
    int cnt;
    public Button button1;
    public Button button2;

    void Start() {
        cnt = GameObject.Find("Player").GetComponent<PlayController>().JumpCnt;

    }

    void Update()
    {
        cnt = GameObject.Find("Player").GetComponent<PlayController>().JumpCnt;
        if (cnt == 0) {
            if (button1.image.sprite !=null )
            GameObject.Find("box_cnt_1").SetActive(false);
            if (button2.image.sprite !=null)
            GameObject.Find("cnt_2").SetActive(false);
        }
        else if (cnt == 1) {
            if (GameObject.Find("cnt_2").activeSelf)
            GameObject.Find("cnt_2").SetActive(false);
        }
        else if (cnt == 2) {
            if (GameObject.Find("box_cnt_1").activeSelf)
            GameObject.Find("box_cnt_1").SetActive(false);
            GameObject.Find("cnt_2").SetActive(true);
        }
    }
}
