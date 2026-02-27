using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TankMovement2D : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("References")]
    [SerializeField] private Transform body; // drag Body here

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void Update()
    {
        // WASD / Arrows (classic input)
        moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        // Your body sprite faces UP by default, so this is perfect:
        if (body != null && moveInput.sqrMagnitude > 0.001f)
            body.up = moveInput;
    }

    private void FixedUpdate()
    {
        // Unity 6: linearVelocity is the modern property
        rb.linearVelocity = moveInput * moveSpeed;
    }
}