using UnityEngine;

public abstract class WeaponSO : ScriptableObject, IWeaponController
{
    [Header("Visual & Hold")]
    public GameObject prefab;
    public Vector3 holdPositionOffset;
    public Vector3 holdRotationOffset;

    [Header("Collider (for melee)")]
    public bool isMelee;
    public float hitboxActiveDuration;
    public Vector3 hitboxCenterOffset;
    public Vector3 hitboxSize;
    public LayerMask hitMask;
    public int damage;

    [Header("Cooldown & Animation")]
    public float cooldown;
    public string animationTrigger;  //Nombre del animation Trigger

    protected float lastFireTime;
    protected Transform mountPoint;

    public virtual void Initialize(Transform weaponMount)
    {
        //Se inicializa donde se coloca el arma, calculos de cooldown e instanciacion de el arma
        mountPoint = weaponMount;
        lastFireTime = -cooldown;
        var inst = Instantiate(prefab, mountPoint);
        inst.transform.localPosition = holdPositionOffset;
        inst.transform.localRotation = Quaternion.Euler(holdRotationOffset);
    }

    public abstract void Trigger();
    public virtual void Tick(float deltaTime) { }

    protected bool CanFire() => Time.time >= lastFireTime + cooldown;
}