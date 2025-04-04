using Unity.VisualScripting;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    CharacterController characterController;
    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] int maxJumps = 2;
    [SerializeField] int jumpsLeft = 2;
    [Header("Ground Check")]
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] LayerMask jumpLayers;
    [SerializeField] float groundCheckRadius = 0.1f;
    Vector3 gravityVelocity = Vector3.zero;
    [SerializeField] GamePlayCanvasHandler gamePlayCanvasHandler;
    [Header("Health UI")]
    [SerializeField] HealthBarHandler healthBarHandler;

    int health = 1;
    int coins = 0;
    [SerializeField] int maxHealth = 3;


    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        jumpsLeft = maxJumps;
        //health = maxHealth;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        //Debug.Log($"Jumps Left: {jumpsLeft}");
    }

    public void MovePlayer(Vector3 moveDir)
    {
        moveDir.Normalize();
        characterController.Move(moveDir * moveSpeed * Time.deltaTime);
        transform.LookAt(transform.position + moveDir);
    }

    public void Jump()
    {
        if(CreatureOnGround())
        {
            jumpsLeft = maxJumps;
        } 
        else if(jumpsLeft < 1)
        {
            return;
        }
        jumpsLeft--;
        if(gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0;
        }
        gravityVelocity.y += jumpSpeed;
    }

    public bool CreatureOnGround()
    {
        return Physics.OverlapSphere(groundCheckTransform.position, groundCheckRadius, jumpLayers).Length > 0;
    } 

    public bool CanJump()
    {
        return jumpsLeft > 0;
    }


    void ApplyGravity()
    {
        if (CreatureOnGround() && gravityVelocity.y < 0)
        {
            return;
        }
        gravityVelocity.y += gravity * Time.deltaTime;
        characterController.Move(gravityVelocity * Time.deltaTime);
    }

    public int GetHealth()
    {
        return health;
    }
    
    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }

    public bool IncrementHealth()
    {
        health++;
        if(health > maxHealth)
        {
            health = maxHealth;
            return false;
        }
        healthBarHandler.UpdateHealth();
        return true;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    //Increment coins based on the type collected
    //Update coin ui when collecting coin
    public void IncrementCoins(int amountToIncrement)
    {
        coins += amountToIncrement;
        gamePlayCanvasHandler.UpdateCoinText();
        //Debug.Log("Player coins: " + coins);
    }

    public int GetCoins()
    {
        return coins;
    }

    //Allow to set the coins to an exact amount.
    //For loading later
    public void SetCoins(int newAmount)
    {
        coins = newAmount;
    }
}
