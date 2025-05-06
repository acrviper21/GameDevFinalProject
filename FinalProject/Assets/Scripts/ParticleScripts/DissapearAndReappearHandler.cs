using System.Collections;
using UnityEngine;

public class DissapearAndReappearHandler : MonoBehaviour
{
    [SerializeField] CreatureController player;
    [SerializeField] ParticleSystem playerParticleSystem;
    ParticleSystem ps;
    [SerializeField] float timeToDissapear = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //PlayParticleSystem(player.GetPosition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayParticleSystem(Vector3 playerPosition)
    {
        var main = playerParticleSystem.main;
        main.duration = timeToDissapear;
        main.loop = true;
        playerParticleSystem.Play();
        StartCoroutine(StopParticleSystem(playerParticleSystem));
    }

    IEnumerator StopParticleSystem(ParticleSystem ps)
    {
        yield return new WaitForSeconds(timeToDissapear);
        ps.Stop();
        Destroy(ps.gameObject);
    }
}
