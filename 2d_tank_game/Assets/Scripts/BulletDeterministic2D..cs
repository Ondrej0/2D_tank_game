using UnityEngine;

public class BulletDeterministic2D : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float lifeSeconds = 5f;
    [SerializeField] private float rayLength = 0.2f;
    [SerializeField] private int maxBounces = 1;
    [SerializeField] private LayerMask wallMask;

    private Vector2 direction = Vector2.up;
    private int bouncesUsed = 0;

    public void Fire(Vector2 dir)
    {
        direction = dir.normalized;
        transform.up = direction;
    }

    private void Start()
    {
        Destroy(gameObject, lifeSeconds);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, wallMask);

        if (hit.collider != null)
        {
            if (bouncesUsed >= maxBounces)
            {
                Destroy(gameObject);
                return;
            }

            direction = Vector2.Reflect(direction, hit.normal).normalized;
            transform.up = direction;
            bouncesUsed++;
        }

        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
}