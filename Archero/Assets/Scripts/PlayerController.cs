﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed;
    public float detectDistance = 10f;
    public LayerMask enemyLayer;
    public GameObject projectilePrefab;

    public GameObject targetToAttack;
    private Vector3 change;
    private Rigidbody2D _rigidbody;
    private DynamicJoystick _joystick;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _joystick = FindObjectOfType<DynamicJoystick>();
    }

    // Start is called before the first frame update
    void Start()
    {
        targetToAttack = null;
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

    }

    private void FixedUpdate()
    {
        DetectEnemy();
        Move();
    }

    void Move()
    {
        if (change != Vector3.zero)
        {
            change.Normalize();
            _rigidbody.MovePosition(this.transform.position + change * speed * Time.deltaTime);
        }
    }

    void DetectEnemy()
    {
        int targetIndex = 99;
        float shortestDist = 999f;

        Vector3 dir = Vector3.zero;

        RaycastHit2D[] hit = Physics2D.CircleCastAll(this.transform.position, detectDistance, Vector2.right, detectDistance * 2f, enemyLayer);
        if (hit.Length > 0)
        {
            if (targetToAttack == null)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].collider != null)
                    {
                        float dist = Vector3.Distance(this.transform.position, hit[i].collider.gameObject.transform.position);
                        Debug.Log("Hit " + hit[i].collider.name + " Dist: " + dist);
                        if (dist < shortestDist)
                        {
                            targetIndex = i;
                            shortestDist = dist;
                        }
                    }
                }

                if (targetIndex < hit.Length)
                {
                    targetToAttack = hit[targetIndex].collider.gameObject;
                    dir = (hit[targetIndex].collider.gameObject.transform.position - this.transform.position).normalized;
                    Debug.Log("Direction: " + dir);
                    FireProjectiles(dir);
                }
            }
        }
    }



    void FireProjectiles(Vector3 direction)
    {
        Projectile projectile = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.SetupProjectile(direction);
    }
}
