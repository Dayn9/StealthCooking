﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    // Fields
    private NavMeshAgent agent;
    private AIState state;
    [SerializeField] private Transform[] path;
    private int currentPathIndex;
    private const float DIST_SLOP = 0.1f;
    private float timeAccumulator;
    private const float WAIT_TIME = 3;
    [SerializeField] private Transform player;
    private float viewAngle = 45;
    private float barkVolume = 1;

    // Methods
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = AIState.Wandering;
    }

    public void Update()
    {
        if (CanSeePlayer())
        {
            agent.isStopped = true;
            //SoundManager.AddSound(barkVolume, transform.position);
            state = AIState.Waiting;
            timeAccumulator = 0;
        }
        else
        {
            agent.isStopped = false;
        }

        switch (state)
        {
            case AIState.Wandering:
                if (agent.remainingDistance <= DIST_SLOP)
                {
                    state = AIState.Waiting;
                    timeAccumulator = 0;
                    agent.destination = transform.position;
                }
                break;
            case AIState.Waiting:
                timeAccumulator += Time.deltaTime;
                if (timeAccumulator >= WAIT_TIME)
                {
                    timeAccumulator = 0;
                    state = AIState.Wandering;
                    currentPathIndex = (currentPathIndex + 1) % path.Length;
                    agent.destination = path[currentPathIndex].position;
                }
                break;
        }
    }

    /// <summary>
    /// Determines if this dog can see the center of the player.
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
}