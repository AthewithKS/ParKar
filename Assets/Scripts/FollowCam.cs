using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform Car;
    public Vector3 offset;
    public float smoothSpeed = 10;
    // Start is called before the first frame update
    
    void LateUpdate()
    {
        // Calculate the desired position based on target position and offset
        Vector3 desiredPosition = Car.position + Car.TransformDirection(offset);
        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Make the camera look at the target
        transform.LookAt(Car);
    }
}
