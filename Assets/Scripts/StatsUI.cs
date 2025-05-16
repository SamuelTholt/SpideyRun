using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    public soStaty staty;


    public TextMeshProUGUI najlepsieSkoreText;


    public TextMeshProUGUI najlepsiCasText;


    void Start()
    {
        if (staty != null)
        {
            staty.LoadStats();

            if (najlepsieSkoreText != null)
            {
                najlepsieSkoreText.text = staty.highScore.ToString();
            }

            if (najlepsiCasText != null)
            {
                najlepsiCasText.text = staty.bestTime.ToString("F2");
            }

        }
    }

    void Update()
    {
        if (staty != null)
        {
            staty.LoadStats();

            if (najlepsieSkoreText != null)
            {
                najlepsieSkoreText.text = staty.highScore.ToString();
            }

            if (najlepsiCasText != null)
            {
                najlepsiCasText.text = staty.bestTime.ToString("F2");
            }
        }
    }
}
