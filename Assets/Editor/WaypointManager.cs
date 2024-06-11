using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEditor;

public class WaypointManager : EditorWindow
{
    [MenuItem("WayPoint/WayPoints Editor Tools")]
    public static void ShowWindow()
    {
        GetWindow<WaypointManager>("Waypoints Editor Tools");
    }
    public Transform wayPointOrgin;
    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("wayPointOrgin"));

        if (wayPointOrgin == null) 
        { 
            EditorGUILayout.HelpBox("Please assign a waypoint orgin Transforom", MessageType.Warning); 
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            CreateButtons();
            EditorGUILayout.EndVertical();
        }
        obj.ApplyModifiedProperties();
    }
    void CreateButtons()
    {
        if(GUILayout.Button("Create Waypoint"))
        {
            CreateWayPoint();
        }
    }
    void CreateWayPoint()
    {
        GameObject waypointObject = new GameObject("Waypoint" + wayPointOrgin.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(wayPointOrgin,false);

        WayPoint waypoint = waypointObject.GetComponent<WayPoint>();

        if (wayPointOrgin.childCount > 1)
        {
            waypoint.previousWaypoint = wayPointOrgin.GetChild(wayPointOrgin.childCount - 2).GetComponent<WayPoint>();
            waypoint.previousWaypoint.nextWaypoint = waypoint;

            waypoint.transform.position = waypoint.previousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.previousWaypoint.transform.forward;
        }
        Selection.activeGameObject = waypoint.gameObject;
    }
}