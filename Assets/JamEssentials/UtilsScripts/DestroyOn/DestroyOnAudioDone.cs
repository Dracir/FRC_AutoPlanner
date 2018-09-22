using UnityEngine;

public class DestroyOnAudioDone : MonoBehaviour {

	bool audioStarted;

	AudioSource source;

	void Start () {
		source = GetComponent<AudioSource>();
	}
	
	void Update () {
		if(!audioStarted && source.isPlaying)
			audioStarted = true;

		if(audioStarted && !source.isPlaying)
			Destroy(gameObject);
	}
}
