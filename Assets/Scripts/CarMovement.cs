using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float rychlostPohybu = 25.0f;

    private void Start()
    {
    }
    void Update()
    {
        transform.Translate(Vector3.left * rychlostPohybu * Time.deltaTime);
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
