using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPostions; 
    public EnemyPool enemyPool;
    private WaitForSeconds spawnInterval = new WaitForSeconds(2f);
    private int maxEnemies = 30;

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
            enemy.SetActive(true);
            enemies.Add(enemy);
            yield return spawnInterval; 
        }
    }
}
