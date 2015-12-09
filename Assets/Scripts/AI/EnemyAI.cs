using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Priority_Queue;

public class EnemyAI : MonoBehaviour {

    EnemySight sight;
    Dictionary<EnemyFootUnit.EnemyObjective, float> rewards;

    EnemyFootUnit.EnemyObjective PriorityObjective
    {
        
        get
        {
            EnemyFootUnit.EnemyObjective objective = EnemyFootUnit.EnemyObjective.Idle;
            var enumerator = rewards.GetEnumerator();
            float priority = enumerator.Current.Value;
            
            while (enumerator.MoveNext())
            {
                if(enumerator.Current.Value > priority)
                {
                    priority = enumerator.Current.Value;
                    objective = enumerator.Current.Key;
                }
            }
            return objective;
        }
    }

    public void Awake()
    {
        rewards = new Dictionary<EnemyFootUnit.EnemyObjective, float>();
        sight = gameObject.GetComponent<EnemySight>();
    }

    public EnemyFootUnit.EnemyObjective Decide(Unit unit)
    {
        EnemyFootUnit fu = unit.GetComponent<EnemyFootUnit>();

        float health = fu.GetHP();
        float maxHealth = fu.GetMaxHP();
        float range = fu.GetAttackRange();
        Vector3 position = unit.gameObject.transform.position;
        float distanceToNearestEnemy = sight.DistanceToClosestUnit();
        float numEnemiesInRange = sight.NumEnemiesInRange();
        float numFriendsInRange = sight.NumFriendsInRange();

        //Advance = C1 * health/maxHealth + C2 * distanceToNearestEnemy.normalized/range
        float C1 = 1, C2 = 1, C3 = 1, C4 = 1, C5 = 1, C6 = 1, C7 = 1;
        float advanceReward = C1* health / maxHealth + C2 * distanceToNearestEnemy / range;
        rewards[EnemyFootUnit.EnemyObjective.Advance] = advanceReward;

        //Retreat = C3 * maxHealth/health + C4 * range/distanteToNearestEnemy.normalized + C5 * numEnemiesInRange/numFriendsInRange
        float retreatReward = C3* maxHealth / health + C4 * range / distanceToNearestEnemy + C5 * numEnemiesInRange / numFriendsInRange;
        rewards[EnemyFootUnit.EnemyObjective.Retreat] = retreatReward;
        
        //GetHealth;
        rewards[EnemyFootUnit.EnemyObjective.GetHealth] = 0;

        //Attack = C6 * distanceToNearestEnemy + C6 * chanceToHit() 
        float attackReward = C6 * 1/distanceToNearestEnemy + C7 * fu.ChanceToHit() + C5 * numEnemiesInRange;
        rewards[EnemyFootUnit.EnemyObjective.Attack] = attackReward;

        //FindCover; 
        rewards[EnemyFootUnit.EnemyObjective.FindCover] = 0;

        return PriorityObjective;

    }
}
