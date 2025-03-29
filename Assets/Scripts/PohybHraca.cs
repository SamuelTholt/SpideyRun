using UnityEngine;

public class PohybHraca : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    public Animator playerAnim;
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

         if (playerAnim != null && transform.position == targetPosition)
        {
            playerAnim.SetTrigger("run");
        }
    }

    private void Pohyb()
    {

        if (playerAnim == null) return;

        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && targetPosition != leftPosition)
        {
            if (targetPosition == rightPosition)
            {
                playerAnim.SetTrigger("left_strafe");
                playerAnim.ResetTrigger("run");
                targetPosition = middlePosition;
            }
            else
            {
                playerAnim.SetTrigger("left_strafe");
                playerAnim.ResetTrigger("run");
                targetPosition = leftPosition;

            }

        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && targetPosition != rightPosition)
        {
            if (targetPosition == leftPosition)
            {
                playerAnim.SetTrigger("right_strafe");
                playerAnim.ResetTrigger("run");
                targetPosition = middlePosition;
            }
            else
            {
                playerAnim.SetTrigger("right_strafe");
                playerAnim.ResetTrigger("run");
                targetPosition = rightPosition;
            }
        }
    }
}
