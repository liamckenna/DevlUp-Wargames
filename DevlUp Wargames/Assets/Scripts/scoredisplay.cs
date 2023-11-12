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
        TMP_Text tmp = score.GetComponent<TMP_Text>();
        tmp.text = "You Survived " + GameManager.score + " Hallways...";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
