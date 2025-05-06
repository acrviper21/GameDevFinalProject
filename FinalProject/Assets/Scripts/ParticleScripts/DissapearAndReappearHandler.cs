using System.Collections;
using UnityEngine;

public class DissapearAndReappearHandler : MonoBehaviour
{
    [SerializeField] CreatureController player;
    [SerializeField] ParticleSystem playerParticleSystem;
    [SerializeField] float timeToDissapear = 1f;
    //Used to update when the paricle system is finished playing
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //PlayParticleSystem(player.GetPosition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayParticleSystem(Vector3 playerPosition, TutorialLevelHandler tutorialLevelHandler)
    {
        //Set the particle to the player position
        playerParticleSystem.transform.position = playerPosition;

        //Get the struct to manipulate the particle system
        var main = playerParticleSystem.main;
        main.duration = timeToDissapear;
        main.loop = true;
        playerParticleSystem.Play();
        StartCoroutine(StopParticleSystem(playerParticleSystem, tutorialLevelHandler));
    }

    //Destroy the particle system after it plays
    IEnumerator StopParticleSystem(ParticleSystem ps, TutorialLevelHandler tutorialLevelHandler)
    {
        yield return new WaitForSeconds(timeToDissapear);
        ps.Stop();
        tutorialLevelHandler.SetParticleSystemFinished(true);
        Destroy(ps.gameObject);
    }
}
