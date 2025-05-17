// PlayerWeaponHandler.cs
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    [SerializeField] RawImage[] weaponImage;


    private void Awake()
    {
        inputActions = new InputSystem_Actions();
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

    public void AttackCurrent()
    {
        weapons[currentIndex]?.TryTrigger();
    }
    
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
