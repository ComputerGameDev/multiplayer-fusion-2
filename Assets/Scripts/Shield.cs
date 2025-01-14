using Fusion;
using UnityEngine;

public class Shield : NetworkBehaviour
{
    [SerializeField] private float shieldDuration = 10f; // Duration of the shield
    [SerializeField] private GameObject pickupEffect; // Optional: visual effect when picked up

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a player with a Health component
        if (other.TryGetComponent<Health>(out var health))
        {
            // Activate shield functionality for the player
            health.ActivateShieldRpc(shieldDuration);

            // Optional: Spawn a pickup effect
            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            // Remove the shield power-up from the scene
            if (Object.HasStateAuthority)
            {
                Runner.Despawn(Object);
            }
        }
    }
}
