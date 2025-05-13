using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerControllerSO startingController;
    
    public PlayerControllerSO currentController;
    private PlayerControllerSO lastController;
    public PlayerControllerSO itemSlot1;
    public PlayerControllerSO itemSlot2;
    
    private bool slot1Ready;
    private bool slot2Ready;
    
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
        inputActions.Player.Attack.performed += ctx => UsePower(2);
    }

    private void OnDisable()
    {
        inputActions.Disable();
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
        
        if (slot == 1 && itemSlot1 != null && slot1Ready)
        {
            Debug.Log($"Using Slot 1 Triggered Power: {itemSlot1.name}");
            SetController(itemSlot1, rememberPrevious: true);
            slot1Ready = false;
            powerUpInUse = true;
            itemSlot1 = null;
        }
        
        if (slot == 2 && itemSlot2 != null && slot2Ready)
        {
            Debug.Log($"Using Slot 2 Triggered Power: {itemSlot2.name}");
            SetController(itemSlot2, rememberPrevious: true);
            slot2Ready = false;
            powerUpInUse = true;
            itemSlot2 = null;
        }
    }

    public void AssingController(PlayerControllerSO powerUpController)
    {
        if (!itemSlot1)
        {
            itemSlot1 = Instantiate(powerUpController);
            slot1Ready = true;
            Debug.Log($"{powerUpController.name} has been instantiated.");
        }
        else if (!itemSlot2)
        {
            itemSlot2 = Instantiate(powerUpController);
            slot2Ready = true;
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
}
