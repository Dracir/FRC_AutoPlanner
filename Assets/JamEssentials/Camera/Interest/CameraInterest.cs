using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class CameraInterest
{
	[Tooltip("Size of the no motion box around the current camera position.")]
	/** Size of the no motion box around the current camera position. */
	public Vector2 FrameSize = Vector2.one;

	/** Speed at which the camera goes to it's target location. */
	[Tooltip("Speed at which the camera goes to it's target location.")]
	public float LerpSpeed = 0.25f;

	/** Function getting the ratio of distance between cam center and point location over max effecting distance and returning effective position ratio in that range. */
	public Func<float, float> InterestPointTEaseFunction = EaseFunctions.SmoothStart2;
}
