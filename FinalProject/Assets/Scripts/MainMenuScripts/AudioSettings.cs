using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] AudioMixer mixer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMasterVolume()
    {
        SetVolume("MasterVolume", masterSlider.value);
    }

    public void SetMusicVolume()
    {
        SetVolume("MusicVolume", musicSlider.value);
    }

    public void SetSFXVolume()
    {
        SetVolume("SFXVolume", sfxSlider.value);
    }

    void SetVolume(string groupName, float value)
    {
        float adjustedValue = Mathf.Log10(value) * 20f;
        if(value ==0 )
        {
            adjustedValue = -80f;
        }
        mixer.SetFloat(groupName, adjustedValue);
    }
}
