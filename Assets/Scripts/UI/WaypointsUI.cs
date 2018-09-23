using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointsUI : Singleton<WaypointsUI>
{
	public RectTransform Waypoints;
	public WaypointLineUI WaypointPrefab;

	void Start()
	{
		WaypointsManager.Instance.OnWaypointAdded += WaypointAdded;
	}


	void Update()
	{

	}

	void WaypointAdded(Waypoint waypoint)
	{
		var wpUI = GameObject.Instantiate(WaypointPrefab);
        wpUI.transform.SetParent(Waypoints, false);
        wpUI.Waypoint = waypoint;
	}
}
