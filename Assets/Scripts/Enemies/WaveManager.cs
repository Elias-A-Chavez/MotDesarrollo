using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private DoorController doorController;

    [SerializeField] private int[] enemiesPerWave;

    private readonly List<EnemyDefinition> enemyTypes = new List<EnemyDefinition>
    {
        new BasicEnemyDefinition(),
        new HeavyEnemyDefinition()
    };

    private int currentWave;
    private int remainingEnemies;
    private bool encounterStarted;
    private bool encounterCompleted;

    public void StartEncounter()
    {
        if (encounterStarted || encounterCompleted)
        {
            return;
        }

        if (enemySpawner == null || enemiesPerWave == null || enemiesPerWave.Length == 0)
        {
            Debug.LogError("WaveManager is not configured correctly.");
            return;
        }

        encounterStarted = true;
        currentWave = 0;

        StartNextWave();
    }

    private void StartNextWave()
    {
        if (currentWave >= enemiesPerWave.Length)
        {
            CompleteEncounter();
            return;
        }

        remainingEnemies = enemiesPerWave[currentWave];

        int enemiesToSpawn = remainingEnemies;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            EnemyDefinition definition = enemyTypes[(currentWave + i) % enemyTypes.Count];

            GameObject enemy = enemySpawner.SpawnEnemy(definition, this);

            if (enemy == null)
            {
                remainingEnemies--;
            }
        }

        if (remainingEnemies <= 0)
        {
            currentWave++;
            StartNextWave();
        }
    }

    public void NotifyEnemyDefeated()
    {
        if (!encounterStarted || encounterCompleted || remainingEnemies <= 0)
        {
            return;
        }

        remainingEnemies--;

        if (remainingEnemies <= 0)
        {
            currentWave++;
            StartNextWave();
        }
    }

    private void CompleteEncounter()
    {
        encounterCompleted = true;

        if (doorController != null)
        {
            doorController.StartBossFight();
        }
    }
}
