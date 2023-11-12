using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class currentscore : MonoBehaviour
{
    [SerializeField] Game Manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = "Hallways Passed: " + score;
    }
}
