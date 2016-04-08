using UnityEngine;
using System.Collections;
using InControl;

public class PlayerMovement : MonoBehaviour {

	public int playerNum;
	public float acceleration = 1;
	public ForceMode forcemode;
	Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{
		InputDevice inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
		if (inputDevice == null) return;
		rigidbody.AddForce(new Vector3(acceleration * Time.deltaTime * inputDevice.LeftStickX, 0, acceleration * Time.deltaTime * inputDevice.LeftStickY), forcemode);

	}
}
