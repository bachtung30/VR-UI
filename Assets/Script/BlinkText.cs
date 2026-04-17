using UnityEngine;
using TMPro;
// test 100
public class BlinkText : MonoBehaviour
{
    public float speed = 2f;

    private TextMeshProUGUI text;
    private float timer = 0f;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        timer = 0f; // reset khi hiện lại
    }

    void Update()
    {
        float alpha = Mathf.Abs(Mathf.Sin(Time.time * speed));
        text.alpha = alpha;
    }
}