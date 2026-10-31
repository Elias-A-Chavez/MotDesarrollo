using UnityEngine;

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private Transform[] spawnPoints;

    public GameObject SpawnEnemy(int startingHealth, WaveManager manager)
    {
        if (enemyPool == null || spawnPoints.Length == 0)
        {
            Debug.LogError("EnemyPool or SpawnPoints are not configured.");
            return null;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);

        Transform spawnPoint = spawnPoints[randomIndex];

        GameObject enemy = enemyPool.GetEnemy();

        enemy.transform.SetPositionAndRotation(
            spawnPoint.position,
            spawnPoint.rotation
        );

        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

        if (enemyHealth == null)
        {
            Debug.LogError("The enemy prefab does not contain EnemyHealth.");
            enemyPool.ReturnEnemy(enemy);
            return null;
        }

        enemyHealth.Initialize(startingHealth, manager);

        return enemy;
    }
}
