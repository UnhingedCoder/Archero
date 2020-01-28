using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int enemiesToSpawn;
    public GameObject enemyPrefab;
    public List<int> spawnPointIndices = new List<int>();
    public List<int> readyToSpawnPoints = new List<int>();

    public Transform spawnPosContainer;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies(enemiesToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemies(int totalEnemies)
    {
        //Random.InitState(Random.Range(0, 10));

        Debug.LogError("SpawnEnemies");

        int totalSpawnPoints = spawnPosContainer.childCount;
        spawnPointIndices.Clear();
        for (int i = 0; i < totalSpawnPoints; i++)
        {
            spawnPointIndices.Add(i);
        }

        int spawnPointIndex = 0;
        for (int i = 0; i < totalEnemies; i++)
        {
            spawnPointIndex = Random.Range(0, spawnPointIndices.Count);
            readyToSpawnPoints.Add(spawnPointIndices[spawnPointIndex]);
            spawnPointIndices.RemoveAt(spawnPointIndex);
        }

        for (int j = 0; j < readyToSpawnPoints.Count; j++)
        {
            Instantiate(enemyPrefab, spawnPosContainer.GetChild(readyToSpawnPoints[j]).transform.position, Quaternion.identity);
        }
    }
}
