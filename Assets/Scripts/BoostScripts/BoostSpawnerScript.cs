using UnityEngine;

public class BoostSpawnerScript : MonoBehaviour
{
    public GameObject[] boostPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 0.5f;
    public float spawnTime = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnBoost), 0.5f, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBoost()
    {
        if (boostPrefab == null || boostPrefab.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Boost prefaby alebo spawn pointy nie sú nastavené.");
            return;
        }

        float random = Random.Range(0f, 1f);
        if (random <= 0.1f)
        {
            
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomSpawnIndex];
            Vector3 spawnPosition = spawnPoint.position + new Vector3(0, 1f, 0); // posun do výšky

           
            int randomBoostIndex = Random.Range(0, boostPrefab.Length);
            GameObject selectedBoost = boostPrefab[randomBoostIndex];

           
            Instantiate(selectedBoost, spawnPosition, spawnPoint.rotation);
        }
    }
}
