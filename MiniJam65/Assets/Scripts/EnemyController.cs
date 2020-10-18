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
    public float timeBetweenAttacks;

    private NavMeshAgent agent;
    private SphereCollider detector;
    private Animator anim;
    private State state;
    private Transform target = null;
    private Vector3 startPos;
    private bool stateChanged;
    private float resetTime = 0f;
    private float nextAttackTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        detector = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();

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
                    anim.SetBool("IsWalking", false);
                    agent.SetDestination(startPos);
                    agent.stoppingDistance = 0f;
                    agent.speed = walkSpeed;
                    agent.isStopped = true;
                    stateChanged = false;
                    resetTime = Time.time + timeToReset;
                }
                else
                {
                    if (Time.time > resetTime && agent.isStopped)
                    {
                        anim.SetBool("IsWalking", true);
                        agent.isStopped = false;
                    }
                    else if (agent.isStopped)
                    {
                        anim.SetBool("IsWalking", false);
                    }
                }
                break;
            case State.Chasing:
                if (stateChanged)
                {
                    anim.SetBool("IsWalking", true);
                    agent.stoppingDistance = attackRange - 0.3f;
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
                        agent.SetDestination(target.position);
                    }
                }
                break;

            case State.Attacking:
                if (stateChanged)
                {
                    anim.SetBool("IsWalking", false);
                    stateChanged = false;
                }
                else
                {
                    if (!target)
                    {
                        state = State.Idle;
                        stateChanged = true;
                    }
                    else if (Vector3.Distance(transform.position, target.position) > attackRange)
                    {
                        state = State.Chasing;
                        stateChanged = true;
                    }
                    else
                    {
                        if (Time.time > nextAttackTime)
                        {
                            anim.SetTrigger("Attack");
                            nextAttackTime = Time.time + timeBetweenAttacks;
                        }
                    }
                }
                
                break;
        }
    }

    public void GiveTarget(Transform newTarget)
    {
        state = State.Chasing;
        stateChanged = true;
        target = newTarget;
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
