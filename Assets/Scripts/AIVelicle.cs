using System;
using UnityEngine;

public class AIVelicle : MonoBehaviour
{
    [Header("Lights")]
    public GameObject HeadLights;
    public GameObject BreakLights;

    [Header("Car Engine")]
    public float realvehiclespeed = 5f;
    public float movingSpeed ;
    public float turingSpeed = 50f;
    public float breakSpeed = 5f;

    [Header("Distination Var")]
    public Vector3 destination;
    public bool destinationReached;

    [Header("Raycast Var")]
    public float detectionDistance = 20f;
    public float safeDistance = 10f;

    public float destinationDirection { get; private set; }

    //when destination reached
    public event Action DestinationReached;
    private void Update()
    {
        RaycastVehicleHit();
        Drive();
        BreakLightsOnandOff();
    }
    public void RaycastVehicleHit()
    {
        RaycastHit hit;
        Vector3 raycastOrgin = transform.position + Vector3.up * 1.0f;
        if (Physics.Raycast(raycastOrgin, transform.TransformDirection(Vector3.forward), out hit, detectionDistance))
        {
            if(hit.distance<safeDistance && hit.collider.CompareTag("Traffic"))
            {
               movingSpeed = Mathf.Lerp(movingSpeed, 0, Time.deltaTime * 2);
            }
        }
        else
        {
            movingSpeed = Mathf.Lerp(movingSpeed,realvehiclespeed,Time.deltaTime * 2);
        }
    }
    public void Drive()
    {
        if(transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;
            float DestinationDistance = destinationDirection.magnitude;

            if (DestinationDistance >= breakSpeed)
            {
                //steering
                destinationReached = false;
                Quaternion targetRotaion = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotaion, turingSpeed * Time.deltaTime);

                //move vehicle
                transform.Translate(Vector3.forward * movingSpeed * Time.deltaTime);
            }
             else if(DestinationDistance <breakSpeed && !destinationReached)
            {
                destinationReached = true;
                DestinationReached?.Invoke();
            }
        }
    }
    void BreakLightsOnandOff()
    {
        if (movingSpeed < realvehiclespeed-1f)
        {
            BreakLights.SetActive(true);
        }
        else
        {
            BreakLights.SetActive(false);
        }
    }
    public void LocateDestination(Vector3 destination)
    {
        this.destination = destination;
        destinationReached = false;
    }
}
