// ComboMeleeWeapon.cs
using UnityEngine;
using System.Collections;
using UnityEditor;

[RequireComponent(typeof(Weapon))]
public class ComboMeleeWeapon : Weapon
{
    [Header("Combo Settings")]
    [Tooltip("Animator Trigger names for each combo step")]
    public string[] comboAnimationTriggers;
    [Tooltip("Seconds until combo resets")]
    public float comboResetTime = 1f;

    [Header("Melee Settings")]
    public Vector3 hitboxCenter = Vector3.zero;
    public Vector3 hitboxSize   = new Vector3(1,1,1);
    public LayerMask hitMask;
    
    public int damage = 1;
    [Tooltip("Delay (s) from trigger → hitbox active")]
    public float hitDelay       = 0.1f;
    [Tooltip("How long (s) the hitbox stays active")]
    public float activeDuration = 0.2f;

    private Animator animator;
    private int comboIndex;
    private float lastComboTime;

    public override void Initialize()
    {
        base.Initialize();

        animator = GetComponentInParent<Animator>();
        comboIndex = 0;
        lastComboTime = -comboResetTime;
    }

    protected override void OnTrigger()
    {
        float now = Time.time;
        if (now > lastComboTime + comboResetTime)
            comboIndex = 0;
        
        lastComboTime = now;
        if (animator != null && comboAnimationTriggers.Length > 0)
            animator.SetTrigger(comboAnimationTriggers[comboIndex]);
        
        StartCoroutine(PerformMelee());
        comboIndex = (comboIndex + 1) % comboAnimationTriggers.Length;
    }

    private IEnumerator PerformMelee()
    {
        if (hitDelay > 0f)
            yield return new WaitForSeconds(hitDelay);

        float t = 0f;
        while (t < activeDuration)
        {
            Vector3 worldCenter = transform.TransformPoint(hitboxCenter);
            Collider[] hits = Physics.OverlapBox(
                worldCenter,
                hitboxSize * 0.5f,
                transform.rotation,
                hitMask
            );

            foreach (var c in hits)
            {
                var damageable = c.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damage);
                }
            }
                

            t += Time.deltaTime;
            yield return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Matrix4x4 m = Matrix4x4.TRS(
            transform.TransformPoint(hitboxCenter),
            transform.rotation,
            hitboxSize
        );
        Gizmos.matrix = m;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
