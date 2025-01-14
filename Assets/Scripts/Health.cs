using Fusion;
using UnityEngine;
using System.Collections;

public class Health: NetworkBehaviour
{
    [SerializeField] NumberField HealthDisplay;

    [Networked]
    public int NetworkedHealth { get; set; } = 100;

    // Migration from Fusion 1:  https://doc.photonengine.com/fusion/current/getting-started/migration/coming-from-fusion-v1
    private ChangeDetector _changes;

    [Networked] private bool isShieldActive { get; set; } = false; // Track shield state

    public bool IsShieldActive()
    {
        return isShieldActive;
    }

    public override void Spawned() {
        _changes = GetChangeDetector(ChangeDetector.Source.SimulationState);
        HealthDisplay.SetNumber(NetworkedHealth);
    }

    public override void Render() {
        foreach (var change in _changes.DetectChanges(this, out var previousBuffer, out var currentBuffer)) {
            switch (change) {
                case nameof(NetworkedHealth):
                    //var reader = GetPropertyReader<int>(nameof(NetworkedHealth));
                    //var (previous, current) = reader.Read(previousBuffer, currentBuffer);
                    HealthDisplay.SetNumber(NetworkedHealth);
                    break;
            }
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    // All players can call this function; only the StateAuthority receives the call.
    public void DealDamageRpc(int damage) {
        // The code inside here will run on the client which owns this object (has state and input authority).
        Debug.Log("Received DealDamageRpc on StateAuthority, modifying Networked variable");
        NetworkedHealth -= damage;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void ActivateShieldRpc(float duration)
    {
        StartCoroutine(ShieldDuration(duration));
    }

    private IEnumerator ShieldDuration(float duration)
    {
        isShieldActive = true;
        Debug.Log("Shield activated!");
        yield return new WaitForSeconds(duration);
        isShieldActive = false;
        Debug.Log("Shield expired!");
    }


}
