using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;

    [Header("Mouse Look")]
    public Transform playerCamera;  // Assign your Camera here
    public float mouseSensitivity = 100f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f; // tracks up/down camera angle

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();

        if (Input.GetMouseButtonDown(0)) // Left mouse
        {
            ShootProjectile();
        }
    }

    void HandleMovement()
    {
        // Ground check
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // WASD movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate player (yaw)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera (pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // prevent flipping
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void ShootProjectile()
    {
        if (projectilePrefab == null) return;

        Vector3 spawnPos = playerCamera.position + playerCamera.forward * 1f;
        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb == null) rb = projectile.AddComponent<Rigidbody>();

        rb.useGravity = false;
        rb.AddForce(playerCamera.forward * projectileSpeed, ForceMode.VelocityChange);

        Destroy(projectile, 5f);
    }
}
