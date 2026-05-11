using UnityEngine;
using System.Collections;

public class PanelFadeController : MonoBehaviour
{
    public CanvasGroup panel1;
    public CanvasGroup panel2;

    public float fadeTime = 0.5f;

    void Start()
    {
        // Panel1 hiện
        panel1.alpha = 1;
        panel1.interactable = true;
        panel1.blocksRaycasts = true;

        // Panel2 ẩn
        panel2.alpha = 0;
        panel2.interactable = false;
        panel2.blocksRaycasts = false;
    }

    // 👉 NEXT: panel1 → panel2
    public void FadeToPanel2()
    {
        StartCoroutine(Fade(panel1, panel2));
    }

    // 👉 BACK: panel2 → panel1
    public void FadeToPanel1()
    {
        StartCoroutine(Fade(panel2, panel1));
    }

    IEnumerator Fade(CanvasGroup from, CanvasGroup to)
    {
        float t = 0;

        to.gameObject.SetActive(true);

        // bật panel đích
        to.interactable = true;
        to.blocksRaycasts = true;

        // tắt panel nguồn
        from.interactable = false;
        from.blocksRaycasts = false;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.SmoothStep(0, 1, t / fadeTime);

            from.alpha = 1 - alpha;
            to.alpha = alpha;

            yield return null;
        }

        from.alpha = 1;
        from.interactable = true;
        from.blocksRaycasts = true;
        from.gameObject.SetActive(false);
    }
}