using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

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
}
