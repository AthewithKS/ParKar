using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    [Header("Vehicle Prefabs")]
    public List<GameObject> vehiclePrefabs;

    [Header("Waypoints Parent")]
    public Transform waypointsParent;

    [Header("Spawning Settings")]
    public int numberOfVehiclesToSpawn = 5;

    private List<WayPoint> waypoints;

    private void Start()
    {
        // Initialize waypoints list
        waypoints = new List<WayPoint>();

        // Find all waypoints that are children of the specified parent
        foreach (Transform child in waypointsParent)
        {
            WayPoint waypoint = child.GetComponent<WayPoint>();
            if (waypoint != null)
            {
                waypoints.Add(waypoint);
            }
        }

        if (waypoints.Count == 0)
        {
            Debug.LogWarning("No waypoints found under the specified parent.");
            return;
        }

        StartCoroutine(SpawnVehicles());
    }

    private IEnumerator SpawnVehicles()
    {
        for (int i = 0; i < numberOfVehiclesToSpawn; i++)
        {
            SpawnRandomVehicle();
            yield return new WaitForSeconds(1f); // Optional: delay between spawns
        }
    }

    private void SpawnRandomVehicle()
    {
        if (vehiclePrefabs.Count == 0 || waypoints.Count == 0)
        {
            Debug.LogWarning("No vehicle prefabs or waypoints assigned.");
            return;
        }

        // Select a random vehicle prefab
        int vehicleIndex = Random.Range(0, vehiclePrefabs.Count);
        GameObject vehiclePrefab = vehiclePrefabs[vehicleIndex];

        // Select a random waypoint
        int waypointIndex = Random.Range(0, waypoints.Count);
        WayPoint spawnWaypoint = waypoints[waypointIndex];

        // Calculate the rotation based on the direction to the next waypoint
        Quaternion spawnRotation;
        if (spawnWaypoint.nextWaypoint != null)
        {
            Vector3 directionToNext = (spawnWaypoint.nextWaypoint.transform.position - spawnWaypoint.transform.position).normalized;
            spawnRotation = Quaternion.LookRotation(directionToNext);
        }
        else
        {
            // If there's no next waypoint, use the waypoint's current rotation
            spawnRotation = spawnWaypoint.transform.rotation;
        }

        // Instantiate the vehicle at the waypoint position with the calculated rotation
        GameObject vehicleInstance = Instantiate(vehiclePrefab, spawnWaypoint.transform.position, spawnRotation);

        // Set the current waypoint for the AI to start from
        AICarWayPoints aiCarWayPoints = vehicleInstance.GetComponent<AICarWayPoints>();
        aiCarWayPoints.currentWaypoint = spawnWaypoint;
    }
}
