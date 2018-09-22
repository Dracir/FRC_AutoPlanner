using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraFrame : MonoBehaviour
{

	private List<CameraEdge> Edges = new List<CameraEdge>();

	void Awake()
	{

	}

	void Start()
	{

	}


	void Update()
	{
		var camBound = Camera.main.OrthographicBounds();
		var camWidth = camBound.size.x;
		//var camHeight = camBound.size.y;
		foreach (var edge in Edges)
		{
			var edgePosition = edge.transform.position;
			if (camBound.Contains(edgePosition))
			{
				if (Mathf.Abs(camBound.Left() - edgePosition.x) < camWidth)
				{
					transform.position += new Vector3((camWidth - camBound.Left() - edgePosition.x), 0, 0);
				}
			}
		}
	}

	void RemakeBounds()
	{
		//Bounds.SetPath(0,Edges.Select(e=> (Vector2)e.transform.position).ToArray());
	}

	public void AddEdge(CameraEdge cameraEdge)
	{
		Edges.Add(cameraEdge);
		RemakeBounds();
	}
}
