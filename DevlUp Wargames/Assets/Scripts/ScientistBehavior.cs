using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistBehavior : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] Vector3 startingPosition;
    [SerializeField] Quaternion startingRotation;
    [SerializeField] public direction scientistDirection = direction.North;

    public enum direction {
        North = 0,
        East = 90,
        South = 180,
        West = 270
    }
    void Start()
    {
        this.transform.position = startingPosition;
        this.transform.rotation = startingRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (scientistDirection == direction.North) {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x + (speed / 400), this.transform.localPosition.y, this.transform.localPosition.z);
        } else if (scientistDirection == direction.East) {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z - (speed / 400));
        } else if (scientistDirection == direction.South) {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x - (speed / 400), this.transform.localPosition.y, this.transform.localPosition.z);
        } else if (scientistDirection == direction.West) {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z + (speed / 400));
        } 
    }
}
