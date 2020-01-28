using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileCreator
{
    Player,
    NPC
}

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public ProjectileCreator creator;

    private string compareObjName;
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        compareObjName = creator.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupProjectile(Vector3 targetDir)
    {

        this.transform.rotation = Quaternion.Euler(DetectProjectileFacingDirection(targetDir));
        _rigidBody.velocity = targetDir * speed;
    }

    Vector3 DetectProjectileFacingDirection(Vector3 direction)
    {
        float temp = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        DoDamage(collision);

        if (!collision.gameObject.CompareTag(compareObjName))
        {
            Destroy(this.gameObject);
        }
    }

    public void DoDamage(Collider2D coll)
    {
        HealthController healthCntrl = new HealthController();
        switch (creator)
        {
            case ProjectileCreator.Player:
                {
                    if (coll.gameObject.CompareTag("NPC"))
                    {
                        healthCntrl = coll.gameObject.GetComponent<EnemyController>()._healthController;
                    }
                }
                break;
            case ProjectileCreator.NPC:
                {
                    if (coll.gameObject.CompareTag("Player"))
                    {
                        healthCntrl = coll.gameObject.GetComponent<PlayerController>()._healthController;
                    }
                }
                break;
        }

        if(healthCntrl != null)
            healthCntrl.currentHP -= damage;
    }
}
