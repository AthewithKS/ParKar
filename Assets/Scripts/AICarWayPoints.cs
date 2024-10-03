using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AICarWayPoints : MonoBehaviour,ICarObserver
{
    [Header("Opponet car")]
    public AIVelicle aivehicle;
    public WayPoint currentWaypoint;

    public void Awake()
    {
        aivehicle = GetComponent<AIVelicle>();
        aivehicle.DestinationReached += OnDestinationReached;
    }
    private void OnDestroy()
    {
        aivehicle.DestinationReached -= OnDestinationReached;
    }
    private void Start()
    {
        aivehicle.LocateDestination(currentWaypoint.GetPosition());
    }
    public void OnDestinationReached()
    {
        currentWaypoint = currentWaypoint.nextWaypoint;
        aivehicle.LocateDestination(currentWaypoint.GetPosition());
    }
}
public interface ICarObserver
{
    void OnDestinationReached();
}
