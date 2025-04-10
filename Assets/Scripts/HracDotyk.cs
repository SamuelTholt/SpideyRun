using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HracDotyk : MonoBehaviour
{
    public List<string> Taby;
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
                StartCoroutine(RestartScene());
            }
        }
    }

    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("SampleScene");
        Destroy(gameObject);
    }
}
