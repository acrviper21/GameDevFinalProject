using UnityEngine;
using UnityEngine.AI;

public class EnemyCollisionHandler : MonoBehaviour
{
    UpdatedEnemyController enemyController;

    void Awake()
    {
        enemyController = GetComponentInParent<UpdatedEnemyController>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            enemyController.SetIsChasingPlayer(true);
            enemyController.SetIsPatrolling(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            enemyController.SetIsChasingPlayer(false);
            enemyController.SetIsPatrolling(true);
            enemyController.ResetCoolDownTimer();
        }
    }
}
