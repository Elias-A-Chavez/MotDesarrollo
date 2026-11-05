using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 1000f;
    [SerializeField] private float currentHealth;

    
    [SerializeField] private float regenRate = 1f;
    [SerializeField] private float regenDelay = 5f;

    [SerializeField] private string defeatSceneName = "Defeat";

    private bool isDead = false;
    private float nextRegenTime;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return;

        
        if (Time.time >= nextRegenTime && currentHealth < maxHealth)
        {
            currentHealth += regenRate * Time.deltaTime;

            
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

 
    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;
       
        nextRegenTime = Time.time + regenDelay;

        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        currentHealth = 0;
        SceneManager.LoadScene(defeatSceneName);
    }
}