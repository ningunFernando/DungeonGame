using System;
using UnityEngine;

public interface IWeaponsManager
{
    void Initialize( Transform weaponTransform);
    
    void WeaponData(float knockback, float damage, float Speed, float ComboMax, float currentCombo, Enum Type, Mesh renderer, ParticleSystem particleSystem, GameObject heldTransform);
}
