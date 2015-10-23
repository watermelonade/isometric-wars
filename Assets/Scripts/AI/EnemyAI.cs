using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {


    public EnemyFootUnit.EnemyObjective Decide(Unit unit)
    {
        EnemyFootUnit fu = unit.GetComponent<EnemyFootUnit>();

        float health = fu.GetHP();
        float maxHealth = fu.GetMaxHP();
        float range = fu.GetAttackRange();
        Vector3 position = unit.gameObject.transform.position;
        float distanceToNearestEnemy = fu.DistanceToClosestUnit();
        float numEnemiesInRange = fu.NumEnemiesInRange();
        float numFriendsInRange = fu.NumFriendsInRange();

        SortedDictionary<EnemyFootUnit.EnemyObjective, float> rewards = new SortedDictionary<EnemyFootUnit.EnemyObjective, float>();

        //Advance = C1 * health/maxHealth + C2 * distanceToNearestEnemy.normalized/range
        float C1 = 1, C2 = 1, C3 = 1, C4 = 1, C5 = 1, C6 = 1, C7 = 1;
        rewards[EnemyFootUnit.EnemyObjective.Advance] = C1 * health / maxHealth + C2 * distanceToNearestEnemy / range;
        
        //Retreat = C3 * maxHealth/health + C4 * range/distanteToNearestEnemy.normalized + C5 * numEnemiesInRange/numFriendsInRange
        rewards[EnemyFootUnit.EnemyObjective.Retreat] = C3 * maxHealth / health + C4 * range / distanceToNearestEnemy + C5 * numEnemiesInRange / numFriendsInRange;
        
        //GetHealth;
        rewards[EnemyFootUnit.EnemyObjective.GetHealth] = 0;

        //Attack = C6 * distanceToNearestEnemy + C6 * chanceToHit() 
        rewards[EnemyFootUnit.EnemyObjective.Attack] = C6 * distanceToNearestEnemy + C7 * fu.ChanceToHit();

        //FindCover; 
        rewards[EnemyFootUnit.EnemyObjective.FindCover] = 0;

        return rewards.GetEnumerator().Current.Key;

    }
}
