using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public WeaponManagerSO weaponUsed;
    public MeshFilter meshRender;
    public MeshRenderer meshMaterial;

    private void Start()
    {
        meshRender = GetComponent<MeshFilter>();
        meshMaterial = GetComponent<MeshRenderer>();
    }

    public void AssingWeapon(WeaponManagerSO weaponAssing)
    {
        weaponUsed = weaponAssing;

        weaponUsed.Initialize(transform);


    }
}
