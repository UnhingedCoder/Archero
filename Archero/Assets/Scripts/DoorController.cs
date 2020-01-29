using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    private SpriteRenderer doorSprite;
    private BoxCollider2D coll;
    private PlayerController player;

    private void Awake()
    {
        doorSprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<PlayerController>();
    }

    private void OnEnable()
    {
        coll.enabled = false;
        doorSprite.color = new Color(doorSprite.color.r, doorSprite.color.g, doorSprite.color.b, 1.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        coll.enabled = true;
        doorSprite.color = new Color(doorSprite.color.r, doorSprite.color.g, doorSprite.color.b, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetString("sideshot", "true");
            PlayerPrefs.SetFloat("HP", player._healthController.currentHP);
            PlayerPrefs.SetInt("Coins", player.totalCoins);
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
