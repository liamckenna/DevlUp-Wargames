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
        gm.GenerateNewHall();
        Destroy(this.gameObject);
    }
}
