using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraEssentialPoint : MonoBehaviour
{

	void Start() => CameraInterestComponent.Main.AddPoint(this);

	void OnDrawGizmos()
	{
		var main = CameraInterestComponent.Main;
		var from = main.CurrentPosition;
		var to = transform.position;
		
		Handles.color = new Color(1, 0.1f, 0);
		Handles.DrawLine(from, to);
		Gizmos.color = new Color(1, 0.1f, 0);
		Gizmos.DrawSphere(to, 0.2f);
	}
}
