using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

using Priority_Queue;
using System.Collections.Generic;

public class Map : MonoBehaviour {

	public int width;
	public int height;

   // Unit currentUnit;

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
    public const string wallTile = "W";

    CircleDraw circle;

    void Build()
    {
        board = new GameObject[width, height];
		distanceMap = new int[width, height];
		pathMap = new Coordinate[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject spawn;
                switch (levelBase[y][x])
                {
                    
                    case wallTile:
                        //spawn = Instantiate(Resources.Load("Prefabs/MapTiles/Wall", typeof(GameObject))) as GameObject;
                        board[x, y] = null;
                        //spawn.transform.position = new Vector3(x, 0.5F, y);
                        break;
                    case defaultTile:
                        spawn = Instantiate(Resources.Load("Prefabs/MapTiles/map_prefab", typeof(GameObject))) as GameObject;
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
        string[] lines = Regex.Split(text, "\n");
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
        //currentUnit = u;

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

			Coordinate c = priorityQueue.Dequeue();

			int cDistanceInline = distanceMap[c.X, c.Y] + 2;
			int cDistanceDiagonal = distanceMap[c.X, c.Y] + 3;

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
                    if (board[x, y] == null)
                        continue;

					if(c.X == x || c.Y == y)
					{
						if(cDistanceInline < distanceMap[x,y])
						{
							distanceMap[x,y] = cDistanceInline;
							priorityQueue.Enqueue(new Coordinate (x, y), cDistanceInline);
							pathMap[x,y] = new Coordinate(c.X, c.Y);
						}
					}
					else
					{
						if(cDistanceDiagonal < distanceMap[x,y])
						{
							distanceMap[x,y] = cDistanceDiagonal;
							priorityQueue.Enqueue(new Coordinate (x, y), cDistanceDiagonal);
							pathMap[x,y] = new Coordinate(c.X, c.Y);
						}
					}
				}
			}
		}

		priorityQueue.Clear();

	}




	public void UpdatePathMapAvoidClaimedSpaces (Unit u, List<Unit> units)
	{
		Vector3 currentPositionVect = u.transform.position;
		int currentX = (int)currentPositionVect.x;
		int currentY = (int)currentPositionVect.z;
		bool [,] validSpacesMap = new bool[width, height];
		
		for (int x = 0; x < width; x++){
			for (int y = 0; y < height; y++) { 
				distanceMap [x, y] = 1000000;
				pathMap [x, y] = new Coordinate (-1, -1);
				validSpacesMap[x,y] = !(board[x,y] == null);
			}
		}

		//Unit[] units = FindObjectsOfType(typeof(Unit)) as Unit[];
		foreach (Unit unit in units) {
			Vector3 unitPosition = unit.transform.position;
			validSpacesMap[(int)unitPosition.x,(int)unitPosition.z] = false;
		}

		distanceMap [currentX, currentY] = 0;
		validSpacesMap [currentX, currentY] = true;
		
		HeapPriorityQueue<Coordinate> priorityQueue = new HeapPriorityQueue<Coordinate>(height*width);
		priorityQueue.Enqueue(new Coordinate (currentX, currentY), 0);

		while( priorityQueue.Count > 0)
		{	
			Coordinate c = priorityQueue.Dequeue();
			
			int cDistanceInline = distanceMap[c.X, c.Y] + 2;
			int cDistanceDiagonal = distanceMap[c.X, c.Y] + 3;
			
			for (int x = c.X - 1; x <= c.X + 1; x++)
			{
				for (int y = c.Y - 1; y <= c.Y + 1; y++)
				{
					//if(board[c.X, c.Y] == null)
					//	continue;
					if(!validSpacesMap[c.X,c.Y])
						continue;
					if(x == c.X && y == c.Y)
						continue;
					if(x < 0 || x >= width)
						continue;
					if(y < 0 || y >= height)
						continue;
					if(c.X == x || c.Y == y)
					{
						if(cDistanceInline < distanceMap[x,y])
						{
							distanceMap[x,y] = cDistanceInline;
							priorityQueue.Enqueue(new Coordinate (x, y), cDistanceInline);
							pathMap[x,y] = new Coordinate(c.X, c.Y);
						}
					}
					else
					{
						if(cDistanceDiagonal < distanceMap[x,y])
						{
							distanceMap[x,y] = cDistanceDiagonal;
							priorityQueue.Enqueue(new Coordinate (x, y), cDistanceDiagonal);
							pathMap[x,y] = new Coordinate(c.X, c.Y);
						}
					}
				}
			}
		}
		priorityQueue.Clear();
	}

	public void UpdateUnitPath (Vector3 position, Unit currentUnit, bool showPath)
	{
		RemovePath ();
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
		Vector3 offset = new Vector3 (0F, 1.5F, 0F);
		while (currentX >= 0 && currentY >= 0) 
		{
            if(showPath)
                board[currentX, currentY].GetComponent<MouseClick>().Highlight();

            path.Push(board[currentX, currentY].transform.position + offset);
            current = pathMap[currentX, currentY];
            currentX = current.X;
            currentY = current.Y;
            
		}

        currentUnit.SetPath(path);
		
	}

	
	public bool UpdateUnitPath (Vector3 position, Unit currentUnit, bool showPath, int stepsAway)
	{
		RemovePath ();
		int currentX = (int)position.x;
		int currentY = (int)position.z;
		Coordinate current;
		Stack<Vector3> path = new Stack<Vector3>();
		Vector3 offset = new Vector3 (0F, 1.5F, 0F);
		for (int step = 1; step <= stepsAway && currentX >= 0 && currentY >= 0; step++) {
			current = pathMap [currentX, currentY];
			currentX = current.X;
			currentY = current.Y;
		}
		if (currentX < 0 && currentY < 0)
			return false;

		while (currentX >= 0 && currentY >= 0) 
		{
			if(showPath)
				board[currentX, currentY].GetComponent<MouseClick>().Highlight();
			
			path.Push(board[currentX, currentY].transform.position + offset);
			current = pathMap[currentX, currentY];
			currentX = current.X;
			currentY = current.Y;
			
		}	
		currentUnit.SetPath(path);
		return true;
	}

	public void RemovePath (){
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                if(board[x,y] != null)
                    board[x, y].GetComponent<MouseClick>().Unhighlight() ;		
		
	}
	    
    public void ShowPlayerRange(float range, Vector3 pos)
    {
        
        circle = gameObject.AddComponent<CircleDraw>();
        

        circle.MakeCircle(range, pos);
    }

	public void RemovePlayerRange()
    {
        if (circle)
        {
            circle.RemoveCircle();
        }
    }
}
