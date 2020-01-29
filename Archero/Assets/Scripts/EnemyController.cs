using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject healthPrefab;

    [SerializeField]
    private float healthDropChance;
    [SerializeField]
    private int coinsToDrop;

    [HideInInspector]
    public HealthController _healthController;
    private TargetDetector _targetDetector;
    private EnemySpawner _enemySpawner;

    void Awake()
    {
        _targetDetector = GetComponent<TargetDetector>();
        _healthController = GetComponent<HealthController>();
        _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FireProjectiles", 0.0f, 1.0f / _targetDetector.fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        CheckNPCDeath();
    }

    private void FixedUpdate()
    {
        _targetDetector.DetectEnemy();
    }

    void FireProjectiles()
    {
        if (_targetDetector.targetToAttack == null)
        {
            return;
        }

        if (_healthController.isDead)
            return;

        _targetDetector.CreateProjectiles();

    }

    void CheckNPCDeath()
    {
        if (_healthController.isDead)
        {
            _enemySpawner.ReduceLiveEnemies();
            SpawnLootDrop();
            Destroy(this.gameObject);
        }
    }

    void SpawnLootDrop()
    {
        Debug.LogError("Dropping loot");
        float lootDropChance = Random.Range(0, 100);

        if (lootDropChance <= healthDropChance)
        {
            Instantiate(healthPrefab, this.transform.position, Quaternion.identity);
        }
        else
        {
            for (int i = 0; i < coinsToDrop; i++)
            {
                Vector3 pos = new Vector3(Random.Range(this.transform.position.x - 0.25f, this.transform.position.x + 0.25f),
                    Random.Range(this.transform.position.y - 0.25f, this.transform.position.y + 0.25f), 0);
                Instantiate(coinPrefab, pos, Quaternion.identity);
            }
        }
    }
}
