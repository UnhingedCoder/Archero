using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    public LayerMask targetLayer;
    public float detectDistance = 10f;
    public float fireRate = 1;
    public GameObject projectilePrefab;
    public GameObject targetToAttack;
    public bool hasSideShot;
    public ObjectPooler _objectPooler;
    public ProjectileCreator creator;
    private Vector3 npcDirection;

    private void Awake()
    {
        switch (creator)
        {
            case ProjectileCreator.Player:
                {
                    _objectPooler = GameObject.Find("PlayerBulletPool").GetComponent<ObjectPooler>();
                }
                break;
            case ProjectileCreator.NPC:
                {
                    _objectPooler = GameObject.Find("EnemyBulletPool").GetComponent<ObjectPooler>();
                }
                break;
        } 
    }

    private void OnEnable()
    {
        targetToAttack = null;
        npcDirection = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DetectEnemy()
    {
        int targetIndex = 99;
        float shortestDist = 999f;

        RaycastHit2D[] hit = Physics2D.CircleCastAll(this.transform.position, detectDistance, Vector2.right, detectDistance * 2f, targetLayer);
        if (hit.Length > 0)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider != null)
                {
                    float dist = Vector3.Distance(this.transform.position, hit[i].collider.gameObject.transform.position);
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
                npcDirection = (hit[targetIndex].collider.gameObject.transform.position - this.transform.position).normalized;
            }
        }

    }

    public void CreateProjectiles()
    {
        Projectile projectile = _objectPooler.GetPooledObject(this.transform).GetComponent<Projectile>();
        projectile.gameObject.SetActive(true);
        projectile.SetupProjectile(npcDirection);

        if (hasSideShot)
        {
            Projectile leftProjectile = _objectPooler.GetPooledObject(this.transform).GetComponent<Projectile>();
            leftProjectile.gameObject.SetActive(true);
            Vector2 perPosL = Vector2.Perpendicular(npcDirection);
            leftProjectile.SetupProjectile(perPosL);

            Projectile rightProjectile = _objectPooler.GetPooledObject(this.transform).GetComponent<Projectile>();
            rightProjectile.gameObject.SetActive(true);
            Vector2 perPosR = Vector2.Perpendicular(-npcDirection);
            rightProjectile.SetupProjectile(perPosR);
        }
    }
}
