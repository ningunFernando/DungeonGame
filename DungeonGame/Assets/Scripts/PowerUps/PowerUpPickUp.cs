using UnityEngine;

public class PowerUpPickUp : MonoBehaviour
{
    [SerializeField] private PlayerControllerSO PowerUpToAssign;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (!player) return;

        for (int i = 0; i < player.itemSlot.Length && i <= 2; i++)
        {
            if (player.itemSlot[i] == null)
            {
                player.AssingController(PowerUpToAssign);
                Destroy(gameObject);
                return; 
            }
        }

        Debug.Log("Todos los slots de power-up están ocupados");
    }
}