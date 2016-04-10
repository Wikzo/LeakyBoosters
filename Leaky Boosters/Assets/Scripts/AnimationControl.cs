using UnityEngine;
using System.Collections;

public class AnimationControl : MonoBehaviour {

	private Animator controller; 

	Rigidbody myBody;
	PlayerMovement myMove;

	float speed;
	bool isCharging = false;
	bool fly = false;

	// Use this for initialization
	void Start () {
		controller = GetComponentInChildren<Animator> ();
		myMove = GetComponent<PlayerMovement> ();
		myBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		speed = Mathf.Abs(myBody.velocity.magnitude);

		if (!isCharging)
			isCharging = myMove.IsCharging;
		else if (isCharging)
			isCharging = false;

		fly = myMove.IsCharging;

		
		controller.SetFloat ("speed", speed);
		controller.SetFloat ("speedMult", Mathf.Clamp (speed, 0f, 1f));
		controller.SetBool ("isCharging", isCharging);
		controller.SetBool ("fly", fly);

	}


}
