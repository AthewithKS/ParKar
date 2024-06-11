using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AICarWayPoints : MonoBehaviour
{
    [Header("Opponet car")]
    public AIVelicle aivehicle;
    public WayPoint currentWaypoint;

    public void Awake()
    {
        aivehicle = GetComponent<AIVelicle>();
    }
    private void Start()
    {
        aivehicle.LocateDestination(currentWaypoint.GetPosition());
    }
    private void Update()
    {
        if (aivehicle.destinationReached)
        {
            currentWaypoint = currentWaypoint.nextWaypoint;
            aivehicle.LocateDestination(currentWaypoint.GetPosition());
        }
    }
}
