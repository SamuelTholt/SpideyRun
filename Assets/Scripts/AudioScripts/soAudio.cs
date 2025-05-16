using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "soAudio", menuName = "Scriptable Objects/soAudio")]
public class soAudio : ScriptableObject
{
    public float masterVolume = 0.75f;
    public float musicVolume = 0.75f;
    public float effectsVolume = 0.75f;

    public UnityEvent OnSettingsChanged;

    public void UpdateMasterVolume(float master)
    {
        masterVolume = master;
        OnSettingsChanged.Invoke();
    }

    public void UpdateMusicVolume(float music)
    {
        musicVolume = music;
        OnSettingsChanged.Invoke();
    }

    public void UpdateEffectsVolume(float effects)
    {
        effectsVolume = effects;
        OnSettingsChanged.Invoke();
    }

    public void UpdateAudioSettings(float master, float music, float effects)
    {
        masterVolume = master;
        musicVolume = music;
        effectsVolume = effects;

        OnSettingsChanged.Invoke();
    }

    public void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolume);
        PlayerPrefs.Save();
    }

    public void LoadAudioSettings()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        effectsVolume = PlayerPrefs.GetFloat("EffectsVolume", 1f);

        OnSettingsChanged.Invoke();
    }
}
