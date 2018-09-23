using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waypoint
{
    public string Name;
	public float X;
	public float Y;
	public float StartAngle;
	public float StartSpeed;
	public float EndAngle;
	public float EndSpeed;

	public Waypoint(float x, float y, string name, float startAngle, float startSpeed, float endAngle, float endSpeed)
	{
        Name = name;
		X = x;
		Y = y;
		StartAngle = startAngle;
		StartSpeed = startSpeed;
		EndAngle = endAngle;
		EndSpeed = endSpeed;
	}
}
