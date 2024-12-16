using UnityEngine;
using UnityEngine.Audio;  // Required for AudioMixer
using UnityEngine.UI;     // Required for UI elements like Slider

public class VolumeControl : MonoBehaviour
{
    public string mixerVolumeVariable;
    public AudioMixer masterMixer;  // Reference to the AudioMixer
    public Slider volumeSlider;     // Reference to the UI Slider
    public Image volumeButton;

    public Sprite[] volumeSpriteList = new Sprite[5];

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
        
        masterMixer.SetFloat(mixerVolumeVariable, volume);
        volumeButton.sprite = sliderValue switch
        {
            > 0.75f => volumeSpriteList[4],
            > 0.5f => volumeSpriteList[3],
            > 0.25f => volumeSpriteList[2],
            > 0.0f => volumeSpriteList[1],
            _ => volumeSpriteList[0]
        };
    }
    
    public void ToggleMute()
    {
        if (!masterMixer.GetFloat(mixerVolumeVariable, out var currentVolume)) return;
        if (currentVolume <= -80f)
        {
            SetVolume(volumeSlider.value);
        }
        else
        {
            SetVolume(0);
        }
    }

    // Initialize the slider position based on the stored preferences or current volume in the AudioMixer
    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetVolume);

        if (masterMixer.GetFloat(mixerVolumeVariable, out var currentVolume))
        {
            // Convert dB to a linear slider value between 0 and 1
            volumeSlider.value = Mathf.Pow(10, currentVolume / 20);
        }
        else
        {
            volumeSlider.value = 1;
        }
    }
}