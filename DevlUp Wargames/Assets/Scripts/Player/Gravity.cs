using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravity = -30;
    playerMovement pm;
    Rigidbody rb;
    void Start()
    {
        pm = this.gameObject.GetComponent<playerMovement>();
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    

    public void Grav() {
        Vector3 gravityVector = gravity * Vector3.up;
        rb.AddForce(gravityVector);
    }
}
