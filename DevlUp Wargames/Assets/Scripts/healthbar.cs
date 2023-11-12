using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class healthbar : MonoBehaviour
{

    [SerializeField] Slider healthslider;
    [SerializeField] playerHealth ph;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthslider.value = ph.health / 100f;

    }
}
