using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour {

    private GameObject player;

    private Vector3 offset = new Vector3(0.0f, 1.5f, 0.0f);
    public Vector3 vPlayerStart;// = new Vector3(1.0f, 2.0f, 1.0f);
    public Vector3 vPlayerStart2;
    public Vector3 vPlayerStart3;

    Map map;

    bool endTurn = false;
    Vector3 dest;

    public float moveSpeed = 2.0f;

    List<Unit> units;
    Unit selectedUnit;

    bool unitSelected = false;

    // Use this for initialization
    void Start () {
        //player = GameObject.CreatePrimitive(PrimitiveType.Capsule);//Resources.Load("Prefabs/alien character") as GameObject;
        //player.transform.position = vPlayerStart;

        map = gameObject.AddComponent<Map>();
        map.LoadLevelData("Assets/Resources/LevelData/level1.txt");

        units = new List<Unit>();
        LoadUnits();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            selectedUnit.Deselect();
            unitSelected = false;
            MoveUnits();
        }
	}

    void TileClicked(Vector3 position)
    {
        if (unitSelected)
        {
            selectedUnit.dest = position + offset;
        }

        //dest = position + offset;
        //endTurn = true;
    }

    public void UnitSelected(Unit unit)
    {
        if (selectedUnit)
        {
            selectedUnit.Deselect();
        }

        unitSelected = true;

        selectedUnit = unit;
        map.HighlightRadius(unit.moveRange, unit.gameObject.transform.position);
    }

    void LoadUnits()
    {
        Unit u1 = Instantiate(Resources.Load("Prefabs/FootUnit", typeof(Unit))) as Unit;
        Unit u2 = Instantiate(Resources.Load("Prefabs/FootUnit", typeof(Unit))) as Unit;
        Unit u3 = Instantiate(Resources.Load("Prefabs/FootUnit", typeof(Unit))) as Unit;

        u1.transform.position = vPlayerStart;
        u2.transform.position = vPlayerStart2;
        u3.transform.position = vPlayerStart3;

        units.Add(u1);
        units.Add(u2);
        units.Add(u3);
    }

    void MoveUnits()
    {
        for(int i = 0; i < units.Count; i++)
        {
            units[i].Move();
        }
    }
}
