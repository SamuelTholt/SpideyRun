using UnityEngine;

public class ObjectPrefabMovement : MonoBehaviour
{
    public float rychlostPohybu = 25.0f;
    private Vector3 smerPohybu = Vector3.right;

    private float turnSpeed = 90f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(smerPohybu * rychlostPohybu * Time.deltaTime, Space.World);
        transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("DestroyWall"))
        {
            //Debug.Log("Pavucina znièená: " + gameObject.name);
            Destroy(gameObject);
        }
    }
}
