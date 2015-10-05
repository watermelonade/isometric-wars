using UnityEngine;
using System.Collections;

abstract public class Unit : MonoBehaviour
{
    public int attackDamage = 1;
    public int attackRange = 1;
    public int moveRange = 3;
    public Color oColor;

    public Vector3 startPos;
    internal Vector3 dest;

    public float moveSpeed = 1f;

    public abstract void Move();

    public float tolerance = 0.001f;

    //public abstract void OnMouseClick();


    public void Deselect()
    {
        GetComponent<Renderer>().material.color = oColor;
    }

    public void Select()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }

    void OnMouseDown()
    {
        //gameObject.BroadcastMessage("Unselect");
        GameObject.Find("Main Camera").GetComponent<BattleManager>().SendMessage("UnitSelected", this);
        Select();
    }
}