﻿using UnityEngine;
using System.Collections;

public class map_setup : MonoBehaviour {

	public int board_L = 20;
	public int board_H = 20;

	public GameObject t1;

	//private GameObject[][] board;
	//board = new GameObject[board_L][board_H];

	private GameObject[,] board;// = new GameObject[board_L , board_H];

	// Use this for initialization
	void Start () {
		board = new GameObject[board_L , board_H];

		for (int x = 0; x < board_L; x++) {
			for (int y = 0; y < board_H; y++) {
				//GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
				GameObject spawn = (GameObject)Instantiate(t1);
				board [x,y] = spawn;
				spawn.transform.position = new Vector3 (x, 0.5F, y);

			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		/*if(Input.GetKeyDown ("space"))
			for (int x = 0; x < board_L; x++) 
				for (int y = 0; y < board_H; y++) 
					board [x,y].SetActive (false);*/
				
			
	}
}
