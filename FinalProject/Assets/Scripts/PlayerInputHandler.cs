using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] CreatureController player;
    Vector2 movementInput;
    [SerializeField] Transform cameraTransform;
    [SerializeField] PlayerInput playerInput;
    string uiMap = "UI";
    string playerMap = "Player";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        //Debug.Log($"On Ground: {player.CreatureOnGround()}");
    }

    public void MoveEvent(InputAction.CallbackContext context)
    {
        //Check continuously for input
        //context.Performed just checks if input was performed
        //Without context.performed this will check for input continuously like button pressed and button released
        movementInput = context.ReadValue<Vector2>();
    }

    public void MovePlayer()
    {
        //Disable player movement if dialogue is going on
        if(playerInput.currentActionMap.name == uiMap)
        {
            return;
        }
        
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;

        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0;

        Vector3 finalMovement = Vector3.zero;

        finalMovement = (movementInput.x * cameraRight + movementInput.y * cameraForward);
        
        player.MovePlayer(finalMovement);

    }

    public void JumpEvent(InputAction.CallbackContext context)
    {
        //Disable player movement if dialogue is going on
        if(playerInput.currentActionMap.name == uiMap)
        {
            return;
        }
        if (context.performed)
        {
            player.Jump();
            // if(player.CanJump())
            // {
            //     Debug.Log("Jump");
            //     player.Jump();
            // }
        }
    }
}
