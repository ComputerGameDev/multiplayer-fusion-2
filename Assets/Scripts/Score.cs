using Fusion;
using UnityEngine;

public class Score : NetworkBehaviour
{
    [SerializeField] NumberField ScoreDisplay;

    [Networked]
    public int NetworkedScore { get; set; } = 0;

    // Migration from Fusion 1:  https://doc.photonengine.com/fusion/current/getting-started/migration/coming-from-fusion-v1
    private ChangeDetector _changes;

    public override void Spawned()
    {
        _changes = GetChangeDetector(ChangeDetector.Source.SimulationState);
        ScoreDisplay.SetNumber(NetworkedScore);
    }

    public override void Render()
    {
        foreach (var change in _changes.DetectChanges(this, out var previousBuffer, out var currentBuffer))
        {
            switch (change)
            {
                case nameof(NetworkedScore):
                    //var reader = GetPropertyReader<int>(nameof(NetworkedScore));
                    //var (previous, current) = reader.Read(previousBuffer, currentBuffer);
                    ScoreDisplay.SetNumber(NetworkedScore);
                    break;
            }
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    // All players can call this function; only the StateAuthority receives the call.
    public void DealScoreRpc(int score)
    {
        // The code inside here will run on the client which owns this object (has state and input authority).
        Debug.Log("Received DealScoreRpc on StateAuthority, modifying Networked variable");
        NetworkedScore += score;
    }
}