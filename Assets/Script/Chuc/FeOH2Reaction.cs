using System.Collections;
using UnityEngine;
using TMPro;

public class FeOH2Reaction : MonoBehaviour
{
    [Header("State")]
    public bool isReacting = false;

    [Header("Tags")]
    public string oxygenZoneTag = "OxygenZone";

    [Header("VFX")]
    public ParticleSystem reactionVFX;

    [Header("UI")]
    public TMP_Text reactionText;

    [Header("Material")]
    public Renderer targetRenderer;
    public Color startColor = new Color(0.8f, 0.9f, 0.85f);
    public Color endColor = new Color(0.5f, 0.2f, 0.1f);

    [Header("Settings")]
    public float reactionDuration = 3f;

    private Material instanceMaterial;

    void Start()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponentInChildren<Renderer>();

        if (targetRenderer != null)
            instanceMaterial = targetRenderer.material;

        if (reactionVFX != null)
            reactionVFX.Stop();

        if (reactionText != null)
            reactionText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isReacting) return;

        if (other.CompareTag(oxygenZoneTag))
        {
            StartCoroutine(React());
        }
    }

    IEnumerator React()
    {
        isReacting = true;

        if (reactionVFX != null)
            reactionVFX.Play();

        if (reactionText != null)
        {
            reactionText.text = "Fe(OH)2 + O2 + H2O -> Fe(OH)3";
            reactionText.gameObject.SetActive(true);
        }

        yield return StartCoroutine(LerpColor(startColor, endColor, reactionDuration));
    }

    IEnumerator LerpColor(Color from, Color to, float time)
    {
        float t = 0f;

        while (t < time)
        {
            t += Time.deltaTime;
            Color current = Color.Lerp(from, to, t / time);

            if (instanceMaterial != null)
                instanceMaterial.color = current;

            yield return null;
        }

        if (instanceMaterial != null)
            instanceMaterial.color = to;
    }
}