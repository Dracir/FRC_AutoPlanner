using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraInterestPoint : MonoBehaviour
{
	public float DisplacementForce = 0.6f;
	public float InterestDistance = 12;

	void Start() => CameraInterestComponent.Main.AddPoint(this);

	void OnDrawGizmos()
	{
		var main = CameraInterestComponent.Main;
		var from = main.CurrentPosition;
		var to = transform.position;
		if(Vector3.Distance(from,to) > InterestDistance) return;
		
		Handles.color = new Color(1, 0.5f, 0);
		Handles.DrawDottedLine(from, to, 2);
		Gizmos.color = new Color(1, 0.7f, 0.2f);
		Gizmos.DrawSphere(to,0.2f);

		var influencePoint = (to - from) * DisplacementForce + from;
		Handles.DrawLine(from, influencePoint);
	}
}

