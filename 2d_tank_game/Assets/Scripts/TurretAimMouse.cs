using UnityEngine;

public class TurretAimMouse : MonoBehaviour
{
    [SerializeField] private Transform turret; 
    [SerializeField] private Camera cam;

    private void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    private void Update()
    {
        if (turret == null || cam == null) return;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Vector2 dir = (mouseWorld - turret.position);

        // Your turret sprite faces UP by default — so this is the cleanest way:
        if (dir.sqrMagnitude > 0.0001f)
            turret.up = dir;
    }
}