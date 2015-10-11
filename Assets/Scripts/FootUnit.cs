using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class FootUnit : Unit
{

    //public Vector3 dest;
    bool act;
    Vector3 velocity = Vector3.one;
    float locHP = 10;
    float speed = .1f;

    float timeStartedMoving;
    float timeOfMovement = .8f;
    //Vector3 startPos;

    public Stack<Vector3> path;// = new Stack<Vector3>();

    void Start()
    {
        SetMaxHP(locHP);
        AdjustHP(locHP);
    }

    void Update()
    {
        if (act)
        {
            /*float dist = Vector3.Distance(transform.position, dest);
            for(float i = 0.0f; i < 1.0; i+=(speed * Time.deltaTime) / dist)
            {
                transform.position = Vector3.Lerp(transform.position, dest, i);
                
            }*/

            /*(if (!vEquals(transform.position, dest) )
            {
                transform.position = Vector3.Lerp(startPos, dest, speed*Time.time);//Vector3.SmoothDamp(transform.position, dest, ref velocity, 1.5f);
            } else */

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
                    Camera.main.GetComponent<PlayerController>().UnitFinished();
                    path = null;
                    act = false;
                }
            }
        }

    }

    public override void Move()
    {
        
        if (path != null) {
            act = true;
            timeStartedMoving = Time.time;
            startPos = transform.position;
            dest = path.Pop();
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
        //gameObject.BroadcastMessage("Unselect");
        GameObject.Find("Main Camera").GetComponent<PlayerController>().SendMessage("UnitSelected", this);

        Select();
    }
}