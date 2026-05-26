using TMPro;
using UnityEngine;

public class RedoxValueManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject resultPanel;

    [Header("6 Slots")]
    public ValueSlot reactionNSlot;
    public ValueSlot reactionO2Slot;
    public ValueSlot nh3Slot;
    public ValueSlot o2MainSlot;
    public ValueSlot noSlot;
    public ValueSlot h2oSlot;

    [Header("Current Selected Slot")]
    public ValueSlot selectedSlot;

    [Header("Result UI")]
    public TMP_Text resultText;
    public GameObject explanationTextObject;
    public TMP_Text explanationText;
    public GameObject answerTextObject;
    public TMP_Text answerText;

    [Header("Expected Answers")]
    public int expectedReactionN = 4;
    public int expectedReactionO2 = 5;
    public int expectedNh3 = 4;
    public int expectedO2Main = 5;
    public int expectedNo = 4;
    public int expectedH2o = 6;

    private void Start()
    {
        ResetPuzzle();

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

    public void CheckAnswer()
    {
        bool isCorrect = reactionNSlot != null && reactionO2Slot != null && nh3Slot != null && o2MainSlot != null && noSlot != null && h2oSlot != null
            && reactionNSlot.value == expectedReactionN
            && reactionO2Slot.value == expectedReactionO2
            && nh3Slot.value == expectedNh3
            && o2MainSlot.value == expectedO2Main
            && noSlot.value == expectedNo
            && h2oSlot.value == expectedH2o;

        if (mainPanel != null)
            mainPanel.SetActive(false);

        if (resultPanel != null)
            resultPanel.SetActive(true);

        if (resultText != null)
            resultText.text = isCorrect ? "Đã chính xác" : "Chưa chính xác";

        if (answerTextObject != null)
            answerTextObject.SetActive(isCorrect);

        if (answerText != null)
            answerText.text = isCorrect ? "4NH3 + 5O2 -> 4NO + 6H2O" : "";

        if (explanationTextObject != null)
            explanationTextObject.SetActive(isCorrect);

        if (explanationText != null)
            explanationText.text = isCorrect
                ? "Số electron nhường và nhận đã bằng nhau. Phương trình hoàn chỉnh là 4NH3 + 5O2 -> 4NO + 6H2O."
                : "";
    }

    public void BackToMain()
    {
        if (resultPanel != null)
            resultPanel.SetActive(false);

        if (mainPanel != null)
            mainPanel.SetActive(true);
    }

    public void ResetPuzzle()
    {
        if (reactionNSlot != null)
            reactionNSlot.ResetValue();

        if (reactionO2Slot != null)
            reactionO2Slot.ResetValue();

        if (nh3Slot != null)
            nh3Slot.ResetValue();

        if (o2MainSlot != null)
            o2MainSlot.ResetValue();

        if (noSlot != null)
            noSlot.ResetValue();

        if (h2oSlot != null)
            h2oSlot.ResetValue();

        if (selectedSlot != null)
            selectedSlot.SetSelected(false);

        selectedSlot = null;

        if (reactionNSlot != null)
            SetSelectedSlot(reactionNSlot);

        HideResultUI();

        if (resultPanel != null)
            resultPanel.SetActive(false);
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
