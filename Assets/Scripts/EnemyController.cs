using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

    List<Unit> units;

    Map map;

    bool turn;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (turn)
        {
            for(int i = 0; i < units.Count; i++)
            {
                map.UpdatePathMap(units[i]);
                map.UpdateUnitPath(GetClosestPlayerUnitPos(units[i]), units[i]);
                units[i].Move();
            }
            turn = false;
            BattleManager.FinishTurn();
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
}
