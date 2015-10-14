using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EnemyFootUnit : Unit {

    int range = 3;
    int tilesMoved = 0;
    Vector3 velocity = Vector3.one;

    float locHP = 10;
    float speed = .7f;
    float timeStartedMoving;
    float timeOfMovement = .8f;

    //Vector3 startPos;

	//used in old bool implementation, now using state enum
    //bool act;
	

    public Stack<Vector3> path;

    public override void SetPath(Stack<Vector3> stack)
    {
        path = stack;
    }

    // Use this for initialization
    void Start () {

        SetMaxHP(locHP);
        AdjustHP(locHP);
	}
	
	// Update is called once per frame
	void Update () {
        if (state == UnitState.Moving)
        {
            float timeSinceStarted = Time.time - timeStartedMoving;
            float percentageComplete = timeSinceStarted / timeOfMovement;

            transform.position = Vector3.Lerp(startPos, dest, percentageComplete);//Vector3.SmoothDamp(transform.position, dest, ref velocity, 1.5f);

            if (percentageComplete >= 1.0f)
            {
                if(tilesMoved == range || path.Count == 0)
                {
                    path = null;
                    tilesMoved = 0;
					state = UnitState.Idle;
                    Camera.main.GetComponent<EnemyController>().UnitFinished();
                    tilesMoved = 0;
                }
                else
                {
                    percentageComplete = 0f;
                    timeStartedMoving = Time.time;
                    tilesMoved++;
                    dest = path.Pop();
                    startPos = transform.position;
                }
                
            }
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

    public override void Move()
    {
        if(path != null)
        {
            state = UnitState.Moving;
            timeStartedMoving = Time.time;
            startPos = transform.position;
            dest = path.Pop();
        }
    }

    public override void Finish()
    {
        throw new NotImplementedException();
    }

    internal override void Attack()
    {
        throw new NotImplementedException();
    }
}
