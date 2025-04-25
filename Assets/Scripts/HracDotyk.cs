using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HracDotyk : MonoBehaviour
{
    public List<string> Taby;

    public AudioSource soundOfDead;

    public static bool isDead = false;
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
                Debug.Log("Kolizia s: " + collider.gameObject.name);
                isDead = true;
                StartCoroutine(RestartScene());
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
        isDead = false;
        SceneManager.LoadScene("SampleScene");
        Destroy(gameObject);
    }
}
