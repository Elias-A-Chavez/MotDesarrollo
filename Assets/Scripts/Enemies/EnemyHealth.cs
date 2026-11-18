using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public event System.Action Defeated;

    private int currentHealth;
    private int maxHealth;
    private bool isDead;
    private bool isBoss;

    private WaveManager waveManager;
    private EnemyPool enemyPool;
    private EscapeButtonController escapeButtonController;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    public void Initialize(
        EnemyDefinition definition,
        WaveManager manager,
        EnemyPool pool)
    {
        int startingHealth = definition != null ? definition.StartingHealth : 1;

        maxHealth = Mathf.Max(1, startingHealth);
        currentHealth = maxHealth;

        waveManager = manager;
        enemyPool = pool;

        escapeButtonController = null;
        isBoss = false;
        isDead = false;
    }

    public void InitializeBoss(
        int startingHealth,
        EscapeButtonController escapeController)
    {
        maxHealth = Mathf.Max(1, startingHealth);
        currentHealth = maxHealth;

        waveManager = null;
        enemyPool = null;

        escapeButtonController = escapeController;
        isBoss = true;
        isDead = false;
    }

    public void TakeDamage(int damage)
    {
        if (isDead || damage <= 0)
        {
            return;
        }

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;

        if (isBoss)
        {
            if (escapeButtonController != null)
            {
                escapeButtonController.NotifyBossDefeated();
            }

            Defeated?.Invoke();

            gameObject.SetActive(false);
            return;
        }

        if (waveManager != null)
        {
            waveManager.NotifyEnemyDefeated();
        }

        if (enemyPool != null)
        {
            enemyPool.ReturnEnemy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
