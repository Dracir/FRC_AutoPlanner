using UnityEngine;

public class DestroyOnParticleDone : MonoBehaviour {

	ParticleSystem thisParticleSystem;
	
	void Start () {
		thisParticleSystem = GetComponent<ParticleSystem>();
	}
	
	
	void Update () {
		if(thisParticleSystem.isStopped)
			Destroy(gameObject);
	}
}
