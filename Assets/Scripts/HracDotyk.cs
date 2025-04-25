using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HracDotyk : MonoBehaviour
{
    public List<string> Taby;

    public AudioSource soundOfDead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
                StartCoroutine(RestartScene());
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
