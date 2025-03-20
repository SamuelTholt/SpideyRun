using UnityEngine;

public class PohybHraca : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    private Vector3 leftPosition = new Vector3(15.12413f, 1.055456f, -18.66f);
    private Vector3 middlePosition = new Vector3(15.12413f, 1.055456f, -13.87f);
    private Vector3 rightPosition = new Vector3(15.12413f, 1.055456f, -9.08f);

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = middlePosition;
        transform.position = middlePosition;
    }

    void Update()
    {
        Pohyb();

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void Pohyb()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && targetPosition != leftPosition)
        {
            if (targetPosition == rightPosition)
                targetPosition = middlePosition;
            else
                targetPosition = leftPosition;
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && targetPosition != rightPosition)
        {
            if (targetPosition == leftPosition)
                targetPosition = middlePosition;
            else
                targetPosition = rightPosition;
        }
    }
}
