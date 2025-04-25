using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public soStaty staty;

    public TextMeshProUGUI tvojeSkoreText;
    public TextMeshProUGUI najlepsieSkoreText;

    public TextMeshProUGUI tvojCasText;
    public TextMeshProUGUI najlepsiCasText;


    void Start()
    {
        if (staty != null)
        {
            staty.LoadStats();

            if (tvojeSkoreText != null)
            {
                tvojeSkoreText.text = staty.skore.ToString();
            }
            if (tvojCasText != null)
            {
                tvojCasText.text = staty.cas.ToString("F2");
            }

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

            if (tvojeSkoreText != null)
            {
                tvojeSkoreText.text = staty.skore.ToString();
            }
            if (tvojCasText != null)
            {
                tvojCasText.text = staty.cas.ToString("F2");
            }

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
