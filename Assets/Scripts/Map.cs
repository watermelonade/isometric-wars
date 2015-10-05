using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class Map : MonoBehaviour {

	public int width;
	public int height;

	public GameObject t1;

	private GameObject[,] board;
    public string[][] levelBase;

    public const string defaultTile = "X";

	// Use this for initialization
	void Start () {
		
	}
	

	// Update is called once per frame
	void Update () {

			
	}

    void Build()
    {
        board = new GameObject[width, height];

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
          
    }

    public void HighlightRadius(int radius, Vector3 center)
    {
        int cx = (int)center.x;
        int cy = (int)center.y;
        int r2 = radius * radius;

        int x = 0;
        int y = radius;
        int p = (5 - radius * 4) / 4;

        circleTiles(cx, cy, x, y);
        while (x < y)
        {
            x++;
            if(p < 0)
            {
                p += 2 * x + 1;
            } else
            {
                y--;
                p += 2 * (x - y) + 1;

            }
            circleTiles(cx, cy, x, y);
        }
    }

    internal void circleTiles(int cx, int cy, int x, int y)
    {
        try
        {


            if (x == 0)
            {
                board[cx, cy + y].GetComponent<MouseClick>().Highlight();
                board[cx, cy - y].GetComponent<MouseClick>().Highlight();
                board[cx + y, cy].GetComponent<MouseClick>().Highlight();
                board[cx - cy, cy].GetComponent<MouseClick>().Highlight();
            } else if (x == y)
            {
                board[cx + x, cy + y].GetComponent<MouseClick>().Highlight();
                board[cx - x, cy + y].GetComponent<MouseClick>().Highlight();
                board[cx + x, cy - y].GetComponent<MouseClick>().Highlight();
                board[cx - y, cy - y].GetComponent<MouseClick>().Highlight();
            } else if (x < y)
            {
                board[cx + x, cy + y].GetComponent<MouseClick>().Highlight();
                board[cx - x, cy + y].GetComponent<MouseClick>().Highlight();
                board[cx + x, cy - y].GetComponent<MouseClick>().Highlight();
                board[cx - x, cy - y].GetComponent<MouseClick>().Highlight();
                board[cx + y, cy + x].GetComponent<MouseClick>().Highlight();
                board[cx - y, cy + x].GetComponent<MouseClick>().Highlight();
                board[cx + y, cy - x].GetComponent<MouseClick>().Highlight();
                board[cx - y, cy - x].GetComponent<MouseClick>().Highlight();
            }
        }
        catch(Exception e)
        {

        }
        
    }

  
}
