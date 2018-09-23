using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
	// Start is called before the first frame update
	void Start()
	{
        
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			var name = "Point " + WaypointsManager.Instance.WaypointsCount;
			var wp = new Waypoint(10, 10, name, 0, 0, 0, 0);
			WaypointsManager.Instance.AddPoint(wp);

		}

	}
}
