using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    private EnemyPool enemyPool;
    private WaitForSeconds spawnInterval = new WaitForSeconds(3f);
    private int maxEnemies = 30;

    public Tilemap tilemap; 
    private TilemapRenderer tilemapRenderer;

    private List<Enemy> activeEnemy = new List<Enemy>(); 

    private void Start()
    {
        enemyPool = GetComponent<EnemyPool>();
        tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();

        StartCoroutine(SpawnEnemy()); 
    }

    private IEnumerator SpawnEnemy()
    {
        while(true)
        {
            yield return spawnInterval; 
            if(GameObject.FindGameObjectsWithTag("Enemy").Length<maxEnemies)
            {
                Enemy enemy = enemyPool.GetEnemyFromPool().GetComponent<Enemy>();
                enemy.Health.OnDie += ()=>  enemyPool.ReturnToPool(enemy);
                enemy.Init(); 
                Vector3Int spawnPosition = GetRandomTilePosition();
                enemy.transform.position = spawnPosition;
                enemy.gameObject.SetActive(true); 
                activeEnemy.Add(enemy);
            }
        }
    }

    private Vector3Int GetRandomTilePosition()
    {
        BoundsInt bounds = tilemap.cellBounds;
        Vector3Int randomPosition = new Vector3Int(
            Random.Range(bounds.x, bounds.x + bounds.size.x),
            Random.Range(bounds.y, bounds.y + bounds.size.y),
            0
        );
        return randomPosition;
    }

    public List<Enemy> GetEnemies()
    {
        return activeEnemy;
    }
}
