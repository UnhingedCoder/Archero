using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int enemiesToSpawn;
    public int totalWaves;
    public GameObject enemyPrefab;
    public List<int> spawnPointIndices = new List<int>();
    public List<int> readyToSpawnPoints = new List<int>();

    public Transform spawnPosContainer;

    private int totalLiveEnemies;
    private int currentWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentWave = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (totalLiveEnemies <= 0 && currentWave < totalWaves)
        {
            SpawnEnemies(enemiesToSpawn);
        }
    }

    void SpawnEnemies(int totalEnemies)
    {
        //Random.InitState(Random.Range(0, 10));
        totalLiveEnemies = 0;
        Debug.LogError("SpawnEnemies");

        int totalSpawnPoints = spawnPosContainer.childCount;
        spawnPointIndices.Clear();
        readyToSpawnPoints.Clear();

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
            totalLiveEnemies++;
        }

        currentWave++;
        enemiesToSpawn++;
    }

    public void ReduceLiveEnemies()
    {
        totalLiveEnemies--;
    }
}
