using UnityEngine;

public class PohybSveta : MonoBehaviour
{
    public float rychlostPohybu = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(rychlostPohybu, 0, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("DestroyWall"))
        {
            Destroy(gameObject);

        }
    }
}
