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
    private Animator anim;
    private AIState state;
    private AnimationState animState;
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
        anim = GetComponent<Animator>();
        rng = new System.Random();
        state = AIState.Waiting;
        animState = AnimationState.IdleDown;
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

        if (agent.velocity.x < 0 && Mathf.Abs(agent.velocity.x) >= Mathf.Abs(agent.velocity.z))
        {
            animState = AnimationState.WalkLeft;
        }
        else if (agent.velocity.x > 0 && Mathf.Abs(agent.velocity.x) >= Mathf.Abs(agent.velocity.z))
        {
            animState = AnimationState.WalkRight;
        }
        else if (agent.velocity.z < 0 && Mathf.Abs(agent.velocity.z) > Mathf.Abs(agent.velocity.x))
        {
            animState = AnimationState.WalkDown;
        }
        else if (agent.velocity.z > 0 && Mathf.Abs(agent.velocity.z) > Mathf.Abs(agent.velocity.x))
        {
            animState = AnimationState.WalkUp;
        }
        else if (agent.velocity == Vector3.zero)
        {
            switch (animState)
            {
                case AnimationState.WalkRight:
                    animState = AnimationState.IdleRight;
                    break;
                case AnimationState.WalkLeft:
                    animState = AnimationState.IdleLeft;
                    break;
                case AnimationState.WalkUp:
                    animState = AnimationState.IdleUp;
                    break;
                case AnimationState.WalkDown:
                    animState = AnimationState.IdleDown;
                    break;
            }
        }

        switch (animState)
        {
            case AnimationState.WalkRight:
                anim.Play("WalkRight");
                break;
            case AnimationState.WalkLeft:
                anim.Play("WalkLeft");
                break;
            case AnimationState.WalkUp:
                anim.Play("WalkUp");
                break;
            case AnimationState.WalkDown:
                anim.Play("WalkDown");
                break;
            case AnimationState.IdleRight:
                anim.Play("IdleRight");
                break;
            case AnimationState.IdleLeft:
                anim.Play("IdleLeft");
                break;
            case AnimationState.IdleUp:
                anim.Play("IdleUp");
                break;
            case AnimationState.IdleDown:
                anim.Play("IdleDown");
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
