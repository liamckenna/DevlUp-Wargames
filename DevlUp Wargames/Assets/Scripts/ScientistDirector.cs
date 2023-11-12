using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistDirector : MonoBehaviour
{

    [SerializeField] public GameManager gm;
    [SerializeField] public bool left;
    [SerializeField] public bool right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other) {
        Debug.Log("fic");
        if (other.gameObject.tag == "Scientist") {
            ScientistBehavior sb = other.transform.parent.gameObject.GetComponent<ScientistBehavior>();
            gm.TurnScientists(left, right, sb);
            Debug.Log("Scientist hall number: " + gm.scientistHallNumber + ", Hall Number: " + gm.hallNumber);
            Destroy(this.gameObject);
            
        }
    }
}
