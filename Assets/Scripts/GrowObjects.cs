using System.Collections;
using UnityEngine;

public class GrowObjects : MonoBehaviour
{
    public Transform[] objectsToGrow;
    public float growDuration = 1f;
    public float delayBetweenObjects = 0.1f;

    private Vector3[] targetScales;
    private bool activated = false;

    void Start()
    {
        targetScales = new Vector3[objectsToGrow.Length];

        for (int i = 0; i < objectsToGrow.Length; i++)
        {
            if (objectsToGrow[i] != null)
            {
                targetScales[i] = objectsToGrow[i].localScale;
                objectsToGrow[i].localScale = Vector3.zero;
            }
        }
    }

    public void ActivateGrowth()
    {
        if (activated) return;
        activated = true;
        StartCoroutine(GrowAll());
    }

    private IEnumerator GrowAll()
    {
        for (int i = 0; i < objectsToGrow.Length; i++)
        {
            if (objectsToGrow[i] != null)
            {
                StartCoroutine(GrowOne(objectsToGrow[i], targetScales[i]));
                yield return new WaitForSeconds(delayBetweenObjects);
            }
        }
    }

    private IEnumerator GrowOne(Transform obj, Vector3 targetScale)
    {
        float timer = 0f;
        Vector3 startScale = Vector3.zero;

        while (timer < growDuration)
        {
            timer += Time.deltaTime;
            float t = timer / growDuration;

            obj.localScale = Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        obj.localScale = targetScale;
    }
}