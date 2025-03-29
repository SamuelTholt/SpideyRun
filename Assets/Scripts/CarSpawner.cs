using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 2.0f;
    void Start()
    {
        InvokeRepeating(nameof(SpawnCar), 1.0f, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnCar() 
    {
        if (carPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Car prefabs or spawn points are missing!");
            return;
        }

        int randomCarIndex = Random.Range(0, carPrefabs.Length);
        int randomLaneIndex = Random.Range(0, spawnPoints.Length);

        Quaternion rotation = Quaternion.Euler(0, 180, 0);

        Instantiate(carPrefabs[randomCarIndex], spawnPoints[randomLaneIndex].position, rotation);
    }
}
