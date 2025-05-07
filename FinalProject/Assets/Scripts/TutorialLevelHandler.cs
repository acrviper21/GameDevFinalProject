using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialLevelHandler : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] TutorialDialogue tutorialDialgoue;

    [Header("Coins")]
    [SerializeField] List<CoinPickupHandler> coins;
    bool allCoinsCollected = false;

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

    [Header("Enemy")]
    [SerializeField] List<EnemyController> enemies;

    [Header("Store")]
    [SerializeField] ShopScriptHandler shopHandler;
    [SerializeField] GameObject shopKeeper;
    List<ItemHandler> items;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorialDialgoue.BeginPart1Dialogue();
        //Cache items
        items = shopHandler.GetItemList();

        //Disable enemies until player has attack
        foreach(EnemyController enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
        shopKeeper.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Collect all coins and hearts and then instantiate the particle system
        //After the particle system plays move player to new spawn point
        //Then play the second part of the dialogue
        if(allCoinsCollected && allHeartsCollected && !particleSystemPlayed)
        {
            ps = Instantiate(playerParticleSystem);
            ps.GetComponent<DissapearAndReappearHandler>().PlayParticleSystem(player.GetPosition(), this);
            particleSystemPlayed = true;
            //Hide player in particle system
            player.HidePlayer();
            items[0].SetShowItem(true);
            shopHandler.ShowAvailableItems();
            shopKeeper.SetActive(true);
        }
        //Once particle system is finished, move player to the new spawn point
        if(allCoinsCollected && allHeartsCollected && GetParticleSystemFinished() && !hasPart2DialoguePlayed)
        {
            //Debug.Log("Calling");
            //Move player to new location
            SetDialogue2SpawnPoint();
            ps = Instantiate(playerParticleSystem);
            ps.GetComponent<DissapearAndReappearHandler>().PlayParticleSystem(player.GetPosition(), this);

            //Have player look at direction of store before starting next dialogue
            player.SetRotation(Dialogue2SpawnPoint.forward);
            //Show player in particle system
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


