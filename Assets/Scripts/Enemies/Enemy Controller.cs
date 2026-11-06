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

    void Update()
    {
        Attack?.Attack();
        NavMeshAgent.destination = Player.transform.position; 
    }
}
