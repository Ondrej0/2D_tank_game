using UnityEngine;

public class SmoothFollow2D : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Follow Feel")]
    [Tooltip("How quickly the camera catches up. Higher = snappier.")]
    [SerializeField] private float smoothTime = 0.15f;

    [Tooltip("Small offset in world units (e.g. (0, 0, -10)).")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -15f);

    private Vector3 velocity; // used by SmoothDamp

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = target.position + offset;

        // SmoothDamp gives that nice “camera weight” feel
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPos,
            ref velocity,
            smoothTime
        );
    }
}