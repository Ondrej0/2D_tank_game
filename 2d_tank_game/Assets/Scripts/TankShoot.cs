using UnityEngine;

public class TankShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform muzzle;     // drag Muzzle (or Turret if you don't want Muzzle)
    [SerializeField] private Bullet2D bulletPrefab;

    [Header("Shooting")]
    [SerializeField] private float fireRate = 6f; // bullets per second

    private float nextFireTime;

    private void Update()
    {
        // Left click
        if (Input.GetMouseButton(0))
        {
            TryFire();
        }
    }

    private void TryFire()
    {
        if (Time.time < nextFireTime) return;
        if (muzzle == null || bulletPrefab == null) return;

        nextFireTime = Time.time + (1f / fireRate);

        Bullet2D bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);

        // Your turret faces UP, so forward direction is muzzle.up
        bullet.Fire(muzzle.up);
    }
}