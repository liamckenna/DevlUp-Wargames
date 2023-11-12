using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scoredisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        TMP_Text tmp = GetComponent<TMP_Text>();
        tmp.text = "You Survived " + GameManager.score + " Hallways...";
    }
}
