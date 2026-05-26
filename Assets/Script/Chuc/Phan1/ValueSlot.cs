using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValueSlot : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text valueText;
    public Image backgroundImage;

    [Header("Colors")]
    public Color normalColor = Color.white;
    public Color selectedColor = new Color(1f, 0.9f, 0.3f);

    [Header("Value")]
    public int value = 0;
    public int minValue = -9;
    public int maxValue = 9;

    private MoleculeValueManager moleculeManager;
    private RedoxValueManager redoxManager;

    private void Awake()
    {
        moleculeManager = GetComponentInParent<MoleculeValueManager>();
        redoxManager = GetComponentInParent<RedoxValueManager>();
        RefreshUI();
        SetSelected(false);
    }

    public void SelectSlot()
    {
        if (moleculeManager != null)
        {
            moleculeManager.SetSelectedSlot(this);
            return;
        }

        if (redoxManager != null)
        {
            redoxManager.SetSelectedSlot(this);
            return;
        }

        Debug.LogWarning(gameObject.name + " has no supported manager!");
    }

    public void Increase()
    {
        if (value < maxValue)
        {
            value++;
            RefreshUI();
        }
    }

    public void Decrease()
    {
        if (value > minValue)
        {
            value--;
            RefreshUI();
        }
    }

    public void SetSelected(bool isSelected)
    {
        if (backgroundImage != null)
        {
            backgroundImage.color = isSelected ? selectedColor : normalColor;
        }
    }

    public void RefreshUIFromManager()
    {
        RefreshUI();
    }

    public void ResetValue()
    {
        value = 0;
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (valueText != null)
        {
            valueText.text = value > 0 ? "+" + value : value.ToString();
        }
    }
}