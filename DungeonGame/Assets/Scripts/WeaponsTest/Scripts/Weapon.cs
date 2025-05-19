// Weapon.cs
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Basics")]
    [Tooltip("Seconds between allowed attacks/triggers")]
    public float cooldown = 1f;
    public bool isAttacking;
    protected float lastTriggerTime;
    public Texture image;



    /// <summary>Call once after Instantiate</summary>
    public virtual void Initialize()
    {
        lastTriggerTime = -cooldown;
    }

    /// <summary>Call this from your input callback</summary>
    public void TryTrigger()
    {
        if (Time.time < lastTriggerTime + cooldown) 
            return;

        lastTriggerTime = Time.time;
        OnTrigger();
    }

    /// <summary>Override with your fire/attack logic</summary>
    protected abstract void OnTrigger();

    /// <summary>Optional per-frame hooks (e.g. for cooldown UI)</summary>
    public virtual void Tick(float dt) { }
}