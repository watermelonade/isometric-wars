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

    public PlayerState state;

    public enum PlayerState
    {
        ChooseUnit,
        UnitSelected,
        UnitMoving,
        UnitChoiceMenu,
        Finish
    }

	// Use this for initialization
	void Start () {
        state = PlayerState.ChooseUnit;
	}
	
	// Update is called once per frame
	void Update () {
	    if(turn == true)
        {
            if (state == PlayerState.UnitSelected)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    selectedUnit.AdjustHP(-.5f);
                }
            }

            if(unitsFinished == units.Count)
            {
                unitSelected = false;
                map.RemovePath();
                turn = false;
                unitsFinished = 0;
                state = PlayerState.Finish;
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
        state = PlayerState.ChooseUnit;
    }

    void TileClicked(Vector3 position)
    {

        if (unitSelected)
        {
            if (state == PlayerState.UnitSelected)
            {
                
                map.UpdateUnitPath(position, selectedUnit, true);
                map.RemovePlayerRange();
                selectedUnit.Move();
            }
        }
    }

    public void UnitChoosing()
    {
        state = PlayerController.PlayerState.UnitChoiceMenu;
        map.RemovePath();
    }

    public void UnitSelected(Unit unit)
    {
        if (state == PlayerState.ChooseUnit)
        {
            if (selectedUnit)
            {
                map.RemovePlayerRange();
                selectedUnit.Deselect();
            }

            state = PlayerState.UnitSelected;

            unitSelected = true;

            selectedUnit = unit;

            map.UpdatePathMap(selectedUnit);
            map.ShowPlayerRange(unit.moveRange, unit.transform.position);
        }
    }

    public void ActivateTurn()
    {
        turn = true;
        state = PlayerState.ChooseUnit;
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
