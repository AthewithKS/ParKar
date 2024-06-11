using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class WaypointEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmos(WayPoint waypoint,GizmoType gizmoType)
    {
        if((gizmoType & GizmoType.Selected)!= 0)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.green * 0.5f;
        }
        Gizmos.DrawSphere(waypoint.transform.position, .5f);
        Gizmos.color = Color.white;

        Gizmos.DrawLine(waypoint.transform.position + (waypoint.transform.right * waypoint.wayPointWidth / 2f), waypoint.transform.position - (waypoint.transform.right * waypoint.wayPointWidth / 2f));
        
        if(waypoint.previousWaypoint != null)
        {
            Gizmos.color = Color.red;
            Vector3 offset = waypoint.transform.right * waypoint.wayPointWidth / 2f;
            Vector3 offset2 = waypoint.previousWaypoint.transform.right * waypoint.previousWaypoint.wayPointWidth / 2f;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.previousWaypoint.transform.position + offset2);
        }
        if(waypoint.nextWaypoint != null)
        {
            Gizmos.color = Color.blue;
            Vector3 offset = waypoint.transform.right * -waypoint.wayPointWidth / 2f;
            Vector3 offset2 = waypoint.previousWaypoint.transform.right * -waypoint.previousWaypoint.wayPointWidth / 2f;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.previousWaypoint.transform.position + offset2);

        }
    }
}
