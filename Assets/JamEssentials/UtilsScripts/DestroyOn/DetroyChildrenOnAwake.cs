using UnityEngine;

public class DetroyChildrenOnAwake : MonoBehaviour {

	void Awake () {
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}	
	}
}
