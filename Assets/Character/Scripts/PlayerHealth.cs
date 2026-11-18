using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 1000f;
    [SerializeField] private float currentHealth;

    [SerializeField] private float regenRate = 1f;
    [SerializeField] private float regenDelay = 5f;

    [SerializeField] private string defeatSceneName = "Defeat";

    private bool isDead;
    private float nextRegenTime;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        if (Time.time >= nextRegenTime && currentHealth < maxHealth)
        {
            currentHealth += regenRate * Time.deltaTime;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead || damageAmount <= 0)
        {
            return;
        }

        currentHealth -= damageAmount;
        nextRegenTime = Time.time + regenDelay;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        SceneManager.LoadScene(defeatSceneName);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
