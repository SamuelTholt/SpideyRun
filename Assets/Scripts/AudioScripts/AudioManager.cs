using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Components")]
    public AudioMixer audioMixer;
    public soAudio audioData;

    [Header("Settings")]
    public bool loadFromResources = true;
    public string resourcesPath = "soAudio";

    void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Nastavenie n·zvu objektu pre lepöiu identifik·ciu
        gameObject.name = "AudioManager (Singleton)";

        // NaËÌtanie audio d·t
        InitializeAudioData();

        // Prihl·senie na scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Statick· metÛda pre automatickÈ vytvorenie AudioManager-a
    [System.Obsolete]
    public static AudioManager GetInstance()
    {
        if (Instance == null)
        {
            // Pokus o n·jdenie existuj˙ceho AudioManager-a
            Instance = FindObjectOfType<AudioManager>();

            if (Instance == null)
            {
                // Vytvorenie novÈho GameObject s AudioManager
                GameObject audioManagerGO = new GameObject("AudioManager (Auto-Created)");
                Instance = audioManagerGO.AddComponent<AudioManager>();

                Debug.Log("AudioManager automaticky vytvoren˝");
            }
        }

        return Instance;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SaveAudioSettings();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveAudioSettings();
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveAudioSettings();
        }
    }

    private void InitializeAudioData()
    {
        if (audioData == null && loadFromResources)
        {
            audioData = Resources.Load<soAudio>(resourcesPath);
            if (audioData == null)
            {
                Debug.LogError($"Nepodarilo sa naËÌtaù soAudio z Resources/{resourcesPath}");
                return;
            }
        }

        if (audioData != null)
        {
            audioData.LoadAudioSettings();
            ApplyAudioSettings();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Pri naËÌtanÌ novej scÈny aplikujeme audio nastavenia
        if (audioData != null && audioMixer != null)
        {
            ApplyAudioSettings();
        }
    }

    public void ApplyAudioSettings()
    {
        if (audioData == null || audioMixer == null) return;

        SetMasterVolume(audioData.masterVolume);
        SetMusicVolume(audioData.musicVolume);
        SetSFXVolume(audioData.effectsVolume);
    }

    public void SetMasterVolume(float volume)
    {
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        if (audioMixer != null)
        {
            audioMixer.SetFloat("Master", dB);
        }

        if (audioData != null)
        {
            audioData.UpdateMasterVolume(volume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        if (audioMixer != null)
        {
            audioMixer.SetFloat("Music", dB);
        }

        if (audioData != null)
        {
            audioData.UpdateMusicVolume(volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        if (audioMixer != null)
        {
            audioMixer.SetFloat("Effects", dB);
        }

        if (audioData != null)
        {
            audioData.UpdateEffectsVolume(volume);
        }
    }

    public void SaveAudioSettings()
    {
        if (audioData != null)
        {
            audioData.SaveAudioSettings();
        }
    }

    public soAudio GetAudioData()
    {
        return audioData;
    }
}