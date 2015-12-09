using UnityEngine;
using System.Collections;
using System;

public class EnemySight : MonoBehaviour
{

    float fieldOfViewAngle = 360;
    public bool playerInSight = false;
    public Vector3 personalLastSighting;

    private NavMeshAgent nav;
    private SphereCollider col;
    private Animator anim;
    private Vector3 lastPlayerSighting;
    private GameObject[] players;
    private GameObject[] friends;

    private Animator playerAnim;
    private float playerHealth;
    //private HashIDs hash;
    private Vector3 previousSighting;

    void Awake()
    {
        // Setting up the references.
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
        //lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
        players = GameObject.FindGameObjectsWithTag("player");
        friends = GameObject.FindGameObjectsWithTag("enemy");
        //playerAnim = player.GetComponent<Animator>();
        //playerHealth = player.GetComponent<PlayerHealth>();
        //hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();

        // Set the personal sighting and the previous sighting to the reset position.
        //personalLastSighting = lastPlayerSighting.resetPosition;
        //previousSighting = lastPlayerSighting.resetPosition;
    }

    internal float NumFriendsInRange()
    {
        float numFriends = 0;
        foreach(GameObject friend in friends)
        {
            if(friend != null)
            {
                if(Vector3.Distance(friend.transform.position,gameObject.transform.position) < gameObject.GetComponent<EnemyFootUnit>().attackRange)
                    numFriends++;
            }

        }
        return numFriends;

    }

    internal float NumEnemiesInRange()
    {
        float numPlayers = 0;
        foreach (GameObject player in players)
        {
            if (player != null)
            {
                if (Vector3.Distance(player.transform.position, gameObject.transform.position) < gameObject.GetComponent<EnemyFootUnit>().attackRange)
                    numPlayers++;
            }

        }
        return numPlayers;
    }

    void Update()
    {
        // If the last global sighting of the player has changed...
        if (lastPlayerSighting != previousSighting)
            // ... then update the personal sighting to be the same as the global sighting.
            personalLastSighting = lastPlayerSighting;

        // Set the previous sighting to the be the sighting from this frame.
        previousSighting = lastPlayerSighting;

        // If the player is alive...
        /*if (playerHealth.health > 0f)
            // ... set the animator parameter to whether the player is in sight or not.
            anim.SetBool(hash.playerInSightBool, playerInSight);
        else
            // ... set the animator parameter to false.
            anim.SetBool(hash.playerInSightBool, false);*/
    }

    void OnTriggerStay(Collider other)
    {
        // If the player has entered the trigger sphere...
        foreach (GameObject player in players)
        {
            if (other.gameObject == player)
            {
                // By default the player is not in sight.
                playerInSight = false;

                // Create a vector from the enemy to the player and store the angle between it and forward.
                Vector3 direction = other.transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);

                // If the angle between forward and where the player is, is less than half the angle of view...
                if (angle < fieldOfViewAngle * 0.5f)
                {
                    RaycastHit hit;

                    // ... and if a raycast towards the player hits something...
                    if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                    {
                        // ... and if the raycast hits the player...
                        if (hit.collider.gameObject == player)
                        {
                            // ... the player is in sight.
                            playerInSight = true;

                            // Set the last global sighting is the players current position.
                            lastPlayerSighting = player.transform.position;
                        }
                    }
                }

                /*// Store the name hashes of the current states.
                int playerLayerZeroStateHash = playerAnim.GetCurrentAnimatorStateInfo(0).nameHash;
                int playerLayerOneStateHash = playerAnim.GetCurrentAnimatorStateInfo(1).nameHash;

                // If the player is running or is attracting attention...
                if (playerLayerZeroStateHash == hash.locomotionState || playerLayerOneStateHash == hash.shoutState)
                {
                    // ... and if the player is within hearing range...
                    if (CalculatePathLength(player.transform.position) <= col.radius)
                        // ... set the last personal sighting of the player to the player's current position.
                        personalLastSighting = player.transform.position;
                }*/
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // If the player leaves the trigger zone...
        foreach (GameObject player in players)
        {
            if (other.gameObject == player)
                // ... the player is not in sight.
                playerInSight = false;
        }
    }

    float CalculatePathLength(Vector3 targetPosition)
    {
        // Create a path and set it based on a target position.
        NavMeshPath path = new NavMeshPath();
        if (nav.enabled)
            nav.CalculatePath(targetPosition, path);

        // Create an array of points which is the length of the number of corners in the path + 2.
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        // The first point is the enemy's position.
        allWayPoints[0] = transform.position;

        // The last point is the target position.
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        // The points inbetween are the corners of the path.
        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        // Create a float to store the path length that is by default 0.
        float pathLength = 0;

        // Increment the path length by an amount equal to the distance between each waypoint and the next.
        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }

    public float DistanceToClosestUnit()
    {
        float closestDistance = Mathf.Infinity;

        foreach(GameObject player in players)
        {
            //if this unit is not dead
            if(player != null)
            {
                float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);

                if(distance < closestDistance)
                {
                    closestDistance = distance;
                }
            }
        }

        return closestDistance;
    }
}