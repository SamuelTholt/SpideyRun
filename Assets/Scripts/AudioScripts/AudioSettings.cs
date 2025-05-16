using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public soAudio audioData;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;


    public TextMeshProUGUI masterVolumeText;
    public TextMeshProUGUI musicVolumeText;
    public TextMeshProUGUI sfxVolumeText;

    void Start()
    {
        audioData.LoadAudioSettings();

        InitializeSlider(masterSlider, "Master", audioData.masterVolume);
        InitializeSlider(musicSlider, "Music", audioData.musicVolume);
        InitializeSlider(sfxSlider, "Effects", audioData.effectsVolume);

        ApplyAudioSettings();
        UpdateVolumeTexts();
    }

    private void InitializeSlider(Slider slider, string parameterName, float defaultValue)
    {
        slider.value = defaultValue;
    }

    public void SetMasterVolume(float volume)
    {
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat("Master", dB);
        masterVolumeText.text = $"{dB:F1} dB";
        audioData.UpdateMasterVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat("Music", dB);
        musicVolumeText.text = $"{dB:F1} dB";
        audioData.UpdateMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat("Effects", dB);
        sfxVolumeText.text = $"{dB:F1} dB";
        audioData.UpdateEffectsVolume(volume);
    }

    private void ApplyAudioSettings()
    {
        SetMasterVolume(audioData.masterVolume);
        SetMusicVolume(audioData.musicVolume);
        SetSFXVolume(audioData.effectsVolume);
    }

    private void UpdateVolumeTexts()
    {
        if (audioMixer.GetFloat("Master", out float masterVolume))
        {
            masterVolumeText.text = $"{masterVolume:F1} dB";
        }

        if (audioMixer.GetFloat("Music", out float musicVolume))
        {
            musicVolumeText.text = $"{musicVolume:F1} dB";
        }

        if (audioMixer.GetFloat("Effects", out float sfxVolume))
        {
            sfxVolumeText.text = $"{sfxVolume:F1} dB";
        }
    }

    public void SaveSettings()
    {
        audioData.SaveAudioSettings();
    }

    public void ResetSettings()
    {
        float defaultValue = 1f;

        masterSlider.value = defaultValue;
        musicSlider.value = defaultValue;
        sfxSlider.value = defaultValue;

        SetMasterVolume(defaultValue);
        SetMusicVolume(defaultValue);
        SetSFXVolume(defaultValue);

        // Aktualizujeme ScriptableObject a uložíme zmeny
        audioData.UpdateAudioSettings(defaultValue, defaultValue, defaultValue);
        audioData.SaveAudioSettings();

        UpdateVolumeTexts();
    }
}
