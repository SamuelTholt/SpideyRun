using UnityEngine;

public class PavucinaSpawner : MonoBehaviour
{
    public GameObject webPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 0.5f;
    public float spawnTime = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnWeb), 0.5f, spawnInterval);
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void SpawnWeb() {
        if (webPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Prefab alebo spawn pointy nie sú nastavené.");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        Vector3 spawnPosition = spawnPoint.position + new Vector3(0, 1f, 0);
        Instantiate(webPrefab, spawnPosition, spawnPoint.rotation);
    }
}
