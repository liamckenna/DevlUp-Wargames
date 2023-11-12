using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class firstPersonCamera : MonoBehaviour
{
    

    public float xSens = 600;
    public float ySens = 600;
    
    [SerializeField] playerMovement pm;

    public Transform orientation;

    public Transform holder;

    private float xRotation;
    private float yRotation;

    public Vector3 ogPosition;
    public Quaternion ogRotation;

    public bool canLook = true;
    public bool introAnim = false;
    public bool deathAnim = false;
    public bool deathAnim2 = false;
    float z = 0;

    static float t = 0.0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        xRotation = -50;
        yRotation = -90;
        StartCoroutine(IntroAnim());
        this.transform.localPosition = ogPosition;
        this.transform.localRotation = ogRotation;

    }

    
    void Update()
    {
        if (canLook) {
            z = 0;
            t = 0;
            float xMouse = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSens;
            float yMouse = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySens;

            yRotation += xMouse;
            xRotation -= yMouse;
            xRotation = Mathf.Clamp(xRotation, -66, 90f);

            
        } else if (introAnim) {
            z = 0;
            xRotation = Mathf.Lerp(-60, 0, Mathf.SmoothStep(0, 1, t));
            t += 0.33f * Time.deltaTime;
            Debug.Log(t);
        } else if (deathAnim) {
            this.transform.localPosition = new Vector3(0,Mathf.Lerp(1.3f, -0.15f, t), 0);
            this.transform.localRotation = Quaternion.Euler(this.transform.localRotation.x, this.transform.localRotation.y, Mathf.Lerp(0, 90, t));
            t += Time.deltaTime;
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
    public IEnumerator DeathAnim() {
        canLook = false;
        pm.canMove = false;
        deathAnim = true;
        yield return new WaitForSeconds(1f);
        deathAnim = false;
        t = 0;
        SceneManager.LoadScene("gameover");
    }
}
