using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "soStaty", menuName = "Scriptable Objects/soStaty")]
public class soStaty : ScriptableObject
{
    public int highScore = 0;
    public float bestTime = 0f;

    public int skore = 0;
    public float cas = 0;

    public UnityEvent OnStatsUpdated;

    public void UpdateStats(int score, float time)
    {
        skore = score;
        cas = time;
        if (score > highScore) highScore = score;
        if (time > bestTime) bestTime = time;

        OnStatsUpdated.Invoke();
    }

    public void SaveStats()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetFloat("BestTime", bestTime);
        PlayerPrefs.Save();
    }

    public void LoadStats()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        bestTime = PlayerPrefs.GetFloat("BestTime", 0f);

        OnStatsUpdated.Invoke();
    }
}
