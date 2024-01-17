using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPostions; 
    public MonsterPool enemyPool;
    private WaitForSeconds spawnInterval = new WaitForSeconds(.5f);
    private int maxMonsters = 30;

    public List<GameObject> monsterList = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnMonster()); 
    }

    private IEnumerator SpawnMonster()
    {
        while(monsterList.Count < maxMonsters)
        {
            GameObject monster = enemyPool.Get();
            int randomPosition = Random.Range(0,spawnPostions.Length );
            Transform spawnPosition = spawnPostions[randomPosition];
            monster.transform.position = spawnPosition.position;
            monster.GetComponent<Monster>().Init();

            monster.SetActive(true);
            monsterList.Add(monster);
            yield return spawnInterval; 
        }
    }
}
