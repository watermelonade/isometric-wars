using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    List<Unit> units;

    Unit selectedUnit;
    //Map map;

    bool turn = false;
    bool unitSelected = false;

    float unitsFinished = 0;

    public PlayerState state;
    public static Plane mouseInputPlane;

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
        mouseInputPlane = new Plane(Vector3.zero + new Vector3(0, 3, 0),new Vector3(-1, .6f,0));
        
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
                BattleManager.map.RemovePath();
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
        BattleManager.map.RemovePlayerRange();
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
                BattleManager.map.UpdateUnitPath(position, selectedUnit, true);
                BattleManager.map.RemovePlayerRange();
                selectedUnit.Move();
            }
        }
    }

    public void UnitChoosing()
    {
        state = PlayerController.PlayerState.UnitChoiceMenu;
        BattleManager.map.RemovePath();
    }

    public void UnitSelected(Unit unit)
    {
        if (state == PlayerState.ChooseUnit)
        {
            if (selectedUnit)
            {
                BattleManager.map.RemovePlayerRange();
                selectedUnit.Deselect();
            }

            state = PlayerState.UnitSelected;

            unitSelected = true;

            selectedUnit = unit;

            BattleManager.map.UpdatePathMap(selectedUnit);
            BattleManager.map.ShowPlayerRange(unit.moveRange, unit.transform.position);
        }
    }

    public void ActivateTurn()
    {
        turn = true;
        state = PlayerState.ChooseUnit;
    }

    void FinishTurn()
    {
        //turn = false;
        unitSelected = false;
        BattleManager.map.RemovePath();
        MoveUnits();
        //BattleManager.FinishTurn();
    }

    public void UnitDied(GameObject obj)
    {
        units.Remove(obj.GetComponent<Unit>());
    }
}
