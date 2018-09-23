using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : Singleton<TerrainManager> {

	public TerrainConfig TerrainConfig;
	public TerrainWP TerrainWpPrefab;
	public Transform TerrainWpParent;
	
	void Start () {
		WaypointsManager.Instance.OnWaypointAdded += WaypointAdded;
	}

	private void WaypointAdded(Waypoint wp)
	{
		var newWp = GameObject.Instantiate(TerrainWpPrefab.gameObject);
		newWp.transform.parent = TerrainWpParent;
		newWp.transform.localPosition = new Vector3(wp.X,wp.Y);
		
	}

	void Update () {
		
	}
}
