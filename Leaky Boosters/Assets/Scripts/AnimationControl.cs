using UnityEngine;
using System.Collections;

public class AnimationControl : MonoBehaviour {

	private Animator controller; 

	Rigidbody myBody;
	float speed;

	// Use this for initialization
	void Start () {
		controller = GetComponentInChildren<Animator> ();
		myBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		speed = Mathf.Abs(myBody.velocity.magnitude);

		controller.SetFloat ("speed", speed);
		controller.SetFloat ("speedMult", Mathf.Clamp (speed, 0f, 1f));
	}
}
