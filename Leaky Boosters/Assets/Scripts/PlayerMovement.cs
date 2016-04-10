using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

    //public Renderer cubeRenderer;
	public Color playerColor;

    private ButtonStatus lastButtonState;
    private ButtonStatus currentButtonState;

	private Playerstate playerState;


    public float ChargingAdderPerSecond = 50f;
    public float chargingMultiplier;
    public float MaxCharging = 100;
	private bool isCharging = false;
	public bool IsCharging { get{return isCharging;} set{ isCharging = value; } }

    public float ChargeScaleUpTime = 2f;
    public float ChargeScaleDownTime = 0.5f;
    public Vector3 MaxChargeScale = new Vector3(3,3,3);

	public float hitPower = 10;

	public float jumpPower = 2;

	public SpriteRenderer colorSprite;

    private SpawnZones spawnZones;
    private Transform killZone;

    private AudioSource audioSource;
    public List<AudioClip> ShootSounds;
	public List<AudioClip> hitSounds;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        spawnZones = FindObjectOfType<SpawnZones>();
        killZone = spawnZones.transform;
		colorSprite.color = playerColor;
        //cubeRenderer.material.color = playerColor;

        audioSource = GetComponent<AudioSource>();
		//cubeRenderer.material.color = playerColor;

    }

    void Update()
    {
        // get input
        InputDevice inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
		if (inputDevice == null || SunGUI.gameOver) return;

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
           // cubeRenderer.material.color = Color.red;

            chargingMultiplier += Time.deltaTime * ChargingAdderPerSecond;

            chargingMultiplier = Mathf.Min(chargingMultiplier, MaxCharging);

            transform.localScale = Vector3.Lerp(transform.localScale, MaxChargeScale, Time.deltaTime * ChargeScaleUpTime);

			if (IsCharging && transform.localScale.magnitude >= 3.3f)
				IsCharging = false;

            PlayChargeSound();

        }
        // shooting
        else if (currentButtonState == ButtonStatus.Up && lastButtonState == ButtonStatus.Down)
		{
            // shoot
		    //if (direction != Vector3.zero)
			if (!IsCharging)
				IsCharging = true;
/*			else
				IsCharging = false;*/
			//StartCoroutine (Charging (chargingMultiplier));

		    rigidbody.AddForce(transform.forward*chargingMultiplier, forcemode);
		    // jump
		    /*else
		    {
		        Debug.Log("Jump");
		        //rigidbody.AddForce(Vector3.up * chargingMultiplier * jumpPower, forcemode);
		    }*/

			//print ("im here");

		    chargingMultiplier = 0;

		    StopChargeSound();
		}
        // idle
        else
        {
			//cubeRenderer.material.color = playerColor;

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * ChargeScaleDownTime);

			if (IsCharging && transform.localScale.magnitude <= 3.3f )
			{
				IsCharging = false;
			}

        }

        rigidbody.AddForce(direction, forcemode);

        lastButtonState = currentButtonState;
        /*if (Physics.Raycast(transform.position, Vector3.down, 1))
        {
            playerState = Playerstate.Grounded;
        }
        else
        {
            playerState = Playerstate.Airborne;
        }*/

        //print(isGrounded);
        //print(rigidbody.velocity.y);
        if (rigidbody.velocity.y < -1 && !isGrounded)
        {
            rigidbody.AddForce(Vector3.down*3, ForceMode.Impulse);
            //print("adding velocity down");
        }

        if (transform.position.y < killZone.position.y - 10)
            Respawn();

    }

    private void PlayChargeSound()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();

        audioSource.pitch += Time.deltaTime*0.5f;
        audioSource.pitch = Mathf.Min(audioSource.pitch, 3);
    }

/*	private void PlayHitSound()
	{
		if (!audioSource.isPlaying)
			audioSource.Play();

		audioSource.pitch += Time.deltaTime*0.5f;
		audioSource.pitch = Mathf.Min(audioSource.pitch, 3);
	}*/

    private void StopChargeSound()
    {
        if (!audioSource.isPlaying)
            return;

        audioSource.Stop();
        audioSource.pitch = 1;

        audioSource.PlayOneShot(ShootSounds[Random.Range(0, ShootSounds.Count)]);
    }

	void FixedUpdate(){
		hitOncePrFrame = false;
	}

    private void Respawn()
    {
        transform.position = spawnZones.GetRandomSpawnZone();

        StartCoroutine(BlinkRespawn(6));
    }

    IEnumerator BlinkRespawn(int count)
    {
      //  cubeRenderer.enabled = !cubeRenderer.enabled;
        yield return new WaitForSeconds(0.2f);

        if (count - 1 > 0)
            StartCoroutine(BlinkRespawn(count - 1));
		/* else
            cubeRenderer.enabled = true;*/
    }

    public bool isGrounded
    {
        get
        {
            RaycastHit hit;
            float tresh = 0.2f;
            float height = 1;
            if (Physics.Raycast(transform.position, -transform.up, out hit))
            {
                //Debug.Log(hit.distance);
                if (hit.distance > height / 2 + tresh)
                {
                    return false;
                }
                else return true;
            }
            else return false;
        }
    }
	bool hasBall;
	public void SetHasBall(bool aVal)
	{
		hasBall = aVal;
	}
    bool hitOncePrFrame = false;

	void OnCollisionEnter(Collision collision){
		Rigidbody other = collision.gameObject.GetComponent<Rigidbody>();
		if(other == null || hitOncePrFrame || !other.transform.CompareTag("Player")) return;
		hitOncePrFrame = true;

		float repulsePower = Mathf.Log10(Mathf.Max(1,collision.relativeVelocity.sqrMagnitude - other.velocity.sqrMagnitude));
		other.AddExplosionForce(hitPower * repulsePower, transform.position, 5);

		audioSource.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Count)]);


		if (hasBall && collision.transform.CompareTag("Player"))
		{
			GameController.Instance.GetBallGrabber().LoseFollowers ();
			hasBall = false;
			//print ("Got hit");
		}
	}
/*
	IEnumerator Charging(float timeAmount)
	{
		yield return new WaitForSeconds (timeAmount);
		IsCharging = false;
	}*/
}
