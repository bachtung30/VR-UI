using System.Collections;
using UnityEngine;
using TMPro;

public class CharcoalReaction : MonoBehaviour
{
    [Header("State")]
    public bool isHot = false;
    public bool isBurning = false;

    [Header("Tags")]
    public string fireZoneTag = "FireZone";
    public string oxygenZoneTag = "OxygenZone";

    [Header("VFX")]
    public ParticleSystem glowVFX;
    public ParticleSystem sparkVFX;
    public ParticleSystem smokeVFX;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sizzleClip;

    [Header("UI")]
    public TMP_Text reactionText;

    [Header("Material")]
    public Renderer charcoalRenderer;
    public Color coldEmissionColor = Color.black;
    public Color hotEmissionColor = new Color(0.8f, 0.1f, 0.0f);
    public Color burningEmissionColor = Color.yellow;

    [Header("Burn Settings")]
    public float heatUpTime = 0.5f;
    public float burnDuration = 3f;
    public float shrinkDuration = 3f;

    private Material instanceMaterial;
    private bool hasHeatedOnce = false;

    void Start()
    {
        if (charcoalRenderer == null)
            charcoalRenderer = GetComponentInChildren<Renderer>();

        if (charcoalRenderer != null)
            instanceMaterial = charcoalRenderer.material;

        if (glowVFX != null) glowVFX.Stop();
        if (sparkVFX != null) sparkVFX.Stop();
        if (smokeVFX != null) smokeVFX.Stop();

        if (reactionText != null)
            reactionText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isBurning) return;

        if (!isHot && other.CompareTag(fireZoneTag))
        {
            HeatUp();
            return;
        }

        if (isHot && other.CompareTag(oxygenZoneTag))
        {
            StartCoroutine(BurnInOxygen());
        }
    }

    void HeatUp()
    {
        if (hasHeatedOnce) return;
        hasHeatedOnce = true;
        isHot = true;

        if (glowVFX != null && !glowVFX.isPlaying)
            glowVFX.Play();

        if (instanceMaterial != null)
            StartCoroutine(LerpEmission(coldEmissionColor, hotEmissionColor, heatUpTime));
    }

    IEnumerator BurnInOxygen()
    {
        isBurning = true;

        if (sparkVFX != null && !sparkVFX.isPlaying)
            sparkVFX.Play();

        if (smokeVFX != null && !smokeVFX.isPlaying)
            smokeVFX.Play();

        if (audioSource != null && sizzleClip != null)
            audioSource.PlayOneShot(sizzleClip);

        if (reactionText != null)
        {
            reactionText.text = "C + O2 → CO2";
            reactionText.gameObject.SetActive(true);
        }

        if (instanceMaterial != null)
            yield return StartCoroutine(LerpEmission(hotEmissionColor, burningEmissionColor, 0.3f));

        if (burnDuration > 0f)
            yield return new WaitForSeconds(burnDuration);

        yield return StartCoroutine(ShrinkAndDisappear());
    }

    IEnumerator LerpEmission(Color from, Color to, float time)
    {
        float t = 0f;

        while (t < time)
        {
            t += Time.deltaTime;
            Color current = Color.Lerp(from, to, t / time);
            if (instanceMaterial != null)
                instanceMaterial.SetColor("_EmissionColor", current);
            yield return null;
        }

        if (instanceMaterial != null)
            instanceMaterial.SetColor("_EmissionColor", to);
    }

    IEnumerator ShrinkAndDisappear()
    {
        Vector3 startScale = transform.localScale;
        float t = 0f;

        while (t < shrinkDuration)
        {
            t += Time.deltaTime;
            float factor = 1f - (t / shrinkDuration);
            transform.localScale = startScale * factor;
            yield return null;
        }

        transform.localScale = Vector3.zero;

        if (reactionText != null)
        {
            yield return new WaitForSeconds(1f);
            reactionText.gameObject.SetActive(false);
        }

        Destroy(gameObject);
    }
}