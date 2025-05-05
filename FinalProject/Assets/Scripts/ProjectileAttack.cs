using UnityEngine;
using UnityEngine.Rendering;

public class ProjectileAttack : MonoBehaviour
{
    [Header("Player Attack Stats")]
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float projectileLife = 3f;
    [SerializeField] float timeAlive = 0f;
    [SerializeField] float projectileHeight = .70f;
    [SerializeField] int projectileDamage = 1;

    [SerializeField] CreatureController player;
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
        this.transform.position += this.transform.forward * projectileSpeed * Time.deltaTime;
    }

    //Used to get playerForward direction for projectile
    public void GetPlayerForward(Vector3 playerTransformForward)
    {
        transform.forward = playerTransformForward;
    }

    public void GetplayerTransform(Vector3 playerTransform)
    {
        //Set projectile realtive to the player
        Vector3 newPos = playerTransform - new Vector3(0, projectileHeight, 0);
        transform.position = newPos;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            //Destroy projectile after hit
            Destroy(this.gameObject);
            //Damage enemy after hit
            other.GetComponent<EnemyHealthController>().TakeDamage(projectileDamage);
        }
    }
}
