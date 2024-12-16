using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;  // Required for AudioMixer
using UnityEngine.UI;     // Required for UI elements like Slider

public class VolumeControl : MonoBehaviour
{
    public AudioMixer MasterMixer;  // Reference to the AudioMixer
    public Slider VolumeSlider;     // Reference to the UI Slider
    public Image VolumeButton;
    public Sprite[] VolumeSpriteList = new Sprite[5];

    // This function is called whenever the slider value is changed
    private void SetVolume(float sliderValue)
    {
        // Convert the slider value to the appropriate decibel value
        float volume;
        if (sliderValue > 0f) {
            volume = Mathf.Log10(sliderValue) * 20;  // Converts slider (0.0 - 1.0) to decibels
        } else
        {
            volume = -80f;
        }
        
        MasterMixer.SetFloat("MasterVolume", volume);
        VolumeButton.sprite = sliderValue switch
        {
            > 0.75f => VolumeSpriteList[4],
            > 0.5f => VolumeSpriteList[3],
            > 0.25f => VolumeSpriteList[2],
            > 0.0f => VolumeSpriteList[1],
            _ => VolumeSpriteList[0]
        };
    }
    
    public void ToggleMute()
    {
        if (!MasterMixer.GetFloat("MasterVolume", out var currentVolume)) return;
        if (currentVolume <= -80f)
        {
            SetVolume(VolumeSlider.value);
        }
        else
        {
            SetVolume(0);
        }
    }

    // Initialize the slider position based on the stored preferences or current volume in the AudioMixer
    private void Start()
    {
        VolumeSlider.onValueChanged.AddListener(SetVolume);

        if (MasterMixer.GetFloat("MasterVolume", out var currentVolume))
        {
            // Convert dB to a linear slider value between 0 and 1
            VolumeSlider.value = Mathf.Pow(10, currentVolume / 20);
        }
        else
        {
            VolumeSlider.value = 1;
        }
    }
}
