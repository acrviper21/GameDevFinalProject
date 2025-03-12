using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
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

    void FixedUpdate()
    {
        enemyPosition = new Vector2(transform.position.x, transform.position.z);
        if(Vector2.Distance(enemyPosition, movePointsPosition) <0.1f)
        {
            //Debug.Log("Here");
            GetMovePoint();
        }
        else
        {
            MoveEnemy();
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

    }

    public void MoveEnemy()
    {
        //Get the direction so enemy doesn't rotate on y and fall over
        Vector3 rotateDirection = movePoints[nextMovePointIndex].position - transform.position;
        rotateDirection.y = 0;
        Quaternion lookDirection = Quaternion.LookRotation(rotateDirection);
        Quaternion rotateAmount = Quaternion.Slerp(rb.rotation, lookDirection, Time.deltaTime);
        //rotateAmount.Normalize();
        rb.MoveRotation(rotateAmount);
        rb.linearVelocity = (movePoints[nextMovePointIndex].position - transform.position).normalized * moveSpeed;
    }
}
