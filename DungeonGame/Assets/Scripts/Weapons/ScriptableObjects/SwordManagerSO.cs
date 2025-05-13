using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SwordManagerSO", menuName = "Scriptable Objects/SwordManagerSO")]
public class SwordManagerSO : WeaponManagerSO
{
    public float damage = 10f;
    public float speed = 5f;
    public float knockback = 5f;
    
    public override void WeaponData(float knockback, float damage, float Speed, float ComboMax, float currentCombo, Enum Type, MeshRenderer renderer, ParticleSystem particleSystem, GameObject heldTransform)
    {
        throw new NotImplementedException();
    }
}
