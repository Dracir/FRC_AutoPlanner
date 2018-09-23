using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointLineUI : MonoBehaviour
{
	public Button SelectButton;
	public Text Name;

	private Waypoint WaypointValue;
	public Waypoint Waypoint
	{
		set
		{
			WaypointValue = value;
            UpdateUI();
		}
		get { return WaypointValue; }
	}

	private void UpdateUI()
	{
        Name.text = Waypoint.Name;
        transform.name = Waypoint.Name;
	}
}
