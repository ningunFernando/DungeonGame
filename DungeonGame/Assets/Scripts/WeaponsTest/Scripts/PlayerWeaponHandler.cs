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
    [SerializeField] Transform[] weaponImageTransform;
    private Color[] imageColor = new Color[2];
    [SerializeField] PlayerInput playerInput; 


    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }
    
    private void Start()

    {
        imageColor[0] = Color.white;
        imageColor[1] = Color.white;    
        imageColor[0].a = .5f;
        imageColor[1].a = 1;

        // Instantiate & initialize both weapons:
        for (int i = 0; i < weaponPrefabs.Length; i++)
        {
            Transform mount = (i == currentIndex) ? handMount : backMount;
            var go = Instantiate(weaponPrefabs[i], mount);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;

            weapons[i] = go.GetComponent<Weapon>();
            weapons[i]?.Initialize();
            weaponImage[i].texture = weapons[i].image;
            
        }
    }

    private void Update()
    {
        print(weapons[currentIndex].isAttacking);
       
        // Tick each weapon for combo timing, cooldown UI, etc.
        float dt = Time.deltaTime;
        foreach (var w in weapons)

            w?.Tick(dt);
        
        if (playerInput.actions["Change"].WasPressedThisFrame() && !weapons[currentIndex].isAttacking)
        {
            if (currentIndex == 0)
            {
                SwapTo(1);

            }
            else
            {
                SwapTo(0);
            }
        }
    }

    public void AttackCurrent()
    {
        weapons[currentIndex]?.TryTrigger();
    }

    private void SwapTo(int idx)
    {
        if (idx == currentIndex || idx < 0 || idx >= weapons.Length)
            return;
        int nidx = currentIndex;

        // Stow current weapon
        var prev = weapons[currentIndex].transform;
        prev.SetParent(backMount, worldPositionStays: false);
        prev.localPosition = Vector3.zero;
        prev.localRotation = Quaternion.identity;

        weaponImage[nidx].transform.SetAsFirstSibling();
       
        weaponImage[nidx].color = imageColor[0];
       
        weaponImage[idx].color = imageColor[1];


        weaponImage[idx].transform.position = weaponImageTransform[0].position; 
        weaponImage[nidx].transform.position = weaponImageTransform[1].position; 

        // Draw new weapon
        currentIndex = idx;
        var now = weapons[currentIndex].transform;
        now.SetParent(handMount, worldPositionStays: false);
        now.localPosition = Vector3.zero;
        now.localRotation = Quaternion.identity;
    }
}
