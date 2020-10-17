using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float detectionRange;
    public float attackRange;
    public float walkSpeed;
    public float runSpeed;
    public float timeToReset;

    private NavMeshAgent agent;
    private SphereCollider detector;
    private State state;
    private Transform target;
    private Vector3 startPos;
    private bool stateChanged;
    private float resetTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        detector = GetComponent<SphereCollider>();

        detector.radius = detectionRange;
        state = State.Idle;
        stateChanged = false;
        startPos = transform.position;
        agent.speed = walkSpeed;
    }
    
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (stateChanged)
                {
                    Debug.Log("is idle");
                    agent.SetDestination(startPos);
                    agent.stoppingDistance = 0f;
                    agent.speed = walkSpeed;
                    agent.isStopped = true;
                    stateChanged = false;
                    resetTime = Time.time + timeToReset;
                }
                else
                {
                    Debug.Log("still idle "+ agent.isStopped);
                    if (Time.time > resetTime && agent.isStopped)
                    {
                        agent.isStopped = false;
                    }
                }
                break;
            case State.Chasing:
                if (stateChanged)
                {
                    Debug.Log("is chasing ");
                    agent.stoppingDistance = attackRange;
                    agent.SetDestination(target.position);
                    agent.speed = runSpeed;
                    stateChanged = false;
                    agent.isStopped = false;
                }
                else
                {
                    if (Vector3.Distance(transform.position, target.position) <= attackRange)
                    {
                        state = State.Attacking;
                        stateChanged = true;
                    } else
                    {
                        Debug.Log("still chasing" + agent.isStopped + agent.speed);
                        agent.SetDestination(target.position);
                    }
                }
                break;

            case State.Attacking:
                if (stateChanged)
                {
                    stateChanged = false;
                }
                else
                {
                    if (!target)
                    {
                        state = State.Idle;
                    }
                    else if (Vector3.Distance(transform.position, target.position) > attackRange)
                    {
                        state = State.Chasing;
                    }
                    else
                    {
                        Debug.Log("Attacking");
                    }
                }
                
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player detected");
            state = State.Chasing;
            stateChanged = true;
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player lost");
            state = State.Idle;
            target = null;
            stateChanged = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

enum State
{
    Idle,
    Chasing,
    Attacking
}
