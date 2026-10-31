using UnityEngine;

using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int initialSize = 5;

    private Queue<GameObject> availableEnemies = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            CreateEnemy();
        }
    }

    private void CreateEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform);
        enemy.SetActive(false);

        availableEnemies.Enqueue(enemy);
    }

    public GameObject GetEnemy()
    {
        if (availableEnemies.Count == 0)
        {
            CreateEnemy();
        }

        GameObject enemy = availableEnemies.Dequeue();
        enemy.SetActive(true);

        return enemy;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        availableEnemies.Enqueue(enemy);
    }
}
