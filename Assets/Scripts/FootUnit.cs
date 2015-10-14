using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class FootUnit : Unit
{

    int ap = 12;

    

    //public Vector3 dest;
    bool act;
    Vector3 velocity = Vector3.one;
    float locHP = 10;
    float speed = .1f;

    float timeStartedMoving;
    float timeOfMovement = .2f;
    //Vector3 startPos;

    bool choosing = false;
    bool moving = false;
    public bool hasMoved = false;

    public Stack<Vector3> path;// = new Stack<Vector3>();

    private Unit target;

    void Start()
    {
        gameObject.GetComponent<SphereCollider>().radius = 0;
        SetMaxHP(locHP);
        AdjustHP(locHP);
        SetAttackRange(3);
    }

    void Update()
    {
        if (act)
        {

            if (moving)
            {
                float timeSinceStarted = Time.time - timeStartedMoving;
                float percentageComplete = timeSinceStarted / timeOfMovement;

                transform.position = Vector3.Lerp(startPos, dest, percentageComplete);//Vector3.SmoothDamp(transform.position, dest, ref velocity, 1.5f);

                if (percentageComplete >= 1.0f)
                {
                    if (path.Count != 0)
                    {
                        percentageComplete = 0f;
                        timeStartedMoving = Time.time;
                        dest = path.Pop();
                        startPos = transform.position;
                    }
                    else
                    {
                        moving = false;
                        hasMoved = true;
                        Camera.main.GetComponent<PlayerController>().UnitChoosing();
                        gameObject.GetComponent<SphereCollider>().radius = attackRange;
                        gameObject.AddComponent<ChoiceMenu>();
                    }
                }
            }

        }

    }

    public override void Finish()
    {
        path = null;
        act = false;
        hasMoved = false;
        gameObject.GetComponent<SphereCollider>().radius = 0;
        Destroy(gameObject.GetComponent<ChoiceMenu>());
        Camera.main.GetComponent<PlayerController>().UnitFinished();
    }

    public override void Move()
    {
        if (path != null) {
            Camera.main.GetComponent<PlayerController>().state = PlayerController.PlayerState.UnitMoving;
            act = true;
            moving = true;
            timeStartedMoving = Time.time;
            startPos = transform.position;
            dest = path.Pop();
        } else
        {
            Camera.main.GetComponent<PlayerController>().UnitFinished();
        }
    }

    private bool vEquals(Vector3 x, Vector3 y)
    {
        bool ret = true;
        if (Mathf.Abs(transform.position.x - dest.x) > tolerance)
            ret = false;
        if (Mathf.Abs(transform.position.y - dest.y) > tolerance)
            ret = false;
        if (Mathf.Abs(transform.position.z - dest.z) > tolerance)
            ret = false;

        return ret;

    }

    public override void SetPath(Stack<Vector3> p)
    {
        path = p;
    }

    public void OnMouseDown()
    {
        GameObject.Find("Main Camera").GetComponent<PlayerController>().SendMessage("UnitSelected", this);

        Select();
    }

    void OnTriggerEnter(Collider col)
    {
        if(hasMoved == true && !target)
        {
            if (col.gameObject.name == "enemy")
            {
                target = col.gameObject.GetComponent<Unit>();
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.name == "enemy")
            target = null;
    }

    internal override void Attack()
    {
        if (target)
        {
            target.AdjustHP(-attackDamage);
        }
    }
}