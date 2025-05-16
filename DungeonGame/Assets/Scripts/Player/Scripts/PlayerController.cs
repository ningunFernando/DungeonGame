using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using System.Linq.Expressions;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerControllerSO startingController;

    public PlayerControllerSO currentController;
    private PlayerControllerSO lastController;
    public PlayerControllerSO[] itemSlot = new PlayerControllerSO[2];
    
    public RawImage[] itemSlotImage;
    public Image[] circle;

    
    private bool[] slotReady = new bool[2];
    
    private InputSystem_Actions inputActions;

    private Vector2 moveInput;
    private bool dashInput;

    private bool powerUpInUse = false;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        SetController(startingController, false);
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        
        inputActions.Player.Dash.performed += ctx => dashInput = true;
        inputActions.Player.Dash.canceled += ctx => dashInput = false;

        inputActions.Player.Jump.performed += ctx => UsePower(1);
        inputActions.Player.Crouch.canceled += ctx => UsePower(2);
        
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        if(!currentController) return;

        if (currentController is ICombatController combat)
        {
            combat.HandleAttack();
        }
        
    }
    
    private void Update()
    {
        if (!currentController) return;
        
        currentController.HandleInput(moveInput, dashInput);
        currentController.Tick(Time.deltaTime);
        
    }

    private void UsePower(int slot)
    {
        if(powerUpInUse) return;
        
        if (slot == 1 && itemSlot[0] != null && slotReady[0])
        {
            Debug.Log($"Using Slot 1 Triggered Power: {itemSlot[0].name}");

            SetController(itemSlot[0], rememberPrevious: true);
            StartCoroutine(RunCircle(0, itemSlot[0].duration, itemSlot[0].chargeDuration));
            powerUpInUse = true; 
        }
        
        if (slot == 2 && itemSlot[1] != null && slotReady[1])
        {
            Debug.Log($"Using Slot 2 Triggered Power: {itemSlot[1].name}");
            SetController(itemSlot[1], rememberPrevious: true);
            StartCoroutine(RunCircle(1, itemSlot[1].duration, itemSlot[1].chargeDuration));

            powerUpInUse = true;
        }
    }

    public void AssingController(PlayerControllerSO powerUpController)
    {
        if (!itemSlot[0])
        {
            itemSlot[0] = Instantiate(powerUpController);
            itemSlotImage[0].texture = Instantiate(powerUpController.image);
            slotReady[0] = true;

            Debug.Log($"{powerUpController.name} has been instantiated.");
        }
        else if (!itemSlot[1])
        {
            itemSlot[1] = Instantiate(powerUpController);
            itemSlotImage[1].texture = Instantiate(powerUpController.image);

            slotReady[1] = true;
            Debug.Log($"{powerUpController.name} has been instantiated.");
        }
        else
        {
            Debug.LogWarning($"No more items can be instantiated.");
        }
    }

    public void SetController(PlayerControllerSO newController, bool rememberPrevious = true)
    {
        if(rememberPrevious) lastController = currentController;
        
        currentController = Instantiate(newController);
        currentController.Initialize(transform);
    }
    
    public void RevertToPreviousController()
    {
        if (lastController)
            SetController(lastController, false);
            powerUpInUse = false;
    }
    public IEnumerator RunCircle(int slot, float duration, float chargeDuration)
    {
        float time = 0f;
        circle[slot].fillAmount = 0f;

        while (time < duration)
        {
            circle[slot].fillAmount = time / duration;
            time += Time.deltaTime;
            yield return null;
        }
        slotReady[slot] = false;
        itemSlot[slot] = null;

        circle[slot].fillAmount = 1f;

        StartCoroutine(ChargeCircle(slot, chargeDuration));


    }
    private IEnumerator ChargeCircle(int slot, float duration)
    {
        float time = 0f;
        circle[slot].fillAmount = 0f;

        while (time < duration)
        {
            circle[slot].fillAmount = 1 - (time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        circle[slot].fillAmount = 0f;


    }
}
