using UnityEngine;

public class TankShoot : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private BulletDeterministic2D bulletPrefab;
    [SerializeField] private float fireRate = 6f;

    private float nextFireTime;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            TryFire();
    }

    private void TryFire()
    {
        if (Time.time < nextFireTime) return;
        if (muzzle == null || bulletPrefab == null) return;

        nextFireTime = Time.time + (1f / fireRate);

        var bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        bullet.Fire(muzzle.up); // since your turret art faces UP
    }
}