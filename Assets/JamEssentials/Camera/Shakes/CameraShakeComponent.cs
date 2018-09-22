using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeComponent : CameraMoverBase
{
	public static CameraShakeComponent Main { get { return CameraMotions.Main.GetComponentInChildren<CameraShakeComponent>(); } }
	public CameraShake CameraShake;

	public float Trauma
	{
		set { CameraShake.Trauma = value; }
		get { return CameraShake.Trauma; }
	}

	void Start() => GetComponentInParent<CameraMotions>().Add(this);

	public override void ApplyMovement(Transform camTransform, Camera camera)
	{
		if (CameraShake.Trauma <= 0)
		{
			/*camTransform.localPosition = Vector3.zero;
			camTransform.localRotation = Quaternion.identity;*/
			return;
		}

		if (CameraShake.TranslationalShake)
		{
			float xTranslation = CameraShake.MaxTranslationalShakeOffset.x * CameraShake.ShakeAmount * Mathf.PerlinNoise(0.25f, Time.time * CameraShake.ShakeIntensity);
			float yTranslation = CameraShake.MaxTranslationalShakeOffset.y * CameraShake.ShakeAmount * Mathf.PerlinNoise(0.50f, Time.time * CameraShake.ShakeIntensity);
			camTransform.localPosition += new Vector3(xTranslation, yTranslation, 0);
		}

		if (CameraShake.RotationalShake)
		{
			var z = CameraShake.MaxRotationalAngleOffset * CameraShake.ShakeAmount * (2 * Mathf.PerlinNoise(0.75f, Time.time * CameraShake.ShakeIntensity) - 1);
			camTransform.localRotation = Quaternion.Euler(0, 0, z);
		}

	}

	void Update()
	{
		CameraShake.Trauma -= CameraShake.FalloffRate * Time.deltaTime;
	}
}
