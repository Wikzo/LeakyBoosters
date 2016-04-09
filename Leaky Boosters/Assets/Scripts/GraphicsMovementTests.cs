using UnityEngine;
using System.Collections;

public class GraphicsMovementTests : MonoBehaviour {

    //Public vars
    [Range(0f, 200f)]
    public float speed = 10f;
    public GameObject shockWave;

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

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject shock = Instantiate(shockWave, transform.position - Vector3.up * 0.4f, Quaternion.identity) as GameObject;
            shock.transform.localScale = new Vector3(3f, 3f, 3f);
        }

        body.AddForce(input * speed);
    }
}
