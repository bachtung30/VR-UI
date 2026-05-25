using UnityEngine;
using UnityEngine.InputSystem; // Thư viện bắt buộc

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;

    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundLayer;

    public Transform orientation; // CameraHolder
    public MouseLook mouseLook;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Nếu chuột không khóa (đang hiện menu) -> không di chuyển
        if (mouseLook != null && !mouseLook.IsLocked()) return;

        // 1. Kiểm tra mặt đất
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        // 2. Xử lý di chuyển (Thay thế cho Input.GetAxis)
        float x = 0;
        float z = 0;

        if (Keyboard.current != null)
        {
            // Kiểm tra các phím đơn lẻ
            if (Keyboard.current.wKey.isPressed) z += 1f;
            if (Keyboard.current.sKey.isPressed) z -= 1f;
            if (Keyboard.current.aKey.isPressed) x -= 1f;
            if (Keyboard.current.dKey.isPressed) x += 1f;
        }

        // Tính toán hướng di chuyển dựa trên hướng camera (orientation)
        Vector3 move = orientation.forward * z + orientation.right * x;
        move.y = 0; // Giữ nhân vật không bị bay lên khi nhìn lên trời

        // Áp dụng vận tốc
        rb.velocity = new Vector3(move.normalized.x * speed, rb.velocity.y, move.normalized.z * speed);

        // 3. Xử lý nhảy (Thay thế cho Input.GetKeyDown)
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }
}