using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Alert,
    Investigating,
    Waiting,
    Wandering,
    Following
}

public class LegalGaurdian : MonoBehaviour
{
    [SerializeField] private GameObject folder;

    // Fields
    private System.Random rng;
    private NavMeshAgent agent;
    private AIState state;
    private float timeAccumulator;
    private const float WAIT_TIME = 3f;
    private float alertness;            // the percentage of how full the alertness bar is when they spot the player
    private float alertnessSpeed = 0.01f;       // how quickly the alertness bar fills up
    private Transform followTarget;
    [SerializeField] private Transform[] rooms;         // the patrol rooms to randomly search during the wander state
    private int timesWandered;          // the number of times this gaurdian has wandered to a random room
    private int maxWanderTimes = 3;     // the maximum number of times this gaurdian will wander before leaving
    [SerializeField] private Transform exit;            // where the exit is
    [SerializeField] private Transform player;
    private float viewAngle = 45;       // the angle in degrees of this gaurdians field of view measured from the forward direction
    [SerializeField] private Collider exitCol;
    private const float MIN_VOLUME = 3;

    // Methods
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rng = new System.Random();
        state = AIState.Waiting;
    }

    public void Update()
    {
        if (CanSeePlayer())
        {
            state = AIState.Alert;
            agent.destination = player.position;
            timesWandered = 0;
        }

        switch (state)
        {
            case AIState.Alert:
                alertness += alertnessSpeed * Time.deltaTime;
                if (!CanSeePlayer())
                {
                    state = AIState.Investigating;
                    agent.destination = player.position;
                }
                break;
            case AIState.Investigating:
                if (agent.remainingDistance <= 0)
                {
                    timeAccumulator = -WAIT_TIME;
                    state = AIState.Waiting;
                }
                break;
            case AIState.Waiting:
                timeAccumulator += Time.deltaTime;
                if (timeAccumulator > WAIT_TIME)
                {
                    if (timesWandered == maxWanderTimes)
                    {
                        timesWandered = 0;
                        SetFollowTarget(exit);
                    }
                    else
                    {
                        timeAccumulator = 0f;
                        state = AIState.Wandering;
                        Vector3 nextRoom;
                        while ((nextRoom = rooms[rng.Next(rooms.Length)].position) == transform.position) { }
                        agent.destination = nextRoom;
                        timesWandered++;
                    }
                }
                break;
            case AIState.Wandering:
                if (agent.remainingDistance <= 0)
                {
                    timeAccumulator = 0f;
                    state = AIState.Waiting;
                }
                break;
            case AIState.Following:
                agent.destination = followTarget.position;
                break;
        }
    }

    /// <summary>
    /// Sets the destination of this gaurdian's nav mesh agent if the new destination is within a given radius.
    /// </summary>
    /// <param name="transform">The transform of the new destination.</param>
    /// <param name="radius">The radius to check against.</param>
    public void SetDestination(Vector3 position, float volume)
    {
        if (volume > MIN_VOLUME)
        {
            agent.destination = position;
            state = AIState.Investigating;
        }
    }

    /// <summary>
    /// Sets the target that should be followed no matter where they go.
    /// </summary>
    public void SetFollowTarget(Transform target)
    {
        state = AIState.Following;
        followTarget = target;
    }

    /// <summary>
    /// Determines if this gaurdian can see the center of the player.
    /// </summary>
    /// <returns></returns>
    public bool CanSeePlayer()
    {
        Vector3 lineOfSight = player.position - transform.position;
        if (Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z), new Vector2(lineOfSight.x, lineOfSight.z)) < viewAngle)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, lineOfSight, out hit, lineOfSight.magnitude);

            if (hit.collider != null && hit.collider.gameObject == player.gameObject)
            {
                return true;
            }
        }

        return false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other == exitCol && state == AIState.Following)
        {
            state = AIState.Investigating;
            folder.SetActive(false);
        }
    }
}
