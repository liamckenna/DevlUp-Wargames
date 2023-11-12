using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public int stage = 1;

    [SerializeField] GameObject startingHall1;
    [SerializeField] GameObject startingHall2;
    [SerializeField] GameObject startingHall3;
    [SerializeField] GameObject startingHall4;
    [SerializeField] GameObject startingHall5;
    public List<GameObject> halls;
    
    
    void Start()
    {
        halls = new List<GameObject>(5);
    }

    
    void Update()
    {
        
    }
}
