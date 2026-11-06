using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private int currentHealth;
    private bool isDead;
    private bool isBoss;

    private WaveManager waveManager;
    private EnemyPool enemyPool;
    private EscapeButtonController escapeButtonController;

    public void Initialize(
        int startingHealth,
        WaveManager manager,
        EnemyPool pool)
    {
        currentHealth = Mathf.Max(1, startingHealth);

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
        currentHealth = Mathf.Max(1, startingHealth);

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