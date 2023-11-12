using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class backgroundmusic : MonoBehaviour
{
    public static backgroundmusic instance;
 
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}