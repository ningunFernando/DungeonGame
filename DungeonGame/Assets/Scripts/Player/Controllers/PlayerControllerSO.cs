using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PlayerControllerSO", menuName = "PlayerControllers/PlayerControllerSO")]
public abstract class PlayerControllerSO : ScriptableObject, IPlayerController
{
    protected Transform playerTransform;
    public Texture image;
    public float duration;
    public float chargeDuration;



    public virtual void Initialize(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }

    public abstract void HandleInput(Vector2 movementInput, bool dashInput);
    public abstract void Tick(float deltaTime);

  
}