using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
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
            Destroy(this.gameObject);
        }
    }
}
