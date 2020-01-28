using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
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
   
}
