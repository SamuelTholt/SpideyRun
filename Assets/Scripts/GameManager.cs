using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TextMeshProUGUI textCountWeb;
    public TextMeshProUGUI textTime;

    public CarSpawner carSpawner;

    public TextMeshProUGUI textLevel;
    public TextMeshProUGUI textSpeed;
    public TextMeshProUGUI textInterval;

    public soStaty staty;

    private int totalCountWeb = 0;

    public int actualLevel = 1;

    private int totalCountWebOneLevel = 0;

    private int totalCountNextLevel = 50;

    public float baseCarSpeed = 25.0f;

    private float cas = 0f;

    private bool hraJePozastavena = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        transform.SetParent(null); // m��e� odstr�ni�, ak nie je potrebn�
        // DontDestroyOnLoad(gameObject); // VYMAZA� alebo zakomentova� tento riadok

        if (AudioManager.Instance == null)
        {
            AudioManager.GetInstance();
        }
    }

    void Start()
    {
        // Aplikovanie audio nastaven� na za�iatku sc�ny
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ApplyAudioSettings();
        }
    }


    // Update is called once per frame
    void Update()
    {
        cas += Time.deltaTime;
        textCountWeb.text = totalCountWeb.ToString();
        textTime.text = cas.ToString("F2");
        textLevel.text = actualLevel.ToString();

        UpdateSpeed();
        UpdateInterval();
    }

    // Pozastavenie a obnovenie hry
    public void PrepinacPozastavenia()
    {
        hraJePozastavena = !hraJePozastavena;

        if (hraJePozastavena)
        {
            Time.timeScale = 0f; // Pozastav� �as v hre
        }
        else
        {
            Time.timeScale = 1f; // Obnov� �as v hre
        }
    }

    public void UpdateSpeed() 
    {
        CarMovement[] auta = Object.FindObjectsByType<CarMovement>(FindObjectsSortMode.None);

        if (auta.Length > 0)
        {
            textSpeed.text = auta[0].rychlostPohybu.ToString("F2");
        }
        else
        {
            textSpeed.text = "N/A";
        }
    }

    public void UpdateInterval()
    {
        if (carSpawner != null)
        {
            textInterval.text = carSpawner.spawnInterval.ToString();

        }
    }

    public void PlusCountTakenWebs(int count) 
    {
        totalCountWeb += count;
        totalCountWebOneLevel += count;

        while (totalCountWebOneLevel >= totalCountNextLevel)
        {
            totalCountWebOneLevel -= totalCountNextLevel; // zvy�ok nech�me pre �al�� level
            NextLevel();
        }

    }

    public void NextLevel() 
    { 
        actualLevel++;
        totalCountNextLevel = 50;
        //totalCountWebOneLevel = 0;

        baseCarSpeed = Mathf.Min(baseCarSpeed + 0.05f, 35f);

        CarMovement[] auta = Object.FindObjectsByType<CarMovement>(FindObjectsSortMode.None);
        foreach (CarMovement auto in auta)
        {
            auto.ZvysRychlost();
        }

        if (carSpawner != null)
        {
            carSpawner.ZvysSpawnInterval();
        }
    }

    public void EndGame()
    {
        staty.UpdateStats(totalCountWeb, cas);
        staty.SaveStats();
        SceneManager.LoadScene("GameOverScene"); // Prechod na GameOver sc�nu
    }
}
