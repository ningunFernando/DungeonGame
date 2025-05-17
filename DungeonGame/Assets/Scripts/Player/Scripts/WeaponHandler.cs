using UnityEngine;
using UnityEngine.UI;

public class WeaponHandler : MonoBehaviour
{
    public WeaponManagerSO weaponUsed;
    public MeshFilter meshRender;
    public MeshRenderer meshMaterial;

    HUDInstance hudInstance;


    private void Start()
    {
        meshRender = GetComponent<MeshFilter>();
        meshMaterial = GetComponent<MeshRenderer>();
    }

    public void AssingWeapon(WeaponManagerSO weaponAssing)
    {
        weaponUsed = weaponAssing;

        weaponUsed.Initialize(transform);
        if (hudInstance.weaponImage[0] != null)
        {
            hudInstance.weaponImage[0].texture = Instantiate(weaponUsed.image);
        }else if (hudInstance.weaponImage[1] != null)
        {
            hudInstance.weaponImage[1].texture = Instantiate(weaponUsed.image);

        }


    }
}
