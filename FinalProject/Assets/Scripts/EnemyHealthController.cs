using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] int maxHealth = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damaged enemy, Health: " + health);
        
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
