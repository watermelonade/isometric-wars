using UnityEngine;
using System.Collections;

public class RotateCamera : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	public void Rotate90 () {
		transform.RotateAround (Vector3.zero, Vector3.zero, 90);
	}
}