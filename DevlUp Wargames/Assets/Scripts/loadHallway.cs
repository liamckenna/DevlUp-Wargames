using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class loadHallway : MonoBehaviour
{
    [SerializeField] public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (transform.parent.gameObject == gm.halls[0] || transform.parent.gameObject == gm.halls[1]) {
            Debug.Log("Skipping...");
        } else {
            gm.GenerateNewHall();
        }
        Destroy(this.gameObject);
    }
}
