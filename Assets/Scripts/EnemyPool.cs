using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> enemyPool = new List<GameObject>();

    private int poolSize = 5;

    private void Start()
    {
        InitializePool(); 
    }

    private void InitializePool()
    {
        GameObject[] enemyPrefabs = Resources.LoadAll<GameObject>("Enemies");

        if (enemyPrefabs != null && enemyPrefabs.Length > 0)
        {
            enemies.AddRange(enemyPrefabs);
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = GetRandomEnemyPrefab();
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
    }

    private GameObject GetRandomEnemyPrefab()
    {
        int randomIndex = UnityEngine.Random.Range(0, enemies.Count);
        return enemies[randomIndex];
    }

    public GameObject GetEnemyFromPool()
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }

        GameObject newEnemy = GetRandomEnemyPrefab();
        enemyPool.Add(newEnemy);
        return newEnemy;
    }

    public void ReturnToPool(Enemy enemy)
    {
        enemy.Health.OnDie -= () =>ReturnToPool(enemy);
        enemy.gameObject.SetActive(false);
    }
}
