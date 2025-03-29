using UnityEngine;

public class CoinPickupHandler : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 3f;
    [SerializeField] CreatureController player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player.IncrementCoins();
            Destroy(gameObject);
        }
    }
}
