using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public Transform cameraTransform; // Reference to the camera's Transform

    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Vector2 moveInput; // Stores the player's movement input

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Automatically assign the main camera if not set
        }
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;

        if (cameraTransform != null)
        {
            Vector3 cameraPosition = transform.position;
            cameraPosition.z = cameraTransform.position.z; // Keep the camera's original Z position
            cameraTransform.position = cameraPosition;
        }
    }
}