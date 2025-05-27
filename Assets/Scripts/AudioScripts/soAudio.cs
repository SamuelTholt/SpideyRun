using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "soAudio", menuName = "Scriptable Objects/soAudio")]
public class soAudio : ScriptableObject
{
    [Header("Default Values")]
    public float defaultMasterVolume = 0.75f;
    public float defaultMusicVolume = 0.75f;
    public float defaultEffectsVolume = 0.75f;

    public UnityEvent OnSettingsChanged;

    // Cachovan� hodnoty pre lep�iu performance
    private float? _cachedMasterVolume;
    private float? _cachedMusicVolume;
    private float? _cachedEffectsVolume;

    // Vlastnosti, ktor� ��taj� z PlayerPrefs s cachovan�m
    public float masterVolume
    {
        get
        {
            if (_cachedMasterVolume == null)
                _cachedMasterVolume = PlayerPrefs.GetFloat("MasterVolume", defaultMasterVolume);
            return _cachedMasterVolume.Value;
        }
        set
        {
            _cachedMasterVolume = value;
            PlayerPrefs.SetFloat("MasterVolume", value);
            // Okam�it� ulo�enie
            PlayerPrefs.Save();
        }
    }

    public float musicVolume
    {
        get
        {
            if (_cachedMusicVolume == null)
                _cachedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", defaultMusicVolume);
            return _cachedMusicVolume.Value;
        }
        set
        {
            _cachedMusicVolume = value;
            PlayerPrefs.SetFloat("MusicVolume", value);
            PlayerPrefs.Save();
        }
    }

    public float effectsVolume
    {
        get
        {
            if (_cachedEffectsVolume == null)
                _cachedEffectsVolume = PlayerPrefs.GetFloat("EffectsVolume", defaultEffectsVolume);
            return _cachedEffectsVolume.Value;
        }
        set
        {
            _cachedEffectsVolume = value;
            PlayerPrefs.SetFloat("EffectsVolume", value);
            PlayerPrefs.Save();
        }
    }

    public void UpdateMasterVolume(float master)
    {
        masterVolume = master;
        OnSettingsChanged?.Invoke();
    }

    public void UpdateMusicVolume(float music)
    {
        musicVolume = music;
        OnSettingsChanged?.Invoke();
    }

    public void UpdateEffectsVolume(float effects)
    {
        effectsVolume = effects;
        OnSettingsChanged?.Invoke();
    }

    public void UpdateAudioSettings(float master, float music, float effects)
    {
        masterVolume = master;
        musicVolume = music;
        effectsVolume = effects;
        OnSettingsChanged?.Invoke();
    }

    public void SaveAudioSettings()
    {
        PlayerPrefs.Save();
        Debug.Log("Audio settings saved to PlayerPrefs");
    }

    public void LoadAudioSettings()
    {
        // Vynulovanie cache pre op�tovn� na��tanie
        _cachedMasterVolume = null;
        _cachedMusicVolume = null;
        _cachedEffectsVolume = null;

        OnSettingsChanged?.Invoke();
        Debug.Log($"Audio settings loaded - Master: {masterVolume}, Music: {musicVolume}, Effects: {effectsVolume}");
    }

    public void ResetToDefaults()
    {
        // Vymazanie z PlayerPrefs
        PlayerPrefs.DeleteKey("MasterVolume");
        PlayerPrefs.DeleteKey("MusicVolume");
        PlayerPrefs.DeleteKey("EffectsVolume");

        // Vynulovanie cache
        _cachedMasterVolume = null;
        _cachedMusicVolume = null;
        _cachedEffectsVolume = null;

        PlayerPrefs.Save();
        OnSettingsChanged?.Invoke();
        Debug.Log("Audio settings reset to defaults");
    }

    // Met�da pre debugging
    public void LogCurrentSettings()
    {
        Debug.Log($"Current Audio Settings - Master: {masterVolume}, Music: {musicVolume}, Effects: {effectsVolume}");
    }
}