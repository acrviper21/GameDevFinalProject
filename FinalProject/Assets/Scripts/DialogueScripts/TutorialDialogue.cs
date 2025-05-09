using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialDialogue : MonoBehaviour
{
    [SerializeField] DialogueHandler dialogueHandler;
    bool isDialogueActive = true;
    bool usingController = false;
    List<string> tutorialDialogoue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        tutorialDialogoue = new List<string>();
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
        tutorialDialogoue.Add("Use the left joystick to move around and the right joystick to look around.");
        tutorialDialogoue.Add("Please collect all the coins and hearts to move on!");
    }

    public void SetDialogueForKeyboard()
    {
        tutorialDialogoue.Add("Use WASD to move around and the mouse to look around.");
        tutorialDialogoue.Add("Please collect all the coins and hearts to move on!");
    }

    //Starts the first tutorial dialogue
    public void BeginPart1Dialogue()
    {
        isDialogueActive = true;
        tutorialDialogoue.Clear();
        tutorialDialogoue.Add("Hello there Kazul");
        if(IsUsingController())
        {
            SetDialogueForController();
        }
        else
        {
            SetDialogueForKeyboard();
        }
        dialogueHandler.StartDialogue(tutorialDialogoue);
    }

    public void BeginPart2Dialogoue()
    {
        isDialogueActive = true;
        tutorialDialogoue.Clear();
        tutorialDialogoue.Add("Please head over to the shop to buy your first attack!");
        
        if(IsUsingController())
        {
            tutorialDialogoue.Add("Use the North Button to interact with the item");
        }
        else
        {
            tutorialDialogoue.Add("Use the \"E\" to interact with the item");
        }
        dialogueHandler.StartDialogue(tutorialDialogoue);
    }

    public void BeginPart3Dialogue()
    {
        //Debug.Log("BeginPart3");
        isDialogueActive = true;
        tutorialDialogoue.Clear();
        tutorialDialogoue.Add("Now that you purchased your first attack, please defeat the enemies in this area.");
        
        if(IsUsingController())
        {
            tutorialDialogoue.Add("Use the West Button to attack the enemies.");
        }
        else
        {
            tutorialDialogoue.Add("Use the Left Mouse Button to attack the enemies.");
        }

        tutorialDialogoue.Add("Afterwards, the light will be returned to this area.");
        dialogueHandler.StartDialogue(tutorialDialogoue);
    }

    //Used to check what the player is using
    public bool IsUsingController()
    {
        return Gamepad.all.Count > 0;
    }
}
