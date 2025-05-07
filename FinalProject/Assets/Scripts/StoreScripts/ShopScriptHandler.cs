using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopScriptHandler : MonoBehaviour
{
    //These are what the items will hover over in shop
    [Header("Tables")]
    [SerializeField] List<GameObject> tables;
    [SerializeField] List<ItemHandler> items;
    [SerializeField] float itemFloatAboveTable;

    [Header("Dialogue")]
    [SerializeField] DialogueHandler dialogueHandler;
    [SerializeField] Button BuyButton;
    [SerializeField] Button CancelButton;
    List<string> itemDialogue;

    //List of items to reference for indexing for items in the shop
    enum ItemList
    {
        SingeProjectile = 0,
        TriProjectile = 1
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Show items that aren't purchased or player reached a certain point
        ShowAvailableItems();
        itemDialogue = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowAvailableItems()
    {
        //Loop through tables and set items above them
        for(int i = 0; i < tables.Count && i < items.Count; i++)
        {
            //Show items that haven't been purchase or player reached a certain point in the game
            if(items[i].GetShowItem() && !items[i].GetItemPurchased())
            {
                items[i].transform.position = new Vector3(tables[i].transform.position.x, tables[i].transform.position.y + itemFloatAboveTable, tables[i].transform.position.z);
                items[i].ShowItem();
                continue;
            }
            //Hide purchase items or before certain point in game
            else
            {
                items[i].HideItem();
            }
        }
    }

    //Return the list of items in the shop
    public List<ItemHandler> GetItemList()
    {
        return items;
    }

    public void PurchaseItem(List<string> dialogue)
    {
        BuyButton.gameObject.SetActive(false);
        CancelButton.gameObject.SetActive(false);
        AddDialogue(dialogue);
        dialogueHandler.StartDialogue(itemDialogue);
        StartCoroutine(waitForDialogueToFinish());
    }

    //Wait for dialogue to finish then show buttons to buy item
    IEnumerator waitForDialogueToFinish()
    {
        Debug.Log("Dialogue Active: " + !dialogueHandler.IsDialogueActive());
        yield return new WaitUntil(() => !dialogueHandler.IsDialogueActive());
        Debug.Log("Done");
        dialogueHandler.SetDialoguePanelActive();
        BuyButton.gameObject.SetActive(true);
        CancelButton.gameObject.SetActive(true);
    }

    //Add dialogue depending on the item being interacted with
    public void AddDialogue(List<string> dialogue)
    {
        itemDialogue.Clear();
        itemDialogue.AddRange(dialogue);
        itemDialogue.Add("Buy Item?");
        //Debug.Log("Dialogue Sayings: " + itemDialogue[0]);
    }

    public void BuyButtonUI()
    {
        Debug.Log("Buy Button Pressed");
    }

    public void CancelButtonUI()
    {
        Debug.Log("Don't Buy Button Pressed");
    }

}
