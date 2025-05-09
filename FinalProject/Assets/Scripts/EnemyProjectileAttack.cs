using UnityEngine;

public class EnemyProjectileAttack : MonoBehaviour
{
    [Header("Enemy Attack Stats")]
    [SerializeField] float projectileSpeed = 5f;
    float projectileMovementSpeed = 0f;
    [SerializeField] float projectileLife = 3f;
    [SerializeField] float timeAlive = 0f;
    [SerializeField] float projectileHeight = .70f;
    [SerializeField] int projectileDamage = 1;

    [SerializeField] UpdatedEnemyController enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Destry projectile after a certain amount of time
        timeAlive += Time.deltaTime;
        if(timeAlive >= projectileLife)
        {
            Destroy(this.gameObject);
        }

        //Move projectile forward
        this.transform.position += this.transform.forward * projectileMovementSpeed * Time.deltaTime;
    }

    //Used to get playerForward direction for projectile
    public void GetEnemyForward(Vector3 enemyTransformForward)
    {
        transform.forward = enemyTransformForward;
    }

    public void GetEnemyTransform(Vector3 enemyTransform)
    {
        //Set projectile realtive to the player
        Vector3 newPos = enemyTransform - new Vector3(0, projectileHeight, 0);
        transform.position = newPos;
    }

    public void SetAttackSpeed(float enemySpeed)
    {
        projectileMovementSpeed = enemySpeed + projectileSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Collided");
            //Destroy projectile after hit
            Destroy(this.gameObject);
            //Damage enemy after hit
            other.GetComponent<CreatureController>().DecrementHealth(projectileDamage);
        }
    }
}
