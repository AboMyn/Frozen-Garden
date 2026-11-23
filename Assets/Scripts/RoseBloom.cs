using System.Collections;
using UnityEngine;

public class RoseBloom : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Bloom Settings")]
    public float bloomDuration = 0.5f;

    [Header("Frozen Flower State")]
    public Vector3 frozenScale = new Vector3(3f, 3f, 3f);
    public Vector3 frozenRotation = new Vector3(-35f, 0f, 0f);
    public Color frozenColor = new Color(0.67f, 0.82f, 1f);

    [Header("Bloomed Flower State")]
    public Vector3 bloomedScale = new Vector3(4f, 4f, 4f);
    public Vector3 bloomedRotation = Vector3.zero;
    public Color bloomedColor = Color.white;

    [Header("Platform Swap")]
    public GameObject frozenPlatform;
    public GameObject realPlatform;

    private bool bloomed = false;
    private Renderer flowerRenderer;
    private Material flowerMaterial;

    [Header("Tree Fade Out")]
    public TreeFadeOut[] treesToFade;

    [Header("Final Growth")]
    public GrowObjects roseGrowth;

    void Start()
    {
        transform.localScale = frozenScale;
        transform.localRotation = Quaternion.Euler(frozenRotation);

        flowerRenderer = GetComponentInChildren<Renderer>();

        if (flowerRenderer != null)
        {
            flowerMaterial = flowerRenderer.material;
            flowerMaterial.SetColor("_BaseColor", frozenColor);
        }

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (frozenPlatform != null)
            frozenPlatform.SetActive(true);

        if (realPlatform != null)
            realPlatform.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (bloomed) return;

        if (other.CompareTag("Player"))
        {
            bloomed = true;

            if (audioSource != null)
            {
                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.time = 2f;
                audioSource.Play();
                StartCoroutine(StopSound());
            }

            if (frozenPlatform != null)
                frozenPlatform.SetActive(false);

            if (realPlatform != null)
                realPlatform.SetActive(true);

            if (treesToFade != null)
            {
                foreach (TreeFadeOut tree in treesToFade)
                {
                    if (tree != null)
                        tree.FadeTree();
                }
            }
            
            if (roseGrowth != null)
                roseGrowth.ActivateGrowth();

            StartCoroutine(BloomAnimation());
        }
    }
    IEnumerator StopSound()
    {
        yield return new WaitForSeconds(4f);
        audioSource.Stop();
    }

    private IEnumerator BloomAnimation()
    {
        Vector3 startScale = transform.localScale;
        Quaternion startRotation = transform.localRotation;
        Color startColor = frozenColor;

        Vector3 targetScale = bloomedScale;
        Quaternion targetRotation = Quaternion.Euler(bloomedRotation);
        Color targetColor = bloomedColor;

        float timer = 0f;

        while (timer < bloomDuration)
        {
            timer += Time.deltaTime;
            float t = timer / bloomDuration;

            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, t);

            if (flowerMaterial != null)
            {
                flowerMaterial.SetColor("_BaseColor", Color.Lerp(startColor, targetColor, t));
            }

            yield return null;
        }

        transform.localScale = targetScale;
        transform.localRotation = targetRotation;

        if (flowerMaterial != null)
        {
            flowerMaterial.SetColor("_BaseColor", targetColor);
        }
    }
}