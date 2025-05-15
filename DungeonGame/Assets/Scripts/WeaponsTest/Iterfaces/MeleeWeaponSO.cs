using System.Collections;
using UnityEngine;

[CreateAssetMenu (menuName = "Weapons/MeleeWeaponSO")]
public class MeleeWeaponSO : WeaponSO
{
    public override void Trigger()
    {
        if (!CanFire()) return;
        lastFireTime = Time.time;
        mountPoint.GetComponent<MonoBehaviour>()
            .StartCoroutine(PerformMelee());
    }

    private IEnumerator PerformMelee()
    {
        float t = 0f;
        while (t < hitboxActiveDuration)
        {
            var hits = Physics.OverlapBox(
                mountPoint.TransformPoint(hitboxCenterOffset),
                hitboxSize * 0.5f,
                mountPoint.rotation,
                hitMask
            );
            foreach (var col in hits)
                //col.GetComponent<Health>()?.TakeDamage(damage);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
