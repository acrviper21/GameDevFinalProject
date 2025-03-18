using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            enemyController.SetIsChasing(true);
            //Debug.Log("Chasing");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            enemyController.SetIsChasing(false);
            enemyController.ResetWaitTimer();
            //Debug.Log("Stop Chasing");
        }
    }
}
