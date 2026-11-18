using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int initialSize = 5;

    private readonly Queue<GameObject> availableEnemies = new Queue<GameObject>();
    private readonly HashSet<GameObject> availableSet = new HashSet<GameObject>();

    private void Awake()
    {
        if (enemyPrefab != null && enemyPrefab.scene.IsValid())
        {
            enemyPrefab.SetActive(false);
        }

        for (int i = 0; i < initialSize; i++)
        {
            CreateEnemy();
        }
    }

    private void CreateEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy Prefab is not assigned.");
            return;
        }

        GameObject enemy = Instantiate(enemyPrefab, transform);
        enemy.SetActive(false);

        availableEnemies.Enqueue(enemy);
        availableSet.Add(enemy);
    }

    public GameObject GetEnemy()
    {
        if (availableEnemies.Count == 0)
        {
            CreateEnemy();
        }

        if (availableEnemies.Count == 0)
        {
            return null;
        }

        GameObject enemy = availableEnemies.Dequeue();
        availableSet.Remove(enemy);

        return enemy;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        if (enemy == null || availableSet.Contains(enemy))
        {
            return;
        }

        enemy.SetActive(false);

        availableEnemies.Enqueue(enemy);
        availableSet.Add(enemy);
    }
}