using UnityEngine;
using System.Linq;

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

        GameObject selectedCarType = carPrefabs[randomCarIndex];
        Quaternion rotation = Quaternion.Euler(0, 180, 0);

        if (selectedCarType.name.Contains("car1"))
        {
            rotation = Quaternion.Euler(0, 180, 0);
        }
        else {
            rotation = Quaternion.Euler(0, 0, 0);
        }

        GameObject spawnedCar = Instantiate(carPrefabs[randomCarIndex], spawnPoints[randomLaneIndex].position, rotation);

        string[] changeColorCars = { "car2", "truck1", "truck2", "bus", "monstertruck" };
        if (changeColorCars.Any(name => selectedCarType.name.Contains(name))) {
            SetRandomCarColor(spawnedCar);
        }
        //Instantiate(carPrefabs[randomCarIndex], spawnPoints[randomLaneIndex].position, rotation);
    }


    void SetRandomCarColor(GameObject car)
    {
        MeshRenderer carRenderer = car.GetComponentInChildren<MeshRenderer>();

        if (carRenderer != null)
        {
            
            int materialIndex = -1;
            for (int i = 0; i < carRenderer.materials.Length; i++)
            {
                if (carRenderer.materials[i].name.Contains("CarColor"))
                {
                    materialIndex = i;
                    break;
                }
            }

            if (materialIndex != -1)
            {
                carRenderer.materials[materialIndex].color = new Color(Random.value, Random.value, Random.value);
            }
            else
            {
                Debug.LogWarning("Materiál 'CarColor' nebol nájdený na " + car.name);
            }
        }
    }
}
