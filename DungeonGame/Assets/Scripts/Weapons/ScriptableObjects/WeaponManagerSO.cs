using UnityEngine;
using System;

[CreateAssetMenu(fileName = "WeaponManagerSO", menuName = "Weapons/WeaponManagerSO")]
public abstract class WeaponManagerSO : ScriptableObject, IWeaponsManager
{
    protected Transform weaponTransform;

    public virtual void Initialize(Transform weaponTransform)
    {
        this.weaponTransform = weaponTransform;
    }

    public abstract void WeaponData(float knockback, float damage, float Speed, float ComboMax, float currentCombo, Enum type , MeshRenderer renderer, ParticleSystem particleSystem, GameObject heldTransform);
}
