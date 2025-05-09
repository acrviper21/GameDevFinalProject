using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialLevelHandler : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] TutorialDialogue tutorialDialgoue;

    [Header("Coins")]
    [SerializeField] List<CoinPickupHandler> coins;
    [SerializeField] bool allCoinsCollected = false;

    [Header("Hearts")]
    [SerializeField] List<HealthPickupHandler> hearts;
    [SerializeField] bool allHeartsCollected = false;
    bool hasPart2DialoguePlayed = false;

    [Header("ParticleSystem")]
    [SerializeField] ParticleSystem playerParticleSystem;
    ParticleSystem ps;
    bool particleSystemPlayed = false;
    bool particleSystemFinished = false;

    [Header("Player")]
    [SerializeField] CreatureController player;
    [SerializeField] List<Transform> DialogueSpawnPoints;
    [SerializeField] float PlayerHeightAboveGround = 1.116f;

    [Header("Enemy")]
    [SerializeField] List<EnemyController> enemies;

    [Header("Store")]
    [SerializeField] ShopScriptHandler shopHandler;
    [SerializeField] GameObject shopKeeper;
    List<ItemHandler> items;

    [Header("Tutorial Advancement")]
    [SerializeField] bool tutorialPart1Completed = false;
    [SerializeField] bool tutorialPart2Completed = false;
    [SerializeField] bool tutorialPart3Completed = false;

    [Header("Platforms")]
    [SerializeField] List<GameObject> platforms;
    bool platformsHidden = false;
    bool enemiesHidden = false;

    [Header("Canvas")]
    [SerializeField] GamePlayCanvasHandler gamePlayCanvasHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //UnComment after testing

        
        tutorialDialgoue.BeginPart1Dialogue();
        
        items = shopHandler.GetItemList();

        //Disable enemies until player has attack
        HideEnemies();
        shopKeeper.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Collect all coins and hearts and then instantiate the particle system
        //After the particle system plays move player to new spawn point
        //Then play the second part of the dialogue
        if(allCoinsCollected && allHeartsCollected && !tutorialPart1Completed)
        {
            ps = Instantiate(playerParticleSystem);
            ps.GetComponent<DissapearAndReappearHandler>().PlayParticleSystem(player.GetPosition(), this);
            particleSystemPlayed = true;

            //Hide player in particle system
            player.HidePlayer();
            items[0].SetShowItem(true);
            shopHandler.ShowAvailableItems();
            shopKeeper.SetActive(true);

            tutorialPart1Completed = true;
            gamePlayCanvasHandler.SetIsLoading(true);
        }
        //Once particle system is finished, move player to the new spawn point
        if(tutorialPart1Completed && particleSystemFinished && !tutorialPart2Completed)
        {
            //Set loading screen before player teleports
            if(gamePlayCanvasHandler.GetisLoading())
            {
                //Debug.Log("Loading");
                gamePlayCanvasHandler.ShowLoadingScreen();
            }

            //Move player to new location
            SetDialogue2SpawnPoint();

            //Hide loading screen after player teleports
            gamePlayCanvasHandler.HideLoadingScreen();
            
            ps = Instantiate(playerParticleSystem);
            ps.GetComponent<DissapearAndReappearHandler>().PlayParticleSystem(player.GetPosition(), this);
            //Have player look at direction of store before starting next dialogue
            player.SetRotation(DialogueSpawnPoints[0].forward);

            //Show player in particle system
            player.ShowPlayer();
            tutorialDialgoue.BeginPart2Dialogoue();
            tutorialPart2Completed = true;
        }

        if(tutorialPart1Completed && tutorialPart2Completed && !tutorialPart3Completed && player.GetAttackPrefab() != null)
        {
            BeginTutorialPart3();
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
        Vector3 newPosition = new Vector3(DialogueSpawnPoints[0].position.x, PlayerHeightAboveGround, DialogueSpawnPoints[0].position.z);
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

    public void BeginTutorialPart3()
    {
        if(!gamePlayCanvasHandler.GetisLoading())
        {
            particleSystemPlayed = false;
            gamePlayCanvasHandler.SetIsLoading(true);
            ps = Instantiate(playerParticleSystem);
            ps.GetComponent<DissapearAndReappearHandler>().PlayParticleSystem(player.GetPosition(), this);
        }
        
        if(particleSystemFinished)
        {
            gamePlayCanvasHandler.ShowLoadingScreen();
            //Remove platforms
            HidePlatforms();
            //Spawn enemies
            ShowEnemies();
            Vector3 newPosition = new Vector3(DialogueSpawnPoints[1].position.x, PlayerHeightAboveGround, DialogueSpawnPoints[1].position.z);
            player.SetPosition(newPosition);
            player.SetRotation(DialogueSpawnPoints[1].forward);
            gamePlayCanvasHandler.HideLoadingScreen();


            if(!particleSystemPlayed)
            {
                ps = Instantiate(playerParticleSystem);
                ps.GetComponent<DissapearAndReappearHandler>().PlayParticleSystem(player.GetPosition(), this);
                tutorialDialgoue.BeginPart3Dialogue();
                particleSystemPlayed = true;
                tutorialPart3Completed = true;
            }
        }
    }

    public void HidePlatforms()
    {
        if(platformsHidden)
        {
            return;
        }

        foreach(GameObject platform in platforms)
        {
            platform.SetActive(false);
        }
        platformsHidden = true;
    }

    public void HideEnemies()
    {
        if(enemiesHidden)
        {
            return;
        }

        foreach(EnemyController enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }

        enemiesHidden = true;
    }

    public void ShowEnemies()
    {
        if(!enemiesHidden)
        {
            return;
        }

        foreach(EnemyController enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
        }

        enemiesHidden = false;
    }
}


