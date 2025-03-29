using UnityEngine;

public class CoinPickupHandler : MonoBehaviour
{
    [SerializeField] int coinValue = 1;
    [SerializeField] float rotateSpeed = 3f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip coinPickupSound;
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
            audioSource.PlayOneShot(coinPickupSound);
            player.IncrementCoins(coinValue);
            CollectCoin();
        }
    }

    //Set true if coin has been collected
    //Hide collider and render so audio finishes playing
    public void CollectCoin()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        isCollected = true;
    }

    public bool IsCollected()
    {
        return isCollected;
    }
}
