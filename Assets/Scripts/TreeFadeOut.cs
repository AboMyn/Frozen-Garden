using System.Collections;
using UnityEngine;

public class TreeFadeOut : MonoBehaviour
{
    public float fadeDuration = 1f;

    private Renderer[] renderers;
    private Material[] materials;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();

        materials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            materials[i] = renderers[i].material;

            // Переключаем материал в Transparent режим через код
            materials[i].SetFloat("_Surface", 1);
            materials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            materials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            materials[i].SetInt("_ZWrite", 0);
            materials[i].DisableKeyword("_ALPHATEST_ON");
            materials[i].EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
            materials[i].renderQueue = 3000;
        }
    }

    public void FadeTree()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);

            foreach (Material mat in materials)
            {
                Color c = mat.color;
                c.a = alpha;
                mat.color = c;
            }

            yield return null;
        }

        gameObject.SetActive(false);
    }
}