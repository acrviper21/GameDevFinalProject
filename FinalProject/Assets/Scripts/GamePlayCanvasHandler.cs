using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GamePlayCanvasHandler : MonoBehaviour
{
    [SerializeField] List<Image> healthImages;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] CreatureController player;
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
}
