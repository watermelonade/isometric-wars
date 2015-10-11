using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EnemyFootUnit : Unit {

    float range = 3f;
    float tilesMoved = 0;
    Vector3 velocity = Vector3.one;

    float locHP = 10;
    float speed = .7f;

    Vector3 start;

    bool act;

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
        if (act)
        {
            if (tilesMoved < range)
            {
                if (!vEquals(transform.position, dest))
                {
                    transform.position = Vector3.Lerp(startPos, dest, Time.time);//Vector3.SmoothDamp(transform.position, dest, ref velocity, 1.5f);
                }
                else if (path.Count != 0)
                {
                    tilesMoved++;
                    dest = path.Pop();
                    startPos = transform.position;
                }
                else
                {
                    act = false;
                    tilesMoved = 0;
                }
            } else
            {
                path = null;
                act = false;
                tilesMoved = 0;
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
            act = true;
            start = transform.position;
            dest = path.Pop();
        }
    }
}
