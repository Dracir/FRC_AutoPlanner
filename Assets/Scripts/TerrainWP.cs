using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainWP : MonoBehaviour
{

	public Waypoint Waypoint;

	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.name = Waypoint.Name;
		transform.localPosition = new Vector3(Waypoint.X, Waypoint.Y,0);
	}
}
