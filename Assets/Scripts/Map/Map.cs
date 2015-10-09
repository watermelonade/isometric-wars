﻿using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

using Priority_Queue;
using System.Collections.Generic;

public class Map : MonoBehaviour {

	public int width;
	public int height;

    Unit currentUnit;

	public class Coordinate : PriorityQueueNode
	{
		public int X { get; set; }
		public int Y { get; set; }
		public Coordinate(int x, int y)
		{
			X = x;
			Y = y;
		}
	}

	public GameObject t1;

	private int[,] distanceMap;
	private Coordinate[,] pathMap;


	private GameObject[,] board;
    public string[][] levelBase;

    public const string defaultTile = "X";

    CircleDraw circle;

	// Use this for initialization
	void Start () {
		
	}
	

	// Update is called once per frame
	void Update () {

			
	}

    void Build()
    {
        board = new GameObject[width, height];
		distanceMap = new int[width, height];
		pathMap = new Coordinate[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                switch (levelBase[y][x])
                {
                    case defaultTile:
                        GameObject spawn = Instantiate(Resources.Load("Prefabs/MapTiles/map_prefab", typeof(GameObject))) as GameObject;
                        board[x, y] = spawn;
                        spawn.transform.position = new Vector3(x, 0.5F, y);
                        break;
                }

            }
        }
    }

    public void LoadLevelData(string file)
    {
        
        string text = System.IO.File.ReadAllText(file);
        string[] lines = Regex.Split(text, "\r\n");
        height = lines.Length;

        levelBase = new string[height][];

        
        for (int i = 0; i < height; i++)
        {
            string[] stringsOfLine = Regex.Split(lines[i], " ");
            if(i == 0)
            {
                width = stringsOfLine.Length;
            }
            levelBase[i] = stringsOfLine;
        }

        Build(); 
		//UpdatePathMap();
          
    }



	public void UpdatePathMap (Unit u)
	{

        //SortedList<int, int[]> sortedList = new SortedList <int, int[]>(); 
        //var sortedList = new SortedList();
        currentUnit = u;

		Vector3 currentPositionVect = u.transform.position;
		int currentX = (int)currentPositionVect.x;
		int currentY = (int)currentPositionVect.z;

		for (int x = 0; x < width; x++){
			for (int y = 0; y < height; y++) { 
				distanceMap [x, y] = 1000000;
				pathMap [x, y] = new Coordinate (-1, -1);
			}
		}

		distanceMap [currentX, currentY] = 0;
		//sortedList.Add(0,currentPosition);

		HeapPriorityQueue<Coordinate> priorityQueue = new HeapPriorityQueue<Coordinate>(height*width); //need refine O(height*width)
		priorityQueue.Enqueue(new Coordinate (currentX, currentY), 0);

		//while (sortedList.Count > 0)
		while( priorityQueue.Count > 0)
		{
			//int[] u = (int[])sortedList.GetByIndex(0);
			//sortedList.RemoveAt(0);

			Coordinate c = priorityQueue.Dequeue();

			int uDistance = distanceMap[c.X, c.Y] + 1;

			for (int x = c.X - 1; x <= c.X + 1; x++)
			{
				for (int y = c.Y - 1; y <= c.Y + 1; y++)
				{
					if(x == c.X && y == c.Y)
						continue;
					if(x < 0 || x >= width)
						continue;
					if(y < 0 || y >= height)
						continue;
					
					if(uDistance < distanceMap[x,y])
					{
						distanceMap[x,y] = uDistance;
						priorityQueue.Enqueue(new Coordinate (x, y), uDistance);
						pathMap[x,y] = new Coordinate(c.X, c.Y);
					}
				}
			}
		}

		priorityQueue.Clear();

		/*for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {

				if(distanceMap[x,y] == 1)
				{
					GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
					cube.transform.position = new Vector3 ((float)x, 1F, (float)y);
				}
			}
		}*/
	}

	public void UpdatePath (Vector3 position)
	{
		ChangeBack ();
		//board[1,1].SendMessage("Highlight");

		int currentX = (int)position.x;
		int currentY = (int)position.z;
		/*if (currentX >= width || currentX < 0)
			return;
		if (currentY >= height || currentY < 0)
			return;
		*/	
		Coordinate current;
        Stack<Vector3> path = new Stack<Vector3>();
		while (currentX >= 0 && currentY >= 0) 
		{
			board[currentX,currentY].GetComponent<MouseClick>().Highlight();
            path.Push(board[currentX, currentY].transform.position);
			current = pathMap[currentX,currentY];
			currentX = current.X;
			currentY = current.Y;
		}

        currentUnit.SetPath(path);
		
	}

	void ChangeBack (){
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                board[x, y].GetComponent<MouseClick>().Unhighlight() ;		
		
	}
	    
    public void ShowPlayerRange(float range, Vector3 pos)
    {
        
        circle = gameObject.AddComponent<CircleDraw>();
        

        circle.MakeCircle(range, pos);
    }

	public void RemovePlayerRange()
    {
        circle.RemoveCircle();
    }
}
