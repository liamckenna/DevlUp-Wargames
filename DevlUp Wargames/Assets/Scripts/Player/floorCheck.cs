using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorCheck : MonoBehaviour
{
    public playerMovement pm;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Geometry") {
            pm.grounded = true;
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Geometry") {
            pm.grounded = true;          
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Geometry") {
            pm.grounded = false;
            Debug.Log("Exit");
        }
    }
}
