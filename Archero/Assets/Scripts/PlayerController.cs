using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed;
    [HideInInspector]
    public HealthController _healthController;

    private Vector3 change;
    private Rigidbody2D _rigidbody;
    private DynamicJoystick _joystick;
    private TargetDetector _targetDetector;
    private GameObject _deathScreenController;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _joystick = FindObjectOfType<DynamicJoystick>();
        _targetDetector = GetComponent<TargetDetector>();
        _healthController = GetComponent<HealthController>();
        _deathScreenController = FindObjectOfType<DeathScreenController>().gameObject;
    }

    private void OnEnable()
    {
        _deathScreenController.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FireProjectiles", 0.0f, 1.0f / _targetDetector.fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;

#if UNITY_EDITOR
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
#elif UNITY_ANDROID
        change.x = _joystick.Horizontal;
        change.y = _joystick.Vertical;
#endif
        CheckPlayerDeath();
    }

    private void FixedUpdate()
    {
        Move();
        _targetDetector.DetectEnemy();
    }

    void Move()
    {
        if (change != Vector3.zero)
        {
            change.Normalize();
            _rigidbody.MovePosition(this.transform.position + change * speed * Time.deltaTime);
        }
    }


    void FireProjectiles()
    {
        if (_targetDetector.targetToAttack == null)
        {
            return;
        }

        if (_healthController.isDead)
            return;

        if (change == Vector3.zero)
        {
            _targetDetector.CreateProjectiles();
        }

    }

    void CheckPlayerDeath()
    {
        if (_healthController.isDead)
        {
            _deathScreenController.SetActive(true);
        }
    }
}
