using UnityEngine;
using UnityEngine.AI;

public class MeleeAttack : IEnemyAttackStrategy
{
    private readonly Transform enemyTransform;
    private readonly NavMeshAgent agent;
    private readonly GameObject player;
    private readonly float detectionRadius;

    public MeleeAttack(
        Transform enemyTransform,
        NavMeshAgent agent,
        GameObject player,
        float detectionRadius = 10000f)
    {
        this.enemyTransform = enemyTransform;
        this.agent = agent;
        this.player = player;
        this.detectionRadius = detectionRadius;
    }

    public void Attack()
    {
        if (enemyTransform == null) return;
        if (player == null) return;
        if (agent == null || !agent.enabled || !agent.isOnNavMesh) return;

        Vector3 playerPosition = player.transform.position;
        float distance = Vector3.Distance(enemyTransform.position, playerPosition);

        if (distance > detectionRadius)
        {
            if (agent.hasPath)
            {
                agent.ResetPath();
            }

            return;
        }

        agent.SetDestination(playerPosition);
    }
}
