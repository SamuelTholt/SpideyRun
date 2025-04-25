using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TextMeshProUGUI textCountWeb;
    public TextMeshProUGUI textTime;

    public soStaty staty;

    private int totalCountWeb = 0;

    private float cas = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("In�tancia GameManager bola vytvoren�.");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Duplicitn� GameManager bol zni�en�.");
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cas += Time.deltaTime;
        textCountWeb.text = totalCountWeb.ToString();
        textTime.text = cas.ToString("F2");
    }

    public void plusCountTakenWebs() {
        totalCountWeb++;
    }

    public void EndGame()
    {
        staty.UpdateStats(totalCountWeb, cas);
        staty.SaveStats();
        SceneManager.LoadScene("GameOverScene"); // Prechod na GameOver sc�nu
    }
}
