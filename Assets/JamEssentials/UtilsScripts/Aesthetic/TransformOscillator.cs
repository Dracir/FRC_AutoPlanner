using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformOscillator : MonoBehaviour {

	public OscillationData Translation;
	public OscillationData Rotation;
	public OscillationData Scale;
	
	void Update () {
		if(Translation.Actif)
			transform.localPosition = MakeOscillation(Translation);

		if(Rotation.Actif)
		transform.localRotation = Quaternion.Euler(MakeOscillation(Rotation));

		if(Scale.Actif)
			transform.localScale = MakeOscillation(Scale);
	}


	private Vector3 MakeOscillation(OscillationData o){
		var x = o.Amplitude.x * Mathf.Sin(o.Frequency.x * Time.time) + o.Offset.x;
		var y = o.Amplitude.y * Mathf.Sin(o.Frequency.y * Time.time) + o.Offset.y;
		var z = o.Amplitude.z * Mathf.Sin(o.Frequency.z * Time.time) + o.Offset.z;

		return new Vector3(x,y,z);

	}

	[System.Serializable]
	public struct OscillationData{
		public bool Actif;
		public Vector3 Frequency;
		public Vector3 Amplitude;
		public Vector3 Offset;
	}
}
