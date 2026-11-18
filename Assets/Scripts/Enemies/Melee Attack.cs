using UnityEngine;
using UnityEngine.AI;

public class MeleeAttack : IEnemyAttackStrategy
{
    private readonly Transform enemyTransform;
    private readonly NavMeshAgent agent;
    private readonly GameObject player;
    private readonly IDamageable target;
    private readonly float detectionRadius;
    private readonly float attackRange;
    private readonly int damage;
    private readonly float attackCooldown;

    private float nextAttackTime;

    public MeleeAttack(
        Transform enemyTransform,
        NavMeshAgent agent,
        GameObject player,
        float detectionRadius = 10000f,
        float attackRange = 2.5f,
        int damage = 25,
        float attackCooldown = 1f)
    {
        this.enemyTransform = enemyTransform;
        this.agent = agent;
        this.player = player;
        this.detectionRadius = detectionRadius;
        this.attackRange = attackRange;
        this.damage = damage;
        this.attackCooldown = attackCooldown;

        if (player != null)
        {
            target = player.GetComponent<IDamageable>();
        }
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

        if (distance > attackRange || Time.time < nextAttackTime)
        {
            return;
        }

        nextAttackTime = Time.time + attackCooldown;

        if (target != null)
        {
            target.TakeDamage(damage);
        }
    }
}
