// ComboMeleeWeapon.cs
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Weapon))]
public class ComboMeleeWeapon : Weapon
{
    [System.Serializable]
    public struct ComboStep
    {
        [Tooltip("Animator Trigger parameter for this step")]
        public string triggerName;

        [Tooltip("Delay (s) from trigger → hitbox active")]
        public float hitDelay;

        [Tooltip("Duration (s) that the hitbox stays active")]
        public float activeDuration;

        [Tooltip("Knockback force applied to hit targets")]
        public float knockbackForce;
    }

    [Header("Combo Settings")]
    [Tooltip("Configure each combo step")]
    public ComboStep[] comboSteps;
    [Tooltip("Seconds until combo resets")]
    public float comboResetTime = 1f;

    [Header("Hitbox Settings")]
    public Vector3 hitboxCenter = Vector3.zero;
    public Vector3 hitboxSize   = new Vector3(1,1,1);
    public LayerMask hitMask;
    public int damage           = 1;

    // internal state
    private Animator animator;
    private int comboIndex;
    private float lastComboTime;

    public override void Initialize()
    {
        base.Initialize();
        animator      = GetComponentInParent<Animator>();
        comboIndex    = 0;
        lastComboTime = -comboResetTime;
    }

    protected override void OnTrigger()
    {
        if (comboSteps == null || comboSteps.Length == 0) return;

        float now = Time.time;
        if (now > lastComboTime + comboResetTime)
            comboIndex = 0;

        lastComboTime = now;

        // grab this step's data
        var step = comboSteps[comboIndex];

        // play animation
        if (animator != null && !string.IsNullOrEmpty(step.triggerName))
            animator.SetTrigger(step.triggerName);

        // do hit/damage/knockback with this step's timing
        StartCoroutine(PerformMelee(step));

        // advance combo index
        comboIndex = (comboIndex + 1) % comboSteps.Length;
    }

    private IEnumerator PerformMelee(ComboStep step)
    {
        // wait for the hit frame
        if (step.hitDelay > 0f)
            yield return new WaitForSeconds(step.hitDelay);

        float elapsed = 0f;
        while (elapsed < step.activeDuration)
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
                // damage via IDamageable
                if (c.TryGetComponent<IDamageable>(out var dmgable))
                    dmgable.TakeDamage(damage);

                // apply knockback if they have a Rigidbody
                if (step.knockbackForce != 0f
                    && c.attachedRigidbody != null)
                {
                    // direction away from the weapon
                    Vector3 dir = (c.transform.position - transform.position).normalized;
                    c.attachedRigidbody.AddForce(dir * step.knockbackForce, ForceMode.Impulse);
                }
            }

            elapsed += Time.deltaTime;
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
