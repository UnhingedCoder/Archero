using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text coinValText;

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
        coinValText.text = player.totalCoins.ToString();
    }
}
