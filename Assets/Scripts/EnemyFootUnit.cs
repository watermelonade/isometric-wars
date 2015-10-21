using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EnemyFootUnit : Unit {

    int range = 3;
    int aRange = 3;
    int tilesMoved = 0;
    Vector3 velocity = Vector3.one;

    float locHP = 10;
    float speed = .7f;
    float timeStartedMoving;
    float timeOfMovement = .8f;

    public string unitName = "enemy_unit";

	List<Unit> prey;

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
        //gameObject.GetComponent<SphereCollider>().radius = 0;
        gameObject.name = unitName;
        SetMaxHP(locHP);
        AdjustHP(locHP);
        SetAttackRange(aRange);
	}
	
	// Update is called once per frame
	void Update () {

		switch (state) {
		case UnitState.Moving:
			gameObject.GetComponent<SphereCollider> ().radius = range;
			float timeSinceStarted = Time.time - timeStartedMoving;
			float percentageComplete = timeSinceStarted / timeOfMovement;
			transform.position = Vector3.Lerp (startPos, dest, percentageComplete);

			if (percentageComplete >= 1.0f) {
				if (tilesMoved == range || path.Count == 0) {
					//state = UnitState.Attacking;
					Finish ();
				} else {
					percentageComplete = 0f;
					timeStartedMoving = Time.time;
					tilesMoved++;
					dest = path.Pop ();
					startPos = transform.position;
				}
                
			}
			break;

		/*case UnitState.Attacking:
			foreach ( Unit x in prey)
				x.AdjustHP(-attackDamage);
			Finish ();
			break;*/
        
		case UnitState.Idle:
			break;
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

    void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.name == "player_unit" && state == UnitState.Moving)
        {
            
			col.gameObject.GetComponent<Unit>().AdjustHP(-attackDamage);
        }
        
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
        path = null;
        tilesMoved = 0;
        state = UnitState.Idle;
        gameObject.GetComponent<SphereCollider>().radius = 0;
        tilesMoved = 0;
        Camera.main.GetComponent<EnemyController>().UnitFinished();
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Bullet>())
        {
            AdjustHP(-1);
        }
    }

    internal override void Attack()
    {
        throw new NotImplementedException();
    }
}
