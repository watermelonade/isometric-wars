using UnityEngine;
using System.Collections;

public class map_setup : MonoBehaviour {

	public int board_L = 20;
	public int board_H = 20;

	public GameObject t1;

	// Use this for initialization
	void Start () {
		for (int x = 0; x < board_L; x++) {
			for (int y = 0; y < board_H; y++) {
				//GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
				GameObject spawn = (GameObject)Instantiate(t1);
				spawn.transform.position = new Vector3 (x, 0.5F, y);

			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
