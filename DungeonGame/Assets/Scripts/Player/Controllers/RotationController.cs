using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "RotationController", menuName = "PlayerControllers/RotationController")]
public class RotationController : PlayerControllerSO
{
    public float speedRotation = 90f;


    private float endTime;
    protected PlayerController playerReference;
    private float currentRotationInput;



    public override void Initialize(Transform playerTransform)
    {
        base.Initialize(playerTransform);
        playerReference = playerTransform.GetComponent<PlayerController>();
        endTime = Time.time + duration;
    }
     
    public override void HandleInput(Vector2 movementInput, bool dashInput)
    {
        currentRotationInput = movementInput.x;
    }

    public override void Tick(float deltaTime)
    {
        if(!playerTransform) return;
        
        float rotationAmount = currentRotationInput * speedRotation * deltaTime;
        playerTransform.Rotate(0,rotationAmount,0);

        if (Time.time >= endTime)
        {
            playerReference.RevertToPreviousController();
        }
    }
   
}
