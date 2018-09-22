using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeTests : MonoBehaviour
{




	void Start()
	{

	}


	void Update()
	{
		if (Input.GetKey(KeyCode.Alpha1))
			CameraShakeComponent.Main.Trauma = 0.1f;
		if (Input.GetKey(KeyCode.Alpha2))
			CameraShakeComponent.Main.Trauma = 0.2f;
		if (Input.GetKey(KeyCode.Alpha3))
			CameraShakeComponent.Main.Trauma = 0.3f;
		if (Input.GetKey(KeyCode.Alpha4))
			CameraShakeComponent.Main.Trauma = 0.4f;
		if (Input.GetKey(KeyCode.Alpha5))
			CameraShakeComponent.Main.Trauma = 0.5f;
		if (Input.GetKey(KeyCode.Alpha6))
			CameraShakeComponent.Main.Trauma = 0.6f;
		if (Input.GetKey(KeyCode.Alpha7))
			CameraShakeComponent.Main.Trauma = 0.7f;
		if (Input.GetKey(KeyCode.Alpha8))
			CameraShakeComponent.Main.Trauma = 0.8f;
		if (Input.GetKey(KeyCode.Alpha9))
			CameraShakeComponent.Main.Trauma = 0.9f;
		if (Input.GetKey(KeyCode.Alpha0))
			CameraShakeComponent.Main.Trauma = 1.0f;
	}
}
