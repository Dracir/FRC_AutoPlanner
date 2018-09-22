using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class CameraMotions : MonoBehaviour
{

	public static CameraMotions Main { get { return Camera.main.GetComponentInParent<CameraMotions>(); } }

	private List<CameraMoverBase> Movers = new List<CameraMoverBase>();
	public void Add(CameraMoverBase mover) => Movers.Add(mover);
	public void Remove(CameraMoverBase mover) => Movers.Remove(mover);
	private List<CameraMoverBase> MoversAtEnd = new List<CameraMoverBase>();
	public void AddAtEnd(CameraMoverBase mover) => MoversAtEnd.Add(mover);
	public void RemoveAtEnd(CameraMoverBase mover) => MoversAtEnd.Remove(mover);

	public Camera Camera;

	void Update()
	{
		transform.position = Vector3.zero;
		foreach (var m in Movers)
			m.ApplyMovement(transform,Camera);

		foreach (var m in MoversAtEnd)
			m.ApplyMovement(transform,Camera);
	}
}

public static class CameraMotionExtention
{
	public static CameraMotions CameraMotions(this Camera camera)
	{
		return camera.GetComponentInParent<CameraMotions>();
	}
}