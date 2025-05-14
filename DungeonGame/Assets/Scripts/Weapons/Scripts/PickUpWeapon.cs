using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField] private WeaponManagerSO weaponPcik;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        WeaponHandler wepon = other.GetComponentInChildren<WeaponHandler>();
        if (!wepon) return;

        wepon.AssingWeapon(weaponPcik);
    }
}
