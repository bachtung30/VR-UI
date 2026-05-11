using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems; // Bắt buộc có để xử lý lỗi kẹt focus UI

public class InteractableLesson : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pressEText;
    public GameObject panel1;
    public GameObject panel2;

    [Header("Kéo object Player vào đây để tối ưu hiệu năng")]
    public GameObject playerObject;

    private bool playerInRange = false;
    private bool isOpen = false;
    private bool isProcessing = false; // Chốt chặn chống xung đột đúp phím

    void Start()
    {
        if (pressEText != null) pressEText.SetActive(false);
        if (panel1 != null) panel1.SetActive(false);
        if (panel2 != null) panel2.SetActive(false);
    }

    void Update()
    {
        // Ép chuột hiện liên tục khi bảng đang mở, tránh bị script khác cướp mất
        if (isOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        // Nếu nhân vật trong vùng và không bị vướng luồng xử lý UI
        if (playerInRange && !isProcessing)
        {
            if (pressEText != null && !pressEText.activeSelf)
            {
                pressEText.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(OpenPanelRoutine());
            }
        }
    }

    IEnumerator OpenPanelRoutine()
    {
        isProcessing = true;
        isOpen = true;

        // 👉 FIX LỖI 1: Xóa focus UI cũ tránh kẹt EventSystem
        if (EventSystem.current != null) EventSystem.current.SetSelectedGameObject(null);

        if (pressEText != null) pressEText.SetActive(false);
        if (panel1 != null) panel1.SetActive(true);
        if (panel2 != null) panel2.SetActive(false);

        DisablePlayer(true);

        // 👉 FIX LỖI 2: Nghỉ 0.1s để tránh xung đột input giữa các frame
        yield return new WaitForSeconds(0.1f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isProcessing = false;
    }

    // 👉 GỌI KHI BẤM NEXT
    public void NextPanel()
    {
        if (panel1 != null) panel1.SetActive(false);
        if (panel2 != null) panel2.SetActive(true);
    }

    // 👉 GỌI KHI BẤM CLOSE
    public void CloseLesson()
    {
        if (isProcessing) return;
        StartCoroutine(ClosePanelRoutine());
    }

    IEnumerator ClosePanelRoutine()
    {
        isProcessing = true;
        isOpen = false;

        // 👉 FIX LỖI 1: Xóa focus UI khi đóng
        if (EventSystem.current != null) EventSystem.current.SetSelectedGameObject(null);

        if (panel1 != null) panel1.SetActive(false);
        if (panel2 != null) panel2.SetActive(false);

        // Khóa chuột về giữa màn hình trước
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 👉 FIX LỖI 2: Đợi hết frame xử lý click UI rồi mới mở lại điều khiển
        yield return new WaitForSeconds(0.05f);

        DisablePlayer(false);

        if (playerInRange && pressEText != null)
        {
            pressEText.SetActive(true);
        }

        isProcessing = false;
    }

    void DisablePlayer(bool disable)
    {
        // Ưu tiên dùng biến kéo thả từ Inspector, nếu quên kéo thì mới dùng lệnh Find (tránh lag)
        GameObject targetPlayer = playerObject != null ? playerObject : GameObject.FindGameObjectWithTag("Player");

        if (targetPlayer != null)
        {
            var movement = targetPlayer.GetComponent<PlayerMovement>();
            if (movement != null) movement.enabled = !disable;

            // 👉 FIX LỖI 3: Thêm 'true' để ép Unity quét tìm cả script MouseLook ĐANG BỊ TẮT
            var mouseLook = targetPlayer.GetComponentInChildren<MouseLook>(true);
            if (mouseLook != null) mouseLook.enabled = !disable;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (!isOpen && pressEText != null) pressEText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (pressEText != null) pressEText.SetActive(false);
        }
    }
}