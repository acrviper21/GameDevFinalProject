using System.Collections.Generic;
using Unity.Mathematics;
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

    [Header("Attack")]
    [SerializeField] float projectileCoolDown = 2f;
    [SerializeField] float projectileCoolDownTimer = 0f;
    [SerializeField] bool canShoot = true;
    [SerializeField] GameObject attackPrefab;

    int health = 1;
    int coins = 0;
    [SerializeField] int maxHealth = 3;

    MeshRenderer[] playerRenderers;

    //Used for the unity interactive event for items
    bool collidingWithItem = false;

    [Header("Shop")]
    [SerializeField] ShopScriptHandler shopHandler;
    [SerializeField] ItemHandler itemToPurchase;
    //Used to check if player is interacting with the item
    //Used to check interaction event on map
    [SerializeField] bool interactingWithItem = false;


    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        jumpsLeft = maxJumps;
        //health = maxHealth;
        playerRenderers = GetComponentsInChildren<MeshRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //attackPrefab = Resources.Load<GameObject>("Prefabs/AttackPrefab");
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        //Debug.Log($"Jumps Left: {jumpsLeft}");

        if(!canShoot)
        {
            projectileCoolDownTimer -= Time.deltaTime;
            if(projectileCoolDownTimer <= 0)
            {
                canShoot = true;
            }  
        }     
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

    //Purchases the item and disbables the item so it can't be purchased again
    //Update the shop to display the items that are available afterwards
    public void BuyItem(int itemCost)
    {
        collidingWithItem = false;
        coins -= itemCost;
        itemToPurchase.PurchaseItem();
        shopHandler.ShowAvailableItems();
        gamePlayCanvasHandler.UpdateCoinText();

        //Load prefab resource
        //gameObject.name must match prefab name
        attackPrefab = Resources.Load<GameObject>("Prefabs/" + itemToPurchase.gameObject.name);
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

    //Allow the player to shoot at the enemy
    public void ShootProjectile()
    {
        //If player has shot then return and wait for cooldown
        if(!canShoot || attackPrefab == null)
        {
            return;
        }
        GameObject newProjectile = Instantiate(attackPrefab, transform.position + transform.forward, Quaternion.identity);
        //Pass in player forward direction for the projectile
        newProjectile.GetComponent<ProjectileAttack>().GetPlayerForward(transform.forward);
        //Pass in player transform to get proper height for projectile
        newProjectile.GetComponent<ProjectileAttack>().GetplayerTransform(transform.position);
        //Set the projectile speed so it moves faster than the player
        newProjectile.GetComponent<ProjectileAttack>().SetAttackSpeed(moveSpeed);
        //Add the projectile to the list
        //projectileList.Add(newProjectile);
        
        //If player has shot then set canShoot to false
        //Set the cooldown timer to the cooldown
        canShoot = false;
        projectileCoolDownTimer = projectileCoolDown;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    //Set player to look at the new rotation when disappearing and reappearing
    public void SetRotation(Vector3 newRotation)
    {
        transform.forward = newRotation;
    }

    public void HidePlayer()
    {
        GetComponent<MeshRenderer>().enabled = false;
        foreach(MeshRenderer mr in playerRenderers)
        {
            mr.enabled = false;
        }
    }

    public void ShowPlayer()
    {
        GetComponent<MeshRenderer>().enabled = true;
        foreach(MeshRenderer mr in playerRenderers)
        {
            mr.enabled = true;
        }
    }

    public void Interact()
    {
        if(collidingWithItem)
        {
            interactingWithItem = true;
            
            shopHandler.PurchaseItem(itemToPurchase.GetItemDescription());
        }
        else
        {
            interactingWithItem = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item"))
        {
            collidingWithItem = true;
            //Get item that player is colliding with
            itemToPurchase = other.GetComponent<ItemHandler>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Item"))
        {
            collidingWithItem = false;
        }
    }
    
    public bool GetInteractingWithItem()
    {
        return interactingWithItem;
    }

    public void SetInteractingWithItem(bool interacting)
    {
        interactingWithItem = interacting;
    }

    public int GetItemCost()
    {
        return itemToPurchase.GetItemCost();
    }
}

