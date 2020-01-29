using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    Coins,
    Heart
}

public class CollectableController : MonoBehaviour
{
    public int rewardVal;
    public CollectableType collectableType;
    private PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FlyToPlayer();
    }

    void FlyToPlayer()
    {
        float dist = Vector3.Distance(this.transform.position, player.gameObject.transform.position);
        if (dist < 1.5)
        {
            float step = 5 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.gameObject.transform.position, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (collectableType)
            {
                case CollectableType.Coins:
                    {
                        player.totalCoins += rewardVal;
                    }
                    break;
                case CollectableType.Heart:
                    {
                        player._healthController.Heal(rewardVal);
                    }
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
