using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaypointsManager : Singleton<WaypointsManager>
{

	private List<Waypoint> Waypoints = new List<Waypoint>();

	public Action<Waypoint> OnWaypointAdded;

	public void AddPoint(Waypoint newWaypoint)
	{
		Waypoints.Add(newWaypoint);
		OnWaypointAdded?.Invoke(newWaypoint);
	}

	public int WaypointsCount { get { return Waypoints.Count; } }


	// Start is called before the first frame update
	void Start()
	{
		var startP = TerrainManager.Instance.TerrainConfig.StartPositions[0];
		var startWp = new Waypoint(startP.X,startP.Y,startP.Name,0,0,0,0);
		AddPoint(startWp);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
