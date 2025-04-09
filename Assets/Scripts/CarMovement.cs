using UnityEngine;
using System.Linq;

public class CarMovement : MonoBehaviour
{
    public float rychlostPohybu = 25.0f;
    private Vector3 smerPohybu = Vector3.left;


    private void Start()
    {
        if (gameObject.name.Contains("car1"))
        {
            smerPohybu = Vector3.left;
        }
        else {
            smerPohybu = Vector3.right;
        }
    }
    void Update()
    {
        transform.Translate(smerPohybu * rychlostPohybu * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision detected with: " + other.gameObject.name);

        if (other.gameObject.CompareTag("DestroyWall"))
        {
            Debug.Log("Auto znièené: " + gameObject.name);
            Destroy(gameObject);
        }
    }
}
