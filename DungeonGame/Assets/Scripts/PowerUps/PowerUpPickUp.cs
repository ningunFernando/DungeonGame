using System;
using UnityEngine;

public class PowerUpPickUp : MonoBehaviour
{ 
    [SerializeField] private PlayerControllerSO PowerUpToAssign;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        
        PlayerController player = other.GetComponent<PlayerController>();
        if(!player) return;
        
        player.AssingController(PowerUpToAssign);
        Destroy(gameObject);
        
    }
}
