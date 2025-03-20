using UnityEngine;

public class CestaCreationTrigger : MonoBehaviour
{
    public GameObject roadSection;
    public float x_position = -51.5f;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("TriggerWall") && roadSection != null)
        {
            Instantiate(roadSection, new Vector3(x_position, 11.60863f, -24.75315f), Quaternion.identity);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
