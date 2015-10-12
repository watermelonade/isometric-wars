using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    List<Unit> units;

    Unit selectedUnit;
    Map map;

    bool turn = false;
    bool unitSelected = false;

    float unitsFinished = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(turn == true)
        {
            if (unitSelected)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    selectedUnit.AdjustHP(-.5f);
                }
            }

            
            if (Input.GetKeyDown(KeyCode.Return))
            {
                FinishTurn();

            }

            if(unitsFinished == units.Count)
            {
                turn = false;
                unitsFinished = 0;
                BattleManager.FinishTurn();
            }
            
        }
	}

    public void SetUnits(List<Unit> u)
    {
        units = u;
    }

    public void MoveUnits()
    {
        map.RemovePlayerRange();
        if (selectedUnit)
        {
            selectedUnit.Deselect();
        }
        for (int i = 0; i < units.Count; i++)
        {
            units[i].Move();
        }
    }

    public void UnitFinished()
    {
        unitsFinished++;
    }

    void TileClicked(Vector3 position)
    {

        /*player.transform.position = position + offset;

		//map.UpdatePath (position);
		//map.buildcircle ();
		map.UpdatePathMap ();
		*/

        if (unitSelected)
        {
            //selectedUnit.dest = position + offset;
            map.UpdateUnitPath(position, selectedUnit, true);
        }

        //dest = position + offset;
        //endTurn = true;
    }

    public void UnitSelected(Unit unit)
    {

        if (selectedUnit)
        {
            map.RemovePlayerRange();
            selectedUnit.Deselect();
        }

        //mainCam.GetComponent<CameraManager>().SetTarget(unit.transform);

        unitSelected = true;

        selectedUnit = unit;
        //map.HighlightRadius(unit.moveRange, unit.gameObject.transform.position);
        map.UpdatePathMap(selectedUnit);
        map.ShowPlayerRange(unit.moveRange, unit.transform.position);
    }

    public void ActivateTurn()
    {
        turn = true;
    }

    public void SetMap(Map passedMap)
    {
        map = passedMap;
    }

    void FinishTurn()
    {
        //turn = false;
        unitSelected = false;
        map.RemovePath();
        MoveUnits();
        //BattleManager.FinishTurn();
    }


}
