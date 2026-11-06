using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private Transform[] spawnPoints;

    public GameObject SpawnEnemy(int startingHealth, WaveManager manager)
    {
        if (enemyPool == null || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("EnemyPool or SpawnPoints are not configured.");
            return null;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        if (spawnPoint == null)
        {
            Debug.LogError("Spawn Point is missing.");
            return null;
        }

        GameObject enemy = enemyPool.GetEnemy();

        if (enemy == null)
        {
            Debug.LogError("EnemyPool returned a null enemy.");
            return null;
        }

        enemy.transform.SetPositionAndRotation(
            spawnPoint.position,
            spawnPoint.rotation
        );

        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

        if (enemyHealth == null)
        {
            Debug.LogError("Enemy prefab does not contain EnemyHealth.");
            enemyPool.ReturnEnemy(enemy);
            return null;
        }

        enemyHealth.Initialize(
            startingHealth,
            manager,
            enemyPool
        );

        enemy.SetActive(true);

        return enemy;
    }
}