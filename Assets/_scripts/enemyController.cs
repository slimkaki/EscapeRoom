// //   enemy roamming por : dave game development disponivel em: https://youtu.be/UjkSFoLxesw
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Time Variables
    private float lastTimeIWalked, tempoParado;

    //Attacking
    
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private Vector3 lastPosition;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        lastTimeIWalked = Time.time;
        tempoParado = Time.time;
        lastPosition = this.transform.position;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        // Debug.Log($"last time: {lastTimeIWalked - Time.time} s");

        if (!playerInSightRange && !playerInAttackRange && Time.time  - lastTimeIWalked >= 2f) { Patroling(); lastTimeIWalked = Time.time; } 
        if (!playerInSightRange && playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        lastPosition = this.transform.position;
    }

    private void Patroling()
    {   
        // Debug.Log("Enemy patrolling");
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) {
            if (Vector3.Distance(lastPosition, this.transform.position) == 0f) {
                Vector3 newWayPoint = new Vector3(-this.transform.position.x, -this.transform.position.y, -this.transform.position.z);
                agent.SetDestination(newWayPoint);
            } else {
                agent.SetDestination(walkPoint);
            }
        }
            
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f) { // distanceToWalkPoint.magnitude < 1f && 
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        // Debug.Log("Enemy searching");
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        Debug.DrawLine(transform.position, walkPoint,Color.magenta, 5.0f);
        
        
        Debug.Log(Vector3.Distance(walkPoint,transform.position));
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        // Debug.Log("Enemy chasing");
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {   
        // Debug.Log("Enemy attacking");
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

   
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
