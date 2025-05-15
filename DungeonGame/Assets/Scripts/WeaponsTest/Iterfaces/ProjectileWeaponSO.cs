using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/ProjectileWeaponSo")]
public class ProjectileWeaponSO : WeaponSO
{
    public float projectileSpeed = 20f;

    public override void Trigger()
    {
        if (!CanFire()) return;
        lastFireTime = Time.time;
        var proj = Instantiate(
            prefab,
            mountPoint.position + mountPoint.TransformVector(holdPositionOffset),
            mountPoint.rotation * Quaternion.Euler(holdRotationOffset)
        );
        var rb = proj.GetComponent<Rigidbody>();
        if (rb != null) rb.linearVelocity = mountPoint.forward * projectileSpeed;
    }
}