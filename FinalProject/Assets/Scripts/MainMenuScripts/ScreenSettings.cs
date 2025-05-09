using UnityEngine;
using System;
using TMPro;

public class ScreenSettings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolutionDropDown;
    Resolution[] resolutions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resolutions = Screen.resolutions;
        Resolution currentResolution = Screen.currentResolution;

        foreach(Resolution resolution in resolutions)
        {
            resolutionDropDown.options.Add(new TMP_Dropdown.OptionData(resolution.ToString()));
        }

        resolutionDropDown.value = Array.IndexOf(resolutions, currentResolution);
    }

    public void SetResolution()
    {
        int currentIndex = resolutionDropDown.value;
        Resolution rez = resolutions[currentIndex];
        Screen.SetResolution(rez.width, rez.height, Screen.fullScreen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
