using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textField;
    [SerializeField] float textSpeed = 1f;
    [SerializeField] PlayerInput playerInput;
    [Header("Player Input Maps")]
    //These two are for disabling each other so they don't conflict
    string uiMap = "UI";
    string playerMap = "Player";
    List<string> dialogueSayings = new List<string>();
    private int index;
    bool isCoroutineRunning = false;

    void Awake()
    {
        gameObject.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(List<string> dialogue)
    {
        SwitchToUIMap();
        //Clear list and then set it to new dialogue
        dialogueSayings.Clear();
        dialogueSayings.AddRange(dialogue);
        //Set textfield to empty before displaying next dialogue
        textField.text = string.Empty;
        gameObject.SetActive(true);
        index = 0;
        // foreach (string saying in dialogueSayings)
        // {
        //     Debug.Log(saying);
        // }
        
        StartCoroutine(WriteDialogue(dialogue[index]));
    }

    //Write the dialogue one character at a time
    IEnumerator WriteDialogue(string dialogue)
    {
        //Prevent CoRoutine from running multiple times on same index
        if(isCoroutineRunning)
        {
            yield break;
        }
        
        //Debug.Log("Index: " + index);
        isCoroutineRunning = true;
        textField.text = string.Empty;
        //Debug.Log("Here: " + dialogue[index]);
        //Loop through string in list
        foreach(char letter in dialogue.ToCharArray())
        {
            textField.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        //Set false to know CoRoutine is done
        isCoroutineRunning = false;
    }

    //Advance the dialogue 
    public void NextDialogue()
    {
        //Check if CoRoutine is not running first to prevent index from incrementing more than once.
        if(!isCoroutineRunning)
        {
            index++;
        }
        //Start CoRoutine if there's still dialogue that needs to be said
        if(index < dialogueSayings.Count)
        {
            StartCoroutine(WriteDialogue(dialogueSayings[index]));
        }
        //Wait till dialogue is typed out and then hide dialogue box
        //Switch the map from UI to player to allow movement again
        else if(index >= dialogueSayings.Count && !isCoroutineRunning)
        {
            gameObject.SetActive(false);
            SwitchToPlayerMap();
        }
        
    }

    //Switch to UI Map to prevent buttons from UI and player interfering with each other
    public void SwitchToUIMap()
    {
        playerInput.actions.FindActionMap(playerMap).Disable();
        playerInput.SwitchCurrentActionMap(uiMap);
        //playerInput.enabled = false;
        //Debug.Log("Current: " + playerInput.currentActionMap.name);
    }

    //Switch to Player Map to prevent buttons from UI and player interfering with each other
    public void SwitchToPlayerMap()
    {
        playerInput.SwitchCurrentActionMap(playerMap);
    }
}
