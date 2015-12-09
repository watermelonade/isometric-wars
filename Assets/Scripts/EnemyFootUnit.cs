using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EnemyFootUnit : Unit {

    int range = 3;
    public int attRange = 3;
    int tilesMoved = 0;
    Vector3 velocity = Vector3.one;

    float locHP = 10;
    float speed = .7f;
    float timeStartedMoving;
    float timeOfMovement = .8f;

    public EnemySight sight;
    private Gun gun;
    public string unitName = "enemy_unit";

	List<Unit> prey;

    public Stack<Vector3> path;
    private EnemyAI ai;

    private bool active = false;

    public enum EnemyObjective
    {
        Advance,
        Retreat, 
        GetHealth,
        Attack,
        FindCover,
        Idle
    }

    EnemyObjective objective;

    public override void SetPath(Stack<Vector3> stack)
    {
        path = stack;
    }

    // Use this for initialization
    void Start () {

        gun = gameObject.GetComponent<Gun>();
        ai = gameObject.GetComponent<EnemyAI>();
        sight = gameObject.GetComponent<EnemySight>();
        sight.enabled = false;
        objective = EnemyObjective.Idle;

        gameObject.name = unitName;
        SetMaxHP(locHP);
        AdjustHP(locHP);
        SetAttackRange(attRange);
	}
	
	// Update is called once per frame
	void Update () {

        if(active == true)
            objective = ai.Decide(this);

		switch (objective) {
		    case EnemyObjective.Advance:
                //sight.enabled = false;
                //gameObject.GetComponent<SphereCollider> ().radius = range;
                Move();
			    break;

		    case EnemyObjective.Attack:
                gameObject.GetComponent<SphereCollider>().enabled = false;
                Attack();
			    break;
        
		    case EnemyObjective.FindCover:
                //gameObject.GetComponent<SphereCollider>().radius = .5F;
                break;

            case EnemyObjective.GetHealth:
                break;

            case EnemyObjective.Idle:
                break;

            case EnemyObjective.Retreat:
                break;
		}
	}

    internal float ChanceToHit()
    {
        return 1;
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

    internal void Die()
    {
        Camera.main.GetComponent<EnemyController>().UnitDied(gameObject);
        Destroy(gameObject);
    }

    public override void Move()
    {
        /*if(path != null)
        {
            state = UnitState.Moving;
            timeStartedMoving = Time.time;
            startPos = transform.position;
            dest = path.Pop();
        }*/

        float timeSinceStarted = Time.time - timeStartedMoving;
        float percentageComplete = timeSinceStarted / timeOfMovement;
        transform.position = Vector3.Lerp(startPos, dest, percentageComplete);


        if (percentageComplete >= 1.0f)
        {
            if (tilesMoved == range || path.Count == 0)
            {
                //Finish();
                objective = EnemyObjective.Attack;
                active = false;
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

    public override void Finish()
    {
        path = null;
        tilesMoved = 0;
        state = UnitState.Idle;
        objective = EnemyObjective.Idle;
        active = false;
        sight.enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = true;
        gameObject.GetComponent<SphereCollider>().radius = .5f;
        tilesMoved = 0;
        Camera.main.GetComponent<EnemyController>().UnitFinished();
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Bullet>() && objective != EnemyObjective.Attack)
        {
            AdjustHP(-1);
        }
    }

    internal override void Attack()
    {
        while(gun.Fire( EnemyController.GetClosestPlayerUnitPos(this) ));

        Finish();
    }

    public void Act()
    {
        active = true;
        timeStartedMoving = Time.time;
        startPos = transform.position;
        sight.enabled = true;
        
        BattleManager.map.UpdatePathMapAvoidClaimedSpaces(this, EnemyController.units);
        Vector3 closestPlayerPos = EnemyController.GetClosestPlayerUnitPos(this);

        

        if (!BattleManager.map.UpdateUnitPath(closestPlayerPos, this, false, 1))
        {
            Finish();
        }

        dest = path.Pop();
    }

}
