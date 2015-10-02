using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour {

    public GameObject player;

    private Vector3 offset = new Vector3(0.0f, 1.5f, 0.0f);
    public Vector3 vPlayerStart = new Vector3(0f, 1f, 0f);
    Map map;

    // Use this for initialization
    void Start () {
        player = Resources.Load("Prefabs/alien character") as GameObject;
        player.transform.position = vPlayerStart;

        map = new Map();
        map.LoadLevelData("Assets/Resources/LevelData/level1.txt");
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void TileClicked(Vector3 position)
    {
        player.transform.position = position + offset;
    }
}
