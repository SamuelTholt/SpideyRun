using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceny : MonoBehaviour
{
    public HracDotyk playerDotyk;
    public void prepniDoMenu()
    {
        if (playerDotyk != null)
        {
            playerDotyk.DestroyPlayer();
        }
        SceneManager.LoadScene("Menu");

    }

    public void prepniDoNastavenia()
    {
        SceneManager.LoadScene("Settings");
    }

    public void prepniDoPravidiel()
    {
        SceneManager.LoadScene("Rules");
    }

    public void prepniDoHry()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void prepniDoStatov()
    {
        SceneManager.LoadScene("Stats");
    }

    public void prepniDoGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Koniec()
    {
        Application.Quit();
    }
}
