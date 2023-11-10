using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPersonCamera : MonoBehaviour
{
    

    public float xSens = 400;
    public float ySens = 400;

    public Transform orientation;

    public Transform holder;

    private float xRotation;
    private float yRotation;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        float xMouse = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSens;
        float yMouse = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySens;
    
        yRotation += xMouse;
        xRotation -= yMouse;
        xRotation = Mathf.Clamp(xRotation, -66, 90f);

        holder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Debug.Log(holder.rotation);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        
    }
}
