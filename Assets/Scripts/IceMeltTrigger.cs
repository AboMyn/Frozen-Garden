using System.Collections;
using UnityEngine;

public class IceMeltTrigger : MonoBehaviour
{
    public GameObject[] icePieces;

    public float meltDelay = 0.3f;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;
            StartCoroutine(MeltIce());
        }
    }

    IEnumerator MeltIce()
    {
        foreach (GameObject ice in icePieces)
        {
            if (ice != null)
            {
                // Destroy(ice);
                StartCoroutine(MeltPiece(ice));
                yield return new WaitForSeconds(meltDelay);
            }
        }
    }

        IEnumerator MeltPiece(GameObject ice)
    {
        float t = 0;

        Vector3 start = ice.transform.localScale;

        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            ice.transform.localScale = Vector3.Lerp(start, Vector3.zero, t);
            yield return null;
        }

        Destroy(ice);
    }
}