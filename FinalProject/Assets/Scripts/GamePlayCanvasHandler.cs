using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GamePlayCanvasHandler : MonoBehaviour
{
    [SerializeField] List<Image> healthImages;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] CreatureController player;

    [Header("Store Canvas")]
    [SerializeField] Canvas storeCanvas;
    [SerializeField] List<Button> storeButtons;
    [SerializeField] TextMeshProUGUI itemDescriptionText;

    bool isStoreCanvasActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinText.text = player.GetCoins().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCoinText()
    {
        coinText.text = player.GetCoins().ToString();
    }

    //Update the description of the item with description and price
    public void UpdateItemDescriptionText(string description)
    {
        itemDescriptionText.text = description;
    }

    //Show store canvas when interacting with item
    public void ShowStoreCanvas()
    {
        storeCanvas.gameObject.SetActive(true);
        isStoreCanvasActive = true;
    }

    //Hide store canvas when player is done interacting with item
    //Or player has purchased item
    public void HideStoreCanvas()
    {
        storeCanvas.gameObject.SetActive(false);
        isStoreCanvasActive = false;
    }

    //UI event for when Buy button is pressed
    public void BuyButtonPressed()
    {
        if(player.GetCoins() >= player.GetItemCost())
        {
            player.BuyItem(player.GetItemCost());
            HideStoreCanvas();
        }
        else
        {
            itemDescriptionText.text = "Sorry, you don't have enough coins!";
        }
    }

    //UI event for when Cancel button is pressed
    public void CancelButtonPressed()
    {
        HideStoreCanvas();
    }
}
