using UnityEngine;

public class AudioMenager : Singleton<AudioMenager>
{
	public ItsTheSoundOfBuddles blip;

	public void Play(AudioClip clip, Vector3 position, float volume = 1f, float pitch = 1f, float delay = 0f)
	{
		if (clip == null)
		{
			Debug.LogWarning("Tu veux jouer un son null :/");
			return;
		}

		GameObject newGo = new GameObject(clip.name);
		newGo.transform.position = position;
		newGo.transform.parent = this.transform;

		var source = newGo.AddComponent<AudioSource>();
		source.playOnAwake = false;
		source.loop = false;
		source.pitch = pitch;
		source.clip = clip;
		source.volume = volume;

		if (delay > 0)
			source.PlayDelayed(delay);
		else
			source.Play();

		newGo.AddComponent<DestroyOnAudioDone>();
	}

}

[System.Serializable]
public class ItsTheSoundOfBuddles
{
	public AudioClip[] Clips;

	public void PlayRandom(float volume = 1f, float pitch = 1f, float delay = 0f)
	{
		Play(Vector3.zero, volume, pitch, delay);
	}

	public void Play(Vector3 position, float volume = 1f, float pitch = 1f, float delay = 0f)
	{
		if (Clips.Length == 0)
			return;

		var clip = Clips[Random.Range(0, Clips.Length)];
		AudioMenager.Instance.Play(clip, position, volume, pitch, delay);
	}
}

/*
public class DestroyOnAudioDone : MonoBehaviour
{

	bool audioStarted;

	AudioSource source;

	void Start()
	{
		source = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (!audioStarted && source.isPlaying)
			audioStarted = true;

		if (audioStarted && !source.isPlaying)
			Destroy(gameObject);
	}
}
*/