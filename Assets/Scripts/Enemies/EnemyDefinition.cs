using UnityEngine;

public abstract class EnemyDefinition
{
    private string enemyName;
    private int startingHealth;


    protected EnemyDefinition(string name, int startingHealth)
    {
        this.enemyName = name;
        this.startingHealth = startingHealth;
    }

    public string GetName()
    {
        return enemyName;
    }

    public int GetStartingHealth()
    {
        return startingHealth;
    }
}