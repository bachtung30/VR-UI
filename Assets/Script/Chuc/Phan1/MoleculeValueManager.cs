using TMPro;
using UnityEngine;

public class MoleculeValueManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject resultPanel;

    [Header("Slots")]
    public ValueSlot caSlot;
    public ValueSlot cSlot;
    public ValueSlot oSlot;

    [Header("Current Selected Slot")]
    public ValueSlot selectedSlot;

    [Header("Result UI")]
    public TMP_Text resultText;
    public GameObject explanationTextObject;
    public TMP_Text explanationText;
    public GameObject answerTextObject;
    public TMP_Text answerText;

    [Header("Answer")]
    public int expectedCa = 2;
    public int expectedC = 4;
    public int expectedO = -2;

    private void Start()
    {
        if (caSlot != null)
        {
            caSlot.value = 0;
            caSlot.RefreshUIFromManager();
        }

        if (cSlot != null)
        {
            cSlot.value = 0;
            cSlot.RefreshUIFromManager();
        }

        if (oSlot != null)
        {
            oSlot.value = 0;
            oSlot.RefreshUIFromManager();
        }

        if (caSlot != null)
        {
            SetSelectedSlot(caSlot);
        }

        if (mainPanel != null)
            mainPanel.SetActive(true);

        if (resultPanel != null)
            resultPanel.SetActive(false);

        HideResultUI();
    }

    public void SetSelectedSlot(ValueSlot slot)
    {
        if (slot == null)
            return;

        if (selectedSlot != null)
            selectedSlot.SetSelected(false);

        selectedSlot = slot;
        selectedSlot.SetSelected(true);
    }

    public void IncreaseSelected()
    {
        if (selectedSlot != null)
            selectedSlot.Increase();
    }

    public void DecreaseSelected()
    {
        if (selectedSlot != null)
            selectedSlot.Decrease();
    }

    public void TestClick()
    {
        CheckAnswer();
    }

    public void CheckAnswer()
    {
        bool isCorrect = caSlot.value == expectedCa && cSlot.value == expectedC && oSlot.value == expectedO;

        if (mainPanel != null)
            mainPanel.SetActive(false);

        if (resultPanel != null)
            resultPanel.SetActive(true);

        if (resultText != null)
            resultText.text = isCorrect ? "Đã chính xác" : "Chưa chính xác";

        if (answerTextObject != null)
            answerTextObject.SetActive(isCorrect);

        if (answerText != null)
            answerText.text = isCorrect ? "Đã chính xác" : "";

        if (explanationTextObject != null)
            explanationTextObject.SetActive(isCorrect);

        if (explanationText != null)
            explanationText.text = isCorrect
                ? "Vì tổng số oxi hóa trong hợp chất bằng 0: (+2) + x + (-2).3 = 0 => x = +4"
                : "";
    }

    public void BackToMain()
    {
        if (resultPanel != null)
            resultPanel.SetActive(false);

        if (mainPanel != null)
            mainPanel.SetActive(true);
    }

    private void HideResultUI()
    {
        if (resultText != null)
            resultText.text = "";
            

        if (explanationTextObject != null)
            explanationTextObject.SetActive(false);

        if (answerTextObject != null)
            answerTextObject.SetActive(false);
    }
}