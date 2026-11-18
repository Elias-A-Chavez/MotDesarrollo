using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private NavMeshAgent NavMeshAgent;

    public IEnemyAttackStrategy Attack { get; private set; }

    public void SetAttackStrategy(IEnemyAttackStrategy strategy)
    {
        Attack = strategy;
    }

    private void Start()
    {
        if (Attack == null && Player != null && NavMeshAgent != null)
        {
            Attack = new MeleeAttack(transform, NavMeshAgent, Player);
        }
    }

    private void Update()
    {
        Attack?.Attack();

        if (NavMeshAgent != null && Player != null)
        {
            NavMeshAgent.destination = Player.transform.position;
        }
    }
}
