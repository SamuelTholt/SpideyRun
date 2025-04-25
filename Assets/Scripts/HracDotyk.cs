using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HracDotyk : MonoBehaviour
{
    public List<string> Taby;

    public AudioSource soundOfDead;

    public int maxZivotyHraca = 3;
    public List<RawImage> zivotyHraca;

    private int aktualnyPocetZivotov;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aktualnyPocetZivotov = maxZivotyHraca;
        if (zivotyHraca != null)
        {
            UpdateHeartsUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (Taby.Contains(collider.tag))
        {
            if (collider.gameObject.CompareTag("Car"))
            {
                //Debug.Log("Kolizia s: " + collider.gameObject.name);
                OdoberHP(1);
            }

            if (collider.gameObject.CompareTag("SpiderWeb"))
            {
                //Debug.Log("Hrac narazil na: " + collider.gameObject.name);
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.plusCountTakenWebs();
                } 
                Destroy(collider.gameObject);
            }
        }

    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < zivotyHraca.Count; i++)
        {
            zivotyHraca[i].gameObject.SetActive(i == aktualnyPocetZivotov - 1);
        }
    }

    public void OdoberHP(int pocet)
    {
        aktualnyPocetZivotov = Mathf.Max(0, aktualnyPocetZivotov - pocet);
        UpdateHeartsUI();

        if (aktualnyPocetZivotov <= 0)
        {
            StartCoroutine(RestartScene());
        }
    }

    public void PridajHP(int pocet)
    {
        aktualnyPocetZivotov = Mathf.Min(maxZivotyHraca, aktualnyPocetZivotov + pocet);
        UpdateHeartsUI();
    }


    private IEnumerator RestartScene()
    {
        if (soundOfDead != null)
        {
            soundOfDead.Play();
            yield return new WaitForSeconds(soundOfDead.clip.length);
        }

        if (GameManager.Instance != null)
        {
            Destroy(transform.root.gameObject);
            GameManager.Instance.EndGame();
        }
        else
        {
            Debug.LogError("GameManager instance nie je k dispozícii!");
        }
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }
}
