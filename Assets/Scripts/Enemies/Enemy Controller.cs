using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public IEnemyAttackStrategy Attack { get; private set; }

    public void SetAttackStrategy(IEnemyAttackStrategy strategy)
    {
        Attack = strategy;
    }

    void Update()
    {
        Attack?.Attack();
    }
}
