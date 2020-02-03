using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnDelay;
    public int enemiesToSpawn;
    public int totalWaves;
    public GameObject enemyPrefab;
    public List<int> spawnPointIndices = new List<int>();
    public List<int> readyToSpawnPoints = new List<int>();

    public Transform spawnPosContainer;

    private int totalLiveEnemies;
    private int currentWave = 0;
    private DoorController doorController;
    private float t = 0;

    private void Awake()
    {
        doorController = FindObjectOfType<DoorController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWave = 0;
        t = spawnDelay;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (totalLiveEnemies <= 0 && currentWave < totalWaves)
        {
            t += Time.deltaTime;
            if (t > spawnDelay)
            {
                SpawnEnemies(enemiesToSpawn);
                t = 0f;
            }
        }

        CheckForWaveCompletion();
    }

    void SpawnEnemies(int totalEnemies)
    {
        totalLiveEnemies = 0;

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
            Vector3 pos = new Vector3(Random.Range(spawnPosContainer.GetChild(readyToSpawnPoints[j]).transform.position.x - 0.05f, spawnPosContainer.GetChild(readyToSpawnPoints[j]).transform.position.x + 0.05f),
                    Random.Range(spawnPosContainer.GetChild(readyToSpawnPoints[j]).transform.position.y - 0.05f, spawnPosContainer.GetChild(readyToSpawnPoints[j]).transform.position.y + 0.05f), 0);
            Instantiate(enemyPrefab, pos, Quaternion.identity);
            totalLiveEnemies++;
        }

        currentWave++;
        enemiesToSpawn++;
    }

    public void ReduceLiveEnemies()
    {
        totalLiveEnemies--;
    }

    void CheckForWaveCompletion()
    {
        if (totalLiveEnemies <= 0 && currentWave >= totalWaves)
        {
            doorController.OpenDoor();
        }
    }
}
