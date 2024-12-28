using System;
using UnityEngine;
using UnityEngine.UI;

public enum Axel
{
    Front,
    Rear
}
public enum Drive
{
    Front,
    Rear,
    All
}

[Serializable]
public struct Wheel
{
    public GameObject model;
    public WheelCollider wheelCollider;
    public Axel axel;
}
public class CarController : MonoBehaviour
{
    //speed
    public Text speedText;

    public GameObject breakLight;
    public GameObject HeadLight;
    private bool isHeadlightOn = false;

    public Rigidbody rb;
    public Drive drive;
    public float torque;
    public float braketorque;
    public Wheel[] wheels;
    public Vector3 cm;

    public float turnSence = 1;
    public float maxSteeringAngle = 35;

    public float throttle;
    public float brake;
    public float InputX;

    private bool isEngineRunning = false;
    float minPitch = 1f;
    float maxPitch = 2f;
    void Start()
    {
        rb.centerOfMass = cm; 
    }

    // Update is called once per frame
    void Update()
    {
        //head light
        if (Input.GetKeyDown(KeyCode.L))
        {
            //toggle state
            isHeadlightOn=!isHeadlightOn;

            HeadLight.SetActive(isHeadlightOn);
        }
        
        //break light
        if (brake > 0)
        {
            breakLight.SetActive(true);
        }
        else breakLight.SetActive(false);

        UpdateSpeedText();
        HandleEngineSounds();
    }
    // FixedUpdate is called at a fixed amout of times per secod(50)
    private void FixedUpdate()
    {
        AnimateWheels();
        Move();
        Steering();
        SetInput(Input.GetAxis("Throttle"), Input.GetAxis("Brake"),Input.GetAxis("Horizontal"));

        
    }
    public void LateUpdate()
    {
        
    }
    public void SetInput(float t, float b, float s)
    { 
        throttle = t;
        brake = b;
        InputX = s;
    }
    public void Move()
    {
        foreach(var wheel in wheels)
        {
            if(drive == Drive.All)
                wheel.wheelCollider.motorTorque = torque * throttle;
            if(drive == Drive.Rear)
            {   
                if(wheel.axel==Axel.Rear)
                    wheel.wheelCollider.motorTorque = torque * throttle;
            }
            if (drive == Drive.Front)
            {
                if(wheel.axel == Axel.Front)
                {
                    wheel.wheelCollider.motorTorque = torque * throttle;
                }
            }
            if (wheel.axel == Axel.Rear)
            {
                wheel.wheelCollider.brakeTorque = braketorque * brake;
            }
            else
            {
                wheel.wheelCollider.brakeTorque = 0; // Ensure front wheels have no brake torque
            }
        }
        
    }
    public void Steering()
    {
        foreach(var wheel in wheels)
        {
            if(wheel.axel == Axel.Front)
            {
                float streerAngle = InputX * turnSence * maxSteeringAngle;
                wheel.wheelCollider.steerAngle = streerAngle;
            }
        }
    }
    public void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot ;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.model.transform.position = pos;
            wheel.model.transform.rotation = rot;
        }
    }
    private void UpdateSpeedText()
    {
        // Calculate the speed in km/h
        float speed = rb.velocity.magnitude * 3.6f;
        // Update the UI Text component
        speedText.text =speed.ToString("F2") + " km/h";
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        SoundManager.PlaySound(SoundType.VehicleHit);
    }
    private void HandleEngineSounds()
    {
        float speed = rb.velocity.magnitude * 3.6f; // Speed in km/h

        // Start engine sound if throttle is pressed and engine is not already running
        if(!isEngineRunning)
        {
            isEngineRunning = true;
            SoundManager.PlayLoopingSound(SoundType.EnginIdel); // Start with engine idle sound
        }
        float pitch = Mathf.Lerp(minPitch, maxPitch, speed / 100f);
        SoundManager.SetPitch(pitch);
    }
}
