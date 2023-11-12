using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class playerHealth : MonoBehaviour
{
    public int health = 100;

    public GameManager gm;

    [SerializeField] Camera cam;
    PostProcessVolume volume;
    public float poisonRate = 1;
    void Start()
    {
        StartCoroutine(DecreaseHealth());
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 1) {
            gm.EndGame();
        }
    }

    public void RestoreHealth() {
        health = 100;
    }

    public IEnumerator DecreaseHealth() {
        yield return new WaitForSeconds(poisonRate);
        if (health > 0) {
            health--;
            Debug.Log("Health: " + health);
            StartCoroutine(DecreaseHealth());
        }
        
    }
}
