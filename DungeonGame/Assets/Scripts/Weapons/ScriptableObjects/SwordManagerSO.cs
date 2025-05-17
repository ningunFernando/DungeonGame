using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SwordManagerSO", menuName = "Scriptable Objects/SwordManagerSO")]
public class SwordManagerSO : WeaponManagerSO
{
    public float damage = 10f;
    public float speed = 5f;
    public float knockback = 5f;
    public Mesh mesh;
    public Material material;

    //public ParticleSystem particleSystem;

    public override void WeaponData(float knockback, float damage, float Speed, Texture image, float ComboMax, float currentCombo, Enum Type, Mesh renderer, ParticleSystem particleSystem, GameObject heldTransform)
    {
        throw new NotImplementedException();
    }

    public void MeshChange(WeaponHandler weponHandler)
    {
        weponHandler.meshRender.mesh = mesh;
        weponHandler.meshMaterial.material = material;
    }
}
