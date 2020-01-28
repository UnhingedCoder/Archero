using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed;
    public float detectDistance = 10f;

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
        RaycastHit2D[] hit = Physics2D.CircleCastAll(this.transform.position, detectDistance, Vector2.right, detectDistance * 2f);
        if (hit.Length > 0)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider != null)
                {
                    Debug.Log("hit" + hit[i].collider.name);
                }
            }
        }
    }
}
