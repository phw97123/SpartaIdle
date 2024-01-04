using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPostion; 
    public EnemyPool enemyPool;
    private WaitForSeconds spawnInterval = new WaitForSeconds(1f);
    private int maxEnemies = 30;

    private void Start()
    {
        StartCoroutine(SpawnEnemy()); 
    }

    private IEnumerator SpawnEnemy()
    {
        while(true)
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
            {
                GameObject enemy = enemyPool.Get();
                Transform spawnPosition = spawnPostion; 
                enemy.transform.position = spawnPosition.position;
                enemy.SetActive(true); 
            }
            yield return spawnInterval; 
        }
    }
}
