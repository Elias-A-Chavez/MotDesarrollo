using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public IEnemyAttackStrategy Attack { get; private set; }
    [SerializeField] private GameObject Player;
    public NavMeshAgent NavMeshAgent;
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

    void Update()
    {
        Attack?.Attack();
        NavMeshAgent.destination = Player.transform.position; 
    }
}
