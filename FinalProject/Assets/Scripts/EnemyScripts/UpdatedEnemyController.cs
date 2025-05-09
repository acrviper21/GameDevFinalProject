using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class UpdatedEnemyController : MonoBehaviour
{
    [Header("Movement")]
    NavMeshAgent navMeshAgent;
    [SerializeField] Transform playerTransform;
    bool isPatrolling = false;
    bool ischasingPlayer = false;
    [SerializeField] float patrolCoolDown = 1f;
    float patrolCoolDownTimer = 0f;


    [Header("Move Points")]
    [SerializeField] List<Transform> movePoints;
    int currentMovePointIndex = 0;
    int nextMovePointIndex = 0;
   
    [Header("Attack")]
    [SerializeField] GameObject enemyAttack;
    [SerializeField] float attackCoolDown = 1f;
    float attackTimer = 0f;
    bool canAttack = false;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //FindPlayer();
        //Have enemy start off patrolling
        isPatrolling = true;
        ischasingPlayer = false;
    }
    void Start()
    {
        //Allow player to move to patrol point
        navMeshAgent.isStopped = false;
        GetMovePoints();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPatrolling && navMeshAgent.isStopped)
        {
            CoolDownPatrolMode();
        }

        if(isPatrolling && !navMeshAgent.isStopped)
        {
            MoveToPatrolPoints();
        }

        // if(ischasingPlayer)
        // {
        //     CheckDistanceFromPlayer();
        // }

        if(ischasingPlayer /*&& !navMeshAgent.isStopped*/)
        {
            FindPlayer();
            if(!canAttack)
            {
                attackTimer -= Time.deltaTime;
                if(attackTimer <= 0)
                {
                    canAttack = true;
                }
                return;
            }
            AttackPlayer();
        }
        
        //If still patrolling and at new patrol point then wait a few seconds and move again
        // if(isPatrolling && !canMoveWhilePatrolling)
        // {
        //     Debug.Log("Cooling: " + canMoveWhilePatrolling);
        //     patrolCoolDownTimer -= Time.deltaTime;
        //     if(patrolCoolDownTimer <= 0)
        //     {
        //         canMoveWhilePatrolling = true;
        //         patrolCoolDownTimer = patrolCoolDown;
        //         //Get the next patrol point to move to when done waiting
        //         GetMovePoints();
        //     }
        // }
    }


    // void FixedUpdate()
    // {

    //     if(isPatrolling && canMoveWhilePatrolling)
    //     {
    //         Debug.Log("Can Move: " + canMoveWhilePatrolling);
    //         MoveToNextPoint();
    //     }
    // }

    //Get enemy next move point to move to when patrolling
    void GetMovePoints()
    {
        //Get next point in list to move to
        nextMovePointIndex = Random.Range(0, movePoints.Count);

        //If the patrol point is the same as the enemy is on then get it again
        while(nextMovePointIndex == currentMovePointIndex)
        {
            nextMovePointIndex = Random.Range(0, movePoints.Count);
        }
        currentMovePointIndex = nextMovePointIndex;
    }

    public void MoveToPatrolPoints()
    {
        Vector3 patrolPoint = new Vector3(movePoints[nextMovePointIndex].position.x, 0, movePoints[nextMovePointIndex].position.z);
        navMeshAgent.destination = patrolPoint;
        //Debug.Log("Remaining: " + navMeshAgent.remainingDistance);
        if(navMeshAgent.remainingDistance < .2f)
        {
            navMeshAgent.isStopped = true;
            //canMoveWhilePatrolling = false;
            patrolCoolDownTimer = patrolCoolDown;
        }
    }

    public void CoolDownPatrolMode()
    {
        //Debug.Log("Cooling");
        patrolCoolDownTimer -= Time.deltaTime;

        if(patrolCoolDownTimer <= 0)
        {
            GetMovePoints();
            navMeshAgent.isStopped = false;
        }
    }

    public void FindPlayer()
    {
        navMeshAgent.destination = playerTransform.position;

        if(navMeshAgent.remainingDistance <= 1.0f)
        {
            navMeshAgent.isStopped = true;  
        }
        else
        {
            navMeshAgent.isStopped = false;
        }
        
    }

    public void CheckDistanceFromPlayer()
    {
        navMeshAgent.destination = playerTransform.position;
        Debug.Log("Remaining Distance: " + navMeshAgent.remainingDistance);
        if(navMeshAgent.remainingDistance < 1f)
        {
            Debug.Log("True");
            navMeshAgent.isStopped = true;
        }
        else{
            navMeshAgent.isStopped = false;
        }
    }

    public void SetIsChasingPlayer(bool isChasing)
    {
        ischasingPlayer = isChasing;
    }

    //This is used to reset coolDownTimer when switching to patrol from chasing player
    public void ResetCoolDownTimer()
    {
        patrolCoolDownTimer = patrolCoolDown;
    }

    public void SetIsPatrolling(bool patrolling)
    {
        isPatrolling = patrolling;
    }

    public void AttackPlayer()
    {
        
        if(!canAttack)
        {
            return;
        }

        GameObject newAttack = Instantiate(enemyAttack, transform.position + transform.forward, Quaternion.identity);
        newAttack.GetComponent<EnemyProjectileAttack>().GetEnemyForward(transform.forward);
        newAttack.GetComponent<EnemyProjectileAttack>().GetEnemyTransform(transform.position);
        newAttack.GetComponent<EnemyProjectileAttack>().SetAttackSpeed(navMeshAgent.speed);

        canAttack = false;
        attackTimer = attackCoolDown;
    }
}
