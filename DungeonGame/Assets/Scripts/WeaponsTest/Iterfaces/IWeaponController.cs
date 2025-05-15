using UnityEngine;

public interface IWeaponController
{
    /// Se llama una vez el arma esta equipada
    void Initialize(Transform weaponMount);
    
    /// Se llama cada vez que se presiona una accion en el controlador
    void Trigger();
    
    /// Se llama cada frame el el Susutituto de el update
    void Tick(float deltaTime);
}
