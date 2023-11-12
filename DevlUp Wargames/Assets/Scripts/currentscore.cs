using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class currentscore : MonoBehaviour
{
    TMP_Text tmp;
    // Start is called before the first frame update
    void Start()
    {
        TMP_Text tmp = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //tmp.text = "Hallways Passed: " + GameManager.score;
    }
}
