using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other) {
        Debug.Log(other.tag); 
        this.gameObject.GetComponent<firstPersonCamera>().orientation.transform.parent.GetComponent<playerHealth>().RestoreHealth();
    }
}
