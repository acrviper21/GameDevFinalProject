using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarHandler : MonoBehaviour
{
    [SerializeField] Image healthPipPrefab;
    [SerializeField] CreatureController player;
    [SerializeField] List<Image> pips = new List<Image>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set the hearts to match health when starting
        for(int i = 0; i < player.GetHealth(); i++)
        {
            Image newPip = Instantiate(healthPipPrefab, transform);
            pips.Add(newPip);
        }
    }

    public void UpdateHealth()
    {
        Image newPip = Instantiate(healthPipPrefab, transform);
        pips.Add(newPip);
    }
}
