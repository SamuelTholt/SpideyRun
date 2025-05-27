using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

public class AudioSettings : MonoBehaviour
{
    [Header("Audio Components")]
    public AudioMixer audioMixer;
    public soAudio audioData;

    [Header("Fallback Settings")]
    public bool loadFromResources = true;
    public string resourcesPath = "soAudio";

    [Header("UI Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("UI Text")]
    public TextMeshProUGUI masterVolumeText;
    public TextMeshProUGUI musicVolumeText;
    public TextMeshProUGUI sfxVolumeText;

    [Header("Debug")]
    public bool debugMode = false;

    void Start()
    {
        StartCoroutine(InitializeAudioSettingsCoroutine());
    }

    private IEnumerator InitializeAudioSettingsCoroutine()
    {
        // PoËk·me jeden frame, aby sa vöetko inicializovalo
        yield return null;

        // ZÌskanie AudioManager-a (vytvorÌ sa automaticky ak neexistuje)
        AudioManager audioManager = AudioManager.GetInstance();

        // Pouûitie audio d·t z AudioManager-a
        if (audioData == null)
        {
            audioData = audioManager.GetAudioData();
        }

        // Pouûitie AudioMixer-a z AudioManager-a ak nie je nastaven˝
        if (audioMixer == null && audioManager.audioMixer != null)
        {
            audioMixer = audioManager.audioMixer;
        }

        if (audioData == null)
        {
            Debug.LogError("Nepodarilo sa zÌskaù audio d·ta z AudioManager-a!");
            yield break;
        }

        // ExplicitnÈ naËÌtanie nastavenÌ
        audioData.LoadAudioSettings();

        if (debugMode)
        {
            audioData.LogCurrentSettings();
        }

        // Inicializ·cia sliderov s naËÌtan˝mi hodnotami
        InitializeSliders();

        // Aplikovanie audio nastavenÌ
        ApplyAudioSettings();

        // Aktualiz·cia textov
        UpdateVolumeTexts();

        // Prihl·senie na zmeny (ak pouûÌvate UnityEvent)
        if (audioData.OnSettingsChanged != null)
        {
            audioData.OnSettingsChanged.AddListener(OnAudioSettingsChanged);
        }

        if (debugMode)
        {
            Debug.Log("AudioSettings inicializovanÈ ˙speöne");
        }
    }

    void OnDestroy()
    {
        // Uloûenie nastavenÌ pred zniËenÌm
        if (audioData != null)
        {
            audioData.SaveAudioSettings();

            // Odhl·senie z eventov
            if (audioData.OnSettingsChanged != null)
            {
                audioData.OnSettingsChanged.RemoveListener(OnAudioSettingsChanged);
            }
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && audioData != null)
        {
            audioData.SaveAudioSettings();
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus && audioData != null)
        {
            audioData.SaveAudioSettings();
        }
    }

    private void InitializeSliders()
    {
        if (masterSlider != null)
        {
            masterSlider.value = audioData.masterVolume;
            masterSlider.onValueChanged.RemoveAllListeners();
            masterSlider.onValueChanged.AddListener(SetMasterVolume);
        }

        if (musicSlider != null)
        {
            musicSlider.value = audioData.musicVolume;
            musicSlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = audioData.effectsVolume;
            sfxSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    public void SetMasterVolume(float volume)
    {
        float dB = ConvertToDecibel(volume);

        if (audioMixer != null)
        {
            audioMixer.SetFloat("Master", dB);
        }

        if (masterVolumeText != null)
            masterVolumeText.text = $"{dB:F1} dB";

        if (audioData != null)
        {
            audioData.UpdateMasterVolume(volume);
        }

        if (debugMode)
        {
            Debug.Log($"Master Volume set to: {volume} ({dB:F1} dB)");
        }
    }

    public void SetMusicVolume(float volume)
    {
        float dB = ConvertToDecibel(volume);

        if (audioMixer != null)
        {
            audioMixer.SetFloat("Music", dB);
        }

        if (musicVolumeText != null)
            musicVolumeText.text = $"{dB:F1} dB";

        if (audioData != null)
        {
            audioData.UpdateMusicVolume(volume);
        }

        if (debugMode)
        {
            Debug.Log($"Music Volume set to: {volume} ({dB:F1} dB)");
        }
    }

    public void SetSFXVolume(float volume)
    {
        float dB = ConvertToDecibel(volume);

        if (audioMixer != null)
        {
            audioMixer.SetFloat("Effects", dB);
        }

        if (sfxVolumeText != null)
            sfxVolumeText.text = $"{dB:F1} dB";

        if (audioData != null)
        {
            audioData.UpdateEffectsVolume(volume);
        }

        if (debugMode)
        {
            Debug.Log($"SFX Volume set to: {volume} ({dB:F1} dB)");
        }
    }

    private float ConvertToDecibel(float volume)
    {
        return Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
    }

    private void ApplyAudioSettings()
    {
        if (audioData != null)
        {
            SetMasterVolume(audioData.masterVolume);
            SetMusicVolume(audioData.musicVolume);
            SetSFXVolume(audioData.effectsVolume);
        }
    }

    private void UpdateVolumeTexts()
    {
        if (audioMixer == null) return;

        if (audioMixer.GetFloat("Master", out float masterVolume) && masterVolumeText != null)
        {
            masterVolumeText.text = $"{masterVolume:F1} dB";
        }

        if (audioMixer.GetFloat("Music", out float musicVolume) && musicVolumeText != null)
        {
            musicVolumeText.text = $"{musicVolume:F1} dB";
        }

        if (audioMixer.GetFloat("Effects", out float sfxVolume) && sfxVolumeText != null)
        {
            sfxVolumeText.text = $"{sfxVolume:F1} dB";
        }
    }

    public void SaveSettings()
    {
        if (audioData != null)
        {
            audioData.SaveAudioSettings();
            Debug.Log("Audio nastavenia uloûenÈ!");
        }
    }

    public void ResetSettings()
    {
        if (audioData == null) return;

        // Reset na defaultnÈ hodnoty
        audioData.ResetToDefaults();

        // Aktualiz·cia sliderov
        if (masterSlider != null) masterSlider.value = audioData.masterVolume;
        if (musicSlider != null) musicSlider.value = audioData.musicVolume;
        if (sfxSlider != null) sfxSlider.value = audioData.effectsVolume;

        // Aplikovanie zmien
        ApplyAudioSettings();
        UpdateVolumeTexts();

        Debug.Log("Audio nastavenia resetovanÈ na defaultnÈ hodnoty!");
    }

    // Callback pre zmeny v audio nastaveniach
    private void OnAudioSettingsChanged()
    {
        UpdateVolumeTexts();
    }

    // Pomocn· metÛda pre testovanie
    [ContextMenu("Test Load Settings")]
    public void TestLoadSettings()
    {
        if (audioData != null)
        {
            audioData.LogCurrentSettings();
        }
    }

    [ContextMenu("Force Save Settings")]
    public void ForceSaveSettings()
    {
        SaveSettings();
    }
}