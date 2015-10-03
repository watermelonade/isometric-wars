using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour {

    private GameObject player;

    private Vector3 offset = new Vector3(0.0f, 1.5f, 0.0f);
    public Vector3 vPlayerStart;// = new Vector3(1.0f, 2.0f, 1.0f);

    Map map;

    bool endTurn = false;
    Vector3 dest;

    public float moveSpeed = 2.0f;

    // Use this for initialization
    void Start () {
        player = GameObject.CreatePrimitive(PrimitiveType.Capsule);//Resources.Load("Prefabs/alien character") as GameObject;
        player.transform.position = vPlayerStart;

        map = new Map();
        map.LoadLevelData("Assets/Resources/LevelData/level1.txt");
	}
	
	// Update is called once per frame
	void Update () {
        if (endTurn)
        {
            if (player.transform.position != dest)
            {
                player.transform.position = Vector3.Lerp(player.transform.position, dest, moveSpeed * Time.deltaTime);
            } else
            {
                endTurn = false;
            }
        }
	}

    void TileClicked(Vector3 position)
    {
        dest = position + offset;
        endTurn = true;
    }
}
