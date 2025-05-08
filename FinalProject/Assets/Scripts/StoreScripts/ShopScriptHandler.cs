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

    [Header("Shop")]
    [SerializeField] GamePlayCanvasHandler gamePlayCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Show items that aren't purchased or player reached a certain point
        ShowAvailableItems();
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

    //Update the text of item description before displaying
    //Display the store canvas
    public void PurchaseItem(string itemDescription)
    {
        gamePlayCanvas.UpdateItemDescriptionText(itemDescription);
        gamePlayCanvas.ShowStoreCanvas();
        
    }

}
