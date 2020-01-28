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

    private Vector3 npcDirection;

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
                npcDirection = (hit[targetIndex].collider.gameObject.transform.position - this.transform.position).normalized;
                Debug.Log("Direction: " + npcDirection);
            }
        }

    }

    public void CreateProjectiles()
    {
        Projectile projectile = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.SetupProjectile(npcDirection);
    }
}
