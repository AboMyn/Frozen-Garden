using System.Collections;
using UnityEngine;

public class BounceMushroom : MonoBehaviour
{
    public float bounceForce = 12f;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Visual")]
    public Transform visual;

    [Header("Squash Animation")]
    public float squashY = 0.6f;
    public float squashXZ = 1.15f;
    public float squashTime = 0.08f;
    public float returnTime = 0.12f;

    private Vector3 originalScale;
    private bool isAnimating = false;

    void Start()
    {
        if (visual == null)
            visual = transform;

        originalScale = visual.localScale;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
                rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            }

            // звук
            if (audioSource != null)
            {
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.Play();
            }

            if (!isAnimating)
            {
                StartCoroutine(SquashAndStretch());
            }
        }
    }

    private IEnumerator SquashAndStretch()
    {
        isAnimating = true;

        Vector3 squashedScale = new Vector3(
            originalScale.x * squashXZ,
            originalScale.y * squashY,
            originalScale.z * squashXZ
        );

        float timer = 0f;

        while (timer < squashTime)
        {
            timer += Time.deltaTime;
            float t = timer / squashTime;
            visual.localScale = Vector3.Lerp(originalScale, squashedScale, t);
            yield return null;
        }

        timer = 0f;

        while (timer < returnTime)
        {
            timer += Time.deltaTime;
            float t = timer / returnTime;
            visual.localScale = Vector3.Lerp(squashedScale, originalScale, t);
            yield return null;
        }

        visual.localScale = originalScale;
        isAnimating = false;
    }
}