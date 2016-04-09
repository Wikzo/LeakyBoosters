using UnityEngine;
using System.Collections;

public class GraphicsMovementTests : MonoBehaviour {

    //Public vars
    [Range(0f, 200f)]
    public float speed = 10f;

    //Private vars
    Rigidbody body;

	void Start () {
        body = GetComponent<Rigidbody>();
    }
	
	void Update () {
	    
	}

    void FixedUpdate()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if(input.sqrMagnitude > 1f)
        {
            input.Normalize();
        }

        body.AddForce(input * speed);
    }
}
