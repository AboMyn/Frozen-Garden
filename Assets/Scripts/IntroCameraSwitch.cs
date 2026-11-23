using System.Collections;
using UnityEngine;
using TMPro;

public class IntroCameraSwitch : MonoBehaviour
{
    public Camera introCamera;
    public Camera mainCamera;

    public TextMeshProUGUI objectiveText;

    public float introDuration = 3f;

    void Start()
    {
        mainCamera.gameObject.SetActive(false);
        introCamera.gameObject.SetActive(true);

        if (objectiveText != null)
            objectiveText.gameObject.SetActive(true);

        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence()
    {
        yield return new WaitForSeconds(introDuration);

        if (objectiveText != null)
            objectiveText.gameObject.SetActive(false);

        introCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
    }
}