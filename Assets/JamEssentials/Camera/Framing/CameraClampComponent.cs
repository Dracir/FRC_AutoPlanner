using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClampComponent : CameraMoverBase
{

	void Start() => GetComponentInParent<CameraMotions>().AddAtEnd(this);
	public Vector2 Min;
	public Vector2 Max;

	public bool IsAtMax;// { private set; get; }

	public override void ApplyMovement(Transform camTransform, Camera camera)
	{
		IsAtMax = false;
		var bounds = camera.OrthographicBounds();

		var x = camTransform.localPosition.x;
		if (bounds.Left() <= Min.x)
		{
			IsAtMax = true;
			x = Min.x + bounds.size.x / 2;
		}
		else if (bounds.Right() >= Max.x)
		{
			IsAtMax = true;
			x = Max.x - bounds.size.x / 2;
		}

		var y = camTransform.localPosition.y;
		if (bounds.Bottom() <= Min.y)
		{
			IsAtMax = true;
			y = Min.y + bounds.size.y / 2;
		}
		else if (bounds.Top() >= Max.y)
		{
			IsAtMax = true;
			y = Max.y - bounds.size.y / 2;
		}

		camTransform.localPosition = new Vector3(x, y, camTransform.localPosition.z);
	}
}
