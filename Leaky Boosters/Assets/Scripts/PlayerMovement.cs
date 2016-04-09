using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.SceneManagement;

public enum ButtonStatus
{
    Down,
    Up
}

public enum Playerstate
{
	Grounded,
	Airborne
}

public class PlayerMovement : MonoBehaviour
{

    public int playerNum;
    public float acceleration = 1;
    public ForceMode forcemode;
    Rigidbody rigidbody;

    public Renderer cubeRenderer;
	public Color playerColor;

    private ButtonStatus lastButtonState;
    private ButtonStatus currentButtonState;

	private Playerstate playerState;

    public float ChargingAdderPerSecond = 50f;
    public float chargingMultiplier;
    public float MaxCharging = 100;

    public float ChargeScaleUpTime = 2f;
    public float ChargeScaleDownTime = 0.5f;
    public Vector3 MaxChargeScale = new Vector3(3,3,3);

	public float hitPower = 10;

	public float jumpPower = 2;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

    }

    void Update()
    {
        // get input
        InputDevice inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
        if (inputDevice == null) return;

        // set button state
        currentButtonState = inputDevice.Action1 ? ButtonStatus.Down : ButtonStatus.Up;

        // get direction
        Vector3 direction = new Vector3(acceleration * Time.deltaTime * inputDevice.LeftStickX, 0,
            acceleration * Time.deltaTime * inputDevice.LeftStickY);

		if (direction != Vector3.zero){
			transform.LookAt(transform.position + direction);
		} else {
			//transform.LookAt(transform.position + Vector3.up);
		}

        // charging
        if (currentButtonState == ButtonStatus.Down)
        {
            cubeRenderer.material.color = Color.red;

            chargingMultiplier += Time.deltaTime * ChargingAdderPerSecond;

            chargingMultiplier = Mathf.Min(chargingMultiplier, MaxCharging);

            transform.localScale = Vector3.Lerp(transform.localScale, MaxChargeScale, Time.deltaTime * ChargeScaleUpTime);

        }
        // shooting
        else if (currentButtonState == ButtonStatus.Up && lastButtonState == ButtonStatus.Down)
		{
            // shoot
            if (direction != Vector3.zero)
                rigidbody.AddForce(transform.forward * chargingMultiplier, forcemode);
            // jump
			else{
				Debug.Log("Jump");
				//rigidbody.AddForce(Vector3.up * chargingMultiplier * jumpPower, forcemode);
			}


			chargingMultiplier = 0;
        }
        // idle
        else
        {
			cubeRenderer.material.color = playerColor;

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * ChargeScaleDownTime);

        }

        rigidbody.AddForce(direction, forcemode);

        lastButtonState = currentButtonState;
//		if(Physics.Raycast(transform.position,Vector3.down, 1)){
//			playerState = Playerstate.Grounded;
//		} else {
//			playerState = Playerstate.Airborne;
//		}
//		if(rigidbody.velocity.y < 0 && playerState != Playerstate.Grounded){
//			rigidbody.AddForce(Vector3.down * 100);
//		}

    }

	void FixedUpdate(){
		hitOncePrFrame = false;
	}

	bool hitOncePrFrame = false;
	void OnCollisionEnter(Collision collision){
		Rigidbody other = collision.gameObject.GetComponent<Rigidbody>();
		if(other == null || hitOncePrFrame || !other.transform.CompareTag("Player")) return;
		hitOncePrFrame = true;

		float repulsePower = Mathf.Log10(Mathf.Max(1,collision.relativeVelocity.sqrMagnitude - other.velocity.sqrMagnitude));
		other.AddExplosionForce(hitPower * repulsePower, transform.position, 5);
	}
}
