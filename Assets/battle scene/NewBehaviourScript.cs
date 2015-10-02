using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	
	public Transform prefab;
	void Start() {
		for (int i = 0; i < 10; i++) {
			Instantiate(prefab, new Vector3(i * 2.0F, 0, 0), Quaternion.identity);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
