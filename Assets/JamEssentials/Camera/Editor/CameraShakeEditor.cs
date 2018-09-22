using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraShakeComponent))]
public class CameraShakeEditor : Editor
{

	public override void OnInspectorGUI()
	{
		CameraShakeComponent shakeComponent = (CameraShakeComponent)target;
		CameraShake shake = shakeComponent.CameraShake;

		EditorUtils.FloatField("Trauma", "Total trauma. The shake will be exponential.", ref shake.Trauma, 0, 1);
		EditorUtils.FloatField("Falloff Rate", "Rate at which the Trauma is reduced. A value of 1 would take 1 Time.", ref shake.FalloffRate);
		EditorUtils.FloatField("Shake Intensity", "Speed of motion. <100 : Slow speeds. >100 more intense shakes.", ref shake.ShakeIntensity);
		EditorGUILayout.Space();

		EditorUtils.BoolField("Translational Shake", "Apply Translational shake to the camera (x,y)", ref shake.TranslationalShake);
		if (shake.TranslationalShake)
		{
			EditorGUI.indentLevel++;
			EditorUtils.Vector2Field("Max Offset", "The maximum X,Y transition that can be done on full Trauma.", ref shake.MaxTranslationalShakeOffset);
			EditorGUI.indentLevel--;
		}

		EditorGUILayout.Space();

		EditorUtils.BoolField("Rotational Shake", "Apply Rotational shake on the z axis of the camera", ref shake.RotationalShake);
		if (shake.RotationalShake)
		{
			EditorGUI.indentLevel++;
			EditorUtils.FloatField("Max Angle Offset", "The maximum Z rotation angle that can be done on full Trauma.", ref shake.MaxRotationalAngleOffset, 0, 360);
			EditorGUI.indentLevel--;
		}
	}
}
