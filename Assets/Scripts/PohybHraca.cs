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
    private bool isRolling = false;

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

        if (!isJumping && !isRolling && !isStrafing && !IsStrafeAnimPlaying())
        {
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && targetPosition != leftPosition)
            {
                if (targetPosition == rightPosition)
                {
                    StartCoroutine(Strafe("left_strafe", middlePosition));
                }
                else
                {
                    StartCoroutine(Strafe("left_strafe", leftPosition));
                }
            }

            if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && targetPosition != rightPosition)
            {
                if (targetPosition == leftPosition)
                {
                    StartCoroutine(Strafe("right_strafe", middlePosition));
                }
                else
                {
                    StartCoroutine(Strafe("right_strafe", rightPosition));
                }
            }
        }


        if (!isRolling && !isJumping && !IsStrafeAnimPlaying() && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Jump());
        }

        if (!isRolling && !isJumping && !IsStrafeAnimPlaying() && Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Roll());
        }

    }

    private IEnumerator Strafe(string animationTrigger, Vector3 newPosition)
    {
        isStrafing = true;

        if (playerAnim != null)
        {
            playerAnim.SetTrigger(animationTrigger);
            playerAnim.ResetTrigger("run");
        }

        // Poèkaj, kým sa animácia naozaj zaène (prechod skonèí)
        yield return null; // èaká 1 frame
        while (playerAnim.IsInTransition(0))
        {
            yield return null;
        }

        // Poèkaj, kým sa animácia naozaj spustí
        AnimatorStateInfo stateInfo = playerAnim.GetCurrentAnimatorStateInfo(0);
        while (!stateInfo.IsName("left_strafe_anim") && !stateInfo.IsName("right_strafe_anim"))
        {
            yield return null;
            stateInfo = playerAnim.GetCurrentAnimatorStateInfo(0);
        }

        // Získaj dåžku animácie
        float strafeDuration = stateInfo.length;

        // Nastav novú pozíciu
        targetPosition = newPosition;

        // Poèkaj, kým sa animácia odohrá
        yield return new WaitForSeconds(strafeDuration);

        isStrafing = false;
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

    private IEnumerator Roll()
    {
        isRolling = true;

        if (playerAnim != null)
        {
            playerAnim.SetTrigger("slide");
            playerAnim.ResetTrigger("run");
        }

        // Simulate prone position (rolling in place)
        Vector3 originalPosition = transform.position;
        Vector3 pronePosition = new Vector3(originalPosition.x, originalPosition.y - 0.5f, originalPosition.z); // Lower the height to simulate lying down

        // Get the duration of the roll animation from the Animator
        AnimatorStateInfo stateInfo = playerAnim.GetCurrentAnimatorStateInfo(0);
        float rollDuration = stateInfo.length; // Get the length of the current animation clip (in seconds)

        // Instantly move the character to the prone position
        transform.position = pronePosition; // Directly set the character to the prone position

        // Wait for the duration of the roll animation
        float rollTime = 0f;
        while (rollTime < rollDuration)
        {
            // Slightly move the character to simulate a roll while staying in the same spot
            transform.position = Vector3.Lerp(originalPosition, pronePosition, rollTime / rollDuration);
            rollTime += Time.deltaTime;
            yield return null;
        }

        // After rolling, return to standing position
        transform.position = originalPosition;

        isRolling = false;
    }




}
