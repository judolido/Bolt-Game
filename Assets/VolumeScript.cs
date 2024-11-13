using UnityEngine;
using UnityEngine.Audio;  // Required for AudioMixer
using UnityEngine.UI;     // Required for UI elements like Slider

public class VolumeControl : MonoBehaviour
{
    public AudioMixer MasterMixer;  // Reference to the AudioMixer
    public Slider VolumeSlider;     // Reference to the UI Slider

    // This function is called whenever the slider value is changed
    public void SetVolume(float sliderValue)
    {
        // Convert the slider value to the appropriate decibel value
        float volume = Mathf.Log10(sliderValue) * 20;  // Converts slider (0.0 - 1.0) to decibels
        MasterMixer.SetFloat("MasterVolume", volume);
    }

    // Initialize the slider position based on the current volume in the AudioMixer
    private void Start()
    {
        float currentVolume;
        if (MasterMixer.GetFloat("MasterVolume", out currentVolume))
        {
            // Convert dB to a linear slider value between 0 and 1
            VolumeSlider.value = Mathf.Pow(10, currentVolume / 20);
        }
        else
        {
            VolumeSlider.value = 1; // Set to full volume if no value is found
        }

        // Optionally, add a listener to call SetVolume when the slider is changed
        VolumeSlider.onValueChanged.AddListener(SetVolume);
    }
}
