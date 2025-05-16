using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "CombatControllerSO", menuName = "PlayerControllers/CombatControllerSO")]
public class CombatControllerSO : PlayerControllerSO, ICombatController
{
    [Tooltip("PicK your base movement controller (e.g. WalkMovementSO)")]
    public PlayerControllerSO movementController;
    
    private PlayerControllerSO movementInstance;
    private PlayerWeaponHandler weaponHandler;

    public override void Initialize(Transform playerTransform)
    {
        base.Initialize(playerTransform);
        
        movementInstance = Instantiate(movementController);
        movementInstance.Initialize(playerTransform);
        
        weaponHandler = playerTransform.GetComponent<PlayerWeaponHandler>();
        if (!weaponHandler)
            Debug.LogWarning("CombatControllerSO: no PlayerWeaponHandler found!");
    }

    public override void HandleInput(Vector2 movementInput, bool dashInput)
    {
        movementInstance.HandleInput(movementInput, dashInput);
    }

    public override void Tick(float deltaTime)
    {
        movementInstance.Tick(deltaTime);
    }

    public void HandleAttack()
    {
        weaponHandler?.AttackCurrent();
    }

}
