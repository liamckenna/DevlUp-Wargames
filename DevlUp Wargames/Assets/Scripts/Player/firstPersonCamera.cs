using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPersonCamera : MonoBehaviour
{
    

    public float xSens = 600;
    public float ySens = 600;
    
    [SerializeField] playerMovement pm;

    public Transform orientation;

    public Transform holder;

    private float xRotation;
    private float yRotation;

    public bool canLook = true;
    public bool introAnim = false;

    static float t = 0.0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        xRotation = -50;
        yRotation = -90;
        StartCoroutine(IntroAnim());
    }

    
    void Update()
    {
        if (canLook) {
            float xMouse = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSens;
            float yMouse = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySens;

            yRotation += xMouse;
            xRotation -= yMouse;
            xRotation = Mathf.Clamp(xRotation, -66, 90f);

            
        } else if (introAnim) {
            xRotation = Mathf.Lerp(-60, 0, Mathf.SmoothStep(0, 1, t));
            t += 0.33f * Time.deltaTime;
            Debug.Log(t);
        }
        holder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            //Debug.Log(holder.rotation);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }

    public IEnumerator IntroAnim() {
        canLook = false;
        pm.canMove = false;
        yield return new WaitForSeconds(0.1f);
        xRotation = -50;
        yRotation = -90;
        yield return new WaitForSeconds(0.1f);
        introAnim = true;
        yield return new WaitForSeconds(3f);
        introAnim = false;
        canLook = true;
        pm.canMove = true;
        t = 0;
        

    }
}
