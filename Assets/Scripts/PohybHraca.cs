using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PohybHraca : MonoBehaviour
{

    public float moveSpeed = 10.0f;

    public float jumpVyska = 5.0f;
    public float jumpCas = 2.0f;


    private bool isJumping = false;
    private bool isStrafing = false;
    private bool isRolling = false;

    private float lastRollTime = -Mathf.Infinity;
    private float actionCooldown = 1.5f;

    private float lastStrafeTime = -Mathf.Infinity;
    private float strafeCooldown = 0.7f;

    public Animator playerAnim;
    private Vector3 leftPosition = new Vector3(15.12413f, 1.055456f, -18.66f);
    private Vector3 middlePosition = new Vector3(15.12413f, 1.055456f, -13.87f);
    private Vector3 rightPosition = new Vector3(15.12413f, 1.055456f, -9.08f);

    private Vector3 targetPosition;

    public AudioSource soundOfJump;

    public AudioSource soundOfRoll;

    public AudioSource soundOfStrafe;

    public GameObject shield;


    private bool neznicitelny = false; 
    private float neznicitelnyCas = 0f;

    void Start()
    {
        targetPosition = middlePosition;
        transform.position = middlePosition;

        if (shield != null)
        {
            shield.SetActive(false);
        }
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        Pohyb();

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (playerAnim != null)
        {
            if (transform.position == targetPosition && !isJumping && !isStrafing && !isRolling)
            {
                playerAnim.SetTrigger("run");
            }
            else
            {
                playerAnim.ResetTrigger("run");
            }
        }
        KontrolaNeznicitelnosti();
    }

    private void Pohyb()
    {

        if (playerAnim == null) return;

        // Strafe iba ak nie je žiadna akcia aktívna A je po cooldowne
        if (!isJumping && !isStrafing && Time.time - lastStrafeTime >= strafeCooldown)
        {
            // LEFT STRAFE
            if (Input.GetKeyDown(KeyCode.A) && targetPosition != leftPosition && !isRolling)
            {
                if (soundOfStrafe != null)
                {
                    soundOfStrafe.Play();
                }

                // Resetuj všetky triggery pred nastavením nového
                playerAnim.ResetTrigger("left_strafe");
                playerAnim.ResetTrigger("right_strafe");
                playerAnim.ResetTrigger("run");

                playerAnim.SetTrigger("left_strafe");

                if (targetPosition == rightPosition)
                {
                    targetPosition = middlePosition;
                }
                else
                {
                    targetPosition = leftPosition;
                }

                isStrafing = true;
                lastStrafeTime = Time.time; // Nastav èas posledného strafe
            }

            // RIGHT STRAFE
            else if (Input.GetKeyDown(KeyCode.D) && targetPosition != rightPosition && !isRolling)
            {
                if (soundOfStrafe != null)
                {
                    soundOfStrafe.Play();
                }

                // Resetuj všetky triggery pred nastavením nového
                playerAnim.ResetTrigger("left_strafe");
                playerAnim.ResetTrigger("right_strafe");
                playerAnim.ResetTrigger("run");

                playerAnim.SetTrigger("right_strafe");

                if (targetPosition == leftPosition)
                {
                    targetPosition = middlePosition;
                }
                else
                {
                    targetPosition = rightPosition;
                }

                isStrafing = true;
                lastStrafeTime = Time.time; // Nastav èas posledného strafe
            }
        }

        if (!isJumping && !isStrafing && !isRolling && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Jump());
        }

        if (!isJumping && !isStrafing && !isRolling && Input.GetKeyDown(KeyCode.S)
            && Time.time - lastRollTime >= actionCooldown)
        {
            lastRollTime = Time.time;
            StartCoroutine(Roll());
        }

    }

    public void FinishStrafe()
    {
        StartCoroutine(DelayedStrafeFinish());
    }

    private IEnumerator DelayedStrafeFinish()
    {
        yield return new WaitForSeconds(0.05f);
        isStrafing = false;
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


        if (soundOfJump != null) 
        { 
            soundOfJump.Play();
        }

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

    }

    private IEnumerator Roll()
    {
        isRolling = true;

        if (playerAnim != null)
        {
            playerAnim.SetTrigger("roll");
            playerAnim.ResetTrigger("run");
        }

        CapsuleCollider capsule = GetComponentInChildren<CapsuleCollider>();
        float originalHeight = capsule.height;
        Vector3 originalCenter = capsule.center;

        float targetHeight = originalHeight / 4f;
        Vector3 targetCenter = new Vector3(originalCenter.x, originalCenter.y - 0.5f, originalCenter.z);

        float rollDuration = playerAnim.GetCurrentAnimatorStateInfo(0).length;

        if (soundOfRoll != null)
        {
            soundOfRoll.Play();
        }

        float time = 0f;

        while (time < rollDuration / 2)
        {
            capsule.height = Mathf.Lerp(originalHeight, targetHeight, time / (rollDuration / 2));
            capsule.center = Vector3.Lerp(originalCenter, targetCenter, time / (rollDuration / 2));
            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(rollDuration / 2);
        time = 0f;
        while (time < rollDuration / 2)
        {
            capsule.height = Mathf.Lerp(targetHeight, originalHeight, time / (rollDuration / 2));
            capsule.center = Vector3.Lerp(targetCenter, originalCenter, time / (rollDuration / 2));
            time += Time.deltaTime;
            yield return null;
        }

        capsule.height = originalHeight;
        capsule.center = originalCenter;

        isRolling = false;
    }


    private void KontrolaNeznicitelnosti()
    {
        if (neznicitelny)
        {
            neznicitelnyCas -= Time.deltaTime;
            if (neznicitelnyCas <= 0f)
            {
                DeaktivovatNeznicitelnost();
            }
        }
    }

    public bool JeNeznicitelny()
    {
        return neznicitelny;
    }

    public void AktivovatNeznicitelnost(float trvanie)
    {
        neznicitelny = true;
        neznicitelnyCas = trvanie;

        // Zobraz shield
        if (shield != null)
        {
            shield.SetActive(true);
        }

        Debug.Log($"Neznièite¾nos aktivovaná na {trvanie} sekúnd.");
    }

    public void DeaktivovatNeznicitelnost()
    {
        neznicitelny = false;

        if (shield != null)
        {
            shield.SetActive(false);
        }

        Debug.Log("Neznièite¾nos vypršala.");
    }


}