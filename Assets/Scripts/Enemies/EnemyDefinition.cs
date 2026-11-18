public abstract class EnemyDefinition
{
    public string Name { get; }
    public int StartingHealth { get; }

    protected EnemyDefinition(string name, int startingHealth)
    {
        Name = name;
        StartingHealth = startingHealth;
    }
}
