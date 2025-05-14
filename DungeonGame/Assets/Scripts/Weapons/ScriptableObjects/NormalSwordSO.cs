using UnityEngine;

[CreateAssetMenu(fileName = "NormalSwordSO", menuName = "Scriptable Objects/NormalSwordSO")]
public class NormalSwordSO : SwordManagerSO
{
    protected WeaponHandler weponHandler;
    public override void Initialize(Transform weaponTransform)
    {
        base.Initialize(weaponTransform);
        weponHandler = weaponTransform.GetComponent<WeaponHandler>();
        MeshChange(weponHandler);
    }
}
