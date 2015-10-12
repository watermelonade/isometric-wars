using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

    List<Unit> units;

    Map map;

    bool turn;
    bool moved = false;
    float unitsFinished = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (turn)
        {
            if (moved == false)
            {
                for (int i = 0; i < units.Count; i++)
                {
                    map.UpdatePathMap(units[i]);
                    Vector3 closestPlayerPos = GetClosestPlayerUnitPos(units[i]);
                   
                    map.UpdateUnitPath(closestPlayerPos, units[i], false);
                    units[i].Move();
                }
                moved = true;
            }

            if(unitsFinished == units.Count)
            {
                turn = false;
                unitsFinished = 0f;
                moved = false;
                map.RemovePath();
                BattleManager.FinishTurn();
            }

            
        }
	}

    public void SetUnits(List<Unit> u)
    {
        units = u;
    }

    public void SetMap(Map passedMap)
    {
        map = passedMap;
    }

    public void ActivateTurn()
    {
        turn = true;
    }

    void MoveUnits()
    {
        for(int i = 0; i < units.Count; i++)
        {
            units[i].Move();
        }
    }

    Vector3 GetClosestPlayerUnitPos(Unit unit)
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("player");

        GameObject closest = null;

        foreach(GameObject obj in playerObjects)
        {
            if (closest == null)
            {
                closest = obj;
            }

            if(Vector3.Distance(unit.transform.position,obj.transform.position) <=
               Vector3.Distance(unit.transform.position, closest.transform.position))
            {
                closest = obj;
            }
        }

        return closest.transform.position;
    }

    public void UnitFinished()
    {
        unitsFinished++;
    }

    public void OnMouseDown()
    {
        //gameObject.BroadcastMessage("Unselect");
        //GameObject.Find("Main Camera").GetComponent<PlayerController>().SendMessage("UnitSelected", this);

        //Select();
    }
}
