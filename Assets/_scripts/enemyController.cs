// //   enemy roamming por : dave game development disponivel em: https://youtu.be/UjkSFoLxesw
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class enemyController : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    

    //Patroling
    public Vector3 walkPoint, noise_walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    // Time Variables
    public float lastTimeIWalked, tempoParado;

    //Attacking
    
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private Vector3 lastPosition;
    int isWalkingHash,isRunningHash;
    Animator animator;

    public bool walkingBool, runningBool, heard_something;
    public float heard_time;

    private float timeAtk;
    private bool attacking;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        lastTimeIWalked = Time.time;
        tempoParado = Time.time;
        lastPosition = this.transform.position;
        walkingBool = false;
        heard_something = false;
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        isWalkingHash  = Animator.StringToHash("isWalkingEnemy");
        isRunningHash = Animator.StringToHash("isRunningEnemy");
        Debug.Log(animator);
        timeAtk = Time.time;
        attacking = false;
    }

    private void LateUpdate()
    {
        
        bool isWalking = animator.GetBool(isWalkingHash);

        bool isRunning = animator.GetBool(isRunningHash);

        // if (heard_something && Time.time - heard_time <= 5.0f) {
        //     agent.SetDestination(noise_walkPoint);
        //     SearchWalkPoint();
        // }
        //Check for sight and attack range
        // playerInSightRange         // playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        // Debug.Log($"last time: {lastTimeIWalked - Time.time} s");
        if (Vector3.Distance(transform.position, walkPoint) == 0) { animator.SetBool(isWalkingHash,false); }
        if (!playerInSightRange && !playerInAttackRange && Time.time  - lastTimeIWalked >= 2f) { 
            Patroling(); 
            lastTimeIWalked = Time.time;
            animator.SetBool(isRunningHash,false);
            
        }

        if (playerInSightRange){ 
            ChasePlayer();
            animator.SetBool(isRunningHash,true);
        }
        if (playerInAttackRange) AttackPlayer();

        lastPosition = this.transform.position;
    }

    private void Patroling()
    {   
        attacking = false;
        animator.SetBool(isWalkingHash,true); 
        // Debug.Log("Enemy patrolling");

        if (walkPointSet && Vector3.Distance(lastPosition, this.transform.position) == 0f ) {
            Vector3 newWayPoint = new Vector3(-this.transform.position.x, -this.transform.position.y, -this.transform.position.z);
            walkPoint = newWayPoint;
        }
        
        if (!walkPointSet) SearchWalkPoint();
        
        agent.SetDestination(walkPoint);
            
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
        animator.SetBool(isRunningHash,true);
        // Debug.Log("Enemy attacking");
        //Make sure enemy doesn't move
        if (!attacking){
            attacking = true;
            timeAtk = Time.time;
        } else if (Time.time - timeAtk >= 0.5f){
            Debug.Log("Vc perdeu");
            Cursor.lockState = CursorLockMode.None; 
            SceneManager.LoadScene(3);
        }
        


        // transform.LookAt(player);
        
   
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


}
