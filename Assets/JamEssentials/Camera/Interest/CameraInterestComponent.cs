using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraInterestComponent : CameraMoverBase
{
	public static CameraInterestComponent Main { get { return CameraMotions.Main.GetComponentInChildren<CameraInterestComponent>(); } }

	private List<CameraInterestPoint> InterestPoints = new List<CameraInterestPoint>();
	public void AddPoint(CameraInterestPoint point) => InterestPoints.Add(point);
	public void RemovePoint(CameraInterestPoint point) => InterestPoints.Remove(point);
	public void ClearInterestPoints() => InterestPoints.Clear();

	private List<CameraEssentialPoint> EssentialPoints = new List<CameraEssentialPoint>();
	public void AddPoint(CameraEssentialPoint point) => EssentialPoints.Add(point);
	public void RemovePoint(CameraEssentialPoint point) => EssentialPoints.Remove(point);
	public void ClearEssentialPoints() => EssentialPoints.Clear();


	public CameraInterest CameraMotion;

	public Vector3 EssentialsCenter { private set; get; }
	public Vector3 InterestDisplacement { private set; get; }
	public Vector3 TargetDestination { private set; get; }
	public Vector3 CurrentPosition { private set; get; }

	public bool IsMoving { private set; get; }


	void OnDrawGizmos()
	{
		if (Application.isEditor)
		{
			CurrentPosition = GetTargetPosition();
		}
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(CurrentPosition, new Vector3(CameraMotion.FrameSize.x, CameraMotion.FrameSize.y, 1));

		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(CurrentPosition, TargetDestination);
	}

	void Start()
	{
		CurrentPosition = GetTargetPosition();
		GetComponentInParent<CameraMotions>().Add(this);
	}


	public override void ApplyMovement(Transform camTransform, Camera camera)
	{
		camTransform.position = GetTargetPosition();
	}


	private Vector3 GetTargetPosition()
	{
		//In editor we force get everything
		if (Application.isEditor)
		{
			InterestPoints.Clear();
			foreach (var go in GameObject.FindObjectsOfType<CameraInterestPoint>())
				InterestPoints.Add(go);

			EssentialPoints.Clear();
			foreach (var go in GameObject.FindObjectsOfType<CameraEssentialPoint>())
				EssentialPoints.Add(go);
		}

		//Get all the Essential positions
		EssentialsCenter = Vector3.zero;
		foreach (var p in EssentialPoints)
			EssentialsCenter += p.transform.position;

		// Getting all the displacement points 
		InterestDisplacement = Vector3.zero;
		foreach (var p in InterestPoints)
		{
			var maxDistance = p.InterestDistance;
			var strenght = p.DisplacementForce;
			var pp = p.transform.position;

			var distance = Vector3.Distance(EssentialsCenter, pp);
			if (distance > maxDistance)
				continue;

			var t = CameraMotion.InterestPointTEaseFunction((1 - distance / maxDistance) * strenght);
			var target = Vector3.Lerp(EssentialsCenter, pp, t);

			InterestDisplacement += target - EssentialsCenter;
		}

		// Time to apply
		var x = EssentialsCenter.x + InterestDisplacement.x;
		var y = EssentialsCenter.y + InterestDisplacement.y;

		TargetDestination = new Vector3(x, y, 0);
		IsMoving = !IsInsideFrame(TargetDestination);
		if (IsMoving)
			CurrentPosition = Vector3.Lerp(CurrentPosition, TargetDestination, CameraMotion.LerpSpeed);

		return CurrentPosition;

	}

	private bool IsInsideFrame(Vector3 targetDestination)
	{
		var x = targetDestination.x;
		var y = targetDestination.y;
		var camX = CurrentPosition.x;
		var camY = CurrentPosition.y;
		var halfW = CameraMotion.FrameSize.x / 2;
		var halfH = CameraMotion.FrameSize.y / 2;

		return x > camX - halfW && x < camX + halfW && y > camY - halfH && y < camY + halfH;
	}
}
