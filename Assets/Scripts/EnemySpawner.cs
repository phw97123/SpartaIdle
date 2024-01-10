using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPostions; 
    public EnemyPool enemyPool;
    private WaitForSeconds spawnInterval = new WaitForSeconds(.5f);
    private int maxEnemies = 100;

    public List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnEnemy()); 
    }

    private IEnumerator SpawnEnemy()
    {
        while(enemies.Count < maxEnemies)
        {
            GameObject enemy = enemyPool.Get();
            int randomPosition = Random.Range(0,spawnPostions.Length );
            Transform spawnPosition = spawnPostions[randomPosition];
            enemy.transform.position = spawnPosition.position;
            enemy.GetComponent<Enemy>().Init();

            Health health = enemy.GetComponent<Health>();
            health.OnDie -= OnSpawnEnemyDead; 
            health.OnDie += OnSpawnEnemyDead;

            enemy.SetActive(true);
            enemies.Add(enemy);
            Debug.Log($"{enemies.Count} / {maxEnemies}"); 
            yield return spawnInterval; 
        }
    }

    private void OnSpawnEnemyDead()
    {
        Player.instance.playerData.UpdateExp(20);
    }
}
