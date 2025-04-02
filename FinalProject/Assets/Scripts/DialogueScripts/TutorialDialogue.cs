using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialDialogue : MonoBehaviour
{
    [SerializeField] DialogueHandler dialogueHandler;
    bool usingController = false;
    List<string> tutorialPart1Dialgoue = new List<string> {"Hello there Kazul"};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Gamepad.all.Count > 0)
        {
            //usingController = true;
            SetDialogueForController();
        }
        else
        {
            SetDialogueForKeyboard();
        }
        dialogueHandler.StartDialogue(tutorialPart1Dialgoue);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
