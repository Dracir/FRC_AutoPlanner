using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class CameraShake
{
	/** Total trauma. The shake will be exponential.*/
	public float Trauma;

	/** Apply Translational shake to the camera (x,y).*/
	public bool TranslationalShake;
	/** The maximum X,Y transition that can be done on full Trauma.*/
	public Vector2 MaxTranslationalShakeOffset = new Vector2(1, 1);

	/** Apply Rotational shake on the z axis of the camera.*/
	public bool RotationalShake;
	/**The maximum Z rotation angle that can be done on full Trauma.*/
	public float MaxRotationalAngleOffset = 10;

	/**Rate at which the Trauma is reduced. A value of 1 would take 1 Time.*/
	public float FalloffRate = 1f;
	/**Speed of motion. <100 : Slow speeds. >100 more intense shakes.*/
	public float ShakeIntensity = 100;


	public float ShakeAmount { get { return Trauma * Trauma; } }


}
