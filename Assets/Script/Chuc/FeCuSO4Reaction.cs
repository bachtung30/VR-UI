using System.Collections;
using TMPro;
using UnityEngine;

public class FeCuSO4Reaction : MonoBehaviour
{
    [Header("State")]
    public bool hasReacted = false;

    [Header("Trigger")]
    public string ironTag = "Iron";

    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject resultPanel;

    [Header("Objects")]
    public GameObject resultLiquidObject;
    public GameObject copperLayerObject;

    [Header("Renderers")]
    public Renderer ironRenderer;
    public Renderer liquidRenderer;
    public Renderer copperLayerRenderer;

    [Header("Colors")]
    public Color liquidStartColor = new Color(0.1f, 0.5f, 0.9f);
    public Color liquidEndColor = new Color(0.6f, 0.85f, 0.6f);
    public Color ironStartColor = new Color(0.35f, 0.35f, 0.35f);
    public Color copperColor = new Color(0.75f, 0.35f, 0.15f);

    [Header("Text 3D")]
    public GameObject stepHintText;
    public GameObject feLabel;
    public GameObject cuSo4Label;

    [Header("Result Texts")]
    public GameObject reactionText1;
    public GameObject reactionText2;
    public GameObject reactionText3;

    [Header("Animation")]
    public float colorChangeDuration = 2f;

    private Material liquidMatInstance;
    private Material ironMatInstance;
    private Material copperMatInstance;

    private void Start()
    {
        if (liquidRenderer != null)
            liquidMatInstance = liquidRenderer.material;

        if (ironRenderer != null)
            ironMatInstance = ironRenderer.material;

        if (copperLayerRenderer != null)
            copperMatInstance = copperLayerRenderer.material;

        if (resultLiquidObject != null)
            resultLiquidObject.SetActive(true);

        if (copperLayerObject != null)
            copperLayerObject.SetActive(false);

        if (resultPanel != null)
            resultPanel.SetActive(false);

        if (reactionText1 != null) reactionText1.SetActive(false);
        if (reactionText2 != null) reactionText2.SetActive(false);
        if (reactionText3 != null) reactionText3.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasReacted) return;

        if (other.CompareTag(ironTag))
        {
            StartCoroutine(React());
        }
    }

    private IEnumerator React()
    {
        hasReacted = true;

        if (mainPanel != null)
            mainPanel.SetActive(false);

        if (resultPanel != null)
            resultPanel.SetActive(true);

        if (feLabel != null)
            feLabel.SetActive(false);

        if (cuSo4Label != null)
            cuSo4Label.SetActive(false);

        if (stepHintText != null)
            stepHintText.SetActive(true);

        if (reactionText1 != null)
            reactionText1.SetActive(true);

        if (reactionText2 != null)
            reactionText2.SetActive(true);

        if (reactionText3 != null)
            reactionText3.SetActive(true);

        if (resultLiquidObject != null)
            resultLiquidObject.SetActive(true);

        if (copperLayerObject != null)
            copperLayerObject.SetActive(true);

        float t = 0f;
        while (t < colorChangeDuration)
        {
            t += Time.deltaTime;
            float k = t / colorChangeDuration;

            if (liquidMatInstance != null)
                liquidMatInstance.color = Color.Lerp(liquidStartColor, liquidEndColor, k);

            if (ironMatInstance != null)
                ironMatInstance.color = Color.Lerp(ironStartColor, copperColor, k);

            if (copperMatInstance != null)
                copperMatInstance.color = copperColor;

            yield return null;
        }

        if (liquidMatInstance != null)
            liquidMatInstance.color = liquidEndColor;
    }

    public void BackToMain()
    {
        if (resultPanel != null)
            resultPanel.SetActive(false);

        if (mainPanel != null)
            mainPanel.SetActive(true);
    }
}