using UnityEngine;
using System.Collections;


public class DistanceWeapon : Weapon
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public float projectileVelocity = 20f;
    public Transform projectileSpawnPoint;
    private Animator animator;

    public override void Initialize()
    {
        base.Initialize();
        animator = GetComponentInParent<Animator>();
     
    }
    protected override void OnTrigger()
    {
       StartCoroutine(CastSpell());
    }
    private IEnumerator CastSpell()
    {
        animator.SetTrigger("Spell");
        yield return new WaitForSeconds(0.24f);
        var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        var rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = projectileSpawnPoint.forward * projectileVelocity;

     

    }
}
