using Fusion;
using UnityEngine;

public class ShieldSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject shieldPrefab; // Reference to the shield prefab
    [SerializeField] private Vector3 spawnPosition; // Position to spawn the shield

    public void SpawnShield()
    {
        if (Object.HasStateAuthority)
        {
            Runner.Spawn(shieldPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
