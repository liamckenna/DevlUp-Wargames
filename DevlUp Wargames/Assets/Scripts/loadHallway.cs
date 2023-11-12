using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class loadHallway : MonoBehaviour
{
    [SerializeField] public GameManager gm;

    public bool used = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && !used) {
            if (transform.parent.gameObject == gm.halls[0] || transform.parent.gameObject == gm.halls[1]) {
                Debug.Log("Skipping...");
            } else {
                gm.GenerateNewHall();
            }
            used = true;
        }
        if (other.gameObject.tag == "Scientist") {
            gm.scientistHallNumber++;
            Destroy(this.gameObject);
        }
        
    }
}
