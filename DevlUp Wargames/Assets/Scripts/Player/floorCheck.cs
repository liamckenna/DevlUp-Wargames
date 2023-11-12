using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorCheck : MonoBehaviour
{
    public playerMovement pm;

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Geometry") {
            pm.grounded = true;
        }
    }

    public void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Geometry") {
            pm.grounded = true;          
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Geometry") {
            pm.grounded = false;
            //Debug.Log("Exit");
        }
    }
}
