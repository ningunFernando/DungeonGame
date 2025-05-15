// PlayerWeaponHandler.cs
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Header("Mounts")]
    [Tooltip("Equip mount (e.g. hand)")]
    public Transform handMount;
    [Tooltip("Stow mount (e.g. back)")]
    public Transform backMount;

    [Header("Weapon Prefabs")]
    [Tooltip("Two weapon prefabs (must have Weapon-derived component)")]
    public GameObject[] weaponPrefabs = new GameObject[2];

    private Weapon[] weapons = new Weapon[2];
    private int currentIndex = 0;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Attack.performed += OnAttack;
        inputActions.Player.Jump.performed   += OnSwitchToSlot0; // map Jump → slot 0
        inputActions.Player.Crouch.performed += OnSwitchToSlot1; // map Crouch → slot 1
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Jump.performed   -= OnSwitchToSlot0;
        inputActions.Player.Crouch.performed -= OnSwitchToSlot1;
        inputActions.Disable();
    }

    private void Start()
    {
        // Instantiate & initialize both weapons:
        for (int i = 0; i < weaponPrefabs.Length; i++)
        {
            Transform mount = (i == currentIndex) ? handMount : backMount;
            var go = Instantiate(weaponPrefabs[i], mount);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;

            weapons[i] = go.GetComponent<Weapon>();
            weapons[i]?.Initialize();
        }
    }

    private void Update()
    {
        // Tick each weapon for combo timing, cooldown UI, etc.
        float dt = Time.deltaTime;
        foreach (var w in weapons)
            w?.Tick(dt);
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        weapons[currentIndex]?.TryTrigger();
    }

    private void OnSwitchToSlot0(InputAction.CallbackContext ctx) => SwapTo(0);
    private void OnSwitchToSlot1(InputAction.CallbackContext ctx) => SwapTo(1);

    private void SwapTo(int idx)
    {
        if (idx == currentIndex || idx < 0 || idx >= weapons.Length) 
            return;

        // Stow current
        var prev = weapons[currentIndex].transform;
        prev.SetParent(backMount, worldPositionStays: false);
        prev.localPosition  = Vector3.zero;
        prev.localRotation  = Quaternion.identity;

        // Draw new
        currentIndex = idx;
        var now = weapons[currentIndex].transform;
        now.SetParent(handMount, worldPositionStays: false);
        now.localPosition = Vector3.zero;
        now.localRotation = Quaternion.identity;
    }
}
