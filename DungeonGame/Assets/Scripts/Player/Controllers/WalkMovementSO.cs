using UnityEngine;

[CreateAssetMenu(fileName = "WalkMovementSO", menuName = "PlayerControllers/WalkMovementSO")]
public class WalkMovementSO : PlayerControllerSO
{
    public float moveSpeed = 5f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 0.5f;
    
    private Vector3 inputDirection;
    private bool isDashing;
    private float dashTime;
    private float lastDashTime;
    private Vector3 dashDirection;

    public override void HandleInput(Vector2 movementInput, bool dashInput)
    {
        inputDirection = new Vector3(movementInput.x, 0, movementInput.y).normalized;
        
        if (dashInput && !isDashing && Time.time >= lastDashTime + dashCooldown)
        {
            isDashing = true;
            dashTime = Time.time + dashDuration;
            lastDashTime = Time.time;
            dashDirection = inputDirection;
        }
    }

    public override void Tick(float deltaTime)
    {
        if(!playerTransform) return;
        
        float speed = isDashing ? dashSpeed : moveSpeed;
        
        Vector3 move = inputDirection * speed * deltaTime;
        playerTransform.position += move;

        if (isDashing && Time.time >= dashTime)
        {
            isDashing = false;
        }

        if (inputDirection.sqrMagnitude > 0.001f)
        {
            playerTransform.forward = inputDirection.normalized;
        }
    }
}
