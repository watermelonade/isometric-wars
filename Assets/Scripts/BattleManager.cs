using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour {

    private GameObject player;
    private GameObject mainCam;

    private Vector3 offset = new Vector3(0.0f, 1.5f, 0.0f);

    public Vector3 vPlayerStart;
    public Vector3 vPlayerStart2;
    public Vector3 vPlayerStart3;

    public Vector3 ePlayerStart;
    public Vector3 ePlayerStart2;
    public Vector3 ePlayerStart3;


    Map map;

    public static bool endTurn = false;
    Vector3 dest;

    public float moveSpeed = 2.0f;

    List<Unit> units;
    List<Unit> enemyUnits;

    //Unit selectedUnit;

    bool unitSelected = false;
    public static bool playerTurn = false;
    public static bool enemyTurn = false;

    public static PlayerController pc;
    public static EnemyController ec;

    // Use this for initialization
    void Start () {
        
        mainCam = GameObject.Find("Main Camera");
        
        map = gameObject.AddComponent<Map>();
        map.LoadLevelData("Assets/Resources/LevelData/test.txt");

        units = new List<Unit>();
        enemyUnits = new List<Unit>();
        LoadUnits();

        ec = gameObject.AddComponent<EnemyController>();
        ec.SetUnits(enemyUnits);
        ec.SetMap(map);

        pc = gameObject.AddComponent<PlayerController>();
        pc.SetUnits(units);
        pc.SetMap(map);

        pc.ActivateTurn();
        playerTurn = true;
	}
	
	// Update is called once per frame
	void Update () {

        //if (Input.GetKeyDown("space") && playerTurn)
        //{
        //    //mainCam.GetComponent<CameraManager>().UnsetTarget();
        //    //pc.MoveUnits();
        //}

        if (Input.GetKeyDown(KeyCode.A))
        {
            mainCam.GetComponent<CameraManager>().Rotate("left");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            mainCam.GetComponent<CameraManager>().Rotate("right");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            mainCam.GetComponent<CameraManager>().Rotate("down");
        }

        /*
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                mainCam.GetComponent<CameraManager>().Move("left");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                mainCam.GetComponent<CameraManager>().Move("right");
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                mainCam.GetComponent<CameraManager>().Move("down");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                mainCam.GetComponent<CameraManager>().Move("down");
            }
        } else
        {

        }*/
    }

    /*void TileClicked(Vector3 position)
    {

        player.transform.position = position + offset;

		//map.UpdatePath (position);
		//map.buildcircle ();
		map.UpdatePathMap ();
		

        if (unitSelected)
        {
            //selectedUnit.dest = position + offset;
            map.UpdatePath(position,selectedUnit);
        }

        //dest = position + offset;
        //endTurn = true;
    }*/

    

    void LoadUnits()
    {
        Unit u1 = Instantiate(Resources.Load("Prefabs/Units/FootUnit", typeof(Unit))) as Unit;
        Unit u2 = Instantiate(Resources.Load("Prefabs/Units/FootUnit", typeof(Unit))) as Unit;
        Unit u3 = Instantiate(Resources.Load("Prefabs/Units/FootUnit", typeof(Unit))) as Unit;

        Unit e1 = Instantiate(Resources.Load("Prefabs/Units/EnemyUnit", typeof(Unit))) as Unit;
        Unit e2 = Instantiate(Resources.Load("Prefabs/Units/EnemyUnit", typeof(Unit))) as Unit;
        Unit e3 = Instantiate(Resources.Load("Prefabs/Units/EnemyUnit", typeof(Unit))) as Unit;

        u1.transform.position = vPlayerStart;
        u2.transform.position = vPlayerStart2;
        u3.transform.position = vPlayerStart3;

        e1.transform.position = ePlayerStart;
        e2.transform.position = ePlayerStart2;
        e3.transform.position = ePlayerStart3;

        units.Add(u1);
        units.Add(u2);
        units.Add(u3);

        enemyUnits.Add(e1);
        enemyUnits.Add(e2);
        enemyUnits.Add(e3);
    }


    public static void FinishTurn()
    {
        if (playerTurn)
        {
            playerTurn = false;
            enemyTurn = true;
            ec.ActivateTurn();
        }
        else if(enemyTurn)
        {
            playerTurn = true;
            enemyTurn = false;
            pc.ActivateTurn();
        }
        
    }
    
    
}
