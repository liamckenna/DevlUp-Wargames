using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraHolder : MonoBehaviour
{

    public Transform cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(cameraPosition.position.x, -cameraPosition.position.y, cameraPosition.position.z);
    }
}
