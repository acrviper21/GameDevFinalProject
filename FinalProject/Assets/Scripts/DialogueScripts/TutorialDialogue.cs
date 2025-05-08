using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialDialogue : MonoBehaviour
{
    [SerializeField] DialogueHandler dialogueHandler;
    bool isDialogueActive = true;
    bool usingController = false;
    List<string> tutorialPart1Dialgoue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        tutorialPart1Dialgoue = new List<string>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDialogueActive)
        {
            isDialogueActive = dialogueHandler.IsDialogueActive();  
        }
    }

    public void SetDialogueForController()
    {
        tutorialPart1Dialgoue.Add("Use the left joystick to move around and the right joystick to look around.");
        tutorialPart1Dialgoue.Add("Please collect all the coins and hearts to move on!");
    }

    public void SetDialogueForKeyboard()
    {
        tutorialPart1Dialgoue.Add("Use WASD to move around and the mouse to look around.");
        tutorialPart1Dialgoue.Add("Please collect all the coins and hearts to move on!");
    }

    //Starts the first tutorial dialogue
    public void BeginPart1Dialogue()
    {
        isDialogueActive = true;
        tutorialPart1Dialgoue.Clear();
        tutorialPart1Dialgoue.Add("Hello there Kazul");
        if(IsUsingController())
        {
            SetDialogueForController();
        }
        else
        {
            SetDialogueForKeyboard();
        }
        dialogueHandler.StartDialogue(tutorialPart1Dialgoue);
    }

    public void BeginPart2Dialogoue()
    {
        isDialogueActive = true;
        tutorialPart1Dialgoue.Clear();
        tutorialPart1Dialgoue.Add("Please head over to the shop to buy your first attack!");
        
        if(IsUsingController())
        {
            tutorialPart1Dialgoue.Add("Use the North Button to interact with the item");
        }
        else
        {
            tutorialPart1Dialgoue.Add("Use the \"E\" to interact with the item");
        }
        dialogueHandler.StartDialogue(tutorialPart1Dialgoue);
    }

    public void BeginPart3Dialogue()
    {
        isDialogueActive = true;
        tutorialPart1Dialgoue.Clear();
        
        if(IsUsingController())
        {

        }
        else
        {

        }

    }

    //Used to check what the player is using
    public bool IsUsingController()
    {
        return Gamepad.all.Count > 0;
    }
}
