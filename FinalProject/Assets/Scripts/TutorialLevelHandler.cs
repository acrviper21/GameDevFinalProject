using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelHandler : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] TutorialDialogue tutorialDialgoue;
    [Header("Coins")]
    [SerializeField] List<CoinPickupHandler> coins;
    bool allCoinsCollected = true;
    [Header("Hearts")]
    [SerializeField] List<HealthPickupHandler> hearts;
    bool allHeartsCollected = false;
    bool hasPart2DialoguePlayed = false;
    [Header("ParticleSystem")]
    [SerializeField] ParticleSystem playerParticleSystem;
    ParticleSystem ps;
    bool particleSystemPlayed = false;
    bool particleSystemFinished = false;
    [Header("Player")]
    [SerializeField] CreatureController player;
    [SerializeField] Transform Dialogue2SpawnPoint;
    [SerializeField] float PlayerHeightAboveGround = 1.116f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorialDialgoue.BeginPart1Dialogue();
    }

    // Update is called once per frame
    void Update()
    {
        //Collect all coins and hearts and then instantiate the particle system
        //After the particle system plays move player to new spawn point
        //Then play the second part of the dialogue
        if(allCoinsCollected && allHeartsCollected && !particleSystemPlayed/*!hasPart2DialoguePlayed*/)
        {
            ps = Instantiate(playerParticleSystem);
            ps.GetComponent<DissapearAndReappearHandler>().PlayParticleSystem(player.GetPosition(), this);
            particleSystemPlayed = true;
            player.HidePlayer();
            //Destroy(ps.gameObject);
            // SetDialogue2SpawnPoint();
            // tutorialDialgoue.BeginPart2Dialogoue();
            // hasPart2DialoguePlayed = true;
        }
        if(allCoinsCollected && allHeartsCollected && GetParticleSystemFinished() && !hasPart2DialoguePlayed)
        {
            //Debug.Log("Calling");
            SetDialogue2SpawnPoint();
            player.ShowPlayer();
            tutorialDialgoue.BeginPart2Dialogoue();
            hasPart2DialoguePlayed = true;
        }
    }

    //Removes the coins from the list that has been collected for the tutorial level
    public void CoinCollectedInLevel()
    {
        foreach(CoinPickupHandler coin in coins)
        {
            if(coin.IsCollected())
            {
                coins.Remove(coin);
                break;
            }
        }
        if(coins.Count == 0)
        {
            allCoinsCollected = true;
        }
    }

    //Removes the hearts from the list that has been collected for the tutorial level
    public void HeartsCollectedInLevel()
    {
        foreach(HealthPickupHandler heart in hearts)
        {
            if(heart.IsCollected())
            {
                hearts.Remove(heart);
                break;
            }
        }
        if(hearts.Count == 0)
        {
            allHeartsCollected = true;
        }
    }

    public void SetDialogue2SpawnPoint()
    {
        Vector3 newPosition = new Vector3(Dialogue2SpawnPoint.position.x, PlayerHeightAboveGround, Dialogue2SpawnPoint.position.z);
        player.SetPosition(newPosition);
    }

    public void SetParticleSystemFinished(bool finishedPlaying)
    {
        //Debug.Log("Before: " + finishedPlaying);
        particleSystemFinished = finishedPlaying;
        //Debug.Log("After: " + particleSystemFinished);
    }

    public bool GetParticleSystemFinished()
    {
        return particleSystemFinished;
    }
}
