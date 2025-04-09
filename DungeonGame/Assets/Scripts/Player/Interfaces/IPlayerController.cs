using UnityEngine;
public interface IPlayerController
{
    void Initialize( Transform playerTransform ); 
    void HandleInput( Vector2 movementInput, bool dashInput );
    void  Tick( float deltaTime );
}
