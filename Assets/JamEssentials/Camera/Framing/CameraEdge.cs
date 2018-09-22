using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdge : MonoBehaviour
{

	public static CameraFrame Main { get { return Camera.main.GetComponentInParent<CameraFrame>(); } }

	void Start()
	{
		if (Main != null)
			Main.AddEdge(this);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
