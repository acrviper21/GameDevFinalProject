using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] float coolDownTimer = 5f;
    float coolDownTimerLeft = 0f;
    [SerializeField] bool isChasing = false;
    [SerializeField] Transform playerTransform;
    bool canMove = true;
    [Header("MovePoints")]
    [SerializeField] List<Transform> movePoints;
    int currentMovePointIndex = 0;
    int nextMovePointIndex = 0;
    Rigidbody rb;

    Vector2 enemyPosition;
    Vector2 movePointsPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        nextMovePointIndex = currentMovePointIndex;
        enemyPosition = new Vector2(transform.position.x, transform.position.z);
        movePointsPosition = new Vector2(movePoints[nextMovePointIndex].position.x, movePoints[nextMovePointIndex].position.z);
    }

    void Update()
    {
        //Check to see if enemy moved to new point
        //If so then start wating
        if(!canMove)
        {
            coolDownTimerLeft -= Time.deltaTime;
        }
    
        //After waiting allow the enemy to move again
        if(coolDownTimerLeft <= 0)
        {
            canMove = true;
        }
    }

    void FixedUpdate()
    {
        enemyPosition = new Vector2(transform.position.x, transform.position.z);

        //Check if chasing player
        if(GetIsChasing())
        {
            MoveTowardsPlayer();
            return;
            
        }

        //Check if enemy is at next point
        //If so and waited for a few secs then get a new point
        //Use Vector 2 to ignore the y axis
        if(Vector2.Distance(enemyPosition, movePointsPosition) <0.3f && GetCanMove())
        {
            //Debug.Log("Here");
            GetMovePoint();
            canMove = false;
        }
        //After getting new point have the enemy wait a few secs
        //After watiing then allow enemy to move
        //After moving set the wait timer again
        else if (GetCanMove() && !GetIsChasing())
        {
            MoveEnemy();
            coolDownTimerLeft = coolDownTimer;
        }
        
    }

    public void GetMovePoint()
    {
        //Get Random location to move to
        //If same spot then get a new location
        nextMovePointIndex = Random.Range(0, movePoints.Count);
        while (nextMovePointIndex == currentMovePointIndex)
        {
            nextMovePointIndex = Random.Range(0, movePoints.Count);
        }
        currentMovePointIndex = nextMovePointIndex;
        movePointsPosition = new Vector2(movePoints[nextMovePointIndex].position.x, movePoints[nextMovePointIndex].position.z);
        //Debug.Log($"Next Move Point: {movePoints[nextMovePointIndex].position}");
    }

    public void MoveEnemy()
    {
        //Get the direction so enemy doesn't rotate on y and fall over
        Vector3 rotateDirection = movePoints[nextMovePointIndex].position - transform.position;
        rotateDirection.y = 0;
        Quaternion lookDirection = Quaternion.LookRotation(rotateDirection);
        Quaternion rotateAmount = Quaternion.Slerp(rb.rotation, lookDirection, Time.deltaTime * rotateSpeed);
        //rotateAmount.Normalize();
        rb.MoveRotation(rotateAmount);
        rb.linearVelocity = (movePoints[nextMovePointIndex].position - transform.position).normalized * moveSpeed;
    }

    public void MoveTowardsPlayer()
    {
        Vector3 rotateDirection = playerTransform.position - transform.position;
        rotateDirection.y = 0;
        Quaternion lookDirection = Quaternion.LookRotation(rotateDirection);
        Quaternion rotateAmount = Quaternion.Slerp(rb.rotation, lookDirection, Time.deltaTime * rotateSpeed);
        rb.MoveRotation(rotateAmount);
        rb.linearVelocity = (playerTransform.position - transform.position).normalized * moveSpeed;
    }

    public void SetIsChasing(bool chasing)
    {
        isChasing = chasing;
    }

    public bool GetIsChasing()
    {
        return isChasing;
    }

    //When player is outside enemy's range then wait before moving back to movePoints
    public void ResetWaitTimer()
    {
        coolDownTimerLeft = coolDownTimer;
        GetMovePoint();
        SetCanMove(false);
    }

    public void SetCanMove(bool move)
    {
        canMove = move;
    }

    public bool GetCanMove()
    {
        return canMove;
    }
}
