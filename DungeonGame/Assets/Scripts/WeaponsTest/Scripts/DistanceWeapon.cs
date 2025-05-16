using UnityEngine;

public class DistanceWeapon : Weapon
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public float projectileVelocity = 20f;
    public Transform projectileSpawnPoint;

    protected override void OnTrigger()
    {
        var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        var rb = projectile.GetComponent<Rigidbody>();
        if(rb != null)
            rb.linearVelocity = projectileSpawnPoint.forward * projectileVelocity;
    }
}
