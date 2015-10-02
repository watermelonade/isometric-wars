using UnityEngine;
using System.Collections;

public class turn_controller : MonoBehaviour {

	public GameObject player;

	private Vector3 offset = new Vector3(0.0f, 1.5f, 0.0f);

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

	void Tile_Clicked (Vector3 position){
		player.transform.position = position + offset;

	}
	
}
