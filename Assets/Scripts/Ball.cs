using Fusion;
using UnityEngine;

/**
 * This component represents a ball moving at a constant speed.
 */
public class Ball : NetworkBehaviour
{
    [Networked] private TickTimer lifeTimer { get; set; }

    [SerializeField] float lifeTime = 5.0f;
    [SerializeField] float speed = 5.0f;
    [SerializeField] int damagePerHit = 1;

    public override void Spawned()
    {
        lifeTimer = TickTimer.CreateFromSeconds(Runner, lifeTime);
    }

    public override void FixedUpdateNetwork()
    {
        if (lifeTimer.Expired(Runner))
            Runner.Despawn(Object);
        else
            transform.position += speed * transform.forward * Runner.DeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object hit has a Health component
        Health health = other.GetComponent<Health>();
        Player hitPlayer = other.GetComponent<Player>();

        if (health != null && hitPlayer != null)
        {
            // Reduce the health of the player who was hit
            health.DealDamageRpc(damagePerHit);

            // Find the other player and award them the score
            foreach (var player in FindObjectsOfType<Player>())
            {
                if (player != hitPlayer) // Find the other player
                {
                    Score otherPlayerScore = player.GetComponent<Score>();
                    if (otherPlayerScore != null)
                    {
                        otherPlayerScore.DealScoreRpc(damagePerHit); // Award score to the other player
                        Debug.Log($"Awarding score to {player.name} because {hitPlayer.name} was hit.");
                    }
                }
            }

            // Despawn the ball after a successful hit
            Runner.Despawn(Object);
        }
    }
}
