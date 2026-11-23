using System.Collections;
using UnityEngine;
using TMPro;

public class GameEndTrigger : MonoBehaviour
{
    public GameObject endGamePanel;
    public TextMeshProUGUI endText;

    [Header("Timing")]
    public float showPanelDelay = 5f;

    private bool finished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (finished) return;

        if (other.CompareTag("Player"))
        {
            finished = true;
            StartCoroutine(ShowEndPanel());
        }
    }

    private IEnumerator ShowEndPanel()
    {
        yield return new WaitForSeconds(showPanelDelay);

        if (endGamePanel != null)
            endGamePanel.SetActive(true);

        if (endText != null)
            endText.text = "Spring has awakened!";

        Time.timeScale = 0f;
    }
}