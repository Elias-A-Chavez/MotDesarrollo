using UnityEngine;


public class EnemyHealth : MonoBehaviour, IDamageable
{
    private int currentHealth;
    private bool isDead;
    private WaveManager waveManager;

    public void Initialize(int startingHealth, WaveManager manager)
    {
        currentHealth = Mathf.Max(1, startingHealth);
        waveManager = manager;
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
        isDead = true;

        if (waveManager != null)
        {
            waveManager.NotifyEnemyDefeated();
        }

        gameObject.SetActive(false);
    }
}
