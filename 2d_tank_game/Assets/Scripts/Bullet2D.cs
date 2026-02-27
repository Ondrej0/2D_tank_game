using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet2D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 18f;

    [Header("Bounce")]
    [SerializeField] private int maxBounces = 1;
    [SerializeField] private float bounceCooldown = 0.05f;

    [Header("Lifetime")]
    [SerializeField] private float lifeSeconds = 5f;

    private Rigidbody2D rb;
    private int bouncesLeft;
    private float nextBounceTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bouncesLeft = maxBounces;
    }

    private void Start()
    {
        Destroy(gameObject, lifeSeconds);
    }

    /// Call this immediately after instantiating the bullet.
    public void Fire(Vector2 direction)
    {
        direction = direction.sqrMagnitude > 0.0001f ? direction.normalized : Vector2.up;
        rb.linearVelocity = direction * speed;
        transform.up = direction; // optional: makes bullet face travel direction (since your sprites face up)
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Prevent “machine-gun bounce” when staying in contact or corner-colliding
        if (Time.time < nextBounceTime) return;

        // If we somehow have no contacts, do nothing
        if (collision.contactCount == 0) return;

        // Use the first contact normal (good enough for tilemap collisions)
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 normal = contact.normal;

        Vector2 inVel = rb.linearVelocity;

        // If we're basically not moving, kill it
        if (inVel.sqrMagnitude < 0.01f)
        {
            Destroy(gameObject);
            return;
        }

        // If no bounces left, die on impact
        if (bouncesLeft <= 0)
        {
            Destroy(gameObject);
            return;
        }

        // Reflect the velocity around the surface normal
        Vector2 reflected = Vector2.Reflect(inVel, normal);

        rb.linearVelocity = reflected;
        transform.up = reflected; // optional visuals

        bouncesLeft--;
        nextBounceTime = Time.time + bounceCooldown;
    }
}