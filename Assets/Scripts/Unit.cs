using UnityEngine;
using System.Collections;

abstract public class Unit : MonoBehaviour
{
    public int attackDamage = 1;
    public int attackRange = 1;
    public float hp;
    public float maxHP;
    


    public int moveRange = 4;
    public Color oColor;

    public Vector3 startPos;
    internal Vector3 dest;

    public float moveSpeed = 1f;

    public abstract void Move();

    public float tolerance = 0.001f;

    bool hasInstruction = false;

    //public abstract void OnMouseClick();


    public void Deselect()
    {
        GetComponent<Renderer>().material.color = oColor;
    }

    public void Select()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }

    public float GetHP()
    {
        return hp;
    }

    public float GetMaxHP()
    {
        return maxHP;
    }

    public void SetMaxHP(float max)
    {
        maxHP = max;
    }

    public void AdjustHP(float adjustment)
    {
        
        if (adjustment < 0 && hp - adjustment < 0)
        {
            hp = 0;
        }
        else if (adjustment > 0 && hp + adjustment > maxHP)
        {
            hp = maxHP;
        }
        else
        { 
            hp += adjustment;
        }
    }

    void OnMouseDown()
    {
        //gameObject.BroadcastMessage("Unselect");
        GameObject.Find("Main Camera").GetComponent<BattleManager>().SendMessage("UnitSelected", this);

        Select();
    }

    
}