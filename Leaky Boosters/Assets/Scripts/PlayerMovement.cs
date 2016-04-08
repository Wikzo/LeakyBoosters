using UnityEngine;
using System.Collections;
using InControl;

public enum ButtonStatus
{
    Down,
    Up
}

public class PlayerMovement : MonoBehaviour
{

    public int playerNum;
    public float acceleration = 1;
    public ForceMode forcemode;
    Rigidbody rigidbody;

    private Renderer cubeRenderer;


    private ButtonStatus lastButtonState;
    private ButtonStatus currentButtonState;

    public float ChargingAdderPerSecond = 50f;
    public float chargingMultiplier;
    public float MaxCharging = 100;

    public float ChargeScaleUpTime = 2f;
    public float ChargeScaleDownTime = 0.5f;
    public Vector3 MaxChargeScale = new Vector3(3,3,3);

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        cubeRenderer = GetComponent<Renderer>();

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

        if (direction != Vector3.zero)
            transform.forward = direction;
        else
            transform.forward = Vector3.up;

        // charging
        if (currentButtonState == ButtonStatus.Down)
        {
            cubeRenderer.material.color = Color.red;

            chargingMultiplier += Time.deltaTime * ChargingAdderPerSecond;

            chargingMultiplier = Mathf.Min(chargingMultiplier, MaxCharging);

            transform.localScale = Vector3.Lerp(transform.localScale, MaxChargeScale, Time.deltaTime* ChargeScaleUpTime);

        }
        // shooting
        else if (currentButtonState == ButtonStatus.Up && lastButtonState == ButtonStatus.Down)
        {
            // shoot
            if (direction != Vector3.zero)
                rigidbody.AddForce(transform.forward * chargingMultiplier, forcemode);
            // jump
            else
                rigidbody.AddForce(transform.up * chargingMultiplier * 0.5f, forcemode);

            chargingMultiplier = 0;

        }
        // idle
        else
        {
            cubeRenderer.material.color = Color.green;

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * ChargeScaleDownTime);

        }



        

        rigidbody.AddForce(direction, forcemode);

        lastButtonState = currentButtonState;

    }
}
