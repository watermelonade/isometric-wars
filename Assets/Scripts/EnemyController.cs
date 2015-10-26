using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

    public static List<Unit> units;

    //Map map;

    bool turn;
    bool moved = false;
    int unitsFinished = 0;

	bool unitFinished;


	int state = 1;
	const int waitingForTurn = 1, working = 2, waiting = 3, cleanUp = 4;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		switch (state) {
		case waitingForTurn:
			break;

		case working:
			if (unitsFinished == units.Count) {
				state = cleanUp;
			}else{
                    /*Unit currentUnit = units [unitsFinished];
                    map.UpdatePathMapAvoidClaimedSpaces (currentUnit, units);
                    //map.UpdatePathMap(currentUnit);
                    Vector3 closestPlayerPos = GetClosestPlayerUnitPos (currentUnit);
                    if (map.UpdateUnitPath (closestPlayerPos, currentUnit, false, 1))
                        currentUnit.Move ();
                    else {
                        UnitFinished();
                    }*/

                    units[unitsFinished].GetComponent<EnemyFootUnit>().Act();
				    state = waiting;
			}
			break;
		
		case waiting:
			if(unitFinished){
				state = working;
				unitFinished = false;
			}
			break;

		case cleanUp:
			unitsFinished = 0;
			unitFinished = false;
			BattleManager.map.RemovePath ();
			BattleManager.FinishTurn ();
			state = waitingForTurn;
			break;
		}

        /*if (turn) {
			if (moved == false && unitFinished == true) {
				map.clearClaimedSpaces ();
				for (int i = 0; i < units.Count; i++) {
					Unit currentUnit = units [unitsFinished];
					map.UpdatePathMapAvoidClaimedSpaces (currentUnit);
					Vector3 closestPlayerPos = GetClosestPlayerUnitPos (currentUnit);
					if (map.UpdateUnitPath (closestPlayerPos, currentUnit, false, 1))
						currentUnit.Move ();
				}
				moved = true;
			}

			if (unitsFinished == units.Count) {
				turn = false;
			}
     
		} else {
			unitsFinished = 0;
			moved = false;
			map.RemovePath ();
			BattleManager.FinishTurn ();
		}*/
	}

    public void SetUnits(List<Unit> u)
    {
        units = u;
    }

    public void ActivateTurn()
    {
        if(state == waitingForTurn)
			state = working;
		//turn = true;
    }

    void MoveUnits()
    {
        for(int i = 0; i < units.Count; i++)
        {
            units[i].Move();
        }
    }

    public static Vector3 GetClosestPlayerUnitPos(Unit unit)
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
		unitFinished = true;
        state = working;
    }

    public void OnMouseDown()
    {
        //gameObject.BroadcastMessage("Unselect");
        //GameObject.Find("Main Camera").GetComponent<PlayerController>().SendMessage("UnitSelected", this);

        //Select();
    }

    public void UnitDied(GameObject obj)
    {
        units.Remove(obj.GetComponent<Unit>());
        
    }
}
