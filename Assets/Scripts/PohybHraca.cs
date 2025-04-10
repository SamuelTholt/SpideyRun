using System;
using System.Collections;
using UnityEngine;

public class PohybHraca : MonoBehaviour
{
    
    public float moveSpeed = 10.0f;

    public float jumpVyska = 5.0f;
    public float jumpCas = 2.0f;

    private bool isJumping = false;
    private bool isStrafing = false;

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

        if (playerAnim != null)
        {
            if (transform.position == targetPosition && !isJumping && !isStrafing)
            {
                playerAnim.SetTrigger("run");
            }
            else
            {
                playerAnim.ResetTrigger("run");
            }
        }
    }

    private void Pohyb()
    {

        if (playerAnim == null) return;

        if (!isJumping) {
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && targetPosition != leftPosition)
            {
                if (targetPosition == rightPosition)
                {
                    playerAnim.SetTrigger("left_strafe");
                    //playerAnim.ResetTrigger("run");
                    targetPosition = middlePosition;
                    isStrafing = true;
                }
                else
                {
                    playerAnim.SetTrigger("left_strafe");
                    //playerAnim.ResetTrigger("run");
                    targetPosition = leftPosition;
                    isStrafing = true;

                }
                isStrafing = false;
            }

            if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && targetPosition != rightPosition)
            {
                if (targetPosition == leftPosition)
                {
                    playerAnim.SetTrigger("right_strafe");
                    playerAnim.ResetTrigger("run");
                    targetPosition = middlePosition;
                    isStrafing = true;
                }
                else
                {
                    playerAnim.SetTrigger("right_strafe");
                    playerAnim.ResetTrigger("run");
                    targetPosition = rightPosition;
                    isStrafing = true;
                }
                isStrafing = false;
            }
        }

        if (!isJumping && !IsStrafeAnimPlaying() && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Jump());
        }

    }

    private bool IsStrafeAnimPlaying()
    {
        if (playerAnim != null)
        {
            AnimatorStateInfo stateInfo = playerAnim.GetCurrentAnimatorStateInfo(0);
            // Skontroluj, èi je animácia strafe v aktívnom stave
            return stateInfo.IsName("left_strafe_anim") || stateInfo.IsName("right_strafe_anim");
        }
        return false;
    }

    private IEnumerator Jump()
    {
        isJumping = true;

        if (playerAnim != null)
        {
            playerAnim.SetTrigger("jump");
            playerAnim.ResetTrigger("run");
        }

        Vector3 startPos = transform.position;
        Vector3 peakPos = new Vector3(startPos.x, startPos.y + jumpVyska, startPos.z);

        float elapsedTime = 0f;

        // Skok hore
        while (elapsedTime < jumpCas / 2)
        {
            transform.position = Vector3.Lerp(startPos, peakPos, (elapsedTime / (jumpCas / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Skok dole
        elapsedTime = 0f;
        while (elapsedTime < jumpCas / 2)
        {
            transform.position = Vector3.Lerp(peakPos, startPos, (elapsedTime / (jumpCas / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = startPos;
        isJumping = false;

        isStrafing = false;
    }


}
