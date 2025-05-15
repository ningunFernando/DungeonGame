using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Tooltip("Assign your player’s hand bone or mount here")]
    public Transform[] weaponMounts = new Transform[2];

    public WeaponSO[] weaponSlots = new WeaponSO[2];
    private IWeaponController[] controllers = new IWeaponController[2];
    private int currentSlot;

    [Tooltip("Reference to the Animator on your player")]
    public Animator playerAnimator;

    void Start()
    {
        for (int i = 0; i < weaponSlots.Length; i++)
            if (weaponSlots[i] != null)
                AssignWeapon(i, weaponSlots[i]);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchSlot(1);
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (controllers[currentSlot] is WeaponSO so)
                playerAnimator.SetTrigger(so.animationTrigger);
            controllers[currentSlot]?.Trigger();
        }

        float dt = Time.deltaTime;
        foreach (var ctrl in controllers)
            ctrl?.Tick(dt);
    }

    public void AssignWeapon(int slotIdx, WeaponSO data)
    {
        var inst = Instantiate(data);
        inst.Initialize(weaponMounts[slotIdx]);
        controllers[slotIdx] = inst;
    }

    public void SwitchSlot(int idx)
    {
        if (idx >= 0 && idx < weaponMounts.Length)
            currentSlot = idx;
    }
}
