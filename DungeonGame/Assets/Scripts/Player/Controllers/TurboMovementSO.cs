using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TurboMovementSO", menuName = "PlayerControllers/TurboMovementSO")]
public class TurboMovementSO : PlayerControllerSO
{
    public float speed = 15f;

    private float endTime;
    protected PlayerController playerReference;
    private Vector3 inputDirection;


    public override void Initialize(Transform playerTransform)
    {
        base.Initialize(playerTransform);
        playerReference = playerTransform.GetComponent<PlayerController>();
        endTime = Time.time + duration;

    }

    public override void HandleInput(Vector2 movementInput, bool dashInput)
    {
        inputDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
    }

    public override void Tick(float deltaTime)
    {
        if(!playerTransform) return;
        
        Vector3 move = inputDirection * speed * deltaTime;
        playerTransform.position += move;
        
        
        if (inputDirection.sqrMagnitude > 0.001f)
        {
            playerTransform.forward = inputDirection.normalized;
        }

        if (Time.time >= endTime)
        {
            playerReference.RevertToPreviousController();
        }

    }
    
}
