using UnityEngine;
using System.Collections;

public class turn_controller : MonoBehaviour {

	public GameObject player;
	public GameObject camera;

	// Use this for initialization
	void Start () {

	}

	/*bool up;
	bool down;
	bool left;
	bool right;
	bool attack;*/




	// Update is called once per frame
	void Update () {
		/*
		up = Input.GetKeyDown ("w");
		down = Input.GetKeyDown ("s");
		left = Input.GetKeyDown ("a");
		right = Input.GetKeyDown ("d");
		attack = Input.GetKeyDown ("space");

		if (up || down || left || right))
			return;
		*/
		if (!Input.GetMouseButtonDown (0))
			return;


	
	}

	/*
	Vector3 get_cursor_position () {
		Vector3 cam_position = camera.transform.position;
		return;

	
	}*/
	
}
