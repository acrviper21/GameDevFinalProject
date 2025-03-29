using UnityEngine;

public class HealthPickupHandler : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 3f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip healthPickupSound;
    [SerializeField] CreatureController player;
    bool isCollected = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isCollected)
        {
            return;
        }

        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Increment players health if less than max health
            if(player.IncrementHealth())
            {
                audioSource.PlayOneShot(healthPickupSound);
                CollectHeart();
            }
        }
        
    }

    //Set true if this heart has been collected
    //Hide meshRender and collider if it has been collected
    public void CollectHeart()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        isCollected = true;
    }

    public bool IsCollected()
    {
        return isCollected;
    }
}
